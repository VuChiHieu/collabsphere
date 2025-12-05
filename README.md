# CollabSphere (COSRE)

Project-Based Learning Management System with Microservices Architecture

## 🏗️ Architecture

- **Backend:** ASP.NET Core 9.0 (Microservices)
- **Frontend:** React 18 + Vite
- **Database:** PostgreSQL 17
- **Cache:** Redis 7 (Local) / Upstash (Production)
- **Container:** Docker + Docker Compose

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

- .NET SDK 9.0
- Node.js 20+
- PostgreSQL 17
- Docker Desktop
- Git

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

- **Frontend:** http://localhost:5173
- **API Gateway:** http://localhost:5000
- **PostgreSQL:** localhost:5432
- **Redis:** localhost:6379
- **pgAdmin:** http://localhost:5050
  - Email: admin@collabsphere.com
  - Password: admin123

## 👥 Team Members

- **Member 1:** Vũ Chí Hiếu - DevOps & Infrastructure
- **Member 2:** Hà Ngọc Hiếu - BA & Documentation
- **Member 3:** Nguyễn Thúc Gia Khôi - Backend Architect
- **Member 4:** Phạm Nhật Huy - Frontend & DevOps

## 📚 Documentation

Full documentation available on:
- **Confluence:** [CollabSphere Wiki](https://vuchihieu05.atlassian.net/wiki/spaces/CollabSphe/overview)
- **Jira:** [Project Board](https://vuchihieu05.atlassian.net/jira/software/projects/COSRE/boards/34)
- **GitHub Wiki:** [Technical Docs](link-if-any)

## 🔧 Tech Stack

### Backend
- ASP.NET Core 9.0
- Entity Framework Core 9
- PostgreSQL 17
- Redis (Upstash)
- SignalR (Chat)
- WebRTC (Video)

### Frontend
- React 18
- Vite
- TypeScript
- Material-UI / Tailwind CSS

### Cloud Services
- Azure (Backend hosting)
- AWS (Frontend hosting, Bedrock AI)
- Cloudinary (Media storage)

### DevOps
- Docker & Docker Compose
- GitHub Actions (CI/CD)

## 📋 Development Guidelines

### Branch Strategy
- `main` - Production
- `develop` - Integration
- `feature/*` - New features
- `bugfix/*` - Bug fixes
- `hotfix/*` - Critical fixes

### Commit Convention
```
type(scope): subject

Types: feat, fix, docs, style, refactor, test, chore
Example: feat(auth): implement JWT authentication
```

## 📝 License

[Add license information]

---

**Last Updated:** 05/12/2025