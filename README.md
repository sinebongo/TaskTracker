# TaskTracker

A full-stack task management application built with .NET 9 Web API and Angular 20. This application allows users to create, read, update, and delete tasks with priority levels, status tracking, and due dates.

## Architecture

- **Backend**: .NET 9 Minimal API with Clean Architecture
- **Frontend**: Angular 20 with standalone components
- **Database**: Entity Framework Core with In-Memory Database
- **API Documentation**: Swagger/OpenAPI

## Features

- ✅ Create, read, update, and delete tasks
- ✅ Task priorities (Low, Medium, High)
- ✅ Task statuses (New, InProgress, Completed)
- ✅ Due date management
- ✅ Search and sort functionality
- ✅ RESTful API with Swagger documentation
- ✅ Responsive Angular UI
- ✅ CORS-enabled for cross-origin requests

## Prerequisites

Before running this application, make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js](https://nodejs.org/) (version 18 or higher)
- [Angular CLI](https://angular.io/cli) (`npm install -g @angular/cli`)

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/sinebongo/TaskTracker.git
cd TaskTracker
```

### 2. Backend Setup (.NET API)

Navigate to the backend directory:

```bash
cd backend/TaskTrackerAPI
```

Restore NuGet packages:

```bash
dotnet restore
```

Build the project:

```bash
dotnet build
```

Run the API:

```bash
dotnet run
```

The API will be available at:
- HTTP: `http://localhost:5183`
- HTTPS: `https://localhost:7114`
- Swagger UI: `http://localhost:5183` or `https://localhost:7114`

### 3. Frontend Setup (Angular)

Open a new terminal and navigate to the frontend directory:

```bash
cd frontend/TaskTrackerUI
```

Install npm packages:

```bash
npm install
```

Start the development server:

```bash
npm start
```

The Angular application will be available at `http://localhost:4200`

## API Endpoints

The TaskTracker API provides the following endpoints:

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks (supports query `?q=` and sort `?sort=`) |
| GET | `/api/tasks/{id}` | Get task by ID |
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Update an existing task |
| DELETE | `/api/tasks/{id}` | Delete a task |

### Sample Task Object

```json
{
  "id": 1,
  "title": "Sample Task",
  "description": "Task description",
  "status": 0,
  "priority": 1,
  "dueDate": "2025-11-15T00:00:00Z",
  "createdAt": "2025-11-10T00:00:00Z"
}
```

**Status Enum Values:**
- 0: New
- 1: InProgress
- 2: Completed

**Priority Enum Values:**
- 0: Low
- 1: Medium
- 2: High

## Development

### Running Tests

**Backend Tests:**
```bash
cd backend/TaskTrackerAPI
dotnet test
```

**Frontend Tests:**
```bash
cd frontend/TaskTrackerUI
npm test
```

### Building for Production

**Backend:**
```bash
cd backend/TaskTrackerAPI
dotnet publish -c Release
```

**Frontend:**
```bash
cd frontend/TaskTrackerUI
npm run build
```

## Project Structure

```
TaskTracker/
├── backend/
│   └── TaskTrackerAPI/
│       ├── Application/          # Business logic layer
│       ├── Domain/              # Entity models and enums
│       ├── Endpoints/           # API endpoint definitions
│       ├── Infrastructure/      # Data access layer
│       └── Program.cs          # Application entry point
├── frontend/
│   └── TaskTrackerUI/
│       └── src/
│           └── app/
│               ├── components/   # Angular components
│               ├── models/      # TypeScript models
│               └── services/    # Angular services
├── README.md
└── SOLUTION.md
```

## Technologies Used

### Backend
- .NET 9
- ASP.NET Core Minimal APIs
- Entity Framework Core
- In-Memory Database
- Swagger/OpenAPI
- Clean Architecture

### Frontend
- Angular 20
- TypeScript
- RxJS
- Angular CLI
- Standalone Components

## License

This project is licensed under the MIT License.