LearnWell Course Management System

A modular, domain-driven Course, Class & Enrollment Management Platform for LearnWell University.
This Proof-of-Concept demonstrates a scalable backend architecture using:

.NET 9

Clean Architecture + DDD

CQRS (MediatR)

PostgreSQL

Keycloak Authentication

Entity Framework Core

Serilog + Seq Logging

Full Docker Compose Environment

The project digitizes and modernizes student onboarding, class scheduling, course management, and academic workflows.

## 📚 Table of Contents
- [Overview](#overview)
- [Architecture](#architecture)
- [Features](#features)
- [Installation & Setup](#installation--setup)
- [Docker Compose](#docker-compose)
- [Keycloak Configuration](#keycloak-configuration)
- [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Tech Stack](#tech-stack)





📘 ## 🛠 Overview

The LearnWell Course Management System provides:

Course Management

Class Management

Shared classes across courses (e.g., Programming & Business share Math 101)

Student Management

Enrollment Management

Scheduling

Keycloak-based authentication

Full CQRS pipeline (Commands + Queries)

Clean Architecture with modular boundaries

Backend is DDD-driven, separating domain logic, persistence, infrastructure, and application workflows clearly.

🚀 Features

CRUD for Courses, Classes, Students, and Enrollments

Many-to-many Course ↔ Class relationships

Students enrolled across multiple classes

Authentication + Authorization via Keycloak

Full Docker automation

Seq-based centralized logging

PostgreSQL with EF Core migrations

Domain-driven entities and value objects

MediatR-powered CQRS

High-Level System Diagram

          ┌───────────────────────────────┐
          │          .NET 9 API           │
          │     (CQRS + DDD + MediatR)    │
          └───────────────▲──────────────┘
                          │ EF Core
          ┌───────────────┴──────────────┐
          │          PostgreSQL           │
          └───────────────▲──────────────┘
                          │ Authentication
          ┌───────────────┴──────────────┐
          │           Keycloak            │
          └───────────────────────────────┘


Clean Architecture Layers
src/
├── LearnWell.CourseManagement.Api           → Presentation Layer
├── LearnWell.CourseManagement.Application    → CQRS, Handlers, Business Logic
├── LearnWell.CourseManagement.Domain         → Entities, Value Objects, Enums
└── LearnWell.CourseManagement.Infrastructure → EF Core, Repositories, Configurations


✔ Domain is pure (no dependencies)
✔ Application uses MediatR
✔ Infrastructure performs persistence
✔ API is thin → calls queries/commands

🧰 Technology Stack
Component	Technology
Backend	.NET 9
Architecture	Clean Architecture + DDD
ORM	EF Core 9
DB	PostgreSQL
Auth	Keycloak (OIDC)
Messaging	MediatR (CQRS)
Logging	Serilog + Seq
Cache	Redis
Containers	Docker Compose
📦 Prerequisites

Install the following:

1️⃣ .NET 9 SDK

Verify:

dotnet --version

2️⃣ Docker & Docker Compose
3️⃣ EF Tools (optional)
dotnet tool install --global dotnet-ef

📥 Installation

Clone the repository:

git clone https://github.com/<your-org>/LearnWell_CourseManagement.git
cd LearnWell_CourseManagement

⚙️ Environment Variables

Create .env in root:

POSTGRES_USER=postgres
POSTGRES_PASSWORD=Abcd1234!
POSTGRES_DB=coursemanagement

KEYCLOAK_ADMIN=admin
KEYCLOAK_ADMIN_PASSWORD=admin

🐳 Docker Compose Setup

Everything runs inside Docker:

API

PostgreSQL

Keycloak

Redis

Seq Logging

Run:

docker compose up -d --build


Stop:

docker compose down

🔐 Keycloak Setup

Keycloak Admin Console:

http://localhost:8080


Login:

Admin: admin

Password: admin

You may import a realm or configure:

Realm: coursemanagement

Clients:

coursemanagement-auth-client

coursemanagement-admin-client

🗄 Database Setup

Postgres starts automatically in Docker:

Host: coursemanagement-db
Database: coursemanagement
User: postgres
Password: Abcd1234!


EF migrations run on API startup.

Manual migration:

dotnet ef database update

🧭 Running EF Migrations

From Infrastructure directory:

cd src/LearnWell.CourseManagement.Infrastructure
dotnet ef migrations add InitialCreate -s ../LearnWell.CourseManagement.Api


Apply:

dotnet ef database update -s ../LearnWell.CourseManagement.Api

🧱 Project Structure

src/
├── LearnWell.CourseManagement.Api
│   ├── Controllers
│   ├── Extensions
│   └── Program.cs
│
├── LearnWell.CourseManagement.Application
│   ├── Abstractions
│   ├── Behaviors
│   ├── Courses
│   ├── Classes
│   └── Students
│
├── LearnWell.CourseManagement.Domain
│   ├── Entities
│   ├── ValueObjects
│   ├── Enums
│   └── Errors
│
└── LearnWell.CourseManagement.Infrastructure
    ├── Database
    ├── Configurations
    └── Repositories


📡 API Usage

Once containers start:

API:     http://localhost:5000
Swagger: http://localhost:5000/swagger

Example Endpoints
Get Students of a Course
GET /api/v1/courses/{courseId}/students

Get Classes of a Course
GET /api/v1/courses/{courseId}/classes

Create a Course
POST /api/v1/courses


Payload:

{
  "title": "Programming 101",
  "code": "PROG-101",
  "credits": 3
}

📚 Entity Relationship Diagram (Text)
Course (1) ───── (M) ClassCourse (M) ───── (1) Class

Student (1) ─── (M) Enrollment (M) ───── (1) Class

📜 Logging (Serilog + Seq)
Seq UI:
http://localhost:5341

Program.cs
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://coursemanagement-seq:5341")
    .CreateLogger();

🏁 Running the Application

Run everything:

docker compose up --build


Then access:

Service	URL
API	http://localhost:5000

Swagger	http://localhost:5000/swagger

Keycloak	http://localhost:8080

Seq	http://localhost:5341

Postgres	localhost:5432
🧪 Unit Testing

Tests are in:

tests/LearnWell.CourseManagement.Tests


Run:

dotnet test
