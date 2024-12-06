#!/bin/bash

# Start the Docker containers
echo "Starting Docker containers..."
docker-compose -f docker-compose.override.yml up -d