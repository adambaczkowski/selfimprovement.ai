+++
title = "2. Ollama"
description = "Guide on how to use install and use Ollama"
weight = 2
+++

### Installation
You need to have configured WSL

Execute this script:
```bash
curl -fsSL https://ollama.com/install.sh | sh
```
### Web UI

```bash
 docker run -d -p 3000:8080 --add-host=host.docker.internal:host-gateway -v open-webui:/app/backend/data --name open-webui --restart always ghcr.io/open-webui/open-webui:main
```

![](/images/ollama_terminal.png "Ollama Terminal")

### Configuration

