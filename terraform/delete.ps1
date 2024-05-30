terraform init

terraform destroy

# Define the variables
$Location = "polandcentral"
$RgName = "dev-rg"
$StorageAccountName = "selfimprovementstorage"
$ContainerName = "tfstate"

# Get the current subscription ID and set the active subscription
$SubscriptionId = az account list --query "[?isDefault].id" --output tsv
az account set --subscription $SubscriptionId

# Retrieve the primary key of the storage account to use in deleting the container
$AccountKey = $(az storage account keys list --account-name $StorageAccountName --resource-group $RgName --query '[0].value' -o tsv)

# Delete the container in the storage account
az storage container delete --name $ContainerName --account-name $StorageAccountName --account-key $AccountKey

# Delete the storage account
az storage account delete --name $StorageAccountName --resource-group $RgName --yes

# Retrieve the service principal's application ID created for the resource group and delete the service principal
$spAppId = $(az ad sp list --display-name $RgName --query "[].appId" -o tsv)
az ad sp delete --id $spAppId

# Delete the resource group
az group delete --name $RgName --yes --no-wait

Remove-Item -Path "terraform.tfstate", "terraform.tfstate.backup", ".terraform.lock.hcl", "main.tfplan", "terraform.tfvars" -ErrorAction SilentlyContinue
Remove-Item -Path ".terraform" , ".ssh" -Recurse -Force -ErrorAction SilentlyContinue