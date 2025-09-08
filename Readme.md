# Prod API (.NET) 🚀

**Cel:** Stworzenie API produkcyjnej jakości w ASP.NET Core z autoryzacją, logowaniem, testami i infrastrukturą w Azure.

Stack: **.NET 8, EF Core, FluentValidation, Serilog, Azure Entra ID (OIDC), Terraform, Docker, GitHub Actions**

---

## 🎯 Funkcjonalność

- CRUD dla `TaskItem` (Id, Tytuł, Opis, Status)
- Walidacja danych (`FluentValidation`)
- Logowanie i obsługa błędów (`Serilog`, middleware)
- Autoryzacja i role (`Admin`, `Worker`)
- Testy jednostkowe i integracyjne
- CI/CD (GitHub Actions → Docker → Azure App Service)
- IaC (`Terraform` → App Service, Azure SQL, Key Vault)
- Monitoring (`Application Insights`)

---

## 🛠 Architektura

UI (opcjonalny) → API (.NET) → DB (Azure SQL)
│
Observability (App Insights)
CI/CD: GitHub Actions → GHCR → Azure App Service
IaC: Terraform (App Service, SQL, Key Vault)

---

## 🚦 Roadmap

### Etap 1 – Fundamenty (tydz. 1)

- [x] Projekt ASP.NET Core Web API
- [x] Encja `TaskItem` + CRUD (EF Core + SQL)
- [x] Migracje bazy danych

### Etap 2 – Jakość kodu (tydz. 2)

- [x] Walidacja (FluentValidation)
- [x] Logi (Serilog)
- [x] Obsługa błędów (middleware)
- [ ] Testy jednostkowe (xUnit + Moq)
- [ ] Testy integracyjne (WebApplicationFactory)

### Etap 3 – Bezpieczeństwo (tydz. 3)

- [ ] Autoryzacja JWT / Azure Entra ID
- [ ] Role (`Admin`, `Worker`)
- [ ] Zabezpieczenie Swagger

### Etap 4 – DevOps / IaC (tydz. 4)

- [ ] Dockerfile (multi-stage)
- [ ] GitHub Actions (build, test, docker push)
- [ ] Terraform (App Service, SQL, Key Vault, Connection String)

### Etap 5 – Observability & Extras (tydz. 5)

- [ ] Application Insights (trace’y, metryki)
- [ ] Resilience (`Polly`)
- [ ] README z diagramem
- [ ] (Opcjonalnie) prosty frontend (Next.js)

---

## 🧑‍💻 Jak uruchomić (dev)

```bash
dotnet new webapi -n ProdApi
cd ProdApi
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package FluentValidation.AspNetCore
dotnet add package Serilog.AspNetCore
dotnet add package xunit
dotnet add package Moq
```
