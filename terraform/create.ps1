
az login

az account show

$SubscriptionId = az account list --query "[?isDefault].id" --output tsv
az account set --subscription $SubscriptionId

$spJson = az ad sp create-for-rbac --name "dev-sp" --role "Contributor" --scopes "/subscriptions/$SubscriptionId" --output json
$sp = $spJson | ConvertFrom-Json

$ClientId = $sp.appId
$ClientSecret = $sp.password
$TenantId = $sp.tenant

# ssh
mkdir .ssh -Force
ssh-keygen -t rsa -b 4096 -N "YourPassword123$!" -C "user@selfimprovement.ai" -q -f  .ssh/id_rsa
$sshKey = Get-Content .ssh/id_rsa.pub

# Save variables to .tfvars file
@"
arm_subscription_id = "$SubscriptionId"
arm_tenant_id       = "$TenantId"
arm_client_id       = "$ClientId"
arm_client_secret   = "$ClientSecret"
arm_ssh_key         = "$sshKey"
"@ | Out-File -FilePath "terraform.tfvars" -Encoding utf8

# Apply a Terraform execution plan

terraform fmt

terraform validate

terraform init -upgrade

terraform plan -out main.tfplan

terraform apply main.tfplan

# Verify the results

az aks get-credentials -n aks-dev-cluster -g aks-dev

kubectl get svc
