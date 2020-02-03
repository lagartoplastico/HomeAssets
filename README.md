# HomeAssets

Manage informative cards about home services as:
 
 - electrical service
 - gas service
 - water supply service
 - and internet.

You need to create a "db.env" and "webapp.env" files with the following environment variables:
 
### db.env

POSTGRES_USER=value1
POSTGRES_PASSWORD=value2
POSTGRES_DB=value3

"value1", "value2" and "value3" can be change by you to what you consider best secure values.

### webapp.env

ConnectionStrings:HomeAssetsDB=Host=db;Port=5432;Database=value3;Username=value1;Password=value2
SmtpSettings:From=value4
SmtpSettings:Server=mail
SmtpSettings:Username=value5
SmtpSettings:Password=value6

"value4" should be a mail address e.g. example@example.com.
"value5" and "value6" should match the mail server credentials.
