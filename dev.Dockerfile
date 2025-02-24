# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

COPY FoP_IMT.sln .

# Copy project files and restore dependencies
# Doing this to cache the restore step
COPY src/FoP_IMT.Server/*.csproj ./src/FoP_IMT.Server/
COPY src/FoP_IMT.Client/*.csproj ./src/FoP_IMT.Client/
COPY src/FoP_IMT.Application/*.csproj ./src/FoP_IMT.Application/
COPY src/FoP_IMT.Infrastructure/*.csproj ./src/FoP_IMT.Infrastructure/
COPY src/FoP_IMT.Domain/*.csproj ./src/FoP_IMT.Domain/
COPY src/FoP_IMT.Shared/*.csproj ./src/FoP_IMT.Shared/
COPY src/FoP_IMT.DataContracts/*.csproj ./src/FoP_IMT.DataContracts/
COPY src/FoP_IMT.DataGenerator/*.csproj ./src/FoP_IMT.DataGenerator/

RUN dotnet restore ./FoP_IMT.sln

# Copy the remaining source code
COPY . .

ENV ASPNETCORE_URLS=http://+:5000

# COPY the entrypoint script
COPY dev-docker-entrypoint.sh .
RUN chmod +x ./dev-docker-entrypoint.sh

COPY ./src/FoP_IMT.Server/* /app/FoP_IMT.Server/

# Set the entry point
ENTRYPOINT [ "./dev-docker-entrypoint.sh" ]