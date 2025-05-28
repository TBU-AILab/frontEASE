#!/bin/bash

# Load environment variables from .env file
if [ -f "/app/.env" ]; then
  set -a
  source /app/.env
  set +a
fi

# Sanitize line endings (in case .env has \r\n from Windows)
API_BASE_URL=$(echo "$API_BASE_URL" | tr -d '\r')
PYTHON_BASE_URL=$(echo "$PYTHON_BASE_URL" | tr -d '\r')

# Get the Client config file
CLIENT_CONFIG_FILE_DEV="/app/src/FrontEASE.Client/wwwroot/appsettings.Development.json"
CLIENT_CONFIG_FILE_PROD="/app/src/FrontEASE.Client/wwwroot/appsettings.json"

# Replace BaseUrl for API if set
if [ -n $API_BASE_URL ]; then
  echo "Setting API_BASE_URL to $API_BASE_URL"
  sed -i "s|\"BaseUrl\": \"http://localhost:4000\"|\"BaseUrl\": \"${API_BASE_URL}\"|g" $CLIENT_CONFIG_FILE_DEV
  sed -i "s|\"BaseUrl\": \"http://localhost:4000\"|\"BaseUrl\": \"${API_BASE_URL}\"|g" $CLIENT_CONFIG_FILE_PROD
else
  echo "API_BASE_URL is not set. Skipping replacement."
fi

# Replace BaseUrl for PythonCore if set
if [ -n $PYTHON_BASE_URL ]; then
  echo "Setting PYTHON_BASE_URL to $PYTHON_BASE_URL"
  sed -i "s|\"BaseUrl\": \"http://localhost:8086\"|\"BaseUrl\": \"${PYTHON_BASE_URL}\"|g" $CLIENT_CONFIG_FILE_DEV
  sed -i "s|\"BaseUrl\": \"http://localhost:8086\"|\"BaseUrl\": \"${PYTHON_BASE_URL}\"|g" $CLIENT_CONFIG_FILE_PROD
else
  echo "PYTHON_BASE_URL is not set. Skipping replacement."
fi
