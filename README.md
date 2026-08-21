# FactoryFlow

## Development Setup

### Frontend

```bash
cd frontend
npm install
npm run dev
```

The frontend runs on:

```text
http://localhost:5173
```

### Backend

```bash
cd backend
npm install
npm run build
npm start
```

The backend runs on:

```text
http://localhost:3000
```

Health endpoint:

```text
GET /api/health
```

## Environment Variables

Create `.env` files based on the provided `.env.example` files.

### Frontend

```env
VITE_API_URL=http://localhost:3000
```

### Backend

```env
PORT=3000
FRONTEND_URL=http://localhost:5173
```

## Documentation

Project documentation is available in the `docs` folder:

* Project Vision
* Business Requirements
* Core Modules
* Initial Architecture
* Initial ER Diagram

