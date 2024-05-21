terraform {
  required_version = ">=1.0"

  required_providers {
    azapi = {
      source  = "azure/azapi"
      version = "1.13.1"
    }
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.104.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "3.6.1"
    }
    time = {
      source  = "hashicorp/time"
      version = "0.11.1"
    }
  }

  backend "azurerm" {
    resource_group_name  = "dev-rg"
    storage_account_name = "dev-storage"
    container_name       = "tfstate"
    key                  = "terraform.tfstate"
  }
}

provider "azurerm" {
  features {}

  subscription_id = var.arm_subscription_id
  tenant_id       = var.arm_tenant_id
  client_id       = var.arm_client_id
  client_secret   = var.arm_client_secret
}