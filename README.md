# CleanAuth

## Description

CleanAuth is a web application that implements registration and authentication features following Clean Architecture principles. It restricts non-authenticated users from accessing user management functionalities, including an admin panel. Authenticated users can manage users via a table interface displaying user details such as id, name, email, last login time, registration time, and status (active/blocked).

The user management table includes checkboxes for multiple selection, with a toolbar providing actions like Block (red button), Unblock (icon), and Delete (icon).

**CSS framework:** Bootstrap.

## Technologies Used

- **Frontend:** JavaScript/TypeScript, Angular
- **Backend:** C#, .NET, ASP.NET Core
- **Database:** SQL Server 
- **Authentication:** JWT (JSON Web Tokens)

## Packages

- `BCrypt.Net-Next` Version 4.0.3
- `Microsoft.AspNetCore.Authentication.JwtBearer` Version 8.0.7
- `Microsoft.EntityFrameworkCore` Version 8.0.7
- `Microsoft.EntityFrameworkCore.SqlServer` Version 8.0.7
- `Microsoft.EntityFrameworkCore.Tools` Version 8.0.7

## Backend (ASP.NET Core) Structure


## Architecture

CleanAuth follows Clean Architecture principles, which organizes the application into distinct layers:
- **Presentation Layer:** Angular components for the frontend UI.
- **Application Layer:** Implements use cases and business logic.
- **Domain Layer:** Contains core business entities and logic.
- **Infrastructure Layer:** Provides implementations for database access and external services.

## Authors

- **Md Enayet Hossain**
  - Email: md.enayet.hossain329@gmail.com

## Contact

For inquiries or support, visit [Md Enayet Hossain's Portfolio](https://portfolio-enayet-hossain.vercel.app/home).
