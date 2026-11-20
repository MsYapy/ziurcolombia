# 🤖 AI Skills - Guía para Asistentes de IA

Este documento proporciona contexto estructurado para asistentes de IA (como GitHub Copilot, ChatGPT, Claude, etc.) que trabajen en el proyecto Ziur.

---

## 📋 Información del Proyecto

**Nombre**: Ziur - Sistema de Gestión de Inventario  
**Framework**: Blazor .NET 10  
**Arquitectura**: Clean Architecture (4 capas)  
**Patrón**: Repository Pattern, Dependency Injection  
**Referencia**: BlazorApp2 (código optimizado sin sobre-ingeniería)

---

## 🏗️ Arquitectura del Proyecto

### Estructura de Capas

```
┌─────────────────────────────────────┐
│      Ziur.Client (Frontend)         │
│   Blazor WebAssembly Components    │
└─────────────┬───────────────────────┘
              │
              ↓
┌─────────────────────────────────────┐
│      Ziur (Backend Server)          │
│   ASP.NET Core API Controllers      │
└─────────────┬───────────────────────┘
              │
              ↓
┌─────────────────────────────────────┐
│   Ziur.Infrastructure (Services)    │
│   HttpClient, External APIs         │
└─────────────┬───────────────────────┘
              │
              ↓
┌─────────────────────────────────────┐
│      Ziur.Shared (Models)           │
│      DTOs, Interfaces               │
└─────────────────────────────────────┘
```

### Flujo de Datos

```
[External API] → [InventoryService] → [InventoryController] 
                                            ↓
                                  [InventoryServiceClient]
                                            ↓
                                  [Blazor Components]
```

---

## 📁 Guía de Ubicación de Archivos

### ¿Dónde agregar nuevo código?

#### **Modelos de Datos (DTOs)**
- **Ubicación**: `Ziur.Shared/Models/DTOs/`
- **Propósito**: Transferir datos entre capas
- **Ejemplo**: `MovementDto.cs`
- **Patrón**:
```csharp
namespace Ziur.Shared.Models.DTOs;

public class NombreDto
{
    public int Id { get; set; }
    public string Nombre { get; set; } = string.Empty;
}
```

#### **Servicios de Infrastructure**
- **Ubicación**: `Ziur.Infrastructure/Services/`
- **Propósito**: Consumir APIs externas
- **Ejemplo**: `InventoryService.cs`
- **Patrón**:
```csharp
namespace Ziur.Infrastructure.Services;

public class NombreService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<NombreService> _logger;
    
    public NombreService(HttpClient httpClient, ILogger<NombreService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
}
```

#### **Controllers (API Endpoints)**
- **Ubicación**: `Ziur/Controllers/Api/`
- **Propósito**: Exponer endpoints REST
- **Ejemplo**: `InventoryController.cs`
- **Patrón**:
```csharp
namespace Ziur.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class NombreController : ControllerBase
{
    private readonly NombreService _service;
    
    [HttpGet]
    public async Task<ActionResult<List<Dto>>> Get()
    {
        // Implementation
    }
}
```

#### **Blazor Components**
- **Ubicación**: `Ziur.Client/Features/{Feature}/Components/`
- **Propósito**: Componentes reutilizables de UI
- **Ejemplo**: `DocumentoGrid.razor`
- **Patrón**:
```razor
@using Ziur.Shared.Models.DTOs

<div class="component-container">
    <!-- HTML -->
</div>

@code {
    [Parameter]
    public List<Dto> Data { get; set; } = new();
}
```

#### **Páginas Blazor**
- **Ubicación**: `Ziur.Client/Features/{Feature}/Pages/`
- **Propósito**: Páginas completas con routing
- **Ejemplo**: `CatalogPage.razor`
- **Patrón**:
```razor
@page "/ruta"
@rendermode InteractiveServer
@inject IService Service

<PageTitle>Título</PageTitle>

<!-- Content -->

@code {
    protected override async Task OnInitializedAsync()
    {
        // Initialize
    }
}
```

#### **Servicios del Cliente**
- **Ubicación**: `Ziur.Client/Features/{Feature}/Services/`
- **Propósito**: Proxy para llamar al backend
- **Ejemplo**: `InventoryServiceClient.cs`
- **Patrón**:
```csharp
namespace Ziur.Client.Features.Inventory.Services;

public class NombreServiceClient
{
    private readonly HttpClient _httpClient;
    
    public async Task<List<Dto>> GetAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Dto>>("api/endpoint");
    }
}
```

#### **Estilos CSS**
- **Ubicación**: `Ziur.Client/wwwroot/css/`
- **Archivos**:
  - `app.css` - Estilos globales, variables, utilidades
  - `components.css` - Estilos de componentes
  - `pages/{nombre}.css` - Estilos específicos de páginas

---

## 🎯 Convenciones de Código

### Naming Conventions

```csharp
// Classes & Interfaces - PascalCase
public class InventoryService { }
public interface IInventoryService { }

// Methods - PascalCase
public async Task<List<MovementDto>> GetMovementsAsync() { }

// Parameters & Variables - camelCase
private readonly HttpClient _httpClient;
public string firstName { get; set; }

// Constants - PascalCase
public const string SectionName = "ZiurApiSettings";

// Private fields - _camelCase
private readonly ILogger<InventoryService> _logger;
```

### Async Patterns

```csharp
// ✅ Correcto
public async Task<List<MovementDto>> GetMovementsAsync(CancellationToken cancellationToken = default)
{
    var response = await _httpClient.GetAsync("endpoint", cancellationToken);
    return await response.Content.ReadFromJsonAsync<List<MovementDto>>(cancellationToken);
}

// ❌ Incorrecto
public List<MovementDto> GetMovements()
{
    var task = _httpClient.GetAsync("endpoint");
    task.Wait(); // Blocking
    return task.Result;
}
```

### Exception Handling

```csharp
try
{
    _logger.LogInformation("Starting operation");
    var result = await Operation();
    return result;
}
catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
{
    _logger.LogInformation("Operation was cancelled");
    throw;
}
catch (HttpRequestException httpEx)
{
    _logger.LogError(httpEx, "HTTP error occurred");
    throw new ApplicationException("User-friendly message", httpEx);
}
catch (Exception ex)
{
    _logger.LogError(ex, "Unexpected error");
    throw new ApplicationException("Unexpected error message", ex);
}
```

---

## 🔧 Configuración

### User Secrets Pattern

```csharp
// 1. Definir clase de configuración
public class ApiSettings
{
    public const string SectionName = "ZiurApiSettings";
    public string BaseUrl { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}

// 2. Leer en Program.cs
var apiSettings = builder.Configuration
    .GetSection(ApiSettings.SectionName)
    .Get<ApiSettings>() 
    ?? throw new InvalidOperationException("Settings not configured");

// 3. Registrar HttpClient
builder.Services.AddHttpClient<InventoryService>(client =>
{
    client.BaseAddress = new Uri(apiSettings.BaseUrl);
    client.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", apiSettings.Token);
});
```

---

## 🎨 Patrones de UI

### Componente con Estados (Loading/Error/Success)

```razor
@if (errorMessage is not null)
{
    <div class="error-state">
        <i class="bi bi-exclamation-triangle-fill"></i>
        <p>@errorMessage</p>
        <button @onclick="Retry">Reintentar</button>
    </div>
}
else if (data is null)
{
    <div class="loading-state">
        <div class="loading-spinner"></div>
        <p>Cargando...</p>
    </div>
}
else if (data.Count == 0)
{
    <div class="empty-state">
        <i class="bi bi-inbox"></i>
        <p>No hay datos</p>
    </div>
}
else
{
    <!-- Render data -->
}
```

### SearchBar Pattern

```razor
<input 
    type="text" 
    value="@SearchTerm" 
    @oninput="OnInputChanged"
    placeholder="Buscar..."
/>

@code {
    [Parameter] public string SearchTerm { get; set; } = string.Empty;
    [Parameter] public EventCallback<string> SearchTermChanged { get; set; }
    
    private async Task OnInputChanged(ChangeEventArgs e)
    {
        await SearchTermChanged.InvokeAsync(e.Value?.ToString() ?? string.Empty);
    }
}
```

---

## 🚀 Comandos Útiles

```bash
# Build
dotnet build

# Run
dotnet run --project Ziur

# Restore packages
dotnet restore

# Add package
dotnet add Ziur.Client package PackageName

# User secrets
dotnet user-secrets set "Key:SubKey" "Value" --project Ziur

# Create migration (si usas EF Core)
dotnet ef migrations add MigrationName --project Ziur.Infrastructure

# Clean
dotnet clean
```

---

## ❗ Reglas Importantes

### ✅ HACER

1. **Siempre usar async/await** para operaciones I/O
2. **Inyectar ILogger** en todos los servicios
3. **Usar CancellationToken** en métodos async
4. **Validar null** antes de usar objetos
5. **Usar using statements** para IDisposable
6. **Seguir Clean Architecture**: nunca referenciar capas superiores
7. **Usar record types** para DTOs inmutables (opcional)
8. **Implementar IAsyncDisposable** cuando sea necesario

### ❌ NO HACER

1. **NO usar .Result o .Wait()** (causa deadlocks)
2. **NO exponer detalles de excepción** al usuario
3. **NO hardcodear strings** de configuración
4. **NO mezclar lógica de negocio** en controllers
5. **NO usar tipos anónimos** en APIs públicas
6. **NO ignorar warnings** del compilador
7. **NO sobre-ingenierizar** - mantener simple como BlazorApp2

---

## 📚 Referencias Clave

### Archivos de Referencia (BlazorApp2)

Cuando necesites un ejemplo, consulta estos archivos en BlazorApp2:

- **InventoryService**: `BlazorApp2\Infrastructure\Services\InventoryService.cs`
- **ApiSettings**: `BlazorApp2\Infrastructure\Configuration\ApiSettings.cs`
- **MovementTable**: `BlazorApp2.Client\Features\Inventory\Components\MovementTable.razor`
- **CatalogPage**: `BlazorApp2.Client\Features\Inventory\Pages\CatalogPage.razor`
- **Program.cs (Server)**: `BlazorApp2\Program.cs`

### Librerías Utilizadas

```xml
<!-- Ziur.Client -->
<PackageReference Include="IgniteUI.Blazor" />

<!-- Ziur (Server) -->
<PackageReference Include="Microsoft.AspNetCore.Components.WebAssembly.Server" />

<!-- Ziur.Infrastructure -->
<PackageReference Include="Microsoft.Extensions.Http" />
```

---

## 🎯 Ejemplos de Prompts para IA

### Agregar Nueva Feature

```
"Crea un nuevo servicio en Ziur.Infrastructure llamado ProductService 
que consuma el endpoint /api/productos de la API externa. 
Sigue el mismo patrón que InventoryService con manejo de errores y logging."
```

### Crear Componente

```
"Crea un componente Blazor ProductGrid.razor en Ziur.Client/Features/Products/Components/ 
que muestre una tabla de productos con sorting. 
Debe seguir el mismo patrón que DocumentoGrid.razor."
```

### Agregar Endpoint

```
"Crea un nuevo controller ProductController en Ziur/Controllers/Api/ 
con un endpoint GET /api/product/list que retorne List<ProductDto>. 
Usa ProductService inyectado y sigue el patrón de InventoryController."
```

---

## 🔍 Troubleshooting

### Error: "Unable to resolve service"
**Solución**: Verificar que el servicio esté registrado en `Program.cs`

### Error: "Nullable reference type"
**Solución**: Inicializar con `= string.Empty` o `= new()` o usar `?`

### Build warnings sobre nullability
**Solución**: Usar null-forgiving operator `!` solo cuando estés seguro, o validar con `if (obj is null)`

---

## ✨ Principios de Diseño

1. **KISS** (Keep It Simple, Stupid): Código simple como BlazorApp2
2. **DRY** (Don't Repeat Yourself): Reutilizar componentes
3. **SOLID**: Especialmente Single Responsibility
4. **Clean Architecture**: Dependencias apuntan hacia adentro
5. **Async First**: Todo I/O debe ser asíncrono

---

**Este documento está diseñado para acelerar el desarrollo con IA manteniendo consistencia y calidad del código.**

---

© 2024 Ziur Software - AI Skills Documentation
