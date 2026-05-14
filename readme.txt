# Sistema de Encuestas - ACME

Aplicación web desarrollada en ASP.NET MVC5 que permite a los usuarios crear, administrar y responder encuestas mediante enlaces únicos.

---

## Funcionalidades

* Registro de usuarios
* Inicio de sesión seguro (contraseñas encriptadas con SHA-256)
* Control de acceso mediante autorización

### Gestión de Encuestas

* Crear encuestas dinámicas
* Editar nombre y descripción
* Eliminar encuestas
* Visualizar resultados

### Enlaces Públicos

* Generación automática de un **link único por encuesta**
* Cualquier usuario puede responder sin iniciar sesión

### Respuestas

* Soporte para distintos tipos de preguntas:

  * Texto
  * Número
  * Fecha
* Almacenamiento estructurado de respuestas

---
## Tecnologías Utilizadas

* ASP.NET MVC (.NET Framework)
* SQL Server
* C#
* HTML, CSS, JavaScript
* AngularJS (para formularios dinámicos)
---

## Requisitos

* Visual Studio 2019 o superior
* SQL Server
* .NET Framework (versión del proyecto)

---

## Instalación y Configuración

### Clonar el repositorio

```bash
git clone https://github.com/Leop171/WebApplication2.git
```

---

### 2️⃣ Configurar la Base de Datos

1. Abrir SQL Server
2. Ejecutar el script:

```
/Database/acmeDB
```

Esto creará:
* Base de datos `DBACME`
* Tablas necesarias

---

### Configurar cadena de conexión

En el archivo `Web.config`:

```xml
<connectionStrings>
  <add name="DBACME"
       connectionString="Server=.;Database=DBACME;Trusted_Connection=True;"
       providerName="System.Data.SqlClient" />
</connectionStrings>
```
o configurar según el tipo de conexión establecida con SQL Server

---

### Ejecutar el proyecto

* Abrir la solución en Visual Studio
* Ejecutar con IIS Express

---

## Uso del Sistema

###  Usuario autenticado puede:

* Crear encuestas
* Editarlas
* Eliminarlas
* Ver resultados

###  Usuario sin sesión:

* Puede responder encuestas usando el enlace generado

---

## Seguridad

* Contraseñas protegidas mediante hashing con SHA-256
* Uso de tokens para manejo de sesión
* Control de acceso con `[Authorize]`

---

##  Estructura del Proyecto

```
/Controllers
/Models
/Views
/Content
/Scripts
/Database
    acmeDB.sql
README.md
```

---

## Consideraciones

* Asegúrate de ejecutar el script SQL antes de iniciar
* Configura correctamente la cadena de conexión
* El puerto de localhost puede variar según tu entorno

---
