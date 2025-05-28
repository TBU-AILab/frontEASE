#!/bin/bash

# Check if SEED_DB environment variable is set to true
if [ "$SEED_DB" = "true" ]; then
  echo "Running data generator..."
  dotnet ./datagenerator/FrontEASE.DataGenerator.dll
fi

# Run the server project
echo "Starting server..."
dotnet FrontEASE.Server.dll --urls "http://*:4000"