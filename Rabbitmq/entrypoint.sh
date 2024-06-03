#!/bin/sh
openssl rand -base64 32 | tr -d '\n' | cut -c1-64 > /var/lib/rabbitmq/.erlang.cookie
chmod 600 /var/lib/rabbitmq/.erlang.cookie
exec docker-entrypoint.sh rabbitmq-server
