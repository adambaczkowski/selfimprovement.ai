# localdev
kind create cluster --name dev --image "kindest/node:v1.29.1"

## monitoring
kubectl apply -f ./k8s/namespaces/monitoring.yml
kubectl create -f ./k8s/monitoring/manifests/setup/
kubectl create -f ./k8s/monitoring/manifests/

## ingress nginx
kubectl apply -f ./k8s/namespaces/ingress-nginx.yml
kubectl -n ingress-nginx apply -f ./k8s/ingress/manifests/
kubectl -n ingress-nginx apply -f ./k8s/ingress/services/
kubectl -n ingress-nginx apply -f ./k8s/ingress/features/
### if above line fails run -> " kubectl delete -A ValidatingWebhookConfiguration ingress-nginx-admission "

## postgres
kubectl -n postgres create secret generic postgresql `
  --from-literal POSTGRES_USER="postgresadmin" `
  --from-literal POSTGRES_PASSWORD='admin123' `
  --from-literal POSTGRES_DB="postgresdb" `
  --from-literal REPLICATION_USER="replicationuser" `
  --from-literal REPLICATION_PASSWORD='replicationPassword'

kubectl apply -f ./k8s/namespaces/postgres.yml
kubectl -n postgres apply -f ./k8s/postgres/

### to login to pgadmin get IP of postgres instance from this command -> " kubectl get pod postgres-0 -o wide -n postgres " and provide credentials from above secret

## rabbitmq
kubectl apply -f ./k8s/namespaces/rabbitmq.yml
kubectl -n rabbitmq apply -f ./k8s/rabbitmq/

## ollama
kubectl apply -f ./k8s/namespaces/ollama.yml
kubectl -n ollama apply -f ./k8s/ollama/

# port forwarding
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
kubectl -n postgres port-forward svc/pgadmin-service 5050:80
kubectl -n monitoring port-forward svc/grafana 3000
kubectl -n rabbitmq port-forward rabbitmq-0 15672:15672
kubectl -n monitoring port-forward svc/prometheus-operated 9090
kubectl -n ollama port-forward svc/ollama 11434:80
kubectl -n ollama port-forward svc/open-webui 1337:8080