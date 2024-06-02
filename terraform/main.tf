data "azurerm_client_config" "current" {}

resource "azurerm_container_registry" "acr-dev" {
  name                = "acrselfimprovement"
  resource_group_name = var.rg-name
  location            = var.location
  sku                 = "Standard"
  admin_enabled       = true
}

# resource "azurerm_kubernetes_cluster" "aks-dev" {
#   location            = var.location
#   name                = "aks-dev-cluster"
#   resource_group_name = var.rg-name
#   dns_prefix          = "aks-dev-dns"

#   service_principal {
#     client_id     = var.arm_client_id
#     client_secret = var.arm_client_secret
#   }

#   default_node_pool {
#     name            = "agentpool"
#     vm_size         = "Standard_D2_v2"
#     node_count      = var.node_count
#     type            = "VirtualMachineScaleSets"
#     os_disk_size_gb = 250
#   }

#   linux_profile {
#     admin_username = var.username

#     ssh_key {
#       key_data = var.arm_ssh_key
#     }
#   }
#   network_profile {
#     network_plugin    = "kubenet"
#     load_balancer_sku = "standard"
#   }
# }

resource "azurerm_key_vault" "key_vault" {
  name                       = "selfimprovementKeyVault"
  location                   = var.location
  resource_group_name        = var.rg-name
  sku_name                   = "standard"
  tenant_id                  = var.arm_tenant_id
  soft_delete_retention_days = 7
  purge_protection_enabled   = false

   access_policy {
    tenant_id = var.arm_tenant_id
    object_id = data.azurerm_client_config.current.object_id

    key_permissions = [
      "Create",
      "Get",
    ]

    secret_permissions = [
      "Set",
      "Get",
      "Delete",
      "Purge",
      "Recover"
    ]
  }
}

# resource "azurerm_key_vault_secret" "postgres_user_secret" {
#   name         = "postgresUser"
#   value        = var.postgres_user
#   key_vault_id = azurerm_key_vault.key_vault.id
# }

# resource "azurerm_key_vault_secret" "postgres_password_secret" {
#   name         = "postgresPassword"
#   value        = var.postgres_password
#   key_vault_id = azurerm_key_vault.key_vault.id
# }

# resource "azurerm_key_vault_secret" "postgres_db_name_secret" {
#   name         = "postgresDbName"
#   value        = var.postgres_db_name
#   key_vault_id = azurerm_key_vault.key_vault.id
# }

resource "azurerm_key_vault_secret" "pgadmin_email_secret" {
  name         = "pgAdminEmail"
  value        = var.pgadmin_email
  key_vault_id = azurerm_key_vault.key_vault.id
}

resource "azurerm_key_vault_secret" "pgadmin_password_secret" {
  name         = "pgAdminPassword"
  value        = var.pgadmin_password
  key_vault_id = azurerm_key_vault.key_vault.id
}

resource "azurerm_key_vault_secret" "rabbitmq_user_secret" {
  name         = "rabbitmqUser"
  value        = var.rabbitmq_user
  key_vault_id = azurerm_key_vault.key_vault.id
}

resource "azurerm_key_vault_secret" "rabbitmq_password_secret" {
  name         = "rabbitmqPassword"
  value        = var.rabbitmq_password
  key_vault_id = azurerm_key_vault.key_vault.id
}
