# Authenticate with Azure
```plaintext
az login

az aks install-cli
```

# You will get output like this:
```plaintext
[
  {
    "cloudName": "AzureCloud",
    "homeTenantId": "some-hash",
    "id": "some-hash",
    "isDefault": true,
    "managedByTenants": [],
    "name": "Azure subscription 1",
    "state": "Enabled",
    "tenantId": "some-hash",
    "user": {
      "name": "email@email.com",
      "type": "user"
    }
  }
]
```
# Get tenantId from here

# Depending on shell you use grab this parameters (example for powershell):
```plaintext
$TENANT_ID = 'some-hash'
```

# Get subscription id:
```plaintext
(az account list --output json | ConvertFrom-Json).id

$SUBSCRIBTION = 'some-hash'
```
# Set subscription
```plaintext
az account set --subscription $SUBSCRIPTION
```

# Resource group
```plaintext
$RESOURCEGROUP = 'aks-dev-resource-group'
az group create -n $RESOURCEGROUP -l polandcentral
```

# After creating resource group you will get something like this:
```plaintext
{
  "id": "/subscriptions/<some-hash>/resourceGroups/aks-dev-resource-group",
  "location": "polandcentral",
  "managedBy": null,
  "name": "aks-dev-resource-group",
  "properties": {
    "provisioningState": "Succeeded"
  },
  "tags": null,
  "type": "Microsoft.Resources/resourceGroups"
}
```

# Service principal
```plaintext
$SERVICE_PRINCIPAL_JSON=$(az ad sp create-for-rbac --name aks-dev-service-principal -o json)
$SERVICE_PRINCIPAL = $(echo $SERVICE_PRINCIPAL_JSON | jq -r '.appId')
$SERVICE_PRINCIPAL_SECRET = $(echo $SERVICE_PRINCIPAL_JSON | jq -r '.password')
```

# Reset the credential if you have any sinlge or double quote on password
```plaintext
az ad sp credential reset --name "aks-dev-service-principal"
```

# Grant contributor role over the resource group to our service principal

```plaintext
az role assignment create --assignee $SERVICE_PRINCIPAL `
--scope "/subscriptions/$SUBSCRIPTION/resourceGroups/$RESOURCEGROUP" `
--role Contributor
```

# You will get something similar to this: 
```plaintext
{
  "condition": null,
  "conditionVersion": null,
  "createdBy": null,
  "createdOn": "2024-05-18T10:11:03.881874+00:00",
  "delegatedManagedIdentityResourceId": null,
  "description": null,
  "id": "/subscriptions/<some-hash>/resourceGroups/aks-dev-resource-group/providers/Microsoft.Authorization/roleAssignments/<some-hash>",
  "name": "<some-hash>",
  "principalId": "<some-hash>",
  "principalType": "ServicePrincipal",
  "resourceGroup": "aks-dev-resource-group",
  "roleDefinitionId": "/subscriptions/<some-hash>/providers/Microsoft.Authorization/roleDefinitions/<some-hash>",
  "scope": "/subscriptions/<some-hash>/resourceGroups/aks-dev-resource-group",
  "type": "Microsoft.Authorization/roleAssignments",
  "updatedBy": "<some-hash>",
  "updatedOn": "2024-05-18T10:11:04.255725+00:00"
}
```
# Get terrafrom (On Windows) :
```powershell
winget install --id=Hashicorp.Terraform  -e
```

# Create ssh key
```powershell
mkdir .ssh -Force
ssh-keygen -t rsa -b 4096 -N "YourPassword123$!" -C "user@selfimprovement.ai" -q -f  .ssh/id_rsa
$SSH_KEY = Get-Content .ssh/id_rsa.pub
```

# In terrform direcory use:
```powershell
terraform init
```
```powershell
terraform plan -var serviceprinciple_id=$SERVICE_PRINCIPAL `
    -var serviceprinciple_key="$SERVICE_PRINCIPAL_SECRET" `
    -var tenant_id=$TENTANT_ID `
    -var subscription_id=$SUBSCRIPTION `
    -var ssh_key="$SSH_KEY" `
    -out=tfplan
```
```powershell
terraform apply tfplan
```

# After deployment
```powershell
az aks get-credentials --resource-group aks-dev --name aks-dev
kubectl get svc
```
# Get EXTERNAL-IP and paste it to browser
```plaintext
NAME                TYPE           CLUSTER-IP   EXTERNAL-IP     PORT(S)        AGE
kubernetes          ClusterIP      10.0.0.1     <none>          443/TCP        7m46s
terraform-example   LoadBalancer   10.0.57.67   20.215.96.241   80:30920/TCP   5m48s
```

# Destroy
```powershell
terraform destroy -var serviceprinciple_id=$SERVICE_PRINCIPAL `
    -var serviceprinciple_key="$SERVICE_PRINCIPAL_SECRET" `
    -var tenant_id=$TENTANT_ID `
    -var subscription_id=$SUBSCRIPTION `
    -var ssh_key="$SSH_KEY"
```


# NEW STEPS

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

resource_group_name=$(terraform output -raw resource_group_name)

az aks list --resource-group $resource_group_name --query "[].{\"K8s cluster name\":name}" --output table

echo "$(terraform output kube_config)" > ./azurek8s

cat ./azurek8s

export KUBECONFIG=./azurek8s