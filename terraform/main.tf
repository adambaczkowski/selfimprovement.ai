resource "azurerm_container_registry" "acr-dev" {
  name                = "acrselfimprovement"
  resource_group_name = var.rg-name
  location            = var.location
  sku                 = "Standard"
  admin_enabled       = true
}

resource "azurerm_kubernetes_cluster" "aks-dev" {
  location            = var.location
  name                = "aks-dev-cluster"
  resource_group_name = var.rg-name
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

resource "azurerm_key_vault" "key_vault" {
  name                       = "selfimprovementKeyVault"
  location                   = var.location
  resource_group_name        = var.rg-name
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
  object_id    = var.arm_client_id

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

resource "azurerm_key_vault_secret_pg_admin" "key_vault" {
  name         = "pg_admin_secret"
  value        = var.pg_admin_secret
  key_vault_id = azurerm_key_vault.key_vault.id
}