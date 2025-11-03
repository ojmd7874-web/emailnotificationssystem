# EmailNotifications - Quick Start

Prerequisites
- Docker Desktop installed and running.
- .NET 8 SDK installed (compatible with Visual Studio 2022).
- Visual Studio 2022 (or use dotnet CLI + multiple terminals).

Overview and instructions
- RabbitMQ is required for the queue consumer. Start RabbitMQ with the provided Docker Compose file:
  - docker compose -f docker-compose.rabbitmq-only.yml up
- Database is created and migrations are applied automatically when the application is started. The application uses a local SQL Server database file (LocalDB / .mdf).
- Startup projects to run together: EmailNotifications.API, EmailNotifications.UI, EmailNotifications.EmailQueueConsumer.
- For testing the loading of xml, use the sample-load-data.xml from root folder in the Admin part from UI project.

Admin UI:
<img width="1422" height="533" alt="image" src="https://github.com/user-attachments/assets/5f4351a9-befd-4cc8-beb3-3afd44bc3f93" />
Clients UI:
<img width="1431" height="519" alt="image" src="https://github.com/user-attachments/assets/3069f98e-4d2b-4c7c-86cb-78b4de9c4ba9" />
Templates UI:
<img width="1411" height="764" alt="image" src="https://github.com/user-attachments/assets/6f7f23d2-b372-44c0-976e-1aaff2dcd3ab" />


