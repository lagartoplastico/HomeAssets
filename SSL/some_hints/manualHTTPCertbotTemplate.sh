#!/bin/bash

# The --dry-run verify if the attempt is succsessfull. Then you can remove it in order to get a certificate.

docker run -it --rm --name certbot \
    --volume "/tmp/etc-letsencrypt:/etc/letsencrypt" \
    --volume "/tmp/var-lib-letsencrypt:/var/lib/letsencrypt" \
    certbot/certbot certonly \
    --dry-run \
    --manual --manual-public-ip-logging-ok \
    --email admin@example.com --agree-tos \
    --domain example.com --rsa-key-size 2048