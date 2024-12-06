#!/bin/bash

# Build the Docker images
echo "Building Docker images..."
docker-compose -f docker-compose.override.yml build