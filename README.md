# Ziur - Sistema de Gestión de Inventario

<div align="center">

![Ziur Screenshot](docs/images/catalog-screenshot.png)

Sistema de gestión de inventario desarrollado con **Blazor .NET 10** siguiendo **Clean Architecture**. Interfaz moderna y profesional para la gestión de movimientos de documentos.

[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![Blazor](https://img.shields.io/badge/Blazor-WebAssembly-512BD4?logo=blazor)](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)
[![License](https://img.shields.io/badge/License-Private-red)]()

</div>

---

## 📋 Tabla de Contenidos

- [🚀 Inicio Rápido](#-inicio-rápido)
- [🏗️ Arquitectura](#️-arquitectura)
- [🎨 Características](#-características)
- [📦 Proyectos](#-proyectos)
- [🛠️ Tecnologías](#️-tecnologías)
- [🔐 Configuración](#-configuración)
- [🧪 Testing](#-testing)
- [📚 Documentación](#-documentación)

---

## 🚀 Inicio Rápido

### Prerequisitos

- **.NET 10 SDK** (Visual Studio 2025+ incluye .NET 10)
- **Visual Studio 2025** o VS Code
- **Git**

### Instalación

```bash
# 1. Clonar el repositorio
git clone https://github.com/MsYapy/ziurcolombia.git
cd Ziur

# 2. Configurar user secrets
cd Ziur
dotnet user-secrets init
dotnet user-secrets set "ZiurApiSettings:BaseUrl" "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/"
dotnet user-secrets set "ZiurApiSettings:Token" "ae8bad44-7348-11ee-b962-0242ac120002"

# 3. Restaurar dependencias
cd ..
dotnet restore

# 4. Compilar la solución
dotnet build

# 5. Ejecutar la aplicación
dotnet run --project Ziur
```

La aplicación estará disponible en: **https://localhost:7217**

---

## 🏗️ Arquitectura

El proyecto está organizado siguiendo **Clean Architecture** con 4 capas bien definidas:

```
Ziur/
├── 📦 Ziur.Shared/           # Modelos compartidos, DTOs, contratos
├── 🔧 Ziur.Infrastructure/   # Servicios de infraestructura, APIs externas
├── 🎨 Ziur.Client/           # Frontend (Blazor WebAssembly)
├── ⚙️  Ziur/                  # Backend (ASP.NET Core Server)
└── 🧪 Ziur.Tests/            # Tests unitarios (xUnit + Moq)
```

### Flujo de Datos

```
┌─────────────────┐
│   Ziur API      │  API Externa
│   (REST)        │
└────────┬────────┘
         │
         ▼
┌─────────────────────┐
│ InventoryService    │  Consume API
│ (Infrastructure)    │
└──────────┬──────────┘
           │
           ▼
┌──────────────────────┐
│ InventoryController  │  Endpoint REST
│ (Server)             │
└──────────┬───────────┘
           │
           ▼
┌──────────────────────────┐
│ InventoryServiceClient   │  Cliente HTTP
│ (Client)                 │
└──────────┬───────────────┘
           │
           ▼
┌──────────────────────────┐
│ DocumentoGrid Component  │  UI
│ (Blazor Component)       │
└──────────────────────────┘
```

---

## 🎨 Características

### ✨ Funcionalidades Implementadas

- ✅ **Catálogo de Movimientos**: Visualización profesional de documentos
- 🔍 **Búsqueda en Tiempo Real**: Filtrado instantáneo por código o descripción
- ⬆️⬇️ **Sorting Interactivo**: Ordenamiento por cualquier columna (código, descripción, estado)
- 🎨 **Tema Claro/Oscuro**: Soporte completo de temas con persistencia
- ⚡ **Estados de UI**: Loading skeleton, mensajes de error, estado vacío
- 🛡️ **Error Boundary**: Captura global de errores en componentes
- 📱 **Diseño Responsivo**: Optimizado para desktop, tablet y móvil
- 🔒 **Autenticación Bearer**: Integración segura con API externa
- 🧪 **Tests Unitarios**: Cobertura de controladores con xUnit + Moq

### 🎯 API Endpoints

#### `GET /api/inventory/movements`
Obtiene todos los movimientos del catálogo.

**Response**: `200 OK`
```json
[
  {
    "codigo": 29,
    "descripcion": "Ajuste al Inventario",
    "vActiva": false
  },
  {
    "codigo": 51,
    "descripcion": "Avance Produccion",
    "vActiva": false
  }
]
```

**Error Responses**:
- `400 Bad Request` - Error de la API externa
- `499 Client Closed Request` - Solicitud cancelada
- `500 Internal Server Error` - Error inesperado

---

## 📦 Proyectos

### 📘 Ziur.Shared
**Modelos compartidos entre cliente y servidor**

- `Models/DTOs/MovementDto.cs` - DTO para movimientos de inventario

### 🔧 Ziur.Infrastructure
**Capa de infraestructura para servicios externos**

- `Services/InventoryService.cs` - Servicio para consumir la API de Ziur
- `Interfaces/IInventoryService.cs` - Contrato del servicio (testeable)
- `Configuration/ApiSettings.cs` - Configuración de la API externa

### 🎨 Ziur.Client (Blazor WebAssembly)
**Frontend interactivo con componentes reutilizables**

**Features**:
- `Features/Inventory/Components/DocumentoGrid.razor` - Grid con sorting
- `Features/Inventory/Components/SearchBar.razor` - Búsqueda con filtrado
- `Features/Inventory/Pages/CatalogPage.razor` - Página principal
- `Features/Inventory/Services/InventoryServiceClient.cs` - Cliente HTTP

**Layout**:
- `Shared/Layout/AppHeader.razor` - Header con título y tema
- `Shared/Layout/Sidebar.razor` - Navegación lateral
- `Shared/Layout/ThemeToggle.razor` - Switcher de tema

**Estilos**:
- `wwwroot/css/app.css` - Estilos base y variables CSS
- `wwwroot/css/components.css` - Estilos de componentes
- `wwwroot/css/catalog.css` - Estilos del catálogo
- `wwwroot/css/layout.css` - Estilos del layout

### ⚙️ Ziur (ASP.NET Core Server)
**Backend API con manejo robusto de errores**

- `Controllers/Api/InventoryController.cs` - Endpoint REST
- `Program.cs` - Configuración DI, HttpClient con Bearer token
- `Components/Routes.razor` - Routing con manejo de errores

### 🧪 Ziur.Tests
**Tests unitarios con xUnit y Moq**

- `InventoryControllerTests.cs` - Tests del controlador:
  - ✅ Caso exitoso (200 OK con datos)
  - ✅ Manejo de errores (400 BadRequest)
  - ✅ Cancelación (499)

**Ejecutar tests**:
```bash
dotnet test Ziur.Tests
```

---

## 🛠️ Tecnologías

| Categoría | Tecnología | Versión |
|-----------|-----------|---------|
| **Framework** | .NET | 10.0 |
| **Frontend** | Blazor WebAssembly | 10.0 |
| **Backend** | ASP.NET Core | 10.0 |
| **UI Components** | Ignite UI for Blazor | 24.x |
| **Icons** | Bootstrap Icons | 1.11.x |
| **Testing** | xUnit | 2.9.0 |
| **Mocking** | Moq | 4.20.70 |
| **Architecture** | Clean Architecture | - |

---

## 🔐 Configuración

### 🔧 User Secrets (Desarrollo)

**Recomendado para desarrollo local**:

```bash
cd Ziur
dotnet user-secrets init
dotnet user-secrets set "ZiurApiSettings:BaseUrl" "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/"
dotnet user-secrets set "ZiurApiSettings:Token" "ae8bad44-7348-11ee-b962-0242ac120002"
```

### 🌍 Variables de Entorno (Producción)

**Para despliegue en producción**:

```bash
ZiurApiSettings__BaseUrl=https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/
ZiurApiSettings__Token=ae8bad44-7348-11ee-b962-0242ac120002
```

### ⚙️ appsettings.json (Opcional)

```json
{
  "ZiurApiSettings": {
    "BaseUrl": "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/",
    "Token": "ae8bad44-7348-11ee-b962-0242ac120002"
  }
}
```

> ⚠️ **Advertencia**: Nunca commitear credenciales en el código fuente.

---

## 🧪 Testing

### Ejecutar Tests

```bash
# Ejecutar todos los tests
dotnet test

# Ejecutar tests con verbosidad
dotnet test --verbosity normal

# Ejecutar solo tests del proyecto Ziur.Tests
dotnet test Ziur.Tests

# Ver cobertura de código (requiere coverlet)
dotnet test /p:CollectCoverage=true
```

### Tests Implementados

**InventoryControllerTests.cs**:
1. `GetMovements_ReturnsOkResult_WhenDataExists` ✅
2. `GetMovements_ReturnsBadRequest_WhenApplicationExceptionThrown` ✅
3. `GetMovements_Returns499_WhenRequestCancelled` ✅

**Resultado**: `3 tests, 3 passed, 0 failed`

---

## 📊 Estructura de Branches y PRs

El desarrollo siguió una metodología estructurada con 6 PRs:

| PR | Branch | Descripción |
|----|--------|-------------|
| #1 | `chore/setup-architecture` | Configuración inicial y estructura |
| #2 | `feat/api-integration-and-security` | Integración con API y seguridad |
| #3 | `feat/frontend-grid` | Componentes de UI y grid |
| #4 | `feat/error-handling` | Manejo de errores y ErrorBoundary |
| #5 | `docs/final-delivery` | Documentación final |
| #6 | `fix/bugs-and-testing` | Tests unitarios y correcciones |

---

## 📚 Documentación

- 📖 [ENTREGA.md](ENTREGA.md) - Documento de entrega del proyecto
- 🤖 [AI_SKILLS.md](AI_SKILLS.md) - Guía para IA sobre el proyecto
- 📝 [README.md](README.md) - Este documento

---

## 🤝 Contribuir

Este proyecto sigue **Conventional Commits**:

```
feat:     Nueva funcionalidad
fix:      Corrección de bug
docs:     Cambios en documentación
style:    Cambios de formato (sin afectar lógica)
refactor: Refactorización de código
test:     Agregar o modificar tests
chore:    Tareas de mantenimiento
```

**Ejemplo**:
```bash
git commit -m "feat: add dark mode toggle to header"
git commit -m "fix: resolve sorting issue in DocumentoGrid"
git commit -m "test: add unit tests for InventoryController"
```

---

## 🐛 Problemas Conocidos

Ninguno reportado actualmente. Si encuentras un bug, por favor crea un issue.

---

## 🗺️ Roadmap

- [ ] Agregar paginación al grid
- [ ] Implementar filtros avanzados
- [ ] Agregar exportación a Excel/PDF
- [ ] Integrar autenticación de usuarios
- [ ] Tests de integración
- [ ] CI/CD con GitHub Actions

---

## 📄 Licencia

Este proyecto es **privado y confidencial**. Todos los derechos reservados.

---

## ✨ Desarrollado por

**Ziur Software Colombia**  
Sistema de inventario empresarial con tecnología de punta.

---

<div align="center">

**¿Preguntas o sugerencias?**  
Contacta al equipo de desarrollo.

</div>
