#!/bin/bash

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