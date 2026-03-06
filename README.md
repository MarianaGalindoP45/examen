# Sistema de Gestión de investario
Este proyecto consiste en un sistema básico de gestión de inventario desarrollado en ASP.NET Core MVC utilizando Entity Framework Core y una base de datos relacional.

El sistema permite administrar productos, categorías y movimientos de inventario (entradas y salidas), así como consultar el stock actual de cada producto.

# Funcionalidades principales

Gestión de Categorías
-Crear categorías
- Visualizar categorías
- Editar categorías
- Eliminar categorías (solo si no tienen productos asociados)

 Gestión de Productos
- Crear productos
- Visualizar productos
- Editar productos
- Eliminar productos
- Asignar categoría a cada producto
- Configurar un stock mínimo

Entradas de Inventario
- Registrar entradas de inventario
- Asociar entradas a productos
- Registrar cantidad, fecha y nota

Salidas de Inventario
- Registrar salidas de inventario
- Descontar productos del stock
- Consulta de Stock

Visualizar el stock actual de cada producto
El stock se calcula mediante:

- Stock = Entradas - Salidas
- Alerta de Stock Bajo

El sistema muestra visualmente los productos que están por debajo de su stock mínimo.

# Requerimientos No Funcionales
1. El sistema debe garantizar la integridad de los datos mediante validaciones como:
- No permitir SKU duplicados.
- No permitir campos obligatorios vacíos.
- No permitir stock negativo en las salidas de inventario.
- Validar que los datos ingresados cumplan con los formatos esperados.
2. Interfaz Usable
3. Persistencia de datos

Preguntas de análisis
1. ¿Cuál sería el prompt que le darías a la IA para generar este módulo?
"Genera un módulo de gestión de inventario en ASP.NET Core MVC usando Entity Framework Core.
El sistema debe permitir administrar productos y categorías, registrar entradas y salidas de inventario y calcular el stock actual de cada producto como la diferencia entre entradas y salidas.
Debe incluir validaciones para evitar SKU duplicados, campos obligatorios vacíos y evitar stock negativo.
También debe permitir visualizar los productos con stock bajo en base a un stock mínimo configurado."

2. Funcionalidades extra que se podrían agregar
- Dashboard con estadísticas de inventario
- Exportación de reportes a Excel o PDF
- Notificaciones automáticas de stock bajo
- Registro de cambios en el inventario

3. Funcionalidades básicas que debe tener el sistema y buenas prácticas 

Las funcionalidades mínimas que debe cumplir el sistema son:

- CRUD de categorías
- CRUD de productos
- Registro de entradas de inventario
- Registro de salidas de inventario
- Cálculo del stock actual
- Visualización de productos con stock bajo

Para garantizar calidad en el desarrollo se aplica
- Separación clara entre controladores, modelos y vistas
- Uso de nombres descriptivos en clases y métodos
- Manejo adecuado de errores y validaciones del modelo
