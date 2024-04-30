# DsrPractic2024

## To launch the application

Before starting using applications you need to do next steps:

#### Step №1 Setup project files

Change this parameters in  `env.worker` file and appsettings.json in Worker project to use some smtp server:

```
MailSettings__From=andreydsr2024@yandex.ru
MailSettings__Host=smtp.yandex.ru
MailSettings__Port=587
MailSettings__Login=andreydsr2024@yandex.ru
MailSettings__Password=ocammqjpjcpkhkfe
```
The settings of the current mail server are working and you can use them. When specifying your mailbox, you must generate a password for external services. When connecting, it is used, not the account password.
Above is an example of how those fields should be written.  

```
To start a project, go to the project folder and run the commands:
docker-compose build
docker-compose up
```
Further instructions are in the file "Инструкция.docx "
