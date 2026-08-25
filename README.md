# FactoryFlow

Manufacturing Workflow Management System.

## Development Setup

### Backend

```bash
cd backend
dotnet restore
dotnet build
dotnet run --project src/FactoryFlow.Api
```

Backend:

```text
https://localhost:7010
```

Health endpoint:

```text
GET /api/health
```

### Frontend

```bash
cd frontend
npm install
npm run dev
```

Frontend:

```text
http://localhost:5173
```

## Environment Variables

Create `frontend/.env` from `frontend/.env.example`:

```env
VITE_API_URL=https://localhost:7010
```

## Documentation

See the `docs` folder for project vision, requirements, architecture, and ER diagram.
