# Developer Evaluation Project

## Project Overview

This project is built using Domain-Driven Design (DDD) principles, with a focus on cleanly separating the domain logic from infrastructure concerns. We employed the CQRS Mediator pattern to manage command and query responsibilities effectively, ensuring that the codebase remains maintainable and scalable.

In order to handle cross-domain references efficiently, the External Identities pattern has been implemented. This approach uses denormalization of entity descriptions, which simplifies the process of integrating and referencing entities from other domains.

For asynchronous communication and event-driven workflows, RabbitMQ has been utilized to implement events publishing. Key business events—such as SaleCreated, SaleModified, SaleCancelled, and ItemCancelled—are published to RabbitMQ, ensuring that our services remain decoupled and can react to changes in the system in real time.

The project leverages the full tech stack provided, integrating established tools and frameworks to ensure robust development, thorough testing, and streamlined deployment processes.

## Tech Stack

Backend:
- **.NET 8.0**: A free, cross-platform, open source developer platform for building many different types of applications.
  - Git: https://github.com/dotnet/core
- **C#**: A modern object-oriented programming language developed by Microsoft.
  - Git: https://github.com/dotnet/csharplang

Testing:
- **xUnit**: A free, open source, community-focused unit testing tool for the .NET Framework.
  - Git: https://github.com/xunit/xunit

Frontend:
- **Angular**: A platform for building mobile and desktop web applications.
  - Git: https://github.com/angular/angular

Databases:
- **PostgreSQL**: A powerful, open source object-relational database system.
  - Git: https://github.com/postgres/postgres
- **MongoDB**: A general purpose, document-based, distributed database.
  - Git: https://github.com/mongodb/mongo

Message Brokers
- **RabbitMQ**: A powerfull message broker
   - Git: https://github.com/rabbitmq/rabbitmq-server

## Run the application

### HTTPS Certificate Generation (Mandatory)
Generate the HTTPS certificate by running the following command:

```bash
dotnet dev-certs https -ep "C:\Users\junho\AppData\Roaming\ASP.NET\https\certificate.pfx" -p credential --trust
```

> Note: You can generate the certificate in any location and choose any filename or password. If you change the path or password, update the corresponding settings in:
> - docker-compose.yml (volumes configuration)
>
> - appsettings.json (certificate password)

### Running the Project

Build Containers:

```bash
docker compose build
```

Run Containers:

```bash
docker compose up --build
```

## API Overview
This API provides full CRUD functionality for sales records. It supports:

Sales Details:

Sale number
Sale date
Customer information
Total sale amount
Branch information
Product Details:

List of products with quantities, unit prices, discounts, and total per item
Cancellation Status:
Mark sales or individual items as cancelled.

## Business Rules
Discounts:

4 or more identical items: 10% discount
10 to 20 identical items: 20% discount
Fewer than 4 items: No discount
Limitations:

A maximum of 20 identical items can be sold
Optional Event Logging
Event logging is implemented for:

SaleCreated
SaleModified
SaleCancelled
ItemCancelled
These events are logged in the application log and send to RabbitMQ queues.