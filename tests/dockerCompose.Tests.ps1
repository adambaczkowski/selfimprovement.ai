# https://pester.dev/docs/introduction/installation
# Install-Module -Name Pester
# Install-Module -Name powershell-yaml
# choco install Pester

# Run test
# Invoke-Pester -Path ./tests/DockerCompose.tests.ps1

$dockerComposeFilePath = './docker-compose-local.yml'
$dockerCompose = Get-Content $dockerComposeFilePath -Raw | ConvertFrom-Yaml

Describe "Docker Compose Validation Tests" {

    Context "Postgres Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.postgres.container_name | Should -Be "postgres"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.postgres.build.context | Should -Be "./Database/postgres"
            $dockerCompose.services.postgres.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Sets the required environment variables" {
            $dockerCompose.services.postgres.environment | Should -Contain "POSTGRES_USER=admin"
            $dockerCompose.services.postgres.environment | Should -Contain "POSTGRES_PASSWORD=selfimprovement123"
            $dockerCompose.services.postgres.environment | Should -Contain "POSTGRES_DB=SelfImprovementDb"
        }

        It "Maps the correct port" {
            $dockerCompose.services.postgres.ports[0] | Should -Be "5432:5432"
        }

        It "Defines the necessary volumes correctly" {
            $dockerCompose.services.postgres.volumes[0] | Should -Match "postgres:/var/lib/postgresql/data"
        }

        It "Configures logging correctly" {
            $dockerCompose.services.postgres.logging.options.'max-size' | Should -Be "10m"
            $dockerCompose.services.postgres.logging.options.'max-file' | Should -Be "3"
        }
    }

    Context "PgAdmin Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.pgadmin.container_name | Should -Be "pgadmin"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.pgadmin.build.context | Should -Be "./Database/pgadmin"
            $dockerCompose.services.pgadmin.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Sets the required environment variables for default email and password" {
            $dockerCompose.services.pgadmin.environment | Should -Contain "PGADMIN_DEFAULT_EMAIL=admin@selfimprovement.ai"
            $dockerCompose.services.pgadmin.environment | Should -Contain "PGADMIN_DEFAULT_PASSWORD=selfimprovement123"
        }

        It "Maps the correct port from 5050 to 80" {
            $dockerCompose.services.pgadmin.ports[0] | Should -Be "5050:80"
        }

        It "Defines the necessary volume for configuration" {
            $dockerCompose.services.pgadmin.volumes[0] | Should -Be "./Database/Lib/config.py:/pgadmin4/web/config_local.py"
        }
    }

    Context "Open-WebUI Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services['open-webui'].container_name | Should -Be "open-webui"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services['open-webui'].build.context | Should -Be "./Ollama/webui"
            $dockerCompose.services['open-webui'].build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port from 3001 to 8080" {
            $dockerCompose.services['open-webui'].ports[0] | Should -Be "3001:8080"
        }

        It "Defines the necessary volume for backend data" {
            $dockerCompose.services['open-webui'].volumes[0] | Should -Be "open-webui:/app/backend/data"
        }
    }

    Context "Ollama Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.ollama.container_name | Should -Be "ollama"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.ollama.build.context | Should -Be "./Ollama"
            $dockerCompose.services.ollama.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port for service" {
            $dockerCompose.services.ollama.ports[0] | Should -Be "11434:11434"
        }

        It "Defines the necessary volume for storing configuration" {
            $dockerCompose.services.ollama.volumes[0] | Should -Be "ollama:/root/.ollama"
        }

        It "Ensures the TTY is enabled" {
            $dockerCompose.services.ollama.tty | Should -Be $true
        }

        It "Configures GPU resources correctly" {
            $dockerCompose.services.ollama.deploy.resources.reservations.devices[0].driver | Should -Be "nvidia"
            $dockerCompose.services.ollama.deploy.resources.reservations.devices[0].count | Should -Be 1
            $dockerCompose.services.ollama.deploy.resources.reservations.devices[0].capabilities | Should -Contain "gpu"
        }
    }

    Context "Azurite Service Configuration" {
        It "Uses the correct Docker image" {
            $dockerCompose.services.azurite.image | Should -Be "mcr.microsoft.com/azure-storage/azurite"
        }

        It "Has the correct container name and hostname" {
            $dockerCompose.services.azurite.container_name | Should -Be "azurite"
            $dockerCompose.services.azurite.hostname | Should -Be "azurite"
        }

        It "Maps the correct ports for Azurite services" {
            $dockerCompose.services.azurite.ports | Should -Contain "10000:10000"
            $dockerCompose.services.azurite.ports | Should -Contain "10001:10001"
            $dockerCompose.services.azurite.ports | Should -Contain "10002:10002"
        }

        It "Defines the necessary volume for data storage" {
            $dockerCompose.services.azurite.volumes[0] | Should -Be "azurite_data:/data"
        }

        It "Executes the correct command to start the Azurite service" {
            $dockerCompose.services.azurite.command | Should -Be "azurite-blob --blobHost 0.0.0.0 -l /data"
        }
    }

    Context "Grafana Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.grafana.container_name | Should -Be "grafana"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.grafana.build.context | Should -Be "./Monitoring/Grafana"
            $dockerCompose.services.grafana.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port from 3002 to 3000" {
            $dockerCompose.services.grafana.ports[0] | Should -Be "3002:3000"
        }

        It "Defines the necessary volumes for Grafana" {
            $dockerCompose.services.grafana.volumes[0] | Should -Be "./Monitoring/Grafana/lib:/var/lib/grafana"
        }

        It "Sets the required environment variables for admin access" {
            $dockerCompose.services.grafana.environment | Should -Contain "GF_SECURITY_ADMIN_USER=admin"
            $dockerCompose.services.grafana.environment | Should -Contain "GF_SECURITY_ADMIN_PASSWORD=admin"
        }
    }

    Context "Loki Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.loki.container_name | Should -Be "loki"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.loki.build.context | Should -Be "./Monitoring/Loki"
            $dockerCompose.services.loki.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port from 3100 to 3100" {
            $dockerCompose.services.loki.ports[0] | Should -Be "3100:3100"
        }

        It "Defines the necessary volume for Loki configuration" {
            $dockerCompose.services.loki.volumes[0] | Should -Be "./Monitoring/Loki:/etc/loki"
        }

        It "Executes the correct command to start the Loki service" {
            $dockerCompose.services.loki.command | Should -Be "-config.file=/etc/loki/loki-config.yml"
        }
    }

    Context "Promtail Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.promtail.container_name | Should -Be "promtail"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.promtail.build.context | Should -Be "./Monitoring/Promtail"
            $dockerCompose.services.promtail.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Defines the necessary volume for log monitoring" {
            $dockerCompose.services.promtail.volumes[0] | Should -Be "/var/log:/var/log"
        }

        It "Executes the correct commands for configuration" {
            $dockerCompose.services.promtail.command | Should -Contain "-config.expand-env=true"
            $dockerCompose.services.promtail.command | Should -Contain "-config.file=/etc/promtail/config.yml"
        }
    }

    Context "Prometheus Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.prometheus.container_name | Should -Be "prometheus"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.prometheus.build.context | Should -Be "./Monitoring/Prometheus"
            $dockerCompose.services.prometheus.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port" {
            $dockerCompose.services.prometheus.ports[0] | Should -Be "9090:9090"
        }

        It "Defines the necessary volumes correctly" {
            $dockerCompose.services.prometheus.volumes | Should -Contain "prometheus_data:/prometheus"
            $dockerCompose.services.prometheus.volumes | Should -Contain "./Monitoring/Prometheus/prometheus.yml:/etc/prometheus/prometheus.yml"
        }

        It "Uses the correct commands for configuration" {
            $dockerCompose.services.prometheus.command | Should -Contain "--config.file=/etc/prometheus/prometheus.yml"
            $dockerCompose.services.prometheus.command | Should -Contain "--storage.tsdb.path=/prometheus"
            $dockerCompose.services.prometheus.command | Should -Contain "--web.console.libraries=/usr/share/prometheus/console_libraries"
            $dockerCompose.services.prometheus.command | Should -Contain "--web.console.templates=/usr/share/prometheus/consoles"
            $dockerCompose.services.prometheus.command | Should -Contain "--web.external-url=http://localhost:80/prometheus/"
        }
    }

    Context "Node-Exporter Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services['node-exporter'].container_name | Should -Be "node-exporter"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services['node-exporter'].build.context | Should -Be "./Monitoring/NodeExporter"
            $dockerCompose.services['node-exporter'].build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port for Prometheus node metrics" {
            $dockerCompose.services['node-exporter'].ports[0] | Should -Be "9100:9100"
        }

        It "Defines the necessary volumes for monitoring system internals" {
            $dockerCompose.services['node-exporter'].volumes | Should -Contain "/proc:/host/proc:ro"
            $dockerCompose.services['node-exporter'].volumes | Should -Contain "/sys:/host/sys:ro"
            $dockerCompose.services['node-exporter'].volumes | Should -Contain "/:/host/rootfs:ro"
        }

        It "Executes the correct commands for node metrics collection" {
            $dockerCompose.services['node-exporter'].command | Should -Contain "--path.procfs=/host/proc"
            $dockerCompose.services['node-exporter'].command | Should -Contain "--path.sysfs=/host/sys"
            $dockerCompose.services['node-exporter'].command | Should -Contain '--collector.filesystem.ignored-mount-points="^/(sys|proc|dev|host|etc)($$|/)"'
        }
    }

    Context "RabbitMQ Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.rabbitmq.container_name | Should -Be "rabbitmq"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.rabbitmq.build.context | Should -Be "./Rabbitmq"
            $dockerCompose.services.rabbitmq.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct ports for AMQP and management interface" {
            $dockerCompose.services.rabbitmq.ports | Should -Contain "5672:5672" # AMQP
            $dockerCompose.services.rabbitmq.ports | Should -Contain "15672:15672" # Management Plugin
        }

        It "Defines the necessary volume for RabbitMQ data" {
            $dockerCompose.services.rabbitmq.volumes[0] | Should -Be "rabbitmq_data:/var/lib/rabbitmq"
        }
    }

    Context "Nginx Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.nginx.container_name | Should -Be "nginx"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.nginx.build.context | Should -Be "./Nginx"
            $dockerCompose.services.nginx.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct ports for HTTP and HTTPS" {
            $dockerCompose.services.nginx.ports | Should -Contain "80:80"
            $dockerCompose.services.nginx.ports | Should -Contain "443:443"
        }

        It "Defines the necessary volume for the Nginx configuration" {
            $dockerCompose.services.nginx.volumes[0] | Should -Be "./Nginx/local_conf/nginx.conf:/etc/nginx/nginx.conf"
        }
    }

    Context "Frontend Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.frontend.container_name | Should -Be "selfimprovement-frontend"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.frontend.build.context | Should -Be "./Source/Web/"
            $dockerCompose.services.frontend.build.dockerfile | Should -Be "Dockerfile.local"
        }

        It "Maps the correct port" {
            $dockerCompose.services.frontend.ports[0] | Should -Be "3000:3000"
        }
    }

    Context "Identity Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.identity.container_name | Should -Be "selfimprovement-identity"
        }

        It "Sets the required environment variables" {
            $dockerCompose.services.identity.environment | Should -Contain "SelfImprovementDbContext=Host=postgres;Port=5432;Database=SelfImprovementDb;User ID=admin;Password=selfimprovement123;Include Error Detail=true"
        }

        It "Maps the correct port" {
            $dockerCompose.services.identity.ports[0] | Should -Be "8080:8080"
        }
    }

    Context "Goal Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.goal.container_name | Should -Be "selfimprovement-goal"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.goal.build.context | Should -Be "./Source"
            $dockerCompose.services.goal.build.dockerfile | Should -Be "GoalApi/Dockerfile.local"
        }

        It "Maps the correct port" {
            $dockerCompose.services.goal.ports[0] | Should -Be "8081:8080"
        }

        It "Sets the required environment variables for database and messaging" {
            $dockerCompose.services.goal.environment | Should -Contain "SelfImprovementDbContext=Host=postgres;Port=5432;Database=SelfImprovementDb;User ID=admin;Password=selfimprovement123;Include Error Detail=true"
            $dockerCompose.services.goal.environment | Should -Contain "CorsOrigin=http://localhost:3000"
            $dockerCompose.services.goal.environment | Should -Contain "RabbitMqConnectionUrl=amqp://guest:guest@host.docker.internal:5672/"
        }

        It "Limits the CPU and memory resources correctly" {
            $dockerCompose.services.goal.deploy.resources.limits.cpus | Should -Be '0.5'
            $dockerCompose.services.goal.deploy.resources.limits.memory | Should -Be '500M'
        }
    }

    Context "Prompt Service Configuration" {
        It "Has the correct container name" {
            $dockerCompose.services.prompt.container_name | Should -Be "selfimprovement-prompt"
        }

        It "Uses the correct build context and Dockerfile" {
            $dockerCompose.services.prompt.build.context | Should -Be "./Source"
            $dockerCompose.services.prompt.build.dockerfile | Should -Be "PromptApi/Dockerfile.local"
        }

        It "Sets all necessary environment variables" {
            $dockerCompose.services.prompt.environment | Should -Contain "SelfImprovementDbContext=Host=postgres;Port=5432;Database=SelfImprovementDb;User ID=admin;Password=selfimprovement123;Include Error Detail=true"
            $dockerCompose.services.prompt.environment | Should -Contain "CorsOrigin=http://localhost:3000"
            $dockerCompose.services.prompt.environment | Should -Contain "RabbitMqConnectionUrl=amqp://guest:guest@host.docker.internal:5672/"
            $dockerCompose.services.prompt.environment | Should -Contain "BlobStorageConnectionUrl=UseDevelopmentStorage=true;DevelopmentStorageProxyUri=http://azurite"
            $dockerCompose.services.prompt.environment | Should -Contain "Llama2=host.docker.internal:11434"
            $dockerCompose.services.prompt.environment | Should -Contain "Gpt35=api.openai.com"
            $dockerCompose.services.prompt.environment | Should -Contain "Gpt35_ApiKey=sk-proj-fKa36WMlImH9VtU4woCmT3BlbkFJIlk3D0HFpSxPMy1KTPEb"
            $dockerCompose.services.prompt.environment | Should -Contain "GoalApiServiceUrl=host.docker.internal:8081"
            $dockerCompose.services.prompt.environment | Should -Contain "IdentityApiServiceUrl=host.docker.internal:8080"
        }

        It "Maps the correct port" {
            $dockerCompose.services.prompt.ports[0] | Should -Be "8082:8080"
        }

        It "Limits the CPU and memory resources appropriately" {
            $dockerCompose.services.prompt.deploy.resources.limits.cpus | Should -Be '0.5'
            $dockerCompose.services.prompt.deploy.resources.limits.memory | Should -Be '500M'
        }
    }

}