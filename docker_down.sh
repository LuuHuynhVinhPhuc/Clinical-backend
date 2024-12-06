#!/bin/bash

# Stop the Docker containers
echo "Stopping Docker containers..."
docker-compose -f docker-compose.override.yml down