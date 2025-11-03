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

