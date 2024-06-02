#!/bin/sh
chmod 600 /var/lib/rabbitmq/.erlang.cookie
exec docker-entrypoint.sh rabbitmq-server
