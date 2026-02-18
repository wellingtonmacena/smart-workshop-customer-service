# Core Service - Smart Workshop

## 📋 Visão Geral

O **Core Service** é responsável pelo gerenciamento de cadastros centrais da oficina mecânica Smart Workshop. Este serviço gerencia dados mestres como clientes, funcionários, veículos, insumos e catálogo de serviços.

## 🎯 Responsabilidades

- ✅ **Gerenciamento de Pessoas** (Clientes e Funcionários)
- ✅ **Gerenciamento de Endereços**
- ✅ **Gerenciamento de Veículos**
- ✅ **Catálogo de Peças/Insumos** (Supplies)
- ✅ **Catálogo de Serviços Disponíveis**
- ✅ **Controle de Estoque**

## 🗄️ Banco de Dados

**Tipo:** PostgreSQL  
**Database:** `smart_workshop_core`

### Entidades

1. **Person** - Representa clientes e funcionários
   - Document (CPF/CNPJ)
   - Fullname
   - PersonType (Client/Employee)
   - EmployeeRole (opcional)
   - Email
   - Phone
   - Password
   - Address

2. **Address** - Endereços
   - Street
   - City
   - State
   - ZipCode

3. **Vehicle** - Veículos dos clientes
   - LicensePlate (placa única)
   - Brand (marca)
   - Model (modelo)
   - ManufactureYear
   - PersonId (dono)

4. **Supply** - Peças e insumos
   - Name
   - Price
   - Quantity (estoque)

5. **AvailableService** - Serviços oferecidos
   - Name
   - Price
   - Supplies (peças necessárias)

## 📡 Eventos Publicados

### CustomerCreatedEvent

Publicado quando um novo cliente é cadastrado.

```json
{
  "eventId": "guid",
  "occurredAt": "2026-02-16T00:00:00Z",
  "eventType": "CustomerCreatedEvent",
  "customerId": "guid",
  "fullname": "João Silva",
  "document": "12345678901",
  "email": "joao@example.com",
  "phone": "11 98765-4321"
}
```

### VehicleRegisteredEvent

Publicado quando um novo veículo é cadastrado.

```json
{
  "eventId": "guid",
  "occurredAt": "2026-02-16T00:00:00Z",
  "eventType": "VehicleRegisteredEvent",
  "vehicleId": "guid",
  "licensePlate": "ABC1234",
  "brand": "Toyota",
  "model": "Corolla",
  "manufactureYear": 2020,
  "ownerId": "guid"
}
```

### SupplyStockChangedEvent

Publicado quando o estoque de um insumo é alterado.

```json
{
  "eventId": "guid",
  "occurredAt": "2026-02-16T00:00:00Z",
  "eventType": "SupplyStockChangedEvent",
  "supplyId": "guid",
  "name": "Óleo 5W30",
  "oldQuantity": 10,
  "newQuantity": 5,
  "change": -5
}
```

### ServicePriceUpdatedEvent

Publicado quando o preço de um serviço é atualizado.

```json
{
  "eventId": "guid",
  "occurredAt": "2026-02-16T00:00:00Z",
  "eventType": "ServicePriceUpdatedEvent",
  "serviceId": "guid",
  "name": "Troca de Óleo",
  "oldPrice": 150.0,
  "newPrice": 170.0
}
```

## 🔌 APIs

### People Endpoints

```http
GET /api/people
Response: 200 OK
[
  {
    "id": "guid",
    "fullname": "João Silva",
    "document": "12345678901",
    "personType": "Client",
    "email": "joao@example.com",
    "phone": "11 98765-4321"
  }
]

POST /api/people
Content-Type: application/json
{
  "fullname": "João Silva",
  "document": "12345678901",
  "personType": "Client",
  "email": "joao@example.com",
  "password": "senha123",
  "phone": "11 98765-4321",
  "address": {
    "street": "Rua A, 123",
    "city": "São Paulo",
    "state": "SP",
    "zipCode": "01234-567"
  }
}
Response: 201 Created

GET /api/people/{id}
Response: 200 OK

PUT /api/people/{id}
Content-Type: application/json
Response: 200 OK

DELETE /api/people/{id}
Response: 204 No Content
```

### Vehicles Endpoints

```http
GET /api/vehicles
GET /api/vehicles/{id}
GET /api/vehicles/by-plate/{licensePlate}
POST /api/vehicles
PUT /api/vehicles/{id}
DELETE /api/vehicles/{id}
```

### Supplies Endpoints

```http
GET /api/supplies
GET /api/supplies/{id}
GET /api/supplies/low-stock?threshold=10
POST /api/supplies
PUT /api/supplies/{id}
POST /api/supplies/{id}/add-stock
POST /api/supplies/{id}/remove-stock
DELETE /api/supplies/{id}
```

### Services Endpoints

```http
GET /api/services
GET /api/services/{id}
POST /api/services
PUT /api/services/{id}
DELETE /api/services/{id}
```

## 🏗️ Arquitetura

```
SmartWorkshop.Core.Api          (ASP.NET Core Web API)
SmartWorkshop.Core.Application  (Use Cases / CQRS)
SmartWorkshop.Core.Domain       (Entities / Value Objects / Events)
SmartWorkshop.Core.Infrastructure (EF Core / Repositories / External Services)
```

### Princípios Aplicados

- **Clean Architecture**
- **Domain-Driven Design (DDD)**
- **CQRS** (Command Query Responsibility Segregation)
- **Repository Pattern**
- **Event Sourcing**

## 🚀 Executar Localmente

```bash
# 1. Restaurar dependências
cd smart-workshop-core-service
dotnet restore

# 2. Configurar connection string
# Editar SmartWorkshop.Core.Api/appsettings.json

# 3. Aplicar migrations
cd SmartWorkshop.Core.Api
dotnet ef database update

# 4. Executar
dotnet run
```

O serviço estará disponível em: `http://localhost:5001`

## 🔧 Configuração

### appsettings.json

```json
{
  "ConnectionStrings": {
    "CoreDatabase": "Host=localhost;Database=smart_workshop_core;Username=postgres;Password=postgres"
  },
  "RabbitMQ": {
    "Host": "localhost",
    "Port": 5672,
    "Username": "guest",
    "Password": "guest",
    "Exchange": "smart_workshop_events"
  }
}
```

## 🧪 Testes

```bash
# Testes unitários
dotnet test SmartWorkshop.Core.Domain.Tests

# Testes de integração
dotnet test SmartWorkshop.Core.Integration.Tests
```

## 📦 Dependências

- Microsoft.EntityFrameworkCore
- Npgsql.EntityFrameworkCore.PostgreSQL
- RabbitMQ.Client
- MediatR
- FluentValidation
- Serilog

## 📝 Próximos Passos

- [x] Domain Layer (Entities, Value Objects, Events)
- [x] Infrastructure Layer (DbContext)
- [ ] Application Layer (Use Cases)
- [ ] API Layer (Controllers)
- [ ] Repositories Implementation
- [ ] Event Publishers
- [ ] Validations
- [ ] Unit Tests
- [ ] Integration Tests

## 👥 Contato

Wellington Macena - RM366131
