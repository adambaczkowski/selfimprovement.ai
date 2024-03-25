Write-Host "Local development"

# Check if running as Administrator
if (-not ([Security.Principal.WindowsPrincipal] [Security.Principal.WindowsIdentity]::GetCurrent()).IsInRole([Security.Principal.WindowsBuiltInRole] "Administrator")) {
    Write-Host "Please run this script as an administrator."
    exit
}

function handleError {
    param (
        [string]$errorMessage
    )
    Write-Host "Error: $errorMessage" -ForegroundColor Red
    exit 1
}

# Check if WSL is already installed
$wslInstalled = Get-WindowsOptionalFeature -Online | Where-Object FeatureName -eq "Microsoft-Windows-Subsystem-Linux"
if ($null -ne $wslInstalled) {
    Write-Host "WSL is already installed."
    exit
}

# Enable WSL feature
try {
    Write-Host "Enabling WSL feature..."
    Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux -NoRestart
} catch {
    handleError "Failed to enable WSL feature. Make sure you are connected to the internet and run this script as an administrator."
}

# Enable Virtual Machine Platform feature
try {
    Write-Host "Enabling Virtual Machine Platform feature..."
    Enable-WindowsOptionalFeature -Online -FeatureName VirtualMachinePlatform -NoRestart
} catch {
    handleError "Failed to enable Virtual Machine Platform feature. Make sure you are connected to the internet and run this script as an administrator."
}

# Download and install the Linux kernel update package
try {
    Write-Host "Downloading and installing the Linux kernel update package..."
    Invoke-WebRequest -Uri "https://aka.ms/wsl2kernel" -OutFile "$env:TEMP\wsl_update.msi"
    Start-Process msiexec.exe -ArgumentList "/i $env:TEMP\wsl_update.msi /quiet /norestart" -Wait
    Remove-Item "$env:TEMP\wsl_update.msi"
} catch {
    handleError "Failed to download and install the Linux kernel update package. Make sure you are connected to the internet and run this script as an administrator."
}

# Set WSL 2 as the default version
try {
    Write-Host "Setting WSL 2 as the default version..."
    wsl --set-default-version 2
} catch {
    handleError "Failed to set WSL 2 as the default version."
}

Write-Host "WSL 2 installation completed successfully."