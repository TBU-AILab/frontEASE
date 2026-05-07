import hashlib
import importlib.util
import json
import random
import sys
import traceback
from pathlib import Path
from typing import Any, Callable

from agents.fully_observable_agent_adapter import as_unified
from agents.greedy_heuristic_agent import GreedyHeuristicAgent
from agents.planet_wars_agent import PlanetWarsAgent, UnifiedPlanetWarsAgent
from agents.random_agents import CarefulRandomAgent, PureRandomAgent
from core.forward_model import ForwardModel
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


def other_player(player: Player) -> Player:
    if player == Player.Player1:
        return Player.Player2
    if player == Player.Player2:
        return Player.Player1
    return Player.Neutral


def load_class_from_file(
    file_path: Path,
    class_name: str,
    module_name: str,
) -> type:
    spec = importlib.util.spec_from_file_location(module_name, file_path)
    if spec is None or spec.loader is None:
        raise RuntimeError(f"Could not create import spec for {file_path}")

    module = importlib.util.module_from_spec(spec)
    sys.modules[module_name] = module
    spec.loader.exec_module(module)

    if not hasattr(module, class_name):
        raise RuntimeError(
            f"File {file_path} does not define class '{class_name}'."
        )

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


def make_agent_factory(cls: type) -> Callable[[], UnifiedPlanetWarsAgent]:
    def factory() -> UnifiedPlanetWarsAgent:
        try:
            instance = cls()
        except TypeError as exc:
            raise RuntimeError(
                "Agent class must have a zero-argument constructor."
            ) from exc

        return adapt_agent(instance)

    return factory


def ships_for(model: ForwardModel, player: Player) -> float:
    return float(model.get_ships(player))


def play_one_game(
    candidate_factory: Callable[[], UnifiedPlanetWarsAgent],
    opponent_factory: Callable[[], UnifiedPlanetWarsAgent],
    candidate_player: Player,
    params: GameParams,
    partial_observability: bool,
    seed: int,
) -> dict[str, Any]:
    random.seed(seed)

    before_actions = ForwardModel.n_actions
    before_failed = ForwardModel.n_failed_actions

    if candidate_player == Player.Player1:
        agent1 = candidate_factory()
        agent2 = opponent_factory()
    else:
        agent1 = opponent_factory()
        agent2 = candidate_factory()

    runner = UnifiedGameRunner(
        agent1=agent1,
        agent2=agent2,
        game_params=params,
        partial_observability=partial_observability,
    )

    final_model = runner.run_game()
    winner = final_model.get_leader()

    opponent_player = other_player(candidate_player)

    candidate_ships = ships_for(final_model, candidate_player)
    opponent_ships = ships_for(final_model, opponent_player)

    return {
        "winner": winner.value,
        "candidate_win": winner == candidate_player,
        "draw": winner == Player.Neutral,
        "ticks": int(final_model.state.game_tick),
        "candidate_ships": candidate_ships,
        "opponent_ships": opponent_ships,
        "ship_diff": candidate_ships - opponent_ships,
        "actions": int(ForwardModel.n_actions - before_actions),
        "failed_actions": int(ForwardModel.n_failed_actions - before_failed),
    }


def summarize_games(games: list[dict[str, Any]]) -> dict[str, Any]:
    n = len(games)
    wins = sum(1 for g in games if g["candidate_win"])
    draws = sum(1 for g in games if g["draw"])
    losses = n - wins - draws

    mean_ticks = sum(g["ticks"] for g in games) / n if n else 0.0
    mean_ship_diff = sum(g["ship_diff"] for g in games) / n if n else 0.0
    total_actions = sum(g["actions"] for g in games)
    total_failed_actions = sum(g["failed_actions"] for g in games)

    return {
        "n_games": n,
        "wins": wins,
        "losses": losses,
        "draws": draws,
        "win_rate": wins / n if n else 0.0,
        "draw_rate": draws / n if n else 0.0,
        "mean_ticks": mean_ticks,
        "mean_ship_diff": mean_ship_diff,
        "total_actions": total_actions,
        "total_failed_actions": total_failed_actions,
        "failed_action_rate": (
            total_failed_actions / (total_actions + total_failed_actions)
            if (total_actions + total_failed_actions) > 0
            else 0.0
        ),
    }


def build_opponent_factories(
    request: dict[str, Any],
    tmp_dir: Path,
) -> list[tuple[str, str, Callable[[], UnifiedPlanetWarsAgent]]]:
    """
    Returns list of:
        (opponent_type, opponent_name, opponent_factory)

    opponent_type is either:
        "builtin"
        "history"
    """
    opponent_factories: list[tuple[str, str, Callable[[], UnifiedPlanetWarsAgent]]] = []

    for opponent_name in request.get("opponents", []):
        if opponent_name not in BASELINE_AGENTS:
            raise RuntimeError(f"Unknown built-in opponent: {opponent_name}")

        opponent_cls = BASELINE_AGENTS[opponent_name]
        opponent_factories.append(
            ("builtin", opponent_name, make_agent_factory(opponent_cls))
        )

    for index, opponent in enumerate(request.get("custom_opponents", [])):
        name = str(opponent.get("name") or f"custom_opponent_{index}")
        class_name = str(opponent.get("class_name") or "MyAgent")
        agent_code = str(opponent.get("agent_code") or "")

        if not agent_code.strip():
            continue

        file_path = tmp_dir / f"custom_opponent_{index}.py"
        file_path.write_text(agent_code, encoding="utf-8")

        opponent_cls = load_class_from_file(
            file_path=file_path,
            class_name=class_name,
            module_name=f"custom_opponent_module_{index}",
        )

        opponent_factories.append(
            ("history", name, make_agent_factory(opponent_cls))
        )

    return opponent_factories


def evaluate(request: dict[str, Any], candidate_file: Path) -> dict[str, Any]:
    class_name = request["class_name"]
    n_games = int(request["n_games"])
    partial_observability = bool(request["partial_observability"])
    play_both_sides = bool(request["play_both_sides"])
    seed = int(request["seed"])

    params = GameParams(**request["game_params"])

    candidate_cls = load_class_from_file(
        file_path=candidate_file,
        class_name=class_name,
        module_name="candidate_agent_module",
    )
    candidate_factory = make_agent_factory(candidate_cls)

    opponent_factories = build_opponent_factories(
        request=request,
        tmp_dir=candidate_file.parent,
    )

    if not opponent_factories:
        raise RuntimeError("No opponents were provided.")

    by_opponent: dict[str, Any] = {}
    all_games: list[dict[str, Any]] = []

    for opponent_type, opponent_name, opponent_factory in opponent_factories:
        opponent_games: list[dict[str, Any]] = []

        sides = [Player.Player1, Player.Player2] if play_both_sides else [Player.Player1]

        for side in sides:
            for i in range(n_games):
                game_seed = stable_seed(seed, opponent_name, side.value, i)

                game_result = play_one_game(
                    candidate_factory=candidate_factory,
                    opponent_factory=opponent_factory,
                    candidate_player=side,
                    params=params,
                    partial_observability=partial_observability,
                    seed=game_seed,
                )

                game_result["opponent"] = opponent_name
                game_result["opponent_type"] = opponent_type
                game_result["candidate_player"] = side.value

                opponent_games.append(game_result)
                all_games.append(game_result)

        by_opponent[opponent_name] = {
            **summarize_games(opponent_games),
            "opponent_type": opponent_type,
        }

    aggregate = summarize_games(all_games)

    fitness = aggregate["win_rate"]

    return {
        "ok": True,
        "fitness": fitness,
        "aggregate": aggregate,
        "by_opponent": by_opponent,
        "config": {
            "class_name": class_name,
            "n_games_per_opponent_per_side": n_games,
            "builtin_opponents": request.get("opponents", []),
            "custom_opponents": [
                {
                    "name": item.get("name"),
                    "class_name": item.get("class_name", "MyAgent"),
                }
                for item in request.get("custom_opponents", [])
            ],
            "partial_observability": partial_observability,
            "play_both_sides": play_both_sides,
            "game_params": request["game_params"],
            "seed": seed,
        },
        "errors": [],
    }


def main() -> None:
    if len(sys.argv) != 4:
        raise SystemExit(
            "Usage: python runner.py <candidate_file> <request_json> <output_json>"
        )

    candidate_file = Path(sys.argv[1])
    request_file = Path(sys.argv[2])
    output_file = Path(sys.argv[3])

    try:
        request = json.loads(request_file.read_text(encoding="utf-8"))
        result = evaluate(request, candidate_file)
    except Exception as exc:
        result = {
            "ok": False,
            "fitness": None,
            "aggregate": None,
            "by_opponent": None,
            "config": None,
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