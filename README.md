A modular, domain-driven Course Management System built using .NET 9, EF Core, Clean Architecture, CQRS, and PostgreSQL.
This project digitizes course, class, student, and enrollment management for LearnWell University.

📑 Table of Contents

Overview

Architecture

Technology Stack

Prerequisites

Installation

Database Setup

Run Migrations

Running the API

Environment Variables

Project Structure

API Usage

Unit Testing

Contribution

📘 Overview

LearnWell Course Management is a DDD-driven backend for managing:

Courses

Classes

Students

Enrollments

Scheduling

User access & policies

It uses CQRS + MediatR for request/response handling and PostgreSQL for persistence.

🏛️ Architecture

The project follows strict Clean Architecture:

/src
 ├── LearnWell.CourseManagement.Api         → Presentation
 ├── LearnWell.CourseManagement.Application → CQRS + Business Rules
 ├── LearnWell.CourseManagement.Domain      → Entities + Value Objects
 └── LearnWell.CourseManagement.Infrastructure → EF Core + Repositories


✔ Domain Layer is pure (no dependencies)
✔ Application uses MediatR for commands & queries
✔ Infrastructure implements repositories
✔ API is thin (controllers → handlers)

🧰 Technology Stack
Component	Tech
Backend	.NET 9
Architecture	Clean Architecture + DDD
Database	PostgreSQL
ORM	EF Core 9
Messaging	MediatR
Logging	Serilog (optional: Seq)
Auth	ASP.NET Identity / Policies
📦 Prerequisites

Before setting up the project, install:

1️⃣ .NET 9 SDK

Verify:

dotnet --version


Should show:
9.x.x

2️⃣ PostgreSQL 16+

Download: https://www.postgresql.org/download/

Create a database named:

learnwell_course_db

3️⃣ (Optional) Install EF Tools
dotnet tool install --global dotnet-ef

📥 Installation

Clone the repository:

git clone https://github.com/<your-user>/LearnWell_CourseManagement.git
cd LearnWell_CourseManagement


Restore packages:

dotnet restore

🗄️ Database Setup

Update your connection string in:

src/LearnWell.CourseManagement.Api/appsettings.json


Example:

"ConnectionStrings": {
  "DefaultConnection": "Host=coursemanagement-db; Port=5432;Database=coursemanagement;Username=postgres;Password=yourpassword"
}

🧭 Run Migrations

Inside the Infrastructure project directory:

cd src/LearnWell.CourseManagement.Infrastructure

Add migration
dotnet ef migrations add InitialCreate -s ../LearnWell.CourseManagement.Api

Apply migration
Migration will be applied automatically when the application starts.


✔ -s points to the API startup assembly.

▶️ Running the API

API will be running from docker container

Swagger UI:

https://localhost:5026/swagger

🔐 Environment Variables

Create a file:

src/LearnWell.CourseManagement.Api/appsettings.Development.json


Include:

{
  "ConnectionStrings": {
    "DefaultConnection": "Host=coursemanagement-db;Database=coursemanagement;Username=postgres;Password=yourpassword"
  },
  "Jwt": {
    "Key": "your-secret-key",
    "Issuer": "LearnWell",
    "Audience": "LearnWellUsers"
  }
}

src/
├── LearnWell.CourseManagement.Api
│   ├── Controllers
│   ├── Extensions
│   └── Program.cs
│
├── LearnWell.CourseManagement.Application
│   ├── Courses
│   │   ├── CreateCourse
│   │   └── GetStudentsByCourse
│   ├── Classes
│   ├── Students
│   ├── Abstractions
│   └── Behaviors
│
├── LearnWell.CourseManagement.Domain
│   ├── Entities
│   ├── ValueObjects
│   ├── Enums
│   └── Errors
│
└── LearnWell.CourseManagement.Infrastructure
    ├── Repositories
    ├── Database
    └── Configurations


📡 API Usage Examples
Get Students for a Course
GET /api/v1/courses/{courseId}/students

Get Classes for a Course
GET /api/v1/courses/{courseId}/classes

Create a Course
POST /api/v1/courses


JSON:

{
  "title": "Programming 101",
  "code": "PROG-101",
  "credits": 3
}

🧪 Unit Testing

Tests go inside:

tests/LearnWell.CourseManagement.Tests


Run:

dotnet test
