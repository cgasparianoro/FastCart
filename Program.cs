using System;
using System.Diagnostics;

// 1. Estructura del Proveedor (vive en la Stack)
public struct Proveedor
{
    public int IdProveedor;
    public string NombreCorporativo;
}

// 2. Estructura del Producto (compuesta y vive en la Stack)
public struct Producto
{
    public int SKU;
    public string Nombre;
    public double Precio;
    public int Stock;
    public Proveedor DatosProveedor;
}

// 3. Clase estática con el algoritmo ShellSort
public static class OrdenamientoService
{
    public static void ShellSort(Producto[] catalogo)
    {
        int n = catalogo.Length;
        int gap = 1;

        // Calcular el salto máximo inicial usando la secuencia de Knuth (h = 3h + 1)
        while (gap < n / 3)
        {
            gap = gap * 3 + 1; // 1, 4, 13, 40...
        }

        // Ir reduciendo los saltos hasta llegar a 1
        while (gap >= 1)
        {
            for (int i = gap; i < n; i++)
            {
                Producto temp = catalogo[i];
                int j = i;

                // Desplazar elementos según la regla del comparador
                while (j >= gap && EsMayor(catalogo[j - gap], temp))
                {
                    catalogo[j] = catalogo[j - gap];
                    j -= gap;
                }
                catalogo[j] = temp;
            }
            gap /= 3; // Reducir la brecha (Knuth)
        }
    }

    // Método de comparación (Reglas de negocio de FastCart)
    private static bool EsMayor(Producto a, Producto b)
    {
        // Criterio 1: Precio Descendente (Si el precio de 'a' es menor que 'b', 'a' debe ir después)
        if (a.Precio != b.Precio)
        {
            return a.Precio < b.Precio;
        }

        // Criterio 2: SKU Ascendente (Si empatan en precio, el SKU mayor debe ir después)
        return a.SKU > b.SKU;
    }
}

// 4. Programa principal
public class Program
{
    public static void Main()
    {
        int cantidadProductos = 50; // Mínimo 50 según la especificación
        Producto[] catalogo = GenerarProductosSimulados(cantidadProductos);

        Console.WriteLine("=== ESTADO INICIAL (Primeros 5 productos desordenados) ===");
        MostrarPrimerosCinco(catalogo);

        // Medición de tiempo con Stopwatch
        Stopwatch sw = new Stopwatch();
        
        // Calentamiento rápido (Warm-up) para compilación JIT
        sw.Start();
        sw.Stop();
        sw.Reset();

        // Medición real
        sw.Start();
        OrdenamientoService.ShellSort(catalogo);
        sw.Stop();

        Console.WriteLine("\n=== ESTADO FINAL (Primeros 5 productos ordenados por Precio DESC y SKU ASC) ===");
        MostrarPrimerosCinco(catalogo);

        Console.WriteLine("\n=== EVIDENCIA DE RENDIMIENTO ===");
        Console.WriteLine($"Total de productos procesados: {catalogo.Length}");
        Console.WriteLine($"Tiempo transcurrido (ms)   : {sw.ElapsedMilliseconds} ms");
        Console.WriteLine($"Tiempo transcurrido (µs)   : {sw.Elapsed.TotalMicroseconds:F2} µs");
        Console.WriteLine($"Ticks de hardware          : {sw.ElapsedTicks} ticks");
    }

    // Generador de datos de prueba aleatorios
    private static Producto[] GenerarProductosSimulados(int cantidad)
    {
        Producto[] lista = new Producto[cantidad];
        Random random = new Random(42); // Semilla fija para reproducibilidad

        Proveedor provEjemplo = new Proveedor 
        { 
            IdProveedor = 101, 
            NombreCorporativo = "Logística Global S.A." 
        };

        for (int i = 0; i < cantidad; i++)
        {
            lista[i] = new Producto
            {
                SKU = 1001 + i,
                Nombre = $"Producto_{i + 1}",
                Precio = Math.Round(random.NextDouble() * (9999.99 - 10.0) + 10.0, 2),
                Stock = random.Next(0, 501),
                DatosProveedor = provEjemplo
            };
        }

        // Casos de prueba límite: Forzar precios idénticos para probar desempate por SKU
        lista[5].Precio = 1500.00;
        lista[5].SKU = 9999; // SKU alto

        lista[12].Precio = 1500.00;
        lista[12].SKU = 2000; // SKU medio

        lista[25].Precio = 1500.00;
        lista[25].SKU = 1050; // SKU bajo

        return lista;
    }

    private static void MostrarPrimerosCinco(Producto[] lista)
    {
        for (int i = 0; i < Math.Min(5, lista.Length); i++)
        {
            Console.WriteLine($"[{i + 1}] SKU: {lista[i].SKU} | Precio: ${lista[i].Precio:F2} | Stock: {lista[i].Stock} | Nombre: {lista[i].Nombre}");
        }
    }
}
