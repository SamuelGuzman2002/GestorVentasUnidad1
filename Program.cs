using System;
using System.Collections.Generic;
using System.Globalization;

class Program
{
    static List<string> nombres = new List<string>();
    static List<decimal> precios = new List<decimal>();
    static List<int> stocks = new List<int>();
    static List<int> unidadesVendidas = new List<int>();

    static decimal totalVentas = 0;
    static int cantidadVentas = 0;

    static void Main()
    {
        int opcion;

        do
        {
            ImprimirEncabezado("SISTEMA GESTOR DE VENTAS E INVENTARIO (MINI-POS)");

            Console.WriteLine("1. Registrar nuevo producto en inventario");
            Console.WriteLine("2. Consultar inventario completo");
            Console.WriteLine("3. Registrar una venta");
            Console.WriteLine("4. Ver reporte de caja y estadisticas diarias");
            Console.WriteLine("5. Salir");
            Console.WriteLine();

            opcion = LeerEntero("Seleccione una opcion (1-5): ", 1, 5);

            switch (opcion)
            {
                case 1:
                    RegistrarProducto();
                    break;

                case 2:
                    ConsultarInventario();
                    break;

                case 3:
                    RegistrarVenta();
                    break;

                case 4:
                    MostrarReporte();
                    break;

                case 5:
                    Console.WriteLine();
                    Console.WriteLine("Gracias por utilizar el sistema.");
                    break;
            }

        } while (opcion != 5);
    }

    static void RegistrarProducto()
{
    ImprimirEncabezado("REGISTRAR NUEVO PRODUCTO");

    string nombre;

    while (true)
    {
        Console.Write("Ingrese el nombre del producto: ");
        nombre = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("[ERROR] El nombre no puede estar vacio.");
            continue;
        }

        bool repetido = false;

        for (int i = 0; i < nombres.Count; i++)
        {
            if (nombres[i].Equals(nombre, StringComparison.OrdinalIgnoreCase))
            {
                repetido = true;
                break;
            }
        }

        if (repetido)
        {
            Console.WriteLine("[ERROR] Ya existe un producto con ese nombre.");
        }
        else
        {
            break;
        }
    }

    decimal precio = LeerDecimal("Ingrese el precio unitario ($): ", 0.01m);
    int stock = LeerEntero("Ingrese el stock inicial: ", 0, int.MaxValue);

    nombres.Add(nombre);
    precios.Add(precio);
    stocks.Add(stock);
    unidadesVendidas.Add(0);

    Console.WriteLine();
    Console.WriteLine("[OK] Producto registrado correctamente.");
    Console.WriteLine();
    Console.WriteLine("Presione ENTER para continuar...");
    Console.ReadLine();
}

    static void ConsultarInventario()
{
    ImprimirEncabezado("INVENTARIO COMPLETO");

    if (nombres.Count == 0)
    {
        Console.WriteLine("No hay productos registrados en el inventario.");
    }
    else
    {
        Console.WriteLine($"{"ID",-5} {"PRODUCTO",-30} {"PRECIO",-18} {"STOCK",-10}");
        Console.WriteLine(new string('-', 70));

        for (int i = 0; i < nombres.Count; i++)
        {
            string alerta = "";

            if (stocks[i] < 5)
            {
                alerta = "[ALERTA: BAJO STOCK]";
            }

            Console.WriteLine(
                $"{i + 1,-5} {nombres[i],-30} {precios[i],-18:C} {stocks[i],-10} {alerta}"
            );
        }
    }

    Console.WriteLine();
    Console.WriteLine("Presione ENTER para continuar...");
    Console.ReadLine();
}

    static void RegistrarVenta()
{
    ImprimirEncabezado("REGISTRAR VENTA");

    if (nombres.Count == 0)
    {
        Console.WriteLine("No hay productos disponibles para vender.");
        Console.WriteLine();
        Console.WriteLine("Presione ENTER para continuar...");
        Console.ReadLine();
        return;
    }

    for (int i = 0; i < nombres.Count; i++)
    {
        string alerta = "";

        if (stocks[i] < 5)
        {
            alerta = "[ALERTA: BAJO STOCK]";
        }

        Console.WriteLine(
            $"{i + 1}. {nombres[i]} | Precio: {precios[i]:C} | Stock: {stocks[i]} {alerta}"
        );
    }

    Console.WriteLine();

    int productoSeleccionado = LeerEntero(
        "Seleccione el numero del producto a vender: ",
        1,
        nombres.Count
    );

    int indice = productoSeleccionado - 1;

    int cantidad = LeerEntero(
        "Ingrese la cantidad a comprar: ",
        1,
        int.MaxValue
    );

    while (cantidad > stocks[indice])
    {
        Console.WriteLine(
            $"[ERROR] Stock insuficiente. Solo quedan {stocks[indice]} unidades en inventario."
        );

        cantidad = LeerEntero(
            "Ingrese la cantidad a comprar: ",
            1,
            int.MaxValue
        );
    }

    bool tieneDescuento = false;

    while (true)
    {
        Console.Write("¿Aplica descuento de cliente frecuente (10%)? (S/N): ");
        string respuesta = (Console.ReadLine() ?? "").Trim().ToUpper();

        if (respuesta == "S")
        {
            tieneDescuento = true;
            break;
        }
        else if (respuesta == "N")
        {
            tieneDescuento = false;
            break;
        }
        else
        {
            Console.WriteLine("[ERROR] Ingrese solamente S o N.");
        }
    }

    decimal subtotal;
    decimal descuento;
    decimal iva;

    decimal totalPagar = CalcularFactura(
        precios[indice],
        cantidad,
        tieneDescuento,
        out subtotal,
        out descuento,
        out iva
    );

    stocks[indice] -= cantidad;
    unidadesVendidas[indice] += cantidad;

    totalVentas += totalPagar;
    cantidadVentas++;

    ImprimirEncabezado("TICKET DE VENTA");

    Console.WriteLine($"Producto:          {nombres[indice]} (x{cantidad})");
    Console.WriteLine($"Subtotal:          {subtotal:C}");
    Console.WriteLine($"Descuento (10%):  -{descuento:C}");
    Console.WriteLine($"IVA (19%):        +{iva:C}");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine($"TOTAL A PAGAR:     {totalPagar:C}");
    Console.WriteLine("--------------------------------------------------");
    Console.WriteLine();
    Console.WriteLine(
        $"[OK] Venta efectuada con exito. Stock actualizado: {stocks[indice]} unidades."
    );

    Console.WriteLine();
    Console.WriteLine("Presione ENTER para continuar...");
    Console.ReadLine();
}

    static void MostrarReporte()
{
    ImprimirEncabezado("REPORTE DE CAJA Y ESTADISTICAS DIARIAS");

    Console.WriteLine($"Total de ventas realizadas: {cantidadVentas}");
    Console.WriteLine($"Total acumulado ingresado: {totalVentas:C}");

    decimal promedio = 0;

    if (cantidadVentas > 0)
    {
        promedio = totalVentas / cantidadVentas;
    }

    Console.WriteLine($"Promedio de dinero por venta: {promedio:C}");

    if (cantidadVentas > 0)
    {
        int indiceMayorVenta = 0;

        for (int i = 1; i < unidadesVendidas.Count; i++)
        {
            if (unidadesVendidas[i] > unidadesVendidas[indiceMayorVenta])
            {
                indiceMayorVenta = i;
            }
        }

        Console.WriteLine(
            $"Producto con mayor cantidad de unidades vendidas: {nombres[indiceMayorVenta]} ({unidadesVendidas[indiceMayorVenta]} unidades)"
        );
    }
    else
    {
        Console.WriteLine("Producto con mayor cantidad de unidades vendidas: No hay ventas registradas.");
    }

    Console.WriteLine();
    Console.WriteLine("Presione ENTER para continuar...");
    Console.ReadLine();
}

    static int LeerEntero(string mensaje, int min, int max)
    {
        int numero;

        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(Console.ReadLine(), out numero))
            {
                if (numero >= min && numero <= max)
                {
                    return numero;
                }

                Console.WriteLine($"[ERROR] Opcion fuera de rango. Ingrese un valor entre {min} y {max}.");
            }
            else
            {
                Console.WriteLine("[ERROR] Entrada no valida. Debe ingresar un numero entero.");
            }
        }
    }

    static decimal LeerDecimal(string mensaje, decimal min)
    {
        decimal numero;

        while (true)
        {
            Console.Write(mensaje);

            if (decimal.TryParse(Console.ReadLine(), out numero))
            {
                if (numero >= min)
                {
                    return numero;
                }

                Console.WriteLine($"[ERROR] El valor debe ser mayor o igual a {min}.");
            }
            else
            {
                Console.WriteLine("[ERROR] Entrada no valida. Debe ingresar un numero decimal.");
            }
        }
    }

    static decimal CalcularFactura(
        decimal precio,
        int cantidad,
        bool tieneDescuento,
        out decimal subtotal,
        out decimal descuento,
        out decimal iva)
    {
        subtotal = precio * cantidad;
        descuento = tieneDescuento ? subtotal * 0.10m : 0;
        iva = (subtotal - descuento) * 0.19m;

        return subtotal - descuento + iva;
    }

    static void ImprimirEncabezado(string titulo)
    {
        Console.Clear();
        Console.WriteLine("==================================================");
        Console.WriteLine($"     {titulo}");
        Console.WriteLine("==================================================");
    }
}