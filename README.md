# Ziur - Sistema de Gestión de Inventario

Sistema de gestión de inventario desarrollado con Blazor .NET 10 siguiendo Clean Architecture. Replica la arquitectura de BlazorApp2 con mejores prácticas y código optimizado.

---

## 🏗️ Arquitectura

El proyecto está organizado en 4 capas siguiendo Clean Architecture:

```
Ziur/
├── Ziur.Shared/           # Modelos compartidos, DTOs, interfaces
├── Ziur.Infrastructure/   # Servicios de infraestructura, APIs externas
├── Ziur.Client/           # Frontend (Blazor WebAssembly)
└── Ziur/                  # Backend (ASP.NET Core Server)
```

### Flujo de Datos

```
[Ziur API Externa] 
      ↓
[InventoryService] → [InventoryController] → [InventoryServiceClient]
                                                      ↓
                                              [DocumentoGrid Component]
```

---

## 🚀 Comenzar

### Prerequisitos

- .NET 10 SDK
- Visual Studio 2022+ o VS Code
- Git

### Instalación y Configuración

```bash
# 1. Clonar el repositorio
git clone [URL_DEL_REPO]
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

La aplicación estará disponible en: `https://localhost:7164` (o el puerto configurado)

---

## 📦 Proyectos

### Ziur.Shared
**Propósito**: Modelos compartidos entre cliente y servidor

**Contenido**:
- `Models/DTOs/MovementDto.cs`: DTO para movimientos de inventario

### Ziur.Infrastructure
**Propósito**: Capa de infraestructura para servicios externos

**Contenido**:
- `Services/InventoryService.cs`: Servicio para consumir la API de Ziur
- `Configuration/ApiSettings.cs`: Configuración de la API externa

### Ziur.Client (Blazor WebAssembly)
**Propósito**: Frontend interactivo

**Contenido**:
- `Features/Inventory/Components/DocumentoGrid.razor`: Grid con sorting de movimientos
- `Features/Inventory/Components/SearchBar.razor`: Barra de búsqueda con filtrado
- `Features/Inventory/Pages/CatalogPage.razor`: Página principal del catálogo
- `Features/Inventory/Services/InventoryServiceClient.cs`: Cliente proxy para el servidor
- `wwwroot/css/`: Estilos personalizados (app.css, components.css, catalog.css)

### Ziur (ASP.NET Core Server)
**Propósito**: Backend API

**Contenido**:
- `Controllers/Api/InventoryController.cs`: Endpoint `/api/inventory/movements`
- `Program.cs`: Configuración de servicios, HttpClient con Bearer token
- `Components/Routes.razor`: Routing con ErrorBoundary

---

## 🛠️ Tecnologías

- **.NET 10** - Framework principal
- **Blazor WebAssembly** - Frontend SPA
- **ASP.NET Core** - Backend API
- **Ignite UI for Blazor** - Componentes UI avanzados
- **Bootstrap Icons** - Iconografía
- **Clean Architecture** - Patrón arquitectónico

---

## 🔐 Configuración de Seguridad

### User Secrets (Desarrollo)

Las credenciales se almacenan en user secrets:

```bash
dotnet user-secrets set "ZiurApiSettings:BaseUrl" "https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/"
dotnet user-secrets set "ZiurApiSettings:Token" "ae8bad44-7348-11ee-b962-0242ac120002"
```

### Variables de Entorno (Producción)

En producción, configurar:

```bash
ZiurApiSettings__BaseUrl=https://mainserver.ziursoftware.com/Ziur.API/basedatos_01/ZiurServiceRest.svc/api/
ZiurApiSettings__Token=ae8bad44-7348-11ee-b962-0242ac120002
```

---

## 🎨 Características

### ✅ Implementadas

- 📊 **Catálogo de Movimientos**: Visualización de documentos con grid profesional
- 🔍 **Búsqueda en Tiempo Real**: Filtrado por código o descripción
- ⬆️⬇️ **Sorting Interactivo**: Ordenamiento por cualquier columna
- 🎨 **Tema Claro/Oscuro**: Soporte completo de temas
- ⚡ **Manejo de Errores**: Estados de loading, error y empty
- 🛡️ **ErrorBoundary**: Captura de errores global
- 📱 **Diseño Responsivo**: Optimizado para móviles y tablets
- 🔒 **Autenticación Bearer**: Integración segura con API externa

### 🎯 Endpoints API

#### GET `/api/inventory/movements`
Obtiene todos los movimientos del catálogo.

**Response**: `200 OK`
```json
[
  {
    "codigo": 1,
    "descripcion": "Entrada de mercancía",
    "vActiva": true
  }
]
```

---

## 📊 Estructura de PRs

El desarrollo siguió una estructura de 5 PRs:

1. **PR #1**: `chore/setup-architecture` - Configuración inicial
2. **PR #2**: `feat/api-integration-and-security` - Integración con API
3. **PR #3**: `feat/frontend-grid` - Componentes de UI
4. **PR #4**: `feat/error-handling` - Manejo de errores
5. **PR #5**: `docs/final-delivery` - Documentación final

---

## 🧪 Testing

```bash
# Ejecutar build
dotnet build

# Ejecutar aplicación en desarrollo
dotnet run --project Ziur
```

---

## 📚 Documentación Adicional

- [ENTREGA.md](ENTREGA.md) - Documento de entrega del proyecto
- [AI_SKILLS.md](AI_SKILLS.md) - Guía para IA sobre el proyecto

---

## 🤝 Contribuir

Este proyecto sigue **Conventional Commits**:

```bash
feat: nueva funcionalidad
fix: corrección de bug
docs: cambios en documentación
style: cambios de formato
refactor: refactorización de código
test: agregar o modificar tests
chore: tareas de mantenimiento
```

---

## 📄 Licencia

Este proyecto es privado y confidencial.

---

## ✨ Desarrollado por

**Ziur Software** - Sistema de inventario empresarial


