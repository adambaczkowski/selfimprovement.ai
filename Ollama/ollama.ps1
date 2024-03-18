# Check if WSL is already installed
$wslInstalled = Get-WindowsOptionalFeature -Online | Where-Object FeatureName -eq "Microsoft-Windows-Subsystem-Linux"
if ($wslInstalled -ne $null) {
    Write-Host "Installing Ollama"
    wsl sh -c "curl -fsSL https://ollama.com/install.sh | sh"
    docker run -d -p 3000:8080 --add-host=host.docker.internal:host-gateway -v open-webui:/app/backend/data --name open-webui --restart always ghcr.io/open-webui/open-webui:main
}
else {
    Write-Host "You need WSL"
}

