# Online Exam System (.NET MAUI + SQL Server)

A cross-platform mobile examination management application built with **.NET MAUI (C#)** following the **MVVM (Model-View-ViewModel)** architectural pattern and backed by **Microsoft SQL Server** using stored procedures.

---

## 🚀 Key Features

### Role-Based Access Control
* **Admin:** Manage academic lookup data (Sessions, Exams, Degrees, Semesters, Subjects), onboard Teachers and Students, and assign students to subjects and teachers.
* **Teacher:** Create and manage exam Papers (scoped by Session, Exam, Degree, Semester, and Subject), add/update/delete multiple-choice Questions with answer keys, and view assigned students.
* **Student:** Authenticate via Registration Number (`Reg_no`), browse available exam papers matching their enrolled Session, Degree, Semester, and Subjects, and attempt read-only exams (with answer keys stripped at the database level via `usp_Student_ViewPaper`).

---

## 🛠️ Tech Stack

* **Framework:** .NET 10 / .NET MAUI (Android & Windows)
* **Architecture:** MVVM (`CommunityToolkit.Mvvm`, `CommunityToolkit.Maui`)
* **Database:** Microsoft SQL Server (`Online Exam System`)
* **Data Access:** `Microsoft.Data.SqlClient` executing parameterized Stored Procedures

---

## 📁 Project Structure

```text
Online Exam/
├── Models/
│   └── AppModels.cs            # Data models mapped to SQL stored procedures
├── Services/
│   ├── Database.cs             # Async SQL Server connection & stored procedure calls
│   └── ModalErrorHandler.cs    # Centralized UI error handling
├── Utilities/
│   └── TaskUtilities.cs        # Async task helpers
├── View/
│   └── LoginPage.xaml          # Multi-role login screen (Admin / Teacher / Student)
├── ViewModel/
│   └── LoginViewModel.cs       # Login state, validation, and authentication commands
├── AppShell.xaml               # Shell navigation setup
├── GlobalUsings.cs             # Project-wide global namespaces
└── MauiProgram.cs              # Dependency injection & toolkit configuration
```

---

## 📋 Prerequisites

* **Visual Studio 2022 / 2026** with the *.NET Multi-platform App UI development* workload installed.
* **Android SDK & Android Emulator** (API 34+ recommended).
* **SQL Server / SQL Server Express** and **SQL Server Management Studio (SSMS)**.

---

## 🗄️ Database Setup

### 1. Create Database & Stored Procedures
1. Open SSMS and create a database named `Online Exam System`.
2. Execute your schema creation script followed by `Stored Procedure.sql` to create all `usp_*` procedures.
3. Ensure `AUTO_CLOSE` is disabled so mobile queries do not time out while waking the database:

```sql
USE [master];
GO
ALTER DATABASE [Online Exam System] SET AUTO_CLOSE OFF WITH NO_WAIT;
GO
```

### 2. Configure SQL Server Authentication & Networking
Android devices/emulators cannot use Windows Integrated Authentication and must connect over TCP/IP:

1. Enable **Mixed Mode Authentication** (SQL Server and Windows Authentication) in *SQL Server Properties -> Security*.
2. Open **SQL Server Configuration Manager** -> *SQL Server Network Configuration* -> *Protocols for SQLEXPRESS* (or *MSSQLSERVER*):
   * Enable **TCP/IP**.
   * Under *IP Addresses -> IPAll*, set **TCP Port** to `1433` and restart the SQL Server service.
3. Allow inbound TCP traffic on port `1433` in **Windows Defender Firewall**.
4. Create/configure the SQL login (`Online_exam`) with `CHECK_EXPIRATION = OFF` and the `db_owner` role on `[Online Exam System]`.

---

## ▶️ Running the Application

### 1. Install NuGet Packages (if restoring from source)
```bash
dotnet add package CommunityToolkit.Mvvm
dotnet add package CommunityToolkit.Maui
dotnet add package Microsoft.Data.SqlClient
```

### 2. Configure Connection String
Update the credentials in `Services/Database.cs`:
* **Android Emulator:** Connects to host PC via `tcp:10.0.2.2,1433`
* **Windows / Local:** Connects via `localhost,1433`

### 3. Select Target & Run
Choose your Android Emulator (e.g., `Pixel 7 - API 36`) in Visual Studio and press **F5**.

---

## 🔑 Default Test Credentials (Seed Data)

| Role | Identifier | Password |
| :--- | :--- | :--- |
| **Admin** | `admin` | `admin123` |
| **Teacher** | `alikhan` | `teacher123` |
| **Student** | `FA22-BCS-001` | *Not required* |
