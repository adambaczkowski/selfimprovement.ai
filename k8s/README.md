# localdev
kind create cluster --name dev --image "kindest/node:v1.29.1"

# create namespaces
kubectl create namespace monitoring
kubectl create namespace ingress-nginx
kubectl create namespace postgres
kubectl create namespace rabbitmq
kubectl create namespace ollama

# apply all yamls
kubectl -n monitoring create -f ./k8s/monitoring/manifests/setup/
kubectl -n monitoring create -f ./k8s/monitoring/manifests/
kubectl -n ingress-nginx apply -f ./k8s/ingress/manifests/
kubectl -n ingress-nginx apply -f ./k8s/ingress/services/
kubectl -n ingress-nginx apply -f ./k8s/ingress/features/
kubectl -n postgres apply -f ./k8s/postgres/
kubectl -n rabbitmq apply -f ./k8s/rabbitmq/

# port forwarding
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
kubectl -n postgres port-forward svc/pgadmin-service 80
kubectl -n monitoring port-forward svc/grafana 3000
kubectl -n rabbitmq port-forward rabbitmq-0 8080:15672
kubectl -n monitoring port-forward svc/prometheus-operated 9090