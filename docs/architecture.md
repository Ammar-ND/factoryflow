# FactoryFlow — Initial Architecture

```mermaid
flowchart LR
    Frontend[React Frontend] --> API[ASP.NET Core API]
    API --> Application[Application Layer]
    Application --> Domain[Domain Layer]
    API --> Infrastructure[Infrastructure Layer]
    Infrastructure --> Database[(SQL Server)]
```

## Layers

* **Frontend:** User interface.
* **API:** Receives HTTP requests.
* **Application:** Handles application use cases.
* **Domain:** Contains business rules.
* **Infrastructure:** Handles database and external services.
* **Database:** Stores application data.
