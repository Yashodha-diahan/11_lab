# CampusEats – Lab 11 Deployment

| Tier | Folder | Host |
|---|---|---|
| Frontend (React + Vite) | `campuseats-web/` | Vercel (Root Directory `campuseats-web`, build `npm run build`, output `dist`) |
| Backend (ASP.NET Core 8) | `CampusEats.Api/` | Render (Docker, Root Directory `CampusEats.Api`) |
| Database (PostgreSQL) | EF Core migrations in `CampusEats.Api/Migrations` | Neon |

## Environment variables (set on the platforms, never commit values)

- Frontend: `VITE_API_URL` – deployed API base URL (redeploy after changing; Vite bakes it in at build time)
- Backend: `ConnectionStrings__Default`, `Jwt__Key`, `Jwt__Issuer`, `Jwt__Audience`, `Cors__AllowedOrigins`

## Run locally

```bash
cd CampusEats.Api && dotnet ef database update && dotnet run --launch-profile http   # http://localhost:5229/swagger
cd campuseats-web && cp .env.example .env.local && npm install && npm run dev        # http://localhost:5173
```

## Apply migrations to Neon (PowerShell)

```powershell
$env:ConnectionStrings__Default = "<Neon connection string with SSL Mode=Require>"
dotnet ef database update
```
