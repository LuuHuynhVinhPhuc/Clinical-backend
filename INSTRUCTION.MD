# .NET Application Setup as a Windows Service

This document provides instructions for building, publishing, starting, and setting up a .NET application as a Windows Service.

## Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed
- Administrator access on the Windows machine
- PowerShell or Command Prompt with administrative privileges

## Step 1: Create a .NET Worker Service Project

1. **Open a terminal or command prompt.**
   
2. **Run the following command to create a new Worker Service project:**
   ```bash
   dotnet new worker -n MyWorkerService
3. **Navigate to the project directory:**
   ```bash
   cd MyWorkerService
4. **Publish the Application**
   ```bash
   dotnet publish -c Release -o ./publish

## Step 2: Install the Application as a Windows Service

1. **Open PowerShell as Administrator.**
2. **Install the service using the following command:**
   Replace C:\path\to\your\published\app\MyWorkerService.exe with the path to your published executable.
   
   ### In powershell
   ```powershell
   New-Service -Name "MyWorkerService" -Binary "C:\path\to\your\published\app\MyWorkerService.exe" -StartupType Automatic
   ```

   ### Command Prompt
   ```cmd
   sc create MyWorkerService binPath= "C:\path\to\your\published\app\MyWorkerService.exe" start= auto
   ```

## Step 3: Start the Windows Service
1. In Powershell:
   ```powershell
   Start-Service -Name "MyWorkerService"

2. In Command Prompt:
   ```cmd
   sc start MyWorkerService

## Step 4: Verify Service
1. Open the Services management console by running services.msc.
2. Locate your service ("MyWorkerService") in the list.
3. Check the status and ensure that the service is running. You can also start or stop the service from this console.

#### Alternatively, you could open in Start, search for Services

## Step 7: Uninstall Service (if needed)

1. In Powershell
   ```powershell
   Remove-Service -Name "MyWorkerService"

2. In Command Prompt
   ```cmd
   sc delete MyWorkerService

