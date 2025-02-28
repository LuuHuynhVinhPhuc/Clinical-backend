@echo off
:: Check if Docker Compose is installed
where docker-compose >nul 2>nul
if %ERRORLEVEL% neq 0 (
    echo Error: Docker Compose is not installed or not in PATH.
    exit /b 1
)

echo Stopping Docker containers...
docker-compose -f docker-compose.override.yml down

if %ERRORLEVEL% neq 0 (
    echo Error: Failed to stop Docker containers.
    exit /b 1
)

echo Docker containers stopped successfully!
pause
