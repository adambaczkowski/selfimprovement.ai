terraform {
  backend "azurerm" {
    resource_group_name  = "dev-rg"
    storage_account_name = "selfimprovementstorage"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }
}
