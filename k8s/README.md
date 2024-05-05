# apply all yamls
kubectl apply --recursive -f .

# port forwarding
kubectl -n ingress-nginx port-forward svc/ingress-nginx-controller 443
kubectl -n postgresql port-forward svc/pgadmin-service 80