FROM alpine:3.11
ENV PASS_SMTP=postfix
RUN echo -e "${PASS_SMTP}\n${PASS_SMTP}" | passwd && apk update && apk upgrade && apk add --no-cache postfix
WORKDIR /etc/postfix
COPY ./main.cf ./main.cf
RUN chmod 644 ./main.cf
VOLUME [ "/var/spool/postfix" ]
EXPOSE 25/tcp
CMD [ "postfix", "start-fg" ]