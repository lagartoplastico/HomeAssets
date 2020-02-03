# HomeAssets

Manage informative cards about home services as:
 
 - electrical service
 - gas service
 - water supply service
 - and internet.

You need to create a "db.env", "mail.env" and "webapp.env" files with the following environment variables:
 
### db.env
POSTGRES_USER=value1
POSTGRES_PASSWORD=value2
POSTGRES_DB=value3

Credentials "value1", "value2" and "value3" should be change by you to what you consider best secure values.

### mail.env
PASS_SMTP=value4

Credential "value4" should be change by you to what you consider best secure value.

### webapp.env
ConnectionStrings:HomeAssetsDB=Host=db;Port=5432;Database=value3;Username=value1;Password=value2
SmtpSettings:From=value5
SmtpSettings:Server=mail
SmtpSettings:Username=root
SmtpSettings:Password=value4

"value5" should be a mail address e.g. example@example.com.


Next, you can run the docker-compose file.