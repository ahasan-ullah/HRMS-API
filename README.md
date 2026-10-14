<!-- ═══════════════════════════ ANIMATED HEADER ═══════════════════════════ -->
<p align="center">
  <img src="https://capsule-render.vercel.app/api?type=waving&color=0:0f2027,50:203a43,100:2c5364&height=220&section=header&text=HRMS-API&fontSize=72&fontColor=ffffff&fontAlignY=38&animation=fadeIn&desc=Human%20Resource%20Management%20System%20%E2%80%A2%20RESTful%20Web%20API&descAlignY=58&descSize=18" alt="HRMS-API banner" width="100%"/>
</p>

<p align="center">
  <a href="#-overview">
    <img src="https://readme-typing-svg.demolab.com?font=Fira+Code&weight=600&size=22&duration=3000&pause=800&color=36BCF7&center=true&vCenter=true&multiline=false&repeat=true&width=650&height=45&lines=%F0%9F%91%A5+Employees+%26+Departments;%F0%9F%92%B0+Salaries%2C+Bonuses+%26+Deductions;%F0%9F%A7%BE+Automated+Payroll+with+Tax+Brackets;%F0%9F%94%90+JWT+%2B+Refresh+Tokens+%2B+Role+Based+Access" alt="Typing SVG" />
  </a>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Framework 4.8"/>
  <img src="https://img.shields.io/badge/ASP.NET%20Web%20API-5.3-5C2D91?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Web API"/>
  <img src="https://img.shields.io/badge/Entity%20Framework-6.5-68217A?style=for-the-badge&logo=nuget&logoColor=white" alt="Entity Framework 6"/>
  <img src="https://img.shields.io/badge/SQL%20Server-Express-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white" alt="SQL Server"/>
  <img src="https://img.shields.io/badge/Auth-JWT-000000?style=for-the-badge&logo=jsonwebtokens&logoColor=white" alt="JWT"/>
</p>

<p align="center">
  <img src="https://img.shields.io/github/last-commit/ahasan-ullah/HRMS-API?style=flat-square&color=36BCF7" alt="Last commit"/>
  <img src="https://img.shields.io/github/repo-size/ahasan-ullah/HRMS-API?style=flat-square&color=2c5364" alt="Repo size"/>
  <img src="https://img.shields.io/github/languages/top/ahasan-ullah/HRMS-API?style=flat-square&color=512BD4" alt="Top language"/>
  <img src="https://img.shields.io/github/stars/ahasan-ullah/HRMS-API?style=flat-square&color=yellow" alt="Stars"/>
</p>

<p align="center">
  <a href="#-overview">Overview</a> •
  <a href="#-key-features">Features</a> •
  <a href="#%EF%B8%8F-architecture">Architecture</a> •
  <a href="#-data-model">Data Model</a> •
  <a href="#-api-reference">API Reference</a> •
  <a href="#-getting-started">Getting Started</a> •
  <a href="#-usage-examples">Examples</a>
</p>

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 📖 Overview

**HRMS-API** is a role-based **Human Resource Management System** backend built with **ASP.NET Web API 2** on **.NET Framework 4.8**. It follows a clean **three-layer architecture** (API → BLL → DAL) and covers the everyday work of an HR department, from onboarding employees to generating, approving and paying monthly payroll.

> [!TIP]
> The API is ready for a SPA frontend. CORS is preconfigured for an Angular dev server on `http://localhost:4200`.

<table>
<tr>
<td width="50%" valign="top">

### 🎯 What it does
- Manages **departments** and **employees**
- Tracks **salaries**, **revisions**, **bonuses** and **deductions**
- **Generates payroll** per month with automatic tax calculation
- Moves payroll through a **Draft → Approved → Paid** lifecycle
- Secures everything with **JWT + rotating refresh tokens**

</td>
<td width="50%" valign="top">

### 🧰 Built with
- **ASP.NET Web API 2** (attribute routing)
- **Entity Framework 6** (code-first + migrations)
- **AutoMapper** (entity ⇄ DTO mapping)
- **BCrypt.Net-Next** (password hashing)
- **System.IdentityModel.Tokens.Jwt** (HMAC-SHA256)
- **Newtonsoft.Json**

</td>
</tr>
</table>

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## ✨ Key Features

<table>
<tr>
<td align="center" width="25%">
  <img src="https://img.icons8.com/fluency/64/lock-2.png" width="48" alt="Security"/><br/>
  <b>Authentication</b><br/>
  <sub>Login, refresh-token rotation, logout with revocation, change password</sub>
</td>
<td align="center" width="25%">
  <img src="https://img.icons8.com/fluency/64/conference-call.png" width="48" alt="Employees"/><br/>
  <b>Employee Management</b><br/>
  <sub>CRUD, keyword search, department filter, auto-created user accounts</sub>
</td>
<td align="center" width="25%">
  <img src="https://img.icons8.com/fluency/64/money-bag.png" width="48" alt="Compensation"/><br/>
  <b>Compensation</b><br/>
  <sub>Salaries with allowances, revision history, bonuses, deductions</sub>
</td>
<td align="center" width="25%">
  <img src="https://img.icons8.com/fluency/64/bill.png" width="48" alt="Payroll"/><br/>
  <b>Payroll Engine</b><br/>
  <sub>One-click monthly generation, tax brackets, approval workflow</sub>
</td>
</tr>
</table>

<details open>
<summary><b>🔐 Authentication & Security</b></summary>
<br/>

- 🔑 Username/password login, with passwords hashed using **BCrypt**
- ⏱️ Short-lived **JWT access tokens** (15 min by default, signed with HMAC-SHA256)
- ♻️ **Refresh token rotation**: each refresh revokes the old token and issues a new one (7 days by default)
- 🚪 **Logout** revokes every refresh token belonging to the user
- 🛡️ Custom `JWTAuthFilter` attribute that checks the header, signature, expiry and **role**
- 👮 Three roles: `Admin`, `HR`, `Employee`
- 🚫 Inactive users can't log in or refresh their tokens

</details>

<details>
<summary><b>👥 HR Management</b></summary>
<br/>

- 🏢 Departments with live employee counts
- 👤 Employees with contact, position, bank account, employment status and remarks
- 🔍 Keyword search and filtering by department
- 🤖 **Creating an employee also creates a login account** with the `Employee` role
- 📈 Salary revisions archive the previous salary and keep a full history

</details>

<details>
<summary><b>🧾 Payroll</b></summary>
<br/>

- 📅 Generates payroll for any `month/year` and blocks duplicates for the same period
- ➕ Gross = Basic + House + Medical + Transport allowances + bonuses for the month
- ➖ Net = Gross − deductions for the month − tax
- ⏭️ Skips employees with no active salary
- ✅ Only **Draft** payrolls can be approved or deleted, and only **Approved** payrolls can be marked paid
- 💸 Marking a payroll as paid sets every payroll detail line to `Paid`

</details>

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 🏛️ Architecture

```mermaid
flowchart LR
    Client(["🖥️ Client<br/>Angular / Postman"]) -->|HTTP + Bearer JWT| Filter

    subgraph API["🌐 API Layer"]
        Filter["🛡️ JWTAuthFilter"] --> Controllers["🎮 Controllers"]
    end

    subgraph BLL["🧠 Business Logic Layer"]
        Services["⚙️ Services"]
        DTOs["📦 DTOs"]
        Helpers["🔧 JWTHelper<br/>MapperConfig"]
    end

    subgraph DAL["💾 Data Access Layer"]
        Factory["🏭 DataAccessFactory"] --> Repos["📚 Repositories"]
        Repos --> Ctx["🗄️ HRMSDbContext"]
    end

    Controllers --> Services
    Services --> Factory
    Ctx --> DB[("🛢️ SQL Server<br/>HRMSDb")]

    style API fill:#1e3a5f,stroke:#36BCF7,color:#fff
    style BLL fill:#2d1e5f,stroke:#9b6bff,color:#fff
    style DAL fill:#1e5f3a,stroke:#36f7a0,color:#fff
```

<details>
<summary><b>📁 Project structure</b></summary>

```text
HRMS-API/
├── 🌐 API/                         → HTTP surface (ASP.NET Web API 2)
│   ├── App_Start/WebApiConfig.cs   → Attribute routing & formatters
│   ├── Controllers/                → Auth, User, Department, Employee,
│   │                                 Salary, Bonus, Deduction, Payroll
│   ├── Filters/JWTAuthFilter.cs    → Token + role authorization
│   └── Web.config                  → Connection string, JWT & CORS settings
│
├── 🧠 BLL/                         → Business rules
│   ├── DTOs/                       → Request/response contracts
│   ├── Helpers/JWTHelper.cs        → Token generation & validation
│   ├── Helpers/MapperConfig.cs     → AutoMapper profiles
│   └── Services/                   → AuthService, PayrollService, ...
│
├── 💾 DAL/                         → Persistence
│   ├── Models/                     → EF entities
│   ├── Models/Context/             → HRMSDbContext (Fluent API relations)
│   ├── Interfaces/                 → IRepo<T> + specialized repo contracts
│   ├── Repositories/               → EF implementations
│   ├── Migrations/                 → EF code-first migration history
│   └── DataAccessFactory.cs        → Repository factory
│
└── 📄 HRMS_API_Documentation.pdf
```

</details>

### 🔄 Authentication Flow

```mermaid
sequenceDiagram
    autonumber
    actor U as 👤 User
    participant A as 🌐 AuthController
    participant S as ⚙️ AuthService
    participant DB as 🛢️ Database

    U->>A: POST /api/auth/login {username, password}
    A->>S: Login()
    S->>DB: Find active user + verify BCrypt hash
    S->>DB: Revoke old refresh tokens, store new one
    S-->>U: 🎟️ accessToken (15 min) + refreshToken (7 days)

    Note over U,A: Call protected endpoints with<br/>Authorization: Bearer <accessToken>

    U->>A: POST /api/auth/refresh {refreshToken}
    A->>S: Refresh()
    S->>DB: Validate → revoke old → issue new
    S-->>U: 🔁 New token pair

    U->>A: POST /api/auth/logout (Bearer)
    S->>DB: Revoke all refresh tokens for user
    A-->>U: 👋 Logged out
```

### 📊 Payroll Lifecycle

```mermaid
stateDiagram-v2
    direction LR
    [*] --> Draft: POST /generate
    Draft --> Approved: PUT /approve/{id} (Admin)
    Draft --> [*]: DELETE /delete/{id}
    Approved --> Paid: PUT /markpaid/{id} (Admin)
    Paid --> [*]
```

### 🧮 Tax Brackets

Tax is calculated on **gross salary** (basic + allowances + bonuses):

| Monthly Gross Salary | Tax Rate |
|:---|:---:|
| ≤ 20,000 | **0%** |
| 20,001 – 40,000 | **5%** |
| 40,001 – 80,000 | **10%** |
| > 80,000 | **15%** |

> **Net Salary** = Gross − Deductions (for the month) − Tax

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 🗂️ Data Model

```mermaid
erDiagram
    DEPARTMENT ||--o{ EMPLOYEE : has
    EMPLOYEE |o--o| USER : "login account"
    EMPLOYEE ||--o{ SALARY : earns
    EMPLOYEE ||--o{ SALARY_REVISION : "revised in"
    EMPLOYEE ||--o{ BONUS : receives
    EMPLOYEE ||--o{ DEDUCTION : incurs
    EMPLOYEE ||--o{ PAYROLL_DETAIL : "paid in"
    USER ||--o{ REFRESH_TOKEN : owns
    USER ||--o{ PAYROLL : generates
    USER ||--o{ SALARY_REVISION : approves
    PAYROLL ||--|{ PAYROLL_DETAIL : contains

    DEPARTMENT {
        int DepartmentId PK
        string Name
        string Description
    }
    EMPLOYEE {
        int EmployeeId PK
        int DepartmentId FK
        string FirstName
        string LastName
        string Email
        string Position
        string AccountNumber
        string EmploymentStatus
        date JoiningDate
        bool IsActive
    }
    USER {
        int UserId PK
        string Username
        string PasswordHash
        string Role
        bool IsActive
        datetime LastLogin
    }
    SALARY {
        int SalaryId PK
        int EmployeeId FK
        decimal BasicSalary
        decimal HouseAllowance
        decimal MedicalAllowance
        decimal TransportAllowance
        bool IsActive
    }
    PAYROLL {
        int PayrollId PK
        int PayrollMonth
        int PayrollYear
        decimal TotalAmount
        string Status
        int GeneratedBy FK
    }
    PAYROLL_DETAIL {
        int PayrollDetailId PK
        int PayrollId FK
        int EmployeeId FK
        decimal NetSalary
        decimal TaxAmount
        string PaymentStatus
    }
```

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 📡 API Reference

> **Base URL:** `http://localhost:<port>/api`
> Protected endpoints require `Authorization: Bearer <access_token>`.

### 👮 Access Matrix

| Module | 🔓 Public | 🟢 Any signed-in user | 🟡 Admin, HR | 🔴 Admin only |
|:---|:---:|:---:|:---:|:---:|
| Auth: login / refresh | ✅ | | | |
| Auth: logout / change-password | | ✅ | | |
| Auth: assign-role | | | | ✅ |
| Users | | | | ✅ |
| Departments · Employees · Salaries | | | ✅ | |
| Bonuses · Deductions | | | ✅ | |
| Payroll (view / generate / delete) | | | ✅ | |
| Payroll: approve / mark paid | | | | ✅ |

<details>
<summary><b>🔐 Auth</b> · <code>/api/auth</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/login` | Public | Log in and receive a token pair |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/refresh` | Public | Swap a refresh token for a new pair |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/logout` | Signed in | Revoke all refresh tokens |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/change-password` | Signed in | Change password (min 6 chars) |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/assign-role` | Admin | Set a user's role |

</details>

<details>
<summary><b>👤 Users</b> · <code>/api/user</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin | List all users |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin | Get a user by ID |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/by-username/{username}` | Admin | Get a user by username |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/toggle-active/{id}` | Admin | Activate or deactivate a user |

</details>

<details>
<summary><b>🏢 Departments</b> · <code>/api/department</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List departments |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Get a department |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/create` | Admin, HR | Create a department |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/update` | Admin, HR | Update a department |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete a department |

</details>

<details>
<summary><b>👥 Employees</b> · <code>/api/employee</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List employees |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Get an employee |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/search?keyword=` | Admin, HR | Search by keyword |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/filter/department/{departmentId}` | Admin, HR | Filter by department |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/create` | Admin, HR | Create an employee and their login account |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/update` | Admin, HR | Update an employee |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete an employee |

</details>

<details>
<summary><b>💵 Salaries</b> · <code>/api/salary</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List salaries |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Get a salary record |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/employee/{employeeId}` | Admin, HR | Get an employee's active salary |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/revisions/{employeeId}` | Admin, HR | Get revision history |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/create` | Admin, HR | Create a salary |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/update` | Admin, HR | Update a salary |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/revise` | Admin, HR | Revise a salary (archives the old one) |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete a salary |

</details>

<details>
<summary><b>🎁 Bonuses</b> · <code>/api/bonus</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List bonuses |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Get a bonus |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/employee/{employeeId}` | Admin, HR | List an employee's bonuses |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/create` | Admin, HR | Create a bonus |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/update` | Admin, HR | Update a bonus |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete a bonus |

</details>

<details>
<summary><b>➖ Deductions</b> · <code>/api/deduction</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List deductions |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Get a deduction |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/employee/{employeeId}` | Admin, HR | List an employee's deductions |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/employee/{employeeId}/{month}/{year}` | Admin, HR | Deductions for one period |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/create` | Admin, HR | Create a deduction |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/update` | Admin, HR | Update a deduction |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete a deduction |

</details>

<details>
<summary><b>🧾 Payroll</b> · <code>/api/payroll</code></summary>

| Method | Endpoint | Access | Description |
|:---:|:---|:---:|:---|
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/all` | Admin, HR | List payrolls |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}` | Admin, HR | Payroll summary |
| ![GET](https://img.shields.io/badge/GET-61affe?style=flat-square) | `/{id}/details` | Admin, HR | Per-employee breakdown |
| ![POST](https://img.shields.io/badge/POST-49cc90?style=flat-square) | `/generate` | Admin, HR | Generate payroll for a month |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/approve/{id}` | **Admin** | Draft → Approved |
| ![PUT](https://img.shields.io/badge/PUT-fca130?style=flat-square) | `/markpaid/{id}` | **Admin** | Approved → Paid |
| ![DELETE](https://img.shields.io/badge/DELETE-f93e3e?style=flat-square) | `/delete/{id}` | Admin, HR | Delete a Draft payroll |

</details>

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 🚀 Getting Started

### ✅ Prerequisites

| Tool | Version |
|:---|:---|
| 🟣 Visual Studio | 2022 / 2026 (with the *ASP.NET and web development* workload) |
| ⚙️ .NET Framework Developer Pack | 4.8 |
| 🛢️ SQL Server | Express, LocalDB or full edition |
| 📦 NuGet | Package restore enabled |

### 🛠️ Installation

```bash
# 1️⃣ Clone the repository
git clone https://github.com/ahasan-ullah/HRMS-API.git
cd HRMS-API

# 2️⃣ Open the solution in Visual Studio
start HRMS.slnx
```

**3️⃣ Configure the database** in `API/Web.config`:

```xml
<connectionStrings>
  <add name="HRMSDbContext"
       connectionString="data source=YOUR_SERVER\SQLEXPRESS; initial catalog=HRMSDb; integrated security=true;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

**4️⃣ Configure JWT settings** in `API/Web.config` → `<appSettings>`:

| Key | Default | Purpose |
|:---|:---:|:---|
| `JwtSecretKey` | *(base64 key)* | HMAC-SHA256 signing key |
| `JwtIssuer` | `HRMS` | Token issuer |
| `JwtAudience` | `HRMS` | Token audience |
| `JwtAccessTokenMins` | `15` | Access token lifetime (minutes) |
| `JwtRefreshTokenDays` | `7` | Refresh token lifetime (days) |

> [!WARNING]
> Generate your **own** `JwtSecretKey` before deploying anywhere. Never ship the sample key, and keep production secrets out of source control.

**5️⃣ Create the database.** Open **Tools → NuGet Package Manager → Package Manager Console**, set the *Default project* to `DAL` and run:

```powershell
Update-Database -StartupProjectName API
```

**6️⃣ Run the API.** Set `API` as the startup project and press <kbd>F5</kbd> 🎉

> [!NOTE]
> **CORS:** the allowed origin is `http://localhost:4200`. To use a different frontend, change `Access-Control-Allow-Origin` under `<httpProtocol><customHeaders>` in `Web.config`.

> [!IMPORTANT]
> **First admin user:** the migration seed is empty, so add the first `Admin` user to the `Users` table yourself, with a BCrypt-hashed password. After that, use `PUT /api/auth/assign-role` to promote other users.

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 🧪 Usage Examples

<details open>
<summary><b>🔑 Login</b></summary>

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "your-password"
}
```

**Response**

```json
{
  "AccessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "RefreshToken": "b3f1c9e2-...",
  "ExpiresInMinutes": 15,
  "Role": "Admin",
  "UserId": 1,
  "Username": "admin"
}
```

</details>

<details>
<summary><b>♻️ Refresh token</b></summary>

```http
POST /api/auth/refresh
Content-Type: application/json

{ "refreshToken": "b3f1c9e2-..." }
```

</details>

<details>
<summary><b>👤 Create an employee</b></summary>

```http
POST /api/employee/create
Authorization: Bearer <access_token>
Content-Type: application/json

{
  "departmentId": 1,
  "firstName": "Sadia",
  "lastName": "Islam",
  "email": "sadia@example.com",
  "phone": "01700000000",
  "address": "Dhaka",
  "position": "Software Engineer",
  "accountNumber": "1234567890",
  "joiningDate": "2026-05-01",
  "remark": "Joined via campus hiring"
}
```

> 🤖 This also creates a login account: username **`{firstname}{employeeId}`**, default password **`EMP{employeeId}`**, role **`Employee`**. Employees should change the password after their first login.

</details>

<details>
<summary><b>📈 Revise a salary</b></summary>

```http
POST /api/salary/revise
Authorization: Bearer <access_token>
Content-Type: application/json

{
  "employeeId": 5,
  "newSalary": 55000,
  "reason": "Annual performance increment",
  "revisedBy": 1
}
```

</details>

<details>
<summary><b>🧾 Generate payroll</b></summary>

```http
POST /api/payroll/generate
Authorization: Bearer <access_token>
Content-Type: application/json

{
  "month": 5,
  "year": 2026,
  "generatedBy": 1
}
```

</details>

### ⚠️ Error responses

| Status | When |
|:---:|:---|
| `400 Bad Request` | A validation or business rule failed (for example, payroll already generated for the period) |
| `401 Unauthorized` | The token is missing, malformed, invalid or expired |
| `403 Forbidden` | The token is valid but the role isn't allowed on this endpoint |
| `404 Not Found` | The resource doesn't exist |
| `500 Internal Server Error` | An unexpected server-side failure |

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 📝 Business Rules at a Glance

| # | Rule |
|:---:|:---|
| 1 | Creating an employee also creates a default `Employee` user account |
| 2 | Each employee has at most one **active** salary. Revising it deactivates the old record and logs a revision |
| 3 | A new salary must be greater than zero and different from the current one |
| 4 | Payroll can be generated only **once** per month/year |
| 5 | Payroll skips employees without an active salary |
| 6 | Only `Draft` payrolls can be approved or deleted. Only `Approved` payrolls can be marked `Paid` |
| 7 | Logging in revokes previous refresh tokens, and each refresh rotates the token |
| 8 | New passwords must be at least **6 characters** |
| 9 | Valid roles are `Admin`, `HR` and `Employee` |

<img src="https://raw.githubusercontent.com/andreasbm/readme/master/assets/lines/rainbow.png" alt="divider" width="100%"/>

## 🛣️ Roadmap

- [ ] 📘 Swagger / OpenAPI documentation (Swashbuckle)
- [ ] 🌱 Seed data for the first Admin user
- [ ] 🧪 Unit tests for services
- [ ] 🧍 Employee self-service endpoints (view own payslips)
- [ ] 📄 Payslip PDF export
- [ ] 🔒 Move secrets out of `Web.config`

## 👨‍💻 Author

<p align="center">
  <a href="https://github.com/ahasan-ullah">
    <img src="https://img.shields.io/badge/GitHub-ahasan--ullah-181717?style=for-the-badge&logo=github&logoColor=white" alt="GitHub"/>
  </a>
</p>

<p align="center">
  <b>⭐ If this project helped you, consider giving it a star! ⭐</b>
</p>

<!-- ═══════════════════════════ ANIMATED FOOTER ═══════════════════════════ -->
<p align="center">
  <img src="https://capsule-render.vercel.app/api?type=waving&color=0:2c5364,50:203a43,100:0f2027&height=120&section=footer&animation=twinkling" alt="footer" width="100%"/>
</p>
