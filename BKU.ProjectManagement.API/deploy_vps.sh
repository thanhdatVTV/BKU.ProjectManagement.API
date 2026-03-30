#!/bin/bash

# Exit on error
set -e

COMPOSE_FILE="docker-compose.prod.yml"

echo "--- Step 1: Stop and remove old API/Nginx containers ---"
# Stop existing containers to free up ports and update configurations
docker compose --file $COMPOSE_FILE down --remove-orphans 2>/dev/null || true

echo "--- Step 2: Load new Docker image (API) ---"
if [ -f bku-api.tar.gz ]; then
    docker load -i bku-api.tar.gz
elif [ -f bku-api.tar ]; then
    docker load -i bku-api.tar
else
    echo "Error: No image file found!"
    exit 1
fi

echo "--- Step 3: Start API and Nginx (pointing to VPS SQL Server) ---"
# --force-recreate: Ensure containers are recreated with new image and extra_hosts
docker compose --file $COMPOSE_FILE up -d --force-recreate

echo "--- Step 4: Verify containers are running ---"
sleep 5
docker compose --file $COMPOSE_FILE ps

echo "--- Step 5: Cleanup image tar file ---"
rm -f bku-api.tar.gz bku-api.tar

echo "--- Deployment successful! ---"
echo "Your API is now connecting to SQL Server installed directly on the VPS host."
