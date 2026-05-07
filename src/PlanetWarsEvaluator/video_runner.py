import hashlib
import importlib.util
import json
import random
import sys
import traceback
from pathlib import Path
from typing import Any

import imageio.v2 as imageio
import numpy as np
from PIL import Image, ImageDraw, ImageFont

from agents.fully_observable_agent_adapter import as_unified
from agents.greedy_heuristic_agent import GreedyHeuristicAgent
from agents.planet_wars_agent import PlanetWarsAgent, UnifiedPlanetWarsAgent
from agents.random_agents import CarefulRandomAgent, PureRandomAgent
from core.game_state import GameParams, Player
from core.unified_game_runner import UnifiedGameRunner


BASELINE_AGENTS: dict[str, type] = {
    "PureRandomAgent": PureRandomAgent,
    "CarefulRandomAgent": CarefulRandomAgent,
    "GreedyHeuristicAgent": GreedyHeuristicAgent,
}


def stable_seed(base_seed: int, *parts: object) -> int:
    payload = "|".join(str(p) for p in (base_seed, *parts))
    digest = hashlib.sha256(payload.encode("utf-8")).hexdigest()
    return int(digest[:8], 16)


def load_class_from_file(file_path: Path, class_name: str, module_name: str) -> type:
    spec = importlib.util.spec_from_file_location(module_name, file_path)
    if spec is None or spec.loader is None:
        raise RuntimeError(f"Could not create import spec for {file_path}")

    module = importlib.util.module_from_spec(spec)
    sys.modules[module_name] = module
    spec.loader.exec_module(module)

    if not hasattr(module, class_name):
        raise RuntimeError(f"File {file_path} does not define class '{class_name}'.")

    cls = getattr(module, class_name)
    if not isinstance(cls, type):
        raise RuntimeError(f"'{class_name}' exists but is not a class.")

    return cls


def adapt_agent(agent: object) -> UnifiedPlanetWarsAgent:
    if isinstance(agent, UnifiedPlanetWarsAgent):
        return agent

    if isinstance(agent, PlanetWarsAgent):
        return as_unified(agent)

    raise RuntimeError(
        "Agent must implement either PlanetWarsAgent or UnifiedPlanetWarsAgent."
    )


def make_agent(cls: type) -> UnifiedPlanetWarsAgent:
    return adapt_agent(cls())


def owner_color(owner: Player, candidate_player: Player) -> tuple[int, int, int]:
    if owner == Player.Neutral:
        return (150, 150, 150)

    if owner == candidate_player:
        return (55, 120, 230)

    return (230, 85, 70)


def render_frame(model, params: GameParams, candidate_player: Player, title: str) -> Image.Image:
    width = int(params.width)
    height = int(params.height)

    img = Image.new("RGB", (width, height), (20, 24, 32))
    draw = ImageDraw.Draw(img)

    try:
        font = ImageFont.load_default()
    except Exception:
        font = None

    state = model.state

    draw.text(
        (10, 8),
        f"{title} | tick={state.game_tick}",
        fill=(240, 240, 240),
        font=font,
    )

    # Draw transporters first, so planets stay readable.
    for planet in state.planets:
        transporter = planet.transporter
        if transporter is None:
            continue

        x = float(transporter.s.x)
        y = float(transporter.s.y)
        color = owner_color(transporter.owner, candidate_player)

        r = 5
        draw.ellipse((x - r, y - r, x + r, y + r), fill=color, outline=(255, 255, 255))
        draw.text((x + 6, y + 4), str(int(transporter.n_ships)), fill=(240, 240, 240), font=font)

    # Draw planets.
    for planet in state.planets:
        x = float(planet.position.x)
        y = float(planet.position.y)
        r = max(7.0, float(planet.radius))

        color = owner_color(planet.owner, candidate_player)

        outline = (255, 255, 255)
        if planet.transporter is not None:
            outline = (255, 220, 80)

        draw.ellipse(
            (x - r, y - r, x + r, y + r),
            fill=color,
            outline=outline,
            width=2,
        )

        label = f"{int(planet.n_ships)}"
        draw.text((x - 8, y - 6), label, fill=(255, 255, 255), font=font)

    try:
        leader = model.get_leader()
        status = f"leader={leader.value}"
    except Exception:
        status = ""

    draw.text((10, height - 18), status, fill=(240, 240, 240), font=font)

    return img


def run_one_video(
    candidate_cls: type,
    opponent_cls: type,
    candidate_player: Player,
    params: GameParams,
    partial_observability: bool,
    seed: int,
    fps: int,
    frame_stride: int,
    output_path: Path,
    title: str,
) -> dict[str, Any]:
    random.seed(seed)

    candidate = make_agent(candidate_cls)
    opponent = make_agent(opponent_cls)

    if candidate_player == Player.Player1:
        agent1 = candidate
        agent2 = opponent
    else:
        agent1 = opponent
        agent2 = candidate

    runner = UnifiedGameRunner(
        agent1=agent1,
        agent2=agent2,
        game_params=params,
        partial_observability=partial_observability,
    )

    frames: list[np.ndarray] = []

    tick = 0
    while not runner.forward_model.is_terminal():
        if tick % frame_stride == 0:
            frame = render_frame(
                runner.forward_model,
                params,
                candidate_player,
                title,
            )
            frames.append(np.asarray(frame))

        runner.step_game()
        tick += 1

    # Final frame, repeated briefly so the result is visible.
    final_frame = render_frame(
        runner.forward_model,
        params,
        candidate_player,
        f"{title} | final",
    )
    final_array = np.asarray(final_frame)
    for _ in range(max(1, fps)):
        frames.append(final_array)

    imageio.mimsave(
        str(output_path),
        frames,
        fps=fps,
        codec="libx264",
        quality=7,
        macro_block_size=16,
    )

    winner = runner.forward_model.get_leader()

    return {
        "path": str(output_path),
        "filename": output_path.name,
        "ticks": int(runner.forward_model.state.game_tick),
        "winner": winner.value,
        "candidate_win": winner == candidate_player,
        "candidate_player": candidate_player.value,
    }


def run(request: dict[str, Any], candidate_file: Path, output_dir: Path) -> dict[str, Any]:
    class_name = request["class_name"]
    n_videos = int(request["n_videos"])
    opponent_name = request["opponent"]
    partial_observability = bool(request["partial_observability"])
    seed = int(request["seed"])
    fps = int(request["fps"])
    frame_stride = int(request["frame_stride"])

    candidate_player = Player(request.get("candidate_player", "Player1"))
    params = GameParams(**request["game_params"])

    if opponent_name not in BASELINE_AGENTS:
        raise RuntimeError(f"Unknown opponent: {opponent_name}")

    candidate_cls = load_class_from_file(
        candidate_file,
        class_name,
        "candidate_agent_module",
    )
    opponent_cls = BASELINE_AGENTS[opponent_name]

    videos: list[dict[str, Any]] = []

    for i in range(n_videos):
        game_seed = stable_seed(seed, opponent_name, i)
        output_path = output_dir / f"game_{i:03d}_{class_name}_vs_{opponent_name}.mp4"

        title = f"{class_name} vs {opponent_name} | game {i + 1}/{n_videos}"

        result = run_one_video(
            candidate_cls=candidate_cls,
            opponent_cls=opponent_cls,
            candidate_player=candidate_player,
            params=params,
            partial_observability=partial_observability,
            seed=game_seed,
            fps=fps,
            frame_stride=frame_stride,
            output_path=output_path,
            title=title,
        )

        videos.append(result)

    wins = sum(1 for item in videos if item["candidate_win"])

    return {
        "ok": True,
        "videos": videos,
        "summary": {
            "n_videos": n_videos,
            "wins": wins,
            "losses": n_videos - wins,
            "win_rate": wins / n_videos if n_videos else 0.0,
            "opponent": opponent_name,
        },
        "errors": [],
    }


def main() -> None:
    if len(sys.argv) != 5:
        raise SystemExit(
            "Usage: python video_runner.py <candidate_file> <request_json> <output_json> <output_dir>"
        )

    candidate_file = Path(sys.argv[1])
    request_file = Path(sys.argv[2])
    output_file = Path(sys.argv[3])
    output_dir = Path(sys.argv[4])
    output_dir.mkdir(parents=True, exist_ok=True)

    try:
        request = json.loads(request_file.read_text(encoding="utf-8"))
        result = run(request, candidate_file, output_dir)
    except Exception as exc:
        result = {
            "ok": False,
            "videos": [],
            "summary": {},
            "errors": [
                str(exc),
                traceback.format_exc(),
            ],
        }

    output_file.write_text(
        json.dumps(result, ensure_ascii=False, indent=2),
        encoding="utf-8",
    )


if __name__ == "__main__":
    main()