# Phase 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY src/FrontEASE.Client/*.csproj ./src/FrontEASE.Client/
COPY src/FrontEASE.Shared/*.csproj ./src/FrontEASE.Shared/

RUN dotnet restore ./src/FrontEASE.Client/FrontEASE.Client.csproj

# Copy the remaining source code
COPY . .


RUN dotnet publish -c Release -o out

# Phase 2: Serve
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

RUN rm -rf *
COPY --from=build /app/out/wwwroot/ .

# Replace 'nginx' with the actual user if needed
RUN chown -R nginx:nginx /usr/share/nginx/html

COPY nginx.conf /etc/nginx/conf.d/default.conf

# Copy the replacement and entrypoint scripts
COPY docker-set-urls.sh /app/docker-set-urls.sh
COPY client-entrypoint.sh /app/client-entrypoint.sh
RUN chmod +x /app/docker-set-urls.sh /app/client-entrypoint.sh

ENTRYPOINT ["/app/client-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]
