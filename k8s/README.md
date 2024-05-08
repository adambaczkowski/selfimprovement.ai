# apply all yamls
kubectl apply --recursive -f .

# port forwarding
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
kubectl -n postgresql port-forward svc/pgadmin-service 80
kubectl -n monitoring port-forward svc/grafana 3000
kubectl -n rabbitmq port-forward rabbitmq-0 8080:15672
kubectl -n monitoring port-forward svc/prometheus-operated 9090