# CleanCore.CQRSRepoApi
A modular and scalable .NET Core solution implementing Clean Architecture, CQRS, Entity Framework Core, Repository Pattern, and .NET Identity.

# Clean Architecture API (.NET 8.0)

A modular, scalable, and testable ASP.NET Core Web API solution following **Clean Architecture** principles with **CQRS**, **MediatR**, **FluentValidation**, **Entity Framework Core**, **JWT Authentication**, and **Global Response Wrapping** using `ResponseData<T>`.

---

## 🚀 Features

- ✅ Clean Architecture (Presentation, Application, Domain, Infrastructure)
- ✅ CQRS + MediatR for Commands and Queries
- ✅ Global Validation with FluentValidation Pipeline
- ✅ Centralized `ResponseData<T>` Response Wrapper
- ✅ JWT Authentication using ASP.NET Core Identity
- ✅ Swagger for API testing
- ✅ Modular service registrations
- ✅ Error handling & exception middleware
- ✅ Testable and DRY structure

---

## 📁 Project Structure

SolutionName/
│
├── Application/
│ ├── Common/
│ │ └── Models/ResponseData.cs
│ ├── Auth/Commands/
│ ├── Auth/Queries/
│ ├── Users/Commands/
│ ├── Users/Queries/
│ └── Behaviors/ValidationBehavior.cs
│
├── Domain/
│ └── Entities/User.cs
│
├── Infrastructure/
│ ├── Persistence/ApplicationDbContext.cs
│ └── Identity/IdentityService.cs
│
├── WebApi/
│ ├── Controllers/AuthController.cs
│ ├── Controllers/UsersController.cs
│ ├── Program.cs
│ └── Middlewares/ExceptionMiddleware.cs
│
└── Tests/
└── Unit and Integration Tests

---

## 🛠️ Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
2. Create the Database
Update your appsettings.json with a valid SQL Server connection string:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=CleanArchDb;Trusted_Connection=True;MultipleActiveResultSets=true"
}
Then run:

Update-Database
(Use Package Manager Console or CLI tools)

3. Build and Run the API
Using Visual Studio 2022:

Set WebApi as the startup project

Press F5 to run

Or using CLI:

dotnet build
dotnet run --project WebApi
4. Test the API
Navigate to:
📍 https://localhost:5001/swagger

Use Swagger to test endpoints.

🔐 Authentication
Register: POST /api/Auth/register

Login: POST /api/Auth/login
⮕ Returns JWT token

Use Bearer {token} in Authorization header for protected endpoints.

🧾 Global Response Format
All API responses use a unified wrapper: ResponseData<T>

{
  "id": "guid-id",
  "isSuccess": true,
  "statusCode": 200,
  "message": "Success message",
  "exception": "",
  "exceptionDetails": "",
  "token": "",
  "data": { } // varies by endpoint
}
⚙️ Technologies Used
.NET 6 / .NET 7

Entity Framework Core

ASP.NET Core Identity

MediatR

FluentValidation

Swagger

JWT Token Authentication

xUnit / Moq (for testing)

🧪 Testing
Unit Tests for handlers and services

Integration tests with in-memory WebApplicationFactory

dotnet test
📦 NuGet Packages
Install-Package MediatR
Install-Package MediatR.Extensions.Microsoft.DependencyInjection
Install-Package FluentValidation.AspNetCore
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
Install-Package Microsoft.EntityFrameworkCore.SqlServer
Install-Package Microsoft.EntityFrameworkCore.Tools
Install-Package Swashbuckle.AspNetCore
👨‍💻 Conventions
CQRS pattern is followed for all logic.

Each API only calls _mediator.Send() and returns ResponseData<T>.

Validation is handled globally via ValidationBehavior.

📫 Contact
If you have any questions, feel free to reach out.

Built with ❤️ using Clean Architecture