# 📝 ToDoListTask
A clean **Onion Architecture ASP.NET Core Web API** for managing users and todo items.  
Built with Entity Framework Core, JWT Authentication, FluentValidation, and Docker support.  
**Saleh Nawwar Task**
---

## 🚀 Features
- **User Management** (register, login, roles with JWT)  
- **Todo Items** (CRUD operations, pagination, filtering)  
- **Validation** with FluentValidation  
- **Entity Framework Core** (SQL Server)  
- **Authentication & Authorization** via JWT  
- **Docker & Docker Compose** support  

---

## ⚡ Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (or SQL Server Developer edition)  
- [EF Core Tools](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) → install with:
  ```bash
  dotnet tool install --global dotnet-ef
  ```

---

## 🟢 1. Run the project from GitHub (without Docker)

### 1. Clone repository
```bash
git clone https://github.com/<your-username>/ToDoListTask.git
cd ToDoListTask
```

### 2. Update `appsettings.json`
Set your SQL Server connection string (Windows SQL Express example):
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TodoAppDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Or use SQL authentication:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TodoAppDb;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;"
}
```

### 3. Apply EF Core migrations
```bash
dotnet ef database update -p Infrastructure -s ToDoListTask
```

### 4. Run the API
```bash
cd ToDoListTask
dotnet run
```

API is now running at 👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)  

---

## 🟢 2. Run the project with Docker (from Docker Hub)

### Option A: Run only API (connects to your host SQL Server)
```bash
docker run -d -p 5000:80 \
  -e ConnectionStrings__DefaultConnection="Server=host.docker.internal\\SQLEXPRESS;Database=TodoAppDb;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;" \
  --name todoapp-api \
  <your-dockerhub-username>/todoapp-api:latest
```

Access at 👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)  

---

### Option B: Run API + SQL Server together (recommended, with docker-compose)

1. Create `docker-compose.yml` (already in repo):  
   ```yaml
   version: '3.8'

   services:
     sql-express:
       image: mcr.microsoft.com/mssql/server:2022-latest
       container_name: sql-express
       environment:
         SA_PASSWORD: "YourStrong!Pass123"
         ACCEPT_EULA: "Y"
         MSSQL_PID: "Express"
       ports:
         - "1433:1433"
       volumes:
         - sql-data:/var/opt/mssql

     todoapp-api:
       image: <your-dockerhub-username>/todoapp-api:latest
       container_name: todoapp-api
       environment:
         - ConnectionStrings__DefaultConnection=Server=sql-express;Database=TodoAppDb;User Id=sa;Password=YourStrong!Pass123;TrustServerCertificate=True;
         - ASPNETCORE_ENVIRONMENT=Development
       ports:
         - "5000:80"
       depends_on:
         - sql-express

   volumes:
     sql-data:
   ```

2. Run it:
   ```bash
   docker-compose up -d
   ```

API is now running at 👉 [http://localhost:5000/swagger](http://localhost:5000/swagger)  
SQL Express runs inside a container on port `1433`.  

---

## 🧪 Testing
- Go to Swagger UI: [http://localhost:5000/swagger](http://localhost:5000/swagger)  
- Register a user → Login → Copy JWT token → Authorize requests.  
- Create and manage todo items.  

---

## 📂 Project Structure
- **TodoApp.Domain** → Entities, enums, core logic  
- **TodoApp.Application** → DTOs, services, validation, contracts  
- **TodoApp.Infrastructure** → EF Core DbContext, repositories, migrations  
- **ToDoListTask** → Controllers, Program.cs, dependency injection  

---

## 🛠️ Notes
- **Owner role account** has username "admin" and password "admin" to be able to test api you should login with these credentials
- In **dev mode**, you can use SQL Server Express installed locally.  
- In **Docker mode**, you can either connect to host SQL (`host.docker.internal`) or run SQL in a container with compose.  
- Auto migrations on startup can be enabled in `Program.cs` with:
  ```csharp
  db.Database.Migrate();
  ```
## 🧪 Testing Strategy
Currently, the project relies on **manual testing** through:
- Swagger UI (http://localhost:5000/swagger)
- SQL Server queries for database verification

Automated unit and integration tests are not included at this stage, since the focus was on building the core architecture (Domain, Application, Infrastructure, API) and ensuring functionality manually.  
Future improvements can include adding xUnit/NUnit tests and automated CI/CD pipelines.