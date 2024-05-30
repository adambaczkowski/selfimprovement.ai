$Location = "polandcentral"
$RgName = "dev-rg"
$StorageAccountName = "selfimprovementstorage"
$ContainerName = "tfstate"

$SubscriptionId = az account list --query "[?isDefault].id" --output tsv
az account set --subscription $SubscriptionId

# service principal
$spJson = az ad sp create-for-rbac --name $RgName --role Contributor --scopes /subscriptions/$SubscriptionId --output json

# resource group
az group create --name $RgName --location $Location

# storage account
az storage account create --name $StorageAccountName --resource-group $RgName --location $Location --sku Standard_LRS --kind StorageV2

$AccountKey=$(az storage account keys list --account-name $StorageAccountName --resource-group $RgName --query '[0].value' -o tsv)

# create the container
az storage container create --name $ContainerName --account-name $StorageAccountName --account-key $AccountKey

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
arm_client_id               = "$ClientId"
arm_client_secret           = "$ClientSecret"
arm_tenant_id               = "$TenantId"
arm_subscription_id         = "$SubscriptionId"
arm_ssh_key                 = "$sshKey"
"@ | Out-File -FilePath "terraform.tfvars" -Encoding utf8

terraform init

terraform fmt

terraform validate

terraform plan

terraform apply

# Verify the results

#az aks get-credentials -n aks-dev-cluster -g aks-dev

#kubectl get svc
