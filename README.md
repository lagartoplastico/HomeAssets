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

> **CS**=Host=db;Port=5432;Database=*value3*;Username=*value1*;Password=*value2*

> **From**=*value4*

> **MailServer**=*value5*

> **MailUsername**=*value6*

> **MailPassword**=*value7*

  

### How to fill .env:

- Credentials "*value1*", "*value2*" and "*value3*" should be change by you to what you consider best secure values for the database.

- "*value4*" should be a mail address inside your mailserver that you can use e.g. *username@jdevops.xyz*

- "*value5*", "*value6*" and "*value7*" are data inherent to the mail server that you will use.

  

### Do not change:

- Value db Is the name given to the database service inside the docker-compose file.

- The database runs locally in the default postgres port 5432 but outside the container you can connect it through the 55342 port (see docker-compose.yml file).

  

### Important considerations:

- If you change POSTGRES_USER to a different value that is not the default postgres user, you have to replace into the init_schema.sh file the statements "Owner: postgres" to "Owner: value1". Where value1 is the value you set inside the .env file.

- The example domain used in this project is jdevops.xyz. You have to update this name with an appropiate domain.

- The docker-compose.yml file has a network called "mailserver_net" that references the docker network where the mail server you use runs. So, if your mail server doesn't run in the same docker ecosystem with this app erase the following lines in the docker-compose.yml file:

docker-compose.yml

    -mailserver_net
    
    mailserver_net:
    
    external: true

  

But if your mail server runs in the same docker ecosystem with this app, you have to change inside the docker-compose.yml file the name "mailserver_net" with the appropiate docker network where your mail server connects.

### Add the TLS certificate and key:

- Add the https certificate inside a folder called https in the root path.

- If you want to read your certificate from other location, you should update the volumes part in the docker-compose.yml file.

  

### Run with docker-compose.

Next, you can run the docker-compose file with the following command:

  

    sudo docker-compose up -d

  

And check the HomeAssets webapp in the port 80 or 443 of your browser.
