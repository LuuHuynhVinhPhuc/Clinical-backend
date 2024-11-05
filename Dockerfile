# Use the official .NET SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app
EXPOSE 80
EXPOSE 443

# 
RUN dotnet dev-certs https --export-path ./aspnetapp.pfx --password Clinical123

# Copy the csproj files and restore dependencies
COPY ./src/Clinical-get-back/*.csproj ./src/Clinical-get-back/
COPY ./src/Core/**/*.csproj ./src/Core/
COPY ./src/Infrastructure/**/*.csproj ./src/Infrastructure/
RUN dotnet restore ./src/Clinical-get-back/Clinical-get-back.csproj


# Copy the entire source code
COPY . .

# Build the application
RUN dotnet publish ./src/Clinical-get-back/Clinical-get-back.csproj -c Release -o out

# Use the official ASP.NET runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /app/out .

# Copy the HTTPS certificate
COPY ./aspnetapp.pfx /https/aspnetapp.pfx

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ClinicalBackend.API.dll"]