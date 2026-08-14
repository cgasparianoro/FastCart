using System;
using System.Collections.Generic;
using FastCart.Fase2;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" FASTCART - CATÁLOGO MAESTRO - FASE 3");
        Console.WriteLine("==========================================");
        Console.WriteLine();

        InventarioLista inventario = new InventarioLista();

        // =====================================================
        // INSERCIÓN DINÁMICA DE 15 PRODUCTOS
        // =====================================================

        inventario.InsertarOrdenado(
            new Producto(1001, "Arroz", 45.50m, 30));

        inventario.InsertarOrdenado(
            new Producto(1002, "Leche", 32.75m, 25));

        inventario.InsertarOrdenado(
            new Producto(1003, "Pan", 20.00m, 40));

        inventario.InsertarOrdenado(
            new Producto(1004, "Aceite", 85.90m, 18));

        inventario.InsertarOrdenado(
            new Producto(1005, "Azúcar", 38.25m, 22));

        inventario.InsertarOrdenado(
            new Producto(1006, "Café", 120.00m, 15));

        inventario.InsertarOrdenado(
            new Producto(1007, "Huevos", 75.00m, 36));

        inventario.InsertarOrdenado(
            new Producto(1008, "Harina", 27.50m, 20));

        inventario.InsertarOrdenado(
            new Producto(1009, "Sal", 15.00m, 50));

        inventario.InsertarOrdenado(
            new Producto(1010, "Pasta", 42.00m, 28));

        inventario.InsertarOrdenado(
            new Producto(1011, "Jugo", 55.75m, 17));

        inventario.InsertarOrdenado(
            new Producto(1012, "Galletas", 30.00m, 34));

        inventario.InsertarOrdenado(
            new Producto(1013, "Cereal", 95.50m, 12));

        inventario.InsertarOrdenado(
            new Producto(1014, "Agua", 15.00m, 60));

        inventario.InsertarOrdenado(
            new Producto(1015, "Chocolate", 65.25m, 19));

        // =====================================================
        // 1. MOSTRAR PRODUCTOS
        // =====================================================

        Console.WriteLine("1. PRODUCTOS ORDENADOS POR PRECIO");
        Console.WriteLine("------------------------------------------");

        inventario.MostrarProductos();

        // =====================================================
        // 2. BÚSQUEDA EXITOSA
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("2. BÚSQUEDA DE UN SKU EXISTENTE");
        Console.WriteLine("------------------------------------------");

        try
        {
            Producto encontrado = inventario.BuscarPorSKU(1006);
            Console.WriteLine($"Producto encontrado: {encontrado}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(error.Message);
        }

        // =====================================================
        // 3. BÚSQUEDA FALLIDA CONTROLADA
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("3. BÚSQUEDA DE UN SKU INEXISTENTE");
        Console.WriteLine("------------------------------------------");

        try
        {
            Producto inexistente = inventario.BuscarPorSKU(9999);
            Console.WriteLine($"Producto encontrado: {inexistente}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine($"Excepción controlada: {error.Message}");
        }

        // =====================================================
        // 4. ELIMINACIÓN
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("4. ELIMINACIÓN DEL SKU 1007");
        Console.WriteLine("------------------------------------------");

        inventario.EliminarPorSKU(1007);

        Console.WriteLine("Lista después de eliminar el producto:");
        inventario.MostrarProductos();

        // =====================================================
        // 5. HISTORIAL CRONOLÓGICO
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("5. HISTORIAL CRONOLÓGICO");
        Console.WriteLine("==========================================");

        inventario.ObtenerAuditoria()
                  .ImprimirHistorialCronologico();

        // =====================================================
        // 6. HISTORIAL INVERSO
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("6. HISTORIAL INVERSO");
        Console.WriteLine("==========================================");

        inventario.ObtenerAuditoria()
                  .ImprimirHistorialInverso();

        // =====================================================
        // FINAL
        // =====================================================

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine("Prueba finalizada correctamente.");
        Console.WriteLine("==========================================");
    }
}