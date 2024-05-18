# authenticate with Azure
az login

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
```

```plaintext
# Keep the appId and password for later use!
```

```plaintext
$SERVICE_PRINCIPAL = $(echo $SERVICE_PRINCIPAL_JSON | jq -r '.appId')
$SERVICE_PRINCIPAL_SECRET = $(echo $SERVICE_PRINCIPAL_JSON | jq -r '.password')
```

# grant contributor role over the resource group to our service principal

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
# In terrform direcory use:
```powershell
terrafrom init
```

