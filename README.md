# Timescale WebAPI

WebAPI приложение для работы с timescale данными результатов обработки.
Построено на Clean Architecture с использованием .NET 8, EF Core и PostgreSQL.

## Архитектура

```
Solution/
├── Domain/          Сущности, интерфейсы репозиториев, исключения
├── Application/     Use Cases, DTOs, валидаторы, парсинг CSV
├── Infrastructure/  EF Core, DbContext, реализации репозиториев
└── WebAPI/          Controllers, Middleware, Program.cs, Swagger
```

## Быстрый старт

### Через Docker Compose (рекомендуется)

```bash
# Клонировать репозиторий
git clone <repo-url>
cd <repo-folder>
dotnet publish WebAPI/WebAPI.csproj -c Release -p:ContainerImageTag=latest /t:PublishContainer

# Поднять PostgreSQL + WebAPI
docker compose up -d --build
```

API будет доступен на: `http://localhost:8080`  
Swagger UI: `http://localhost:8080/swagger`

### Локальный запуск

**0. Выключить сборку контейнера

**1. Поднять PostgreSQL:**
```bash
docker run -d \
  --name timescale-postgres \
  -e POSTGRES_USER=postgres \
  -e POSTGRES_PASSWORD=postgres \
  -e POSTGRES_DB=timescaledb \
  -p 5432:5432 \
  postgres:16
```

**2. Применить миграции:**
```bash
dotnet ef database update \
  --project Infrastructure \
  --startup-project WebAPI
```

**3. Запустить API:**
```bash
dotnet run --project WebAPI
```

Swagger UI: `http://localhost:5000/swagger`

## Конфигурация

Строка подключения задаётся в `WebAPI/appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=timescaledb;Username=postgres;Password=postgres"
  }
}
```