# HRMS-API

A role-based **Human Resource Management System (HRMS) Web API** built with **ASP.NET Web API (.NET Framework 4.8)** using a layered architecture.

This solution provides a clean backend for managing core HR operations such as employees, departments, salaries, salary revisions, bonuses, deductions, payroll generation, and authenticated user access with JWT.

---

## Overview

HRMS-API is organized into three main layers:

- **API** – Web API controllers, routing, and authorization filters
- **BLL** – Business rules, DTOs, token helpers, and service logic
- **DAL** – Entity Framework context, database models, repositories, and migrations

The application uses:

- **ASP.NET Web API 2**
- **Entity Framework 6**
- **JWT access tokens + refresh tokens**
- **Role-based authorization** (`Admin`, `HR`, `Employee`)
- **AutoMapper** for object mapping
- **BCrypt** for password hashing

---

## Key Features

### Authentication & Security

- Login with username and password
- JWT access tokens with short expiration
- Refresh token flow for renewing access
- Logout support with refresh token revocation
- Change password endpoint for authenticated users
- Role-based endpoint protection using a custom JWT authorization filter

### HR Management

- Manage departments
- Manage employees
- Search employees by keyword
- Filter employees by department
- Track employee salary records and revisions
- Maintain bonuses and deductions
- Generate payroll by month and year
- Approve payroll and mark payrolls as paid
- View payroll summaries and payroll details

### Data Layer

- Entity Framework code-based migrations
- Repository pattern through the DAL layer
- Strongly typed DTOs for API input/output
- Database relationships configured in `HRMSDbContext`

---

## Solution Structure

```text
HRMS-API/
├── API/   -> Web API project
├── BLL/   -> Business logic, DTOs, helpers, services
├── DAL/   -> Data access, entities, repositories, EF context
└── README.md
```

### API Project

Contains the HTTP surface of the application:

- `Controllers/AuthController.cs`
- `Controllers/UserController.cs`
- `Controllers/DepartmentController.cs`
- `Controllers/EmployeeController.cs`
- `Controllers/SalaryController.cs`
- `Controllers/BonusController.cs`
- `Controllers/DeductionController.cs`
- `Controllers/PayrollController.cs`
- `Filters/JWTAuthFilter.cs`
- `App_Start/WebApiConfig.cs`

### BLL Project

Contains the business logic layer:

- DTOs for requests and responses
- Services for business operations
- JWT helper utilities
- Mapper configuration
- Authentication logic

### DAL Project

Contains the persistence layer:

- Entity Framework `HRMSDbContext`
- Domain models
- Repository interfaces and implementations
- Migrations
- Data access factory

---

## Domain Areas

### Users & Authentication

The system supports user login, refresh token renewal, role assignment, password change, and logout.

Roles used in the application:

- `Admin`
- `HR`
- `Employee`

### Employees

Employees belong to departments and can be searched or filtered. Creating an employee also creates a default employee account automatically.

### Departments

Departments store organizational groups and expose employee counts in response DTOs.

### Salaries

The salary module supports:

- active salary lookup per employee
- salary creation and updates
- salary revisions
- revision history per employee

### Bonuses and Deductions

Bonuses and deductions are tracked per employee and can be queried by employee or time period.

### Payroll

Payroll generation calculates net salary using:

- basic salary
- allowances
- bonuses
- deductions
- tax brackets

Payroll lifecycle:

- `Draft`
- `Approved`
- `Paid`

---

## API Authentication

Authenticated endpoints expect a JWT in the `Authorization` header:

```http
Authorization: Bearer <access_token>
```

The custom `JWTAuthFilter` validates:

- token presence
- token format
- token signature
- token expiration
- allowed roles when role restrictions are applied

---

## Main Endpoints

> Base URL pattern: `api/{controller}`

### Auth

- `POST /api/auth/login`
- `POST /api/auth/refresh`
- `POST /api/auth/logout`
- `PUT /api/auth/change-password`
- `PUT /api/auth/assign-role`

### Users

- `GET /api/user/all`
- `GET /api/user/{id}`
- `GET /api/user/by-username/{username}`
- `PUT /api/user/toggle-active/{id}`

### Departments

- `GET /api/department/all`
- `GET /api/department/{id}`
- `POST /api/department/create`
- `PUT /api/department/update`
- `DELETE /api/department/delete/{id}`

### Employees

- `GET /api/employee/all`
- `GET /api/employee/{id}`
- `GET /api/employee/search?keyword=...
`
- `GET /api/employee/filter/department/{departmentId}`
- `POST /api/employee/create`
- `PUT /api/employee/update`
- `DELETE /api/employee/delete/{id}`

### Salaries

- `GET /api/salary/all`
- `GET /api/salary/{id}`
- `GET /api/salary/employee/{employeeId}`
- `GET /api/salary/revisions/{employeeId}`
- `POST /api/salary/create`
- `PUT /api/salary/update`
- `POST /api/salary/revise`
- `DELETE /api/salary/delete/{id}`

### Bonuses

- `GET /api/bonus/all`
- `GET /api/bonus/{id}`
- `GET /api/bonus/employee/{employeeId}`
- `POST /api/bonus/create`
- `PUT /api/bonus/update`
- `DELETE /api/bonus/delete/{id}`

### Deductions

- `GET /api/deduction/all`
- `GET /api/deduction/{id}`
- `GET /api/deduction/employee/{employeeId}`
- `GET /api/deduction/employee/{employeeId}/{month}/{year}`
- `POST /api/deduction/create`
- `PUT /api/deduction/update`
- `DELETE /api/deduction/delete/{id}`

### Payroll

- `GET /api/payroll/all`
- `GET /api/payroll/{id}`
- `GET /api/payroll/{id}/details`
- `POST /api/payroll/generate`
- `PUT /api/payroll/approve/{id}`
- `PUT /api/payroll/markpaid/{id}`
- `DELETE /api/payroll/delete/{id}`

---

## Prerequisites

Before running the project, make sure you have:

- **Visual Studio Community 2026** or another compatible Visual Studio version
- **.NET Framework 4.8 Developer Pack**
- **SQL Server Express** or another SQL Server instance
- **NuGet Package Restore** enabled

---

## Configuration

### 1) Database Connection

Update the connection string in `API/Web.config` to match your SQL Server instance:

```xml
<connectionStrings>
  <add name="HRMSDbContext" connectionString="data source=YOUR_SERVER; initial catalog=HRMSDb; integrated security=true;" providerName="System.Data.SqlClient" />
</connectionStrings>
```

### 2) JWT Settings

The application uses JWT-related app settings in `API/Web.config`:

- `JwtSecretKey`
- `JwtIssuer`
- `JwtAudience`
- `JwtAccessTokenMins`
- `JwtRefreshTokenDays`

If you change these values, keep them aligned with token generation and validation logic in `BLL.Helpers.JWTHelper`.

### 3) CORS

The API is currently configured to allow requests from:

- `http://localhost:4200`

If your frontend runs on a different origin, update the custom headers in `API/Web.config`.

---

## Database Setup

The project includes Entity Framework migrations in the `DAL` project.

Typical workflow:

1. Open the solution in Visual Studio
2. Restore NuGet packages
3. Ensure the database connection string is correct
4. Apply migrations or update the database from the Package Manager Console

If you are using Package Manager Console, the migration commands typically look like:

- `Enable-Migrations` (if needed)
- `Add-Migration <Name>`
- `Update-Database`

> The repository already contains migration history, so in most cases you only need to update the database.

---

## Running the Project

1. Open the solution in Visual Studio
2. Set `API` as the startup project
3. Restore NuGet packages
4. Verify the SQL Server connection string
5. Build the solution
6. Run the API

The API will start on the configured IIS Express or local IIS endpoint.

---

## Example Usage

### Login

`POST /api/auth/login`

```json
{
  "username": "admin",
  "password": "your-password"
}
```

Response includes:

- access token
- refresh token
- user id
- username
- role
- expiration duration

### Use the Access Token

```http
Authorization: Bearer eyJhbGciOi...
```

### Generate Payroll

`POST /api/payroll/generate`

```json
{
  "month": 5,
  "year": 2026,
  "generatedBy": 1
}
```

---

## Notes

- New employee records automatically create a default employee user account.
- Payroll generation skips employees without an active salary.
- Salary revision creates revision history and deactivates the previous salary record.
- Refresh tokens are stored server-side and revoked on logout or refresh rotation.

---

## Technologies Used

- ASP.NET Web API 2
- .NET Framework 4.8
- Entity Framework 6
- AutoMapper
- BCrypt.Net-Next
- System.IdentityModel.Tokens.Jwt
- Newtonsoft.Json

---

## License

No license file is currently included in the repository.

---

## Author

This project appears to be maintained as part of the **HRMS-API** solution.
