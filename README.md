# Gymawy 🏋️‍♂️

**Gymawy** is a backend API for managing gyms, built with **ASP.NET Core**, following **Clean Architecture**, **CQRS**, and **Domain-Driven Design** principles.  
It supports different types of users like Gym Admins (Owners), Trainers, and Participants, with features like subscriptions, session bookings, payment processing, and secure authentication.

---

## 🚧 Project Status

> This project is under active development.  
> The overall architecture is complete — all upcoming features will follow clean structure, domain events, and complete use case implementation.

---

## 💡 Key Features

- 🔐 **Authentication & Authorization**
  - JWT access & refresh tokens
  - Role-based auth & claims transformation
- 👥 **User Roles**
  - Admins (Gym Owners)
  - Trainers
  - Participants
- 🏋️‍♀️ **Gym Structure**
  - Admins manage gyms
  - Gyms contain rooms
  - Rooms contain sessions
- 📅 **Sessions**
  - Trainers train sessions
  - Participants book & join sessions
- 💳 **Stripe Integration**
  - Subscription plans: Free, Starter, Pro
  - Payments & booking verification
- ☁️ **Cloudinary**
  - Upload and manage images
- 🧱 **Architecture**
  - Clean Architecture
  - CQRS with MediatR
  - Repository & Unit of Work patterns
- 🌍 **Best API Practices**
  - RESTful endpoints
  - Separation of concerns
  - Consistent response models

---

## 📦 Technologies Used

- ASP.NET Core 8
- Entity Framework Core
- MediatR
- Stripe SDK
- Cloudinary SDK
- SQL Server

---

## 🗂️ Folder Structure

```bash
/src
  ├── Domain
  ├── Application
  ├── Infrastructure
  ├── Contract
  └── API (Presentation Layer)
---

##📌 How to Run
Clone the repo

Set your values in appsettings.json

Run database migrations

Run the API from Visual Studio or dotnet run

---

📬 Contact
Author: Saleh Developer

LinkedIn: salehdeveloper

Email: ahmadsalehdev.22@gmail.com
