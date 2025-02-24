# TODO: verify this deployment with Jozef.

# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

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

RUN dotnet restore ./src/FoP_IMT.Server/FoP_IMT.Server.csproj

# Copy the remaining source code
COPY . .

# Publish the application
RUN dotnet publish ./src/FoP_IMT.Server/FoP_IMT.Server.csproj -c Release -o /app/publish
RUN dotnet publish ./src/FoP_IMT.DataGenerator/FoP_IMT.DataGenerator.csproj -c Release -o /app/datagenerator

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Set the working directory
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:5000
# Copy the published output
COPY --from=build /app/publish .
COPY --from=build /app/datagenerator ./datagenerator

# COPY the entrypoint script
COPY docker-entrypoint.sh /app
RUN chmod +x /app/docker-entrypoint.sh

COPY ./FoP_IMT.sln /app/datagenerator/

# Set the entry point
ENTRYPOINT [ "./docker-entrypoint.sh" ]
# ENTRYPOINT ["dotnet", "FoP_IMT.Server.dll"]