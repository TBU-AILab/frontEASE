#!/bin/bash

# Load environment variables from .env file
if [ -f "/app/.env" ]; then
  set -a
  source /app/.env
  set +a
fi

# Get the Client config file
CLIENT_CONFIG_FILE="/app/src/FrontEASE.Client/wwwroot/appsettings.Development.json"

# Replace specific BaseUrl values in the client config
sed -i "s|\"BaseUrl\": \"http://localhost:4000\"|\"BaseUrl\": ${API_BASE_URL}|g" $CLIENT_CONFIG_FILE
sed -i "s|\"BaseUrl\": \"http://localhost:8086\"|\"BaseUrl\": ${PYTHON_BASE_URL}|g" $CLIENT_CONFIG_FILE

# Check if SEED_DB environment variable is set to true
if [ "$SEED_DB" = "true" ]; then
  echo "Running data generator..."
  cd /app/src/FrontEASE.DataGenerator
  dotnet run
fi

# Run the server project
echo "Starting server..."
cd /app/src/FrontEASE.Server
dotnet run --urls "http://*:4000"