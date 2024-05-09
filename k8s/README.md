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
kubectl apply -f ./k8s/namespaces/postgres.yml
kubectl -n postgres apply -f ./k8s/postgres/

## rabbitmq
kubectl apply -f ./k8s/namespaces/rabbitmq.yml
kubectl -n rabbitmq apply -f ./k8s/rabbitmq/

## ollama
kubectl apply -f ./k8s/namespaces/ollama.yml
kubectl -n ollama apply -f ./k8s/ollama/

# port forwarding
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
kubectl -n postgres port-forward svc/pgadmin-service 80
kubectl -n monitoring port-forward svc/grafana 3000
kubectl -n rabbitmq port-forward rabbitmq-0 15672:15672
kubectl -n monitoring port-forward svc/prometheus-operated 9090
kubectl -n ollama port-forward svc/ollama 11434