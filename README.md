# CollabSphere (COSRE)

Project-Based Learning Management System with Microservices Architecture

## 🏗️ Architecture

* **Backend:** ASP.NET Core 9.0 (Microservices)
* **Frontend:** React 18 + Vite
* **Database:** PostgreSQL 17
* **Cache:** Redis 7 (Local) / Upstash (Production)
* **Container:** Docker + Docker Compose

## 📂 Project Structure

```
collabsphere/
├── src/                          # Backend services
│   ├── ApiGateway/              # API Gateway (Ocelot)
│   ├── AuthService/             # Authentication & Authorization
│   ├── UserService/             # User Management
│   ├── AcademicService/         # Subject, Syllabus, Class
│   ├── ProjectService/          # Project Management
│   ├── TeamService/             # Team & Workspace
│   ├── CommunicationService/    # Chat & Video (SignalR, WebRTC)
│   ├── CollaborationService/    # Whiteboard & Text Editor
│   ├── EvaluationService/       # Feedback & Rating
│   ├── NotificationService/     # Email & Real-time Notifications
│   ├── FileService/             # Resource Management
│   └── AIService/               # AI Chatbot & Generation
├── frontend/
│   └── web-app/                 # React application
├── docker/                       # Docker configurations
├── docs/                        # Documentation
└── docker-compose.dev.yml       # Development environment
```

## 🚀 Getting Started

### Prerequisites

* .NET SDK 9.0
* Node.js 20+
* PostgreSQL 17
* Docker Desktop
* Git

### Setup Development Environment

1. **Clone repository:**

```bash
git clone https://github.com/VuChiHieu/collabsphere
cd collabsphere
```

2. **Start Docker services:**

```bash
docker-compose -f docker-compose.dev.yml up -d
```

3. **Setup Backend:**

```bash
cd src
dotnet restore
dotnet build
```

4. **Setup Frontend:**

```bash
cd frontend/web-app
npm install
npm run dev
```

### Access Services

* **Frontend:** [http://localhost:5173](http://localhost:5173)
* **API Gateway:** [http://localhost:5000](http://localhost:5000)
* **PostgreSQL:** localhost:5432
* **Redis:** localhost:6379
* **pgAdmin:** [http://localhost:5050](http://localhost:5050)

  * Email: [admin@collabsphere.com](mailto:admin@collabsphere.com)
  * Password: admin123

## 🗄️ Database Setup

### PostgreSQL Installation

**⚠️ IMPORTANT: Install PostgreSQL locally (NOT in Docker)**

Each team member must install PostgreSQL:

1. **Download PostgreSQL**

   * Version: 17 (recommended) or 16+
   * Components: Install PostgreSQL Server

2. **Installation**

   * Port: `5432`
   * Password: *remember this password!*

3. **Verify PostgreSQL is running**

   * Windows: Services → postgresql-x64-17

---

### Database Configuration (Using pgAdmin Web)

We use **pgAdmin Web (Docker)** for consistency.

#### Step 1: Start Docker Services

```bash
docker-compose -f docker-compose.dev.yml up -d
```

#### Step 2: Access pgAdmin Web

* URL: [http://localhost:5050](http://localhost:5050)
* Email: `admin@collabsphere.com`
* Password: `admin123`

#### Step 3: Quick Setup Steps

1. Add new server (use `host.docker.internal`)
2. Create database: `collabsphere_dev`
3. Create user: `cosre_admin` / `dev123456`
4. Grant all privileges

### Connection String

```
Host=localhost;Port=5432;Database=collabsphere_dev;Username=cosre_admin;Password=dev123456
```

---

## 👥 Team Members

* **Vũ Chí Hiếu** - DevOps & Infrastructure
* **Hà Ngọc Hiếu** - BA & Documentation
* **Nguyễn Thúc Gia Khôi** - Backend Architect
* **Phạm Nhật Huy** - Frontend & DevOps

## 📚 Documentation

* **Confluence:** CollabSphere Wiki
* **Jira:** Project Board
* **GitHub Wiki:** Technical Docs

## 🔧 Tech Stack

### Backend

* ASP.NET Core 9.0
* PostgreSQL 17
* Redis
* SignalR
* WebRTC

### Frontend

* React 18
* Vite
* TypeScript
* Tailwind CSS / MUI

### Cloud

* Azure (Backend)
* AWS (Frontend + Bedrock)
* Cloudinary

### DevOps

* Docker
* GitHub Actions

## 📋 Development Guidelines

### Branch Strategy

* `main`
* `develop`
* `feature/*`
* `bugfix/*`
* `hotfix/*`

### Commit Convention

```
type(scope): subject
```

## 📝 License

[Add license information]

---

**Last Updated:** 05/12/2025
