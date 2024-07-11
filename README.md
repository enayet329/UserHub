# Project Name

## Description

This project implements a web application with registration and authentication features following the principles of Clean Architecture. Non-authenticated users are restricted from accessing user management functionalities, including an admin panel. Authenticated users can manage users via a table interface that displays user details such as id, name, email, last login time, registration time, and status (active/blocked).

The leftmost column of the user management table includes checkboxes for multiple selection, and a toolbar above the table provides actions like Block (red button), Unblock (icon), and Delete (icon).

CSS framework used: Bootstrap.

## Technologies Used

- **Frontend:** JavaScript/TypeScript, Angular
- **Backend:** C#, .NET, ASP.NET Core
- **Database:** SQL Server 
- **Authentication:** JWT (JSON Web Tokens)

## Architecture

This project follows Clean Architecture principles, separating concerns into distinct layers:
- **Presentation Layer:** React components for the frontend UI.
- **Application Layer:** Handles use cases and business logic.
- **Domain Layer:** Contains core business entities and logic.
- **Infrastructure Layer:** Implements database access and external services.

## Authors

- **Md Enayet Hossain**
  - Email: md.enayet.hossain329@gmail.com


## Contact

For inquiries or support, visit [portfolio-enayet-hossain.vercel.app](https://portfolio-enayet-hossain.vercel.app/home).

