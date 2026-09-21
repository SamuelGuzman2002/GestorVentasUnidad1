# Sistema Gestor de Ventas e Inventario Express (Mini-POS)

## Estudiante

Samuel Alfonso Guzmán Acevedo

## Descripción

Proyecto desarrollado en C# con .NET 8 como reto final de la Unidad 1.

La aplicación consiste en un sistema de consola para gestionar el inventario y las ventas de una tienda. Permite registrar productos, consultar el inventario disponible, realizar ventas y visualizar un reporte de caja con estadísticas de la sesión.

## Funcionalidades

1. Registrar nuevos productos en el inventario.
2. Consultar el inventario completo.
3. Mostrar alertas cuando un producto tiene bajo stock.
4. Registrar ventas y validar la cantidad disponible.
5. Aplicar descuento del 10% para clientes frecuentes.
6. Calcular IVA del 19%.
7. Generar un ticket de venta.
8. Actualizar automáticamente el stock después de una venta.
9. Consultar el total de ventas realizadas.
10. Consultar el dinero acumulado durante la sesión.
11. Calcular el promedio de dinero por venta.
12. Mostrar el producto con mayor cantidad de unidades vendidas.

## Tecnologías utilizadas

- C#
- .NET 8
- Aplicación de consola
- Visual Studio Code

## Requisitos

Para ejecutar el proyecto es necesario tener instalado el SDK de .NET 8.

Se puede verificar la instalación utilizando:

```bash
dotnet --version
```

## Cómo clonar y ejecutar el proyecto

Clonar el repositorio:

```bash
git clone https://github.com/SamuelGuzman2002/GestorVentasUnidad1.git
```

Ingresar a la carpeta:

```bash
cd GestorVentasUnidad1
```

Restaurar las dependencias:

```bash
dotnet restore
```

Compilar el proyecto:

```bash
dotnet build
```

Ejecutar la aplicación:

```bash
dotnet run
```

## Menú principal

```text
==================================================
     SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)
==================================================
1. Registrar nuevo producto en inventario
2. Consultar inventario completo
3. Registrar una venta
4. Ver reporte de caja y estadisticas diarias
5. Salir
==================================================
```

## Ejemplo de venta

Ejemplo para un producto con precio de $18.000, una venta de 2 unidades y descuento de cliente frecuente:

```text
Producto:          Cafe Colombiano 500g (x2)
Subtotal:          $36.000
Descuento (10%):   $3.600
IVA (19%):         $6.156
TOTAL A PAGAR:     $38.556
```

## Estructura del proyecto

```text
GestorVentasUnidad1/
├── .gitignore
├── GestorVentasUnidad1.csproj
├── Program.cs
└── README.md
```

Las carpetas `bin/` y `obj/` son generadas automáticamente por .NET y están excluidas del repositorio mediante `.gitignore`.

## Estado del proyecto

El proyecto fue compilado y probado correctamente utilizando:

```bash
dotnet build
```

Resultado:

```text
0 Advertencias
0 Errores
```