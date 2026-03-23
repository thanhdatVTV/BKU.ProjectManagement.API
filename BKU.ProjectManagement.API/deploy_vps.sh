#!/bin/bash

# Exit on error
set -e

echo "--- Loading Docker image ---"
if [ -f bku-api.tar.gz ]; then
    docker load -i bku-api.tar.gz
elif [ -f bku-api.tar ]; then
    docker load -i bku-api.tar
else
    echo "Error: No image file found!"
    exit 1
fi

echo "--- Starting containers ---"
# Using 'docker compose' instead of 'docker-compose'
docker compose --file docker-compose.prod.yml up -d

echo "--- Cleaning up ---"
rm -f bku-api.tar.gz bku-api.tar

echo "--- Deployment successful! ---"
