# localdev
##### https://kind.sigs.k8s.io/docs/user/quick-start/

```plaintext
kind create cluster --name dev --image "kindest/node:v1.29.1"
```

## monitoring
```plaintext
kubectl apply -f ./k8s/namespaces/monitoring.yml
kubectl create -f ./k8s/monitoring/manifests/setup/
kubectl create -f ./k8s/monitoring/manifests/
```
## ingress nginx
```plaintext
kubectl apply -f ./k8s/namespaces/ingress-nginx.yml
kubectl -n ingress-nginx apply -f ./k8s/ingress/manifests/
kubectl -n ingress-nginx apply -f ./k8s/ingress/services/
kubectl -n ingress-nginx apply -f ./k8s/ingress/features/
```
### if above line fails run -> " kubectl delete -A ValidatingWebhookConfiguration ingress-nginx-admission "

## postgres
```plaintext
kubectl -n postgres create secret generic postgresql `
  --from-literal POSTGRES_USER="postgresadmin" `
  --from-literal POSTGRES_PASSWORD='admin123' `
  --from-literal POSTGRES_DB="postgresdb" `
  --from-literal REPLICATION_USER="replicationuser" `
  --from-literal REPLICATION_PASSWORD='replicationPassword'
```

```plaintext
kubectl apply -f ./k8s/namespaces/postgres.yml
kubectl -n postgres apply -f ./k8s/postgres/
```

### to login to pgadmin get IP of postgres instance from this command -> " kubectl get pod postgres-0 -o wide -n postgres " and provide credentials from above secret

## rabbitmq
```plaintext
kubectl apply -f ./k8s/namespaces/rabbitmq.yml
kubectl -n rabbitmq apply -f ./k8s/rabbitmq/
```

## ollama
```plaintext
kubectl apply -f ./k8s/namespaces/ollama.yml
kubectl -n ollama apply -f ./k8s/ollama/
```

# port forwarding
```plaintext
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
```
```plaintext
kubectl -n postgres port-forward svc/pgadmin-service 5050:80
```
```plaintext
kubectl -n monitoring port-forward svc/grafana 3000
```
```plaintext
kubectl -n rabbitmq port-forward rabbitmq-0 15672:15672
```
```plaintext
kubectl -n monitoring port-forward svc/prometheus-operated 9090
```
```plaintext
kubectl -n ollama port-forward svc/ollama 11434:80
```
```plaintext
kubectl -n ollama port-forward svc/open-webui 1337:8080
```