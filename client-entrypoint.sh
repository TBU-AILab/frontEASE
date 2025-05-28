#!/bin/sh

# Replace base URLs before starting nginx
/app/docker-set-urls.sh

# Start NGINX
exec "$@"
