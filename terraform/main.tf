resource "azurerm_resource_group" "aks-dev" {
  location = var.resource_group_location
  name     = "aks-dev"
}

resource "azurerm_kubernetes_cluster" "aks-dev" {
  location            = azurerm_resource_group.aks-dev.location
  name                = "aks-dev-cluster"
  resource_group_name = azurerm_resource_group.aks-dev.name
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
