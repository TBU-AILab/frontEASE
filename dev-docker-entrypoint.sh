#!/bin/bash

# Check if SEED_DB environment variable is set to true
if [ "$SEED_DB" = "true" ]; then
  echo "Running data generator..."
  cd /app/src/FoP_IMT.DataGenerator
  dotnet run
fi

# Run the server project
echo "Starting server..."
cd /app/src/FoP_IMT.Server
dotnet run --urls "http://*:5000"