$Location = "polandcentral"
$RgName = "dev-rg"
$SpName = "dev-sp"
$StorageAccountName = "selfimprovementstorage"
$ContainerName = "tfstate"

$SubscriptionId = az account list --query "[?isDefault].id" --output tsv
az account set --subscription $SubscriptionId

# service principal
$spJson = az ad sp create-for-rbac --name $SpName --role Contributor --scopes /subscriptions/$SubscriptionId --output json
$sp = $spJson | ConvertFrom-Json

$ClientId = $sp.appId
$ClientSecret = $sp.password
$TenantId = $sp.tenant

# resource group
az group create --name $RgName --location $Location

# storage account
az storage account create --name $StorageAccountName --resource-group $RgName --location $Location --sku Standard_LRS --kind StorageV2

$AccountKey=$(az storage account keys list --account-name $StorageAccountName --resource-group $RgName --query '[0].value' -o tsv)

# create the container
az storage container create --name $ContainerName --account-name $StorageAccountName --account-key $AccountKey

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

az aks get-credentials -n aks-dev-cluster -g dev-rg

kubectl get svc


$TOKEN=$(kubectl get secrets --namespace aks-dev-cluster $(kubectl get serviceaccount dev-sp-pipeline --namespace aks-dev-cluster -o jsonpath='{.secrets[0].name}') -o jsonpath='{.data.token}' | base64 --decode)