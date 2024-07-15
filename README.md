# CleanAuth Backend

## Description
CleanAuth is a web application that implements registration and authentication features following Clean Architecture principles. The backend, built with C# and ASP.NET Core, provides the server-side logic and API endpoints for user authentication and management.

Key features include:
- User registration and authentication using JWT
- User management functionalities (accessible only to authenticated users)
- Admin panel for comprehensive user management
- RESTful API endpoints for user operations (block, unblock, delete)

## Technologies Used
- **Language:** C#
- **Framework:** .NET, ASP.NET Core
- **Database:** SQL Server
- **Authentication:** JWT (JSON Web Tokens)

## Packages
- `BCrypt.Net-Next` Version 4.0.3
- `Microsoft.AspNetCore.Authentication.JwtBearer` Version 8.0.7
- `Microsoft.EntityFrameworkCore` Version 8.0.7
- `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.7
- `Microsoft.EntityFrameworkCore.Tools` Version 8.0.7
- `Microsoft.EntityFrameworkCore.Design` Version 8.0.7

## Architecture
CleanAuth follows Clean Architecture principles, which organizes the application into distinct layers:

- **Presentation Layer:** API Controllers and DTOs
- **Application Layer:** Implements use cases and business logic
- **Domain Layer:** Contains core business entities and logic
- **Infrastructure Layer:** Provides implementations for database access and external services

## Backend Structure
```
UserHub/
├── Application/
│   ├── Contracts/
│   │   └── IUserRepository.cs
│   ├── DTOs/
│   │   └── ...
│   └── ResponseDTOs/
│       └── ...
├── Domain/
│   └── Entities/
│       └── User.cs
├── Infrastructure/
│   ├── Data/
│   │   └── UserHubContext.cs
│   ├── DependencyInjection/
│   │   └── ServicesContainer.cs
│   ├── Migrations/
│   │   └── ...
│   └── Repository/
│       └── UserRepository.cs
├── UserHub.API/
│   ├── Controllers/
│   │   └── UserHubController.cs
│   ├── Properties/
│   │   └── launchSettings.json
│   ├── appsettings.json
│   └── appsettings.Development.json
└── program.cs

```

## Getting Started

### Prerequisites
- .NET SDK 8.0 or later
- SQL Server

### Installation
1. Clone the repository:
   ```
   git clone [repository URL]
   ```
2. Navigate to the backend directory:
   ```
   cd UserHub/backend
   ```
3. Restore dependencies:
   ```
   dotnet restore
   ```
4. Update the connection string in `appsettings.json` to point to your SQL Server instance.

5. Apply database migrations:
   ```
   dotnet ef database update
   ```

### Running the Application
To start the backend server:
```
dotnet run --project src/CleanAuth.API
```

The API will be available at `https://localhost:5001/` by default.

## Features
1. **Authentication:**
   - User registration
   - User login with JWT token generation
   - Password hashing using BCrypt

2. **User Management:**
   - Retrieve user details
   - Update user information
   - Block/Unblock users
   - Delete users

3. **Admin Panel:**
   - Restricted access to authenticated admin users
   - Comprehensive user management capabilities

## API Endpoints
- POST /api/auth/register
- POST /api/auth/login
- GET /api/users
- PUT /api/users/{email}
- DELETE /api/users/{email}
- PUT /api/users/{email}/block
- PUT /api/users/{email}/unblock

## Authors
- **Md Enayet Hossain**
  - Email: md.enayet.hossain329@gmail.com

## Contact
For inquiries or support, visit [Md Enayet Hossain's Portfolio](https://portfolio-enayet-hossain.vercel.app/home).
