## Descripción
Este archivo documenta el uso de inteligencia artificial durante el desarrollo del proyecto.

---

## Resolución de errores con llaves foráneas (Foreign Keys)

**Objetivo:**
Solucionar errores relacionados con las relaciones entre tablas en la base de datos.

**Problemas encontrados:**
- Problemas de integridad referencial
- Comportamiento incorrecto al intentar crear registros (la página no respondía como se esperaba)

**Apoyo de la IA:**
- Explicación del funcionamiento de las llaves foráneas en Entity Framework
- Identificación de posibles errores en las relaciones entre modelos

**Solución aplicada:**
- Se corrigieron las relaciones en los modelos
- Se ajustaron las propiedades de navegación
- Se regeneraron migraciones para reflejar correctamente las relaciones

**Nota:**
Para esta sección, no se cuenta con el historial exacto de prompts debido a la pérdida del chat. Sin embargo, el tipo de consulta realizada fue similar a la siguiente:

**Ejemplo de prompt utilizado:**
1. Estoy creando una página web en C# con ASP.NET Core. Tengo un error al momento de crear un producto: la página se queda estática y no muestra ningún mensaje. Adjunto el modelo y el controlador para identificar si existe algún error en la implementación.

---

## Implementación de login con JWT

**Objetivo:**
Implementar un sistema de autenticación utilizando JSON Web Tokens (JWT).

**Apoyo de la IA:**
- Información sobre cómo implementar JWT en un sistema de login
- Explicación de la configuración necesaria en ASP.NET Core
- Orientación para adaptar la implementación a la estructura del proyecto

**Solución aplicada:**
- Se utilizó la información como guía para implementar el login
- Se integró la autenticación con JWT en el proyecto existente
- Se realizaron ajustes para adaptarlo a la estructura y modelos definidos

**Prompts utilizados:**
1. Tengo un proyecto en el que necesito implementar un login con JWT. El proyecto está desarrollado con C# y ASP.NET Core. Necesito ayuda paso a paso para implementar este login, incluyendo configuraciones necesarias y su propósito. También es importante que la solución se adapte a la estructura actual del proyecto (MVC).

## Uso de IA para problemas con Git

**Objetivo:**
Resolver errores al subir cambios al repositorio.

**Problemas encontrados:**
- Error al hacer push por falta de upstream
- Problemas al intentar agregar archivos innecesarios

**Apoyo de la IA:**
- Explicación sobre cómo vincular una rama al repositorio remoto
- Identificación de archivos que no deben subirse (.vs, bin, obj)

**Solución aplicada:**
- Se configuró correctamente la rama remota
- Se creó un archivo `.gitignore`
- Se eliminaron archivos innecesarios del control de versiones
