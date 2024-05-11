# Automatic Synchronization

kubectl -n rabbitmq exec -it rabbitmq-0 bash

rabbitmqctl set_policy ha-fed \
    ".*" '{"federation-upstream-set":"all", "ha-sync-mode":"automatic", "ha-mode":"nodes", "ha-params":["rabbit@rabbitmq-0.rabbitmq.rabbitmq.svc.cluster.local","rabbit@rabbitmq-1.rabbitmq.rabbitmq.svc.cluster.local","rabbit@rabbitmq-2.rabbitmq.rabbitmq.svc.cluster.local"]}' \
    --priority 1 \
    --apply-to queues