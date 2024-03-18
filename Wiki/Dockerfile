FROM alpine:latest
RUN apk update
RUN apk upgrade
RUN apk add --no-cache --repository=https://dl-cdn.alpinelinux.org/alpine/edge/community hugo
RUN hugo version
WORKDIR /site
COPY ./site .
CMD ["hugo", "server", "--disableFastRender", "--bind", "0.0.0.0", "--logLevel", "info"]
