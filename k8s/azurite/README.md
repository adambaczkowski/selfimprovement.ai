# Azurite needs a SSL certificate in order to run

### If OpenSSL is not installed:
```plaintext
docker run --rm -it -v ${PWD}:/work -w /work alpine sh
apk add openssl
```
# 1. Generate the Certificate Signing Request (CSR) using the configuration file:
```plaintext
openssl req -config tls.conf -new -out csr.pem
```
# 2 Generate the self-signed certificate using the CSR:
```plaintext
openssl x509 -req -days 365 -extfile tls.conf -extensions v3_req -in csr.pem -signkey key.pem -out cert.pem
```
# 3. Exit docker containter
```plaintext
exit
```
# 4 Create a Kubernetes secret named azurite-certs with the certificate and key files:
```plaintext
kubectl create secret generic azurite-certs --from-file=cert.pem --from-file=key.pem
```
