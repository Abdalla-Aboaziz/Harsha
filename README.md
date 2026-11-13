# Harsha — ASP.NET Core Deep Dives

A focused learning repository where each project isolates one ASP.NET Core concept, explains *why* it matters, and demonstrates *how* it behaves in practice — not just how to configure it.

> 📬 abdallaaboaziz@gmail.com

---

## Why This Exists

Most tutorials show you *what* to write. These projects answer questions that actually come up in code reviews:

- Why does injecting the same service twice in one request give different instances?
- What happens to the middleware pipeline when you forget to call `next()`?
- Why does `IOptions<T>` not reflect config changes at runtime but `IOptionsMonitor<T>` does?
- What's the actual difference between Conventional and Attribute routing under the hood?

Each project was built to answer questions like these through running, observable code.

---

## Projects

### 1. Model Binding — `Harsha/`
Demonstrates how ASP.NET Core resolves action parameters from multiple sources (route, query string, form body, headers, JSON body) and the order of precedence between them. Includes a custom `IModelBinderProvider` implementation.

### 2. Dependency Injection — `Dependency_Injection/`
Uses **Autofac** alongside the built-in container to make service lifetimes *visible*. Each lifetime (Transient, Scoped, Singleton) exposes its GUID so you can observe the difference across requests in real time — not just read about it.

```
Transient  → new GUID every injection within the same request
Scoped     → same GUID within one request, new GUID on the next
Singleton  → same GUID across all requests for the app's lifetime
```

### 3. Configuration & Options Pattern — `Configurations/`
Shows the practical difference between `IOptions<T>`, `IOptionsSnapshot<T>`, and `IOptionsMonitor<T>` — specifically which ones respond to config file changes without a restart. Loads settings from multiple sources including custom JSON files.

### 4. Structured Logging — `Logging/`
Integrates **Serilog** with four sinks running simultaneously: Console, rolling File, SQL Server, and **Seq** (real-time log monitoring UI). Also demonstrates `HttpLogging` middleware for full request/response tracing.

### 5. Custom Middleware — `MiddleWares/`
Builds middleware components from scratch. Shows the difference between `app.Use()` (passes to next) and `app.Run()` (terminal), and what short-circuiting the pipeline looks like from the outside.

### 6. Routing — `Routing/`
Covers Conventional routing, Attribute routing, and direct endpoint mapping. Demonstrates how route constraints, optional segments, and default values interact.

### 7. HttpClient & External APIs — `ConfigurationStocksApp/`
Typed `HttpClient` calling the **Finhub** financial data API. Shows proper client configuration, DI registration, and separating API concerns into a dedicated service layer.

### 8. Middleware Pipeline Internals — `WebApplication2/`
Focused on pipeline order and behavior. Demonstrates exactly what happens when `next()` is or isn't called, and how middleware wraps the response on the way back out.

### 9. Razor Views & Layouts — `WebApplication2 & Routing/`
MVC views with `_ViewImports`, Tag Helpers, and shared `_Layout` — the foundation for any server-rendered UI layer.

---

## Stack

| | |
|---|---|
| IoC | Built-in DI + Autofac |
| Logging | Serilog (Console · File · SQL Server · Seq) |
| Database | SQL Server / LocalDB |
| External API | Finhub Trading API |

---

## Setup

```bash
git clone https://github.com/Abdalla-Aboaziz/Harsha.git
cd Harsha && dotnet restore

# Each project runs independently
cd Dependency_Injection && dotnet run
```

**Logging project** needs a SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=HarshaLogs;Integrated Security=True;"
}
```

**ConfigurationStocksApp** needs a Finhub API key in `appsettings.json`.

---

## Suggested Study Order

If you're working through these systematically:

```
Routing → Model Binding → Configurations → Dependency Injection
       → Middleware → Logging → HttpClient
```

Start with Routing — everything else builds on understanding how a request reaches your code.
