# Ziur - Sistema de Gestión de Inventario

Sistema de gestión de inventario desarrollado con Blazor .NET 10 siguiendo Clean Architecture.

---

## 🏗️ Arquitectura

El proyecto está organizado en 4 capas:

```
Ziur/
├── Ziur.Shared/           # Modelos compartidos, DTOs, interfaces
├── Ziur.Infrastructure/   # Servicios de infraestructura, APIs externas
├── Ziur.Client/           # Frontend (Blazor WebAssembly)
└── Ziur/                  # Backend (ASP.NET Core API)
```

---

## 🚀 Comenzar

### Prerequisitos

- .NET 10 SDK
- Visual Studio 2022+ o VS Code
- Git

### Instalación

```bash
# Clonar el repositorio
git clone [URL_DEL_REPO]
cd Ziur

# Restaurar dependencias
dotnet restore

# Compilar la solución
dotnet build

# Ejecutar la aplicación
dotnet run --project Ziur
```

La aplicación estará disponible en: `https://localhost:5001`

---

## 📦 Proyectos

### Ziur.Shared
Contiene los modelos compartidos, DTOs e interfaces que son usados tanto por el cliente como por el servidor.

### Ziur.Infrastructure
Capa de infraestructura que contiene servicios para comunicarse con APIs externas y otros recursos.

### Ziur.Client
Frontend desarrollado con Blazor WebAssembly. Contiene la interfaz de usuario y los componentes visuales.

### Ziur (Server)
Backend desarrollado con ASP.NET Core. Expone APIs REST para el cliente.

---

## 🛠️ Tecnologías

- **.NET 10**
- **Blazor WebAssembly**
- **ASP.NET Core**
- **Ignite UI for Blazor**

---

## 📝 Configuración

### User Secrets (Desarrollo)

Para configurar las credenciales de la API externa en desarrollo:

```bash
cd Ziur
dotnet user-secrets init
dotnet user-secrets set "ZiurApi:BaseUrl" "https://api.example.com"
dotnet user-secrets set "ZiurApi:Token" "your-token-here"
```

### Variables de Entorno (Producción)

En producción, configurar las siguientes variables de entorno:

- `ZiurApi__BaseUrl`: URL base de la API de Ziur
- `ZiurApi__Token`: Token de autenticación Bearer

---

## 🧪 Testing

```bash
# Ejecutar tests (cuando estén implementados)
dotnet test
```

---

## 📚 Documentación

La documentación adicional se encuentra en la carpeta `docs/`:

- [Arquitectura](docs/arquitectura.md)
- [Guía de Desarrollo](docs/desarrollo.md)

---

## 🤝 Contribuir

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'feat: add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

---

## 📄 Licencia

Este proyecto es privado y confidencial.

---

## ✨ Autor

Desarrollado como parte de una prueba técnica.
