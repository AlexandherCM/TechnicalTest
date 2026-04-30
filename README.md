# TechnicalTest - ASP.NET MVC 5

Aplicación web desarrollada en **ASP.NET Framework 4.7 + ASP.NET MVC 5** como prueba técnica.

## Configuración de la cadena de conexión

Ir al archivo:

```txt
TechnicalTest/Web.config

Ubicar el siguiente nodo: = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
<connectionStrings>
	<add name="Context" connectionString="data source=ALEX-PC\ALEXSERVER2026;initial catalog=TechnicalTest;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>

= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

Si se usará autenticación de Windows

Solo reemplazar el valor de data source por el servidor o instancia SQL correspondiente.

Ejemplo:
<connectionStrings>
	<add name="Context" connectionString="data source=TU_SERVIDOR_SQL;initial catalog=TechnicalTest;integrated security=True;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>

Ejemplos de data source válidos:

localhost
.\SQLEXPRESS
(localdb)\MSSQLLocalDB
MI-PC\SQLEXPRESS

= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 
Si se usará autenticación de SQL Server con usuario y contraseña

Copiar y reemplazar el nodo por este:

<connectionStrings>
	<add name="Context" connectionString="data source=TU_SERVIDOR_SQL;initial catalog=TechnicalTest;user id=TU_USUARIO;password=TU_PASSWORD;trustservercertificate=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
</connectionStrings>

Reemplazar:

TU_SERVIDOR_SQL
TU_USUARIO
TU_PASSWORD

= = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = = 

La solución implementa:

- Consulta de información por Código Postal consumiendo un servicio externo.
- Procesamiento de información de persona.
- Normalización de nombre completo.
- Detección de preposiciones en nombres.
- Generación de identificador de usuario.
- Registro y consulta de personas usando Entity Framework.
- Manejo de alertas visuales con SweetAlert.
- Separación por capas y uso de inyección de dependencias.

---

## Estructura de la solución

La solución está dividida en 3 proyectos principales:

```txt
TechnicalTest/
│
├── App
│   ├── DTOs
│   ├── Interfaces
│   ├── Presenters
│   ├── Services
│   └── ViewModels
│
├── Domain
│   ├── Persistence
│   ├── PersonaEntity.cs
│   └── PostalCodeXmlResponseEntity.cs
│
└── TechnicalTest
    ├── App_Start
    ├── Controllers
    ├── InterfaceAdapter
    ├── Migrations
    ├── Models
    ├── Scripts
    ├── Views
    ├── Global.asax
    └── Web.config