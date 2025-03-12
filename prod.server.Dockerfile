# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /app

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

RUN dotnet restore ./src/FrontEASE.Server/FrontEASE.Server.csproj

# Copy the remaining source code
COPY . .

# Publish the application
RUN dotnet publish ./src/FrontEASE.Server/FrontEASE.Server.csproj -c Release -o /app/publish
RUN dotnet publish ./src/FrontEASE.DataGenerator/FrontEASE.DataGenerator.csproj -c Release -o /app/datagenerator

# Use the official ASP.NET Core runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0

# Set the working directory
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:4000

# Copy the published output
COPY --from=build /app/publish .
COPY --from=build /app/datagenerator ./datagenerator

# Ensure the path is clean and ready
RUN rm -rf /app/datagenerator/FrontEASE.Server && mkdir -p /app/datagenerator/FrontEASE.Server

# Copy the published server output to the expected path
COPY --from=build /app/publish/ ./datagenerator/FrontEASE.Server/

COPY --from=build /app/publish/FrontEASE.Server.staticwebassets.endpoints.json /app/datagenerator

# COPY the entrypoint script
COPY docker-entrypoint.sh /app
RUN chmod +x /app/docker-entrypoint.sh

COPY ./FrontEASE.sln /app/datagenerator/

# Set the entry point
ENTRYPOINT [ "./docker-entrypoint.sh" ]
