terraform init

terraform destroy

# delete service principal
# $spId = az ad sp list --display-name "dev-sp" --query "[0].appId" -o tsv
# az ad sp delete --id $spId

Remove-Item -Path "terraform.tfstate", "terraform.tfstate.backup", ".terraform.lock.hcl", "main.tfplan", "terraform.tfvars" -ErrorAction SilentlyContinue
Remove-Item -Path ".terraform" , ".ssh" -Recurse -Force -ErrorAction SilentlyContinue

kubectl config delete-context aks-dev-cluster