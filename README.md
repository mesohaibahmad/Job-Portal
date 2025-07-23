# 🧾 Job Application Portal

A full-stack web application for managing job positions and candidate applications with multi-tenancy support.

---

## 📦 Technologies Used

- **Backend**: ASP.NET Core 6, Entity Framework Core, ABP Framework, Hangfire
- **Frontend**: Angular (ABP-generated), TypeScript, Bootstrap
- **Database**: SQL Server
- **Others**: Multi-Tenancy, Role-based Access Control, File Upload, Background Jobs

---

## ⚙️ Setup Instructions

### 🔧 Backend Setup (.NET Core + ABP)

1. **Clone the repository**:
   ```bash
   git clone https://github.com/your-org/job-portal.git
   cd job-portal
   ```

2. **Configure the database**:
   - Update `appsettings.json` with your SQL Server connection string.

3. **Run EF Core migrations**:
   ```bash
   dotnet ef database update -p JobPortal.EntityFrameworkCore -s JobPortal.HttpApi.Host
   ```

4. **Run the backend API**:
   ```bash
   cd src/JobPortal.HttpApi.Host
   dotnet run
   ```

---

### 🌐 Frontend Setup (Angular)

1. **Navigate to Angular app**:
   ```bash
   cd angular
   ```

2. **Install dependencies**:
   ```bash
   npm install
   ```

3. **Run the Angular app**:
   ```bash
   npm start
   ```

4. App will run on: `http://localhost:4200`

---

## 👤 Sample Login Credentials

> Default tenant and user created during seed.

### 🔹 Host Admin
- **Tenant**: Leave blank
- **Username**: `admin`
- **Password**: `123qwe`

### 🔹 Default Tenant Admin
- **Tenant**: `Default`
- **Username**: `admin`
- **Password**: `123qwe`

---

## 🔐 Features

- ✅ Job position and candidate management (CRUD)
- ✅ File upload for resumes
- ✅ Role/Permission control ( Tenant, Admin, Recruiter)
-

---

## 📩 Contact

For any issues, please open an issue on GitHub or contact the maintainer at `sohaib.pak2017@gmail.com`.
