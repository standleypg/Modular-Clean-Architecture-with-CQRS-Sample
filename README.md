# Modular Clean Architecture With CQRS Implementation Template

## Disclaimer

**🚨 Template Disclaimer: This sample application is still in progress**

This repository provides a comprehensive template for implementing a Modular Clean Architecture with CQRS and OData integration. It serves as a starting point and best-practice reference for building scalable, maintainable .NET applications with advanced querying capabilities.

## Getting Started

### Development Environment Setup

- Install .NET 9 SDK or later
- Install Docker Desktop
- Docker Compose is provided for running the database

```bash
# run the database
# cd to Docker folder
docker compose -f docker-compose.yml -p docker up -d postgres
```

- Run the application
- For OData request examples, refer to `Documents/Api.http`. For more information, refer to the [OData documentation](https://learn.microsoft.com/en-us/odata/).

````bash

## Template Structure Overview

```plaintext
Solution/
├── src/
│   ├── Core/                   # Core business logic and contracts
│   │   ├── CompanyName.ProjectName.Domain/
│   │   └── CompanyName.ProjectName.Application/
│   ├── Infrastructure/         # Technical implementations
│   │   ├── CompanyName.ProjectName.Infrastructure/
│   │   └── CompanyName.ProjectName.Infrastructure.Messaging/
│   ├── Presentation/           # UI and API layers
│   │   ├── CompanyName.ProjectName.API/
│   │   └── CompanyName.ProjectName.Web/
│   └── CompanyName.ProjectName.Shared/  # Shared utilities
│
└── tests/                      # Comprehensive test coverage
    ├── CompanyName.ProjectName.Domain.Tests/
    ├── CompanyName.ProjectName.Application.Tests/
    ├── CompanyName.ProjectName.Infrastructure.Tests/
    └── CompanyName.ProjectName.API.Tests/
````

## Quick Start: Adapting the Template

### Namespace Conversion

- Replace `CompanyName` with your organization name
- Replace `ProjectName` with your specific project name

## Key Architectural Components

### Core Layer

- 🏗️ Domain logic and core contracts
- 🚦 Domain logic and core contracts
- 📦 Command/Query interfaces
- 🔔 Domain events and services

### Infrastructure Layer

- 🔧 Technical implementations
- 📋 Database access
- 🛡️ External service integrations
- 🔄 Messaging and event handling (if any)

### Presentation Layer

- 🎮 API endpoints
- 🌍 Web interfaces
- 🛡️ Middleware and filters
- 🔀 Request/response handling

## Shared Layer

- 🛠️ Cross-cutting utilities
- 📊 DTOs
- 🔗 Common extensions

## Best Practices Implemented

- ✅ Dependency Inversion
- ✅ Single Responsibility Principle
- ✅ Separation of Concerns
- ✅ Domain-Driven Design
- ✅ CQRS Pattern
- ✅ Unit of Work Pattern
- ✅ Repository Pattern
- ✅ Extensible Architecture
- ✅ Comprehensive Testing
- and more...

## Technologies & Dependencies

### Recommended Technology Stack

- .NET Core / .NET 8 or 9
- Entity Framework Core
- MediatR for CQRS
- FluentValidation
- Serilog/NLog
- OpenAPI
- Mapster/AutoMapper
- Unit Testing Framework (xUnit/NUnit)

## Customization Checklist

- [ ] Update namespace conventions
- [ ] Define domain entities
- [ ] Implement domain services
- [ ] Create specific commands/queries
- [ ] Configure database context
- [ ] Add authentication/authorization
- [ ] Implement logging strategy
- [ ] Set up dependency injection

## Performance Considerations

- Use asynchronous programming
- Implement caching strategies
- Optimize database queries
- Use bulk operations where possible
- Consider read/write database separation

## Security Recommendations

- Implement input validation
- Apply principle of least privilege
- Use environment-specific configurations
- Implement proper error handling

## Logging & Observability

- Structured logging
- Correlation IDs
- Performance metrics
- Exception tracking
- Distributed tracing support
