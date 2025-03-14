# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /app

COPY FrontEASE.sln .

# Copy project files and restore dependencies
# Doing this to cache the restore step
COPY src/FrontEASE.Server/*.csproj ./src/FrontEASE.Server/
COPY src/FrontEASE.Client/*.csproj ./src/FrontEASE.Client/
COPY src/FrontEASE.Application/*.csproj ./src/FrontEASE.Application/
COPY src/FrontEASE.Infrastructure/*.csproj ./src/FrontEASE.Infrastructure/
COPY src/FrontEASE.Domain/*.csproj ./src/FrontEASE.Domain/
COPY src/FrontEASE.Shared/*.csproj ./src/FrontEASE.Shared/
COPY src/FrontEASE.DataContracts/*.csproj ./src/FrontEASE.DataContracts/
COPY src/FrontEASE.DataGenerator/*.csproj ./src/FrontEASE.DataGenerator/

RUN dotnet restore ./FrontEASE.sln

# Copy the remaining source code
COPY . .

ENV ASPNETCORE_URLS=http://+:4000

# COPY the entrypoint script
COPY dev-docker-entrypoint.sh .
RUN chmod +x ./dev-docker-entrypoint.sh

COPY ./src/FrontEASE.Server/* /app/FrontEASE.Server/

# Set the entry point
ENTRYPOINT [ "./dev-docker-entrypoint.sh" ]