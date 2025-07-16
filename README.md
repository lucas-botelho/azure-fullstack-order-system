# Fullstack Order System

This repository contains a small fullstack system that demonstrates:
- A .NET 8 REST API to create and persist orders.
- A React frontend to submit new orders.
- An Azure Function to consume messages from Azure Service Bus and update the order status to "Processed".
- A CI/CD pipeline to build and deploy the full solution.
- A placeholder infrastructure file using Bicep (`infra.bicep`).

## Solution Structure
/OrdersApi → .NET 8 API for handling orders
/OrderFunction → Azure Function to process orders from Azure Service Bus
/order-frontend → React frontend to submit new orders
/.github or azure-pipelines.yml → CI/CD Pipeline
infra.bicep → Placeholder for infrastructure definition


---

## 1. .NET 8 REST API (`/OrdersApi`)
- **POST /orders**: Receives an order: `{ Id, ProductName, Quantity, Status }`
- Saves the order to **SQL Server** (Entity Framework or mock).
- Sends a message to **Azure Service Bus** (real or simulated).
- Uses **Dependency Injection** and applies **SOLID principles**.
- Endpoint is protected using **JWT Auth** (mock or dummy authority).

---

## 2. React Frontend (`/order-frontend`)
- Simple form to submit a new order.
- Submits data to the .NET API via REST POST.
- Shows success or error feedback to the user.
- Built with functional components and React hooks.

---

## 3. Azure Function (`/OrderFunction`)
- Triggered by **Azure Service Bus** messages.
- Logs the received message.
- Simulates processing the order by updating status to `"Processed"` (can be in-memory or log-based).

---

## 4. CI/CD Pipeline (`azure-pipelines.yml`)
The pipeline includes:
- **Build stage**:
  - Builds the .NET API using `dotnet build`
  - Builds the React frontend using `npm run build`
- **Infrastructure stage**:
  - Placeholder to deploy `infra.bicep` (e.g. storage, function app, service bus)
- **Deployment stage**:
  - Placeholder for deploying the API, frontend and Azure Function

---

## 5. Design Principles

This solution follows several **SOLID** principles:

- **S – Single Responsibility**: Services are split (e.g. `OrderService`, `MessageBusService`)
- **O – Open/Closed**: Code allows for extension (e.g. different storage backends)
- **L – Liskov**: Interfaces respected (`IOrderRepository`, etc.)
- **I – Interface Segregation**: Interfaces define minimal contracts
- **D – Dependency Inversion**: Dependencies injected (e.g. `AppDbContext`, `ServiceBusClient`)

---

## 6. Debugging & Monitoring

In a real system:
- Use **Application Insights** or **Azure Monitor** to observe logs.
- Set up **alerts** for failed Azure Function executions.
- Instrument logs using `ILogger<T>` in .NET and `context.log()` in Azure Functions.

During development:
- Test API locally using **Postman** or **cURL**.
- Test Service Bus flow locally with **Azure Storage Emulator** or **Service Bus Explorer**.
- React frontend uses simple alert feedback and can be tested with **browser dev tools**.

---

## Deliverables

- ✅ .NET 8 API project: `/OrdersApi`
- ✅ React frontend: `/order-frontend`
- ✅ Azure Function project: `/OrderFunction`
- ✅ CI/CD Pipeline: `azure-pipelines.yml`
- ✅ Infrastructure Bicep: `infra.bicep`
- ✅ README with documentation and explanation

---

## Resources Used

- [.NET 8 Minimal APIs](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)
- [Azure Service Bus with .NET](https://learn.microsoft.com/en-us/azure/service-bus-messaging/)
- [Azure Functions documentation](https://learn.microsoft.com/en-us/azure/azure-functions/)
- [React Docs](https://react.dev/)
- [Azure Pipelines YAML schema](https://learn.microsoft.com/en-us/azure/devops/pipelines/yaml-schema)

---


React (Frontend)
   |
   v
.NET 8 API (POST /orders) ─────────┐
   |                               │
   └─> Save order to SQL Server    │
                                   v
                        Azure Service Bus (Queue)
                                   |
                                   v
                        Azure Function (Process Order)
                             └─> Update order status to "Processed"

