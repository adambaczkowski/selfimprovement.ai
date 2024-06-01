output "resource_group_name" {
  value = var.rg-name
}

# output "kubernetes_cluster_name" {
#   value = azurerm_kubernetes_cluster.aks-dev.name
# }

# output "client_certificate" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].client_certificate
#   sensitive = true
# }

# output "client_key" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].client_key
#   sensitive = true
# }

# output "cluster_ca_certificate" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].cluster_ca_certificate
#   sensitive = true
# }

# output "cluster_password" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].password
#   sensitive = true
# }

# output "cluster_username" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].username
#   sensitive = true
# }

# output "host" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config[0].host
#   sensitive = true
# }

# output "kube_config" {
#   value     = azurerm_kubernetes_cluster.aks-dev.kube_config_raw
#   sensitive = true
# }

output "storage_account_name" {
  value = var.storage_account_name
}

output "container_name" {
  value = var.container_name
}