resource "azurerm_resource_group" "resource_group" {
  location = var.resource_group_location
  name     = "dev-rg"
}

resource "azurerm_container_registry" "acr-dev" {
  name                = "acrselfimprovement"
  resource_group_name = azurerm_resource_group.resource_group.name
  location            = azurerm_resource_group.resource_group.location
  sku                 = "Standard"
  admin_enabled       = true
}

resource "azurerm_kubernetes_cluster" "aks-dev" {
  location            = azurerm_resource_group.resource_group.location
  name                = "aks-dev-cluster"
  resource_group_name = azurerm_resource_group.resource_group.name
  dns_prefix          = "aks-dev-dns"

  service_principal {
    client_id     = var.arm_client_id
    client_secret = var.arm_client_secret
  }

  default_node_pool {
    name            = "agentpool"
    vm_size         = "Standard_D2_v2"
    node_count      = var.node_count
    type            = "VirtualMachineScaleSets"
    os_disk_size_gb = 250
  }

  linux_profile {
    admin_username = var.username

    ssh_key {
      key_data = var.arm_ssh_key
    }
  }
  network_profile {
    network_plugin    = "kubenet"
    load_balancer_sku = "standard"
  }
}

resource "azurerm_storage_account" "storage_account" {
  name                     = var.storage_account_name
  resource_group_name      = azurerm_resource_group.resource_group.name
  account_tier             = "Standard"
  location                 = var.location
  account_replication_type = "LRS"
  account_kind             = "StorageV2"
}

resource "azurerm_storage_container" "tfstate_storage" {
  name                  = "tfstate"
  storage_account_name  = azurerm_storage_account.storage_account.name
  container_access_type = "private"
}

resource "azurerm_key_vault" "key_vault" {
  name                       = "selfimprovementKeyVault"
  location                   = azurerm_resource_group.resource_group.location
  resource_group_name        = azurerm_resource_group.resource_group.name
  sku_name                   = "standard"
  tenant_id                  = var.arm_tenant_id
  soft_delete_retention_days = 7
  purge_protection_enabled   = false

  network_acls {
    default_action = "Allow"
    bypass         = "AzureServices"
  }
}

resource "azurerm_key_vault_access_policy" "aks_sp_access" {
  key_vault_id = azurerm_key_vault.key_vault.id
  tenant_id    = var.arm_tenant_id
  object_id    = var.arm_client_id # Make sure this is the object ID of the AKS service principal

  key_permissions = [
    "Get",
    "Create",
    "Delete",
    "List",
    "Update",
    "Import",
    "Backup",
    "Restore",
    "Recover",
    "Purge"
  ]

  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
    "Recover",
    "Backup",
    "Restore",
    "Purge"
  ]

  certificate_permissions = [
    "Get",
    "List",
    "Create",
    "Delete",
    "ManageContacts",
    "GetIssuers",
    "ListIssuers",
    "SetIssuers",
    "DeleteIssuers",
    "ManageIssuers",
    "Recover",
    "Purge"
  ]
}

# resource "random_password" "pgadmin_password" {
#   length           = 32
#   special          = true
#   override_special = "_%@"
# }

# resource "azurerm_key_vault_secret" "pgadmin_password" {
#   name         = "pgadmin-password"
#   value        = random_password.pgadmin_password.result
#   key_vault_id = azurerm_key_vault.key_vault.id
# }