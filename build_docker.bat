@echo off
:: Check if Docker Compose is installed
where docker-compose >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo Error: Docker Compose is not installed or not in PATH.
    exit /b 1
)

echo Building Docker images...
docker-compose -f docker-compose.override.yml build

if %ERRORLEVEL% neq 0 (
    echo Error: Failed to build Docker images.
    exit /b 1
)

echo Build completed successfully!
pause
