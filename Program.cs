using System;
using System.Collections.Generic;
using FastCart.Fase2;
using FastCart.Fase3;
using FastCart.Fase4;
using FastCart.Fase4.Models;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("==========================================");
        Console.WriteLine(" FASTCART - PROYECTO INTEGRADO");
        Console.WriteLine("==========================================");
        Console.WriteLine();

        // =========================================================
        // CREACIÓN DEL INVENTARIO
        // =========================================================

        InventarioLista inventario = new InventarioLista();

        // Inserción dinámica de 15 productos.
        // Se usa InsertarOrdenado para mantener el orden por precio.

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

        // =========================================================
        // PRUEBAS DE LA FASE 2
        // =========================================================

        Console.WriteLine("1. PRODUCTOS ORDENADOS POR PRECIO");
        Console.WriteLine("------------------------------------------");

        inventario.MostrarProductos();

        Console.WriteLine();
        Console.WriteLine("2. BÚSQUEDA DE UN SKU EXISTENTE");
        Console.WriteLine("------------------------------------------");

        try
        {
            Producto encontrado =
                inventario.BuscarPorSKU(1006);

            Console.WriteLine(
                $"Producto encontrado: {encontrado}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(error.Message);
        }

        Console.WriteLine();
        Console.WriteLine("3. BÚSQUEDA DE UN SKU INEXISTENTE");
        Console.WriteLine("------------------------------------------");

        try
        {
            Producto inexistente =
                inventario.BuscarPorSKU(9999);

            Console.WriteLine(
                $"Producto encontrado: {inexistente}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(
                $"Excepción controlada: {error.Message}");
        }

        Console.WriteLine();
        Console.WriteLine("4. ELIMINACIÓN DEL SKU 1007");
        Console.WriteLine("------------------------------------------");

        inventario.EliminarPorSKU(1007);

        Console.WriteLine(
            "Lista después de eliminar el producto:");

        inventario.MostrarProductos();

        // =========================================================
        // AUDITORÍA DE LAS FASES ANTERIORES
        // =========================================================

        Console.WriteLine();
        Console.WriteLine("5. HISTORIAL CRONOLÓGICO");
        Console.WriteLine("------------------------------------------");

        inventario
            .ObtenerAuditoria()
            .ImprimirHistorialCronologico();

        Console.WriteLine();
        Console.WriteLine("6. HISTORIAL INVERSO");
        Console.WriteLine("------------------------------------------");

        inventario
            .ObtenerAuditoria()
            .ImprimirHistorialInverso();

        // =========================================================
        // CREACIÓN DE LAS HERRAMIENTAS DE LA FASE 4
        // =========================================================

        AuditoriaService auditoria =
            inventario.ObtenerAuditoria();

        ColaDespacho colaDespacho =
            new ColaDespacho();

        PilaDevoluciones pilaDevoluciones =
            new PilaDevoluciones();

        DevolucionService devolucionService =
            new DevolucionService(
                pilaDevoluciones,
                inventario,
                auditoria);

        // =========================================================
        // PRUEBA DE LA COLA DE DESPACHO
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "7. COLA DE DESPACHO - FASE 4");

        Console.WriteLine(
            "------------------------------------------");

        Pedido pedido1 = new Pedido(
            1,
            "1001",
            2,
            "Cliente Uno");

        Pedido pedido2 = new Pedido(
            2,
            "1005",
            3,
            "Cliente Dos");

        colaDespacho.EncolarPedido(pedido1);
        colaDespacho.EncolarPedido(pedido2);

        Console.WriteLine(
            $"Pedidos en la cola: " +
            $"{colaDespacho.TotalEncolados()}");

        Console.WriteLine(
            $"¿La cola está vacía? " +
            $"{colaDespacho.EstaVacia()}");

        Pedido? pedidoDespachado =
            colaDespacho.DespacharPedido(
                inventario,
                auditoria);

        if (pedidoDespachado != null)
        {
            Console.WriteLine(
                $"Pedido despachado correctamente.");

            Console.WriteLine(
                $"ID: {pedidoDespachado.IdPedido}");

            Console.WriteLine(
                $"Cliente: {pedidoDespachado.Cliente}");

            Console.WriteLine(
                $"SKU: {pedidoDespachado.SKU}");

            Console.WriteLine(
                $"Cantidad: {pedidoDespachado.Cantidad}");
        }
        else
        {
            Console.WriteLine(
                "No se pudo despachar el pedido.");
        }

        Console.WriteLine(
            $"Pedidos restantes en la cola: " +
            $"{colaDespacho.TotalEncolados()}");

        // =========================================================
        // MOSTRAR STOCK DESPUÉS DEL DESPACHO
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "8. STOCK DESPUÉS DEL DESPACHO");

        Console.WriteLine(
            "------------------------------------------");

        try
        {
            Producto productoDespachado =
                inventario.BuscarPorSKU(1001);

            Console.WriteLine(
                $"Producto: {productoDespachado.Nombre}");

            Console.WriteLine(
                $"Stock actual: {productoDespachado.Stock}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(error.Message);
        }

        // =========================================================
        // PRUEBA DE LA PILA DE DEVOLUCIONES
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "9. PILA DE DEVOLUCIONES - FASE 4");

        Console.WriteLine(
            "------------------------------------------");

        Devolucion devolucion1 = new Devolucion(
            1,
            "1001",
            1,
            "Cliente Uno",
            "Producto no requerido");

        bool devolucionRegistrada =
            devolucionService
                .RegistrarDevolucion(devolucion1);

        if (devolucionRegistrada)
        {
            Console.WriteLine(
                "Devolución registrada correctamente.");
        }
        else
        {
            Console.WriteLine(
                "No se pudo registrar la devolución.");
        }

        Console.WriteLine(
            $"Devoluciones en la pila: " +
            $"{pilaDevoluciones.TotalApilados()}");

        Console.WriteLine(
            $"¿La pila está vacía? " +
            $"{pilaDevoluciones.EstaVacia()}");

        // =========================================================
        // MOSTRAR STOCK DESPUÉS DE LA DEVOLUCIÓN
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "10. STOCK DESPUÉS DE LA DEVOLUCIÓN");

        Console.WriteLine(
            "------------------------------------------");

        try
        {
            Producto productoDevuelto =
                inventario.BuscarPorSKU(1001);

            Console.WriteLine(
                $"Producto: {productoDevuelto.Nombre}");

            Console.WriteLine(
                $"Stock actual: {productoDevuelto.Stock}");
        }
        catch (KeyNotFoundException error)
        {
            Console.WriteLine(error.Message);
        }

        // =========================================================
        // RETIRAR LA DEVOLUCIÓN DE LA PILA
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "11. DESAPILAR DEVOLUCIÓN");

        Console.WriteLine(
            "------------------------------------------");

        Devolucion? devolucionProcesada =
            pilaDevoluciones.Desapilar();

        if (devolucionProcesada != null)
        {
            Console.WriteLine(
                "Devolución retirada de la pila.");

            Console.WriteLine(
                $"ID: {devolucionProcesada.IdDevolucion}");

            Console.WriteLine(
                $"Cliente: {devolucionProcesada.Cliente}");

            Console.WriteLine(
                $"SKU: {devolucionProcesada.SKU}");

            Console.WriteLine(
                $"Cantidad: {devolucionProcesada.Cantidad}");

            Console.WriteLine(
                $"Motivo: {devolucionProcesada.Motivo}");
        }
        else
        {
            Console.WriteLine(
                "No había devoluciones en la pila.");
        }

        Console.WriteLine(
            $"Devoluciones restantes: " +
            $"{pilaDevoluciones.TotalApilados()}");

        // =========================================================
        // AUDITORÍA FINAL
        // =========================================================

        Console.WriteLine();
        Console.WriteLine(
            "12. HISTORIAL FINAL DE AUDITORÍA");

        Console.WriteLine(
            "------------------------------------------");

        auditoria.ImprimirHistorialCronologico();

        Console.WriteLine();
        Console.WriteLine("==========================================");
        Console.WriteLine(
            " PRUEBA FINALIZADA CORRECTAMENTE");
        Console.WriteLine("==========================================");
    }
}