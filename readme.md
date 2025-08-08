\# Prueba Técnica Backend .NET 8



Este proyecto es una \*\*API de Banca\*\* desarrollada en \*\*.NET 8\*\* como parte de una prueba técnica.  

Permite la gestión de clientes, cuentas bancarias y transacciones (depósitos y retiros), siguiendo \*\*Clean Architecture\*\*, \*\*principios SOLID\*\*, \*\*inyección de dependencias\*\* y \*\*pruebas unitarias\*\*.



Desarrollado por \*\*María José Martínez Guillén\*\*.



---



\## 🚀 Características



\- Crear perfiles de clientes

\- Crear cuentas bancarias con saldo inicial (> 0)

\- Consultar saldo actual

\- Registrar depósitos y retiros con validación de fondos

\- Obtener historial de transacciones con saldo después de cada operación

\- Base de datos SQLite con \*\*Entity Framework Core\*\*

\- Pruebas unitarias con \*\*xUnit\*\*, \*\*Moq\*\* y \*\*FluentAssertions\*\*

\- Arquitectura escalable y limpia



---



\## 📂 Estructura del Proyecto



```

PruebaTecnica/

&nbsp;├── Api/           # Proyecto principal Web API

&nbsp;├── Application/   # Lógica de negocio y servicios

&nbsp;├── Domain/        # Entidades y contratos de dominio

&nbsp;├── Infrastructure/# Persistencia con EF Core

&nbsp;└── Tests/         # Pruebas unitarias

```



---



\## ⚙️ Configuración del Entorno



\### 1. Clonar el repositorio

```bash

git clone https://github.com/guillen30/PruebaTecnica

cd PruebaTecnica

```



\### 2. Restaurar dependencias

```bash

dotnet restore

```



\### 3. Aplicar migraciones y crear base de datos SQLite

```bash

cd Api

dotnet ef database update --project ../Infrastructure --startup-project .

```



Se generará un archivo `banca.db` en la carpeta raíz de la API.



\### 4. Ejecutar la API

```bash

dotnet run --project Api

```



La API estará disponible en:

```

HTTPS: https://localhost:7201

HTTP : http://localhost:5206

```



Para probarla, abre en el navegador:

```

https://localhost:7201/swagger

```



---



\## 🧪 Ejecutar Pruebas Unitarias

```bash

dotnet test

```



Deberías ver todas las pruebas en \*\*verde ✅\*\*.



---

