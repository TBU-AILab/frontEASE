# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

# Set the working directory
WORKDIR /app

# Copy project files and restore dependencies
# Doing this to cache the restore step
COPY src/FrontEASE.Client/*.csproj ./src/FrontEASE.Client/
COPY src/FrontEASE.Shared/*.csproj ./src/FrontEASE.Shared/

RUN dotnet restore ./src/FrontEASE.Client/FrontEASE.Client.csproj

# Copy the remaining source code
COPY . .

ENV ASPNETCORE_URLS=http://+:5235

# COPY the entrypoint script
COPY dev-docker-entrypoint.sh .
RUN chmod +x ./dev-docker-entrypoint.sh

# COPY and RUN the url replacement script
COPY docker-set-urls.sh .
RUN chmod +x ./docker-set-urls.sh

CMD ["/bin/bash", "-c", "/app/docker-set-urls.sh && dotnet run --project ./src/FrontEASE.Client/ --urls \"http://*:5235\""]
