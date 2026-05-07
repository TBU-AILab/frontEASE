FROM python:3.11-slim

ARG PLANETWARS_REPO=https://github.com/SimonLucas/planet-wars-rts.git
ARG PLANETWARS_REF=main

ENV PYTHONDONTWRITEBYTECODE=1
ENV PYTHONUNBUFFERED=1
ENV PLANETWARS_ROOT=/opt/planet-wars-rts
ENV PLANETWARS_PYTHONPATH=/opt/planet-wars-rts/app/src/main/python
ENV PYTHONPATH=/opt/planet-wars-rts/app/src/main/python:/app

RUN apt-get update \
    && apt-get install -y --no-install-recommends git ca-certificates \
    && rm -rf /var/lib/apt/lists/*

RUN git clone --depth 1 "${PLANETWARS_REPO}" "${PLANETWARS_ROOT}" \
    && cd "${PLANETWARS_ROOT}" \
    && (git fetch --depth 1 origin "${PLANETWARS_REF}" || true) \
    && (git checkout "${PLANETWARS_REF}" || git checkout FETCH_HEAD || true)

WORKDIR /app

COPY requirements.txt /app/requirements.txt

RUN pip install --no-cache-dir --upgrade pip \
    && pip install --no-cache-dir -r /app/requirements.txt

COPY app.py /app/app.py
COPY runner.py /app/runner.py
COPY video_runner.py /app/video_runner.py

RUN useradd --create-home --shell /usr/sbin/nologin evaluator \
    && mkdir -p /planetwars_reports \
    && chown -R evaluator:evaluator /app /planetwars_reports

USER evaluator

EXPOSE 8090

CMD ["uvicorn", "app:app", "--host", "0.0.0.0", "--port", "8090"]