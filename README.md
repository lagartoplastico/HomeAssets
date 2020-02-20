# HomeAssets

Manage informative cards about home services as:

-  *Electrical service*

-  *Gas service*

-  *Water supply service*

-  *And internet*

  

## Run HomeAssets in a docker + docker-compose platform.

### .env

You need to create a ".env" file with the following environment variables:

  

> **POSTGRES_USER**=*value1*

> **POSTGRES_PASSWORD**=*value2*

> **POSTGRES_DB**=*value3*

> **CONNECTION_STRINGS**=Host=db;Port=5432;Database=*value3*;Username=*value1*;Password=*value2*

> **SENDER_ADDRESS**=*value4*

> **SENDER_NAME**=*value5*

> **MAIL_SERVER**=*value6*

> **SMTP_PORT**=*value7*

> **MAIL_USERNAME**=*value8*

> **MAIL_PASSWORD**=*value9*

> **KESTREL_CERT_NAME**=*value10*

> **KESTREL_CERT_PASS**=*value11*


### How to fill .env:

- Credentials "*value1*", "*value2*" and "*value3*" should be change by you to what you consider best secure values for the database.

- "*value4*" should be a mail address inside your mailserver authorized to send emails e.g. *username@jdevops.xyz*

- "*value5*" The name of the sender e.g. *Pepito Perez*

- "*value6*", "*value7*", "*value8*",  and "*value9*" are data inherent to the mail server that you will use.

- "*value10*" is the name of the certificate used in for HTTPS. This certificate should be place in the SSL directory. "*value11*" is the certificate password.

  

### Do not change:

- Value db Is the name given to the database service inside the docker-compose file.

- The database runs locally in the default postgres port 5432 but outside the container you can connect it through the 55342 port (see docker-compose.yml file).

  

### Important considerations:

- If you change POSTGRES_USER to a different value that is not the default postgres user, you have to replace into the init_schema.sh file the statements "Owner: postgres" to "Owner: value1". Where value1 is the value you set inside the .env file.

- The example domain used in this project is jdevops.xyz. You have to update this name with an appropiate valid domain.

### Run with docker-compose.

Next, you can run the docker-compose file with the following command:

  

    sudo docker-compose up -d

  

And check the HomeAssets webapp in the port 80(HTTP) or 443(HTTPS) of your browser.
