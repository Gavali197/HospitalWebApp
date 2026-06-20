# 🏥 City Care Hospital Management System

![.NET Core](https://img.shields.io/badge/.NET%20Core-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL_Server-CC2927?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

A modern, enterprise-grade Hospital Management System built with ASP.NET Core MVC and SQL Server. This application streamlines hospital operations, clinical record keeping, intelligent patient portals, automated billing, and administrative workflows.

## 📌 Overview

The Hospital Management System simplifies healthcare administration by providing a secure, centralized, and role-based platform. It bridges the gap between hospital staff (Doctors, Nurses, Pharmacists, Front Desk) and patients, reducing manual paperwork and ensuring HIPAA-compliant management of healthcare data.

---

## 📑 Table of Contents
- [Features](#-features)
- [Tech Stack](#-tech-stack)
- [Project Structure](#-project-structure)
- [Security Features](#-security-features)
- [Getting Started](#-getting-started)
- [Screenshots](#-screenshots)
- [Future Enhancements](#-future-enhancements)
- [Contributing](#-contributing)

---

## ✨ Features

### 👨‍⚕️ Patient Portal & Management
* **Dual-Login System:** Patients can securely log in using their registered email or phone number.
* **Patient Dashboard:** Self-service portal for patients to view their medical history, pending invoices, and clinical visits.
* **Master Records:** Comprehensive patient profiles, demographics, and insurance tracking managed by the Front Desk.

### 🩺 Clinical & Diagnostics System
* **Medical Records:** Doctors can log diagnoses, vitals, and clinical notes.
* **Smart Prescriptions:** Integrated pharmacy routing. Doctors can generate and attach medications directly to a patient's visit.
* **Lab Reports:** Dedicated workflow for Lab Technicians to update diagnostic results, instantly reflected on the patient's portal.
* **Printable Documents:** System-generated, official PDF Rx pads for downloading and printing.

### 💳 Financial & Billing Operations
* **Automated Invoicing:** Dynamically calculates room charges, lab fees, doctor consultations, and pharmacy costs upon patient discharge.
* **Payment Gateways:** Integrated with Razorpay for seamless digital transactions.
* **Ledger Adjustments:** Secure interfaces for applying courtesy discounts and tracking partial/full payments.

### 🏢 Hospital Administration
* **Ward & Bed Management:** Real-time tracking of bed occupancy and automated status toggling upon patient admission/discharge.
* **Staff Directory:** Centralized management for hospital personnel and departmental routing.
* **Automated SMTP Emails:** System-triggered email notifications for patient billing and registration alerts.

---

## 🛠️ Tech Stack

**Backend Architecture:**
* C# / ASP.NET Core MVC
* Entity Framework Core (Code-First Approach)
* ASP.NET Core Identity (Role-Based Access Control)

**Database:**
* Microsoft SQL Server

**Frontend UI/UX:**
* Razor Views (`.cshtml`)
* HTML5 / CSS3
* Bootstrap 5 (Responsive Layouts)
* FontAwesome (Iconography)

**Third-Party Integrations:**
* Razorpay API
* System.Net.Mail (SMTP)

---

## 📂 Project Structure

```
HospitalWebApp/
│
├── HospitalManagement/
│   ├── Controllers/        # Route handling and business logic
│   ├── Models/             # Entity classes and enums
│   ├── ViewModels/         # Data transfer objects for secure UI binding
│   ├── Views/              # Razor HTML templates (Staff & Patient Portals)
│   ├── Data/               # DbContext and automated DbSeeder
│   ├── Services/           # Email, Payment, and background services
│   ├── wwwroot/            # Static assets (CSS, JS, Images)
│   └── Program.cs          # Application pipeline and dependency injection
│
└── HospitalManagement.sln
```

🔒 Security Features
Strict RBAC (Role-Based Access Control): 7 distinct authority levels (Admin, Doctor, Nurse, Receptionist, Pharmacist, LabTechnician, Patient).

Smart Routing: Segregated dashboards to prevent patients from accessing administrative endpoints.

Data Isolation: Patient portal explicitly filters data by the authenticated user's LinkedId.

Anti-Forgery Tokens: Protection against Cross-Site Request Forgery (CSRF) on all state-changing operations.

SQL Injection Protection: Enforced via Entity Framework Core LINQ queries.


🚀 Getting Started
Prerequisites
Visual Studio 2022 (or newer)

.NET 8.0 SDK

SQL Server

Git


1. Clone Repository
   ```
   git clone [https://github.com/Gavali197/HospitalWebApp.git](https://github.com/Gavali197/HospitalWebApp.git)
   cd HospitalWebApp

   ```

   2. Configure Database & Services
  Update the connection string and API keys inside appsettings.json:

  ```
    {
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=HospitalDB;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "SenderEmail": "yourhospitalemail@gmail.com",
    "Password": "your-app-password" 
    }
  }
  ```

  3. Apply Database Migrations
  Open the Package Manager Console in Visual Studio and run:

  ```
  Update-Database

  ```

(Note: The DbSeeder.cs will automatically populate the database with default admin/staff accounts and a test patient upon first run).


**📸 Screenshots**



