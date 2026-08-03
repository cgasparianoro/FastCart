using System.Collections.Generic;
using FastCart.Fase3;
using FastCart.Fase3.Models;
namespace FastCart.Fase2;

/// <summary>
/// Administra el catálogo mediante una lista simplemente enlazada.
/// </summary>
public class InventarioLista
{
    private NodoProducto? cabeza;
    private AuditoriaService auditoria = new AuditoriaService();

    /// <summary>
    /// Inserta un producto al inicio.
    /// </summary>
    public void InsertarInicio(Producto producto)
    {
        NodoProducto nuevo = new NodoProducto(producto);

        nuevo.Siguiente = cabeza;

        cabeza = nuevo;
        auditoria.RegistrarEvento(new LogMovimiento
        {
            Timestamp = DateTime.UtcNow,
            TipoOperacion = "INSERTAR_INICIO",
            SKUAfectado = producto.SKU,
            Descripcion = $"Producto {producto.Nombre} insertado al inicio."
        });
    }

    /// <summary>
    /// Inserta un producto ordenado por precio ascendente.
    /// </summary>
    public void InsertarOrdenado(Producto producto)
    {
        NodoProducto nuevo = new NodoProducto(producto);

        if (cabeza == null || producto.Precio < cabeza.Data.Precio)
        {
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
            return;
        }

        NodoProducto actual = cabeza;

        while (actual.Siguiente != null &&
               actual.Siguiente.Data.Precio <= producto.Precio)
        {
            actual = actual.Siguiente;
        }

        nuevo.Siguiente = actual.Siguiente;
        actual.Siguiente = nuevo;
        auditoria.RegistrarEvento(new LogMovimiento
        {
                Timestamp = DateTime.UtcNow,
    TipoOperacion = "INSERTAR_ORDENADO",
    SKUAfectado = producto.SKU,
    Descripcion = $"Producto {producto.Nombre} insertado ordenadamente."
});
        }
    /// <summary>
    /// Busca un producto por SKU.
    /// </summary>
    public Producto BuscarPorSKU(int sku)
    {
        NodoProducto? actual = cabeza;

        while (actual != null)
        {
            if (actual.Data.SKU == sku)
            {
                auditoria.RegistrarEvento(new LogMovimiento
{
    Timestamp = DateTime.UtcNow,
    TipoOperacion = "BUSQUEDA",
    SKUAfectado = sku,
    Descripcion = $"Se consultó el producto {actual.Data.Nombre}."
});
                return actual.Data;
            }

            actual = actual.Siguiente;
        }

        throw new KeyNotFoundException($"SKU {sku} no encontrado.");
    }

    /// <summary>
    /// Elimina un producto por SKU.
    /// </summary>
    public void EliminarPorSKU(int sku)
    {
        if (cabeza == null)
            return;

        if (cabeza.Data.SKU == sku)
        {
            cabeza = cabeza.Siguiente;
            auditoria.RegistrarEvento(new LogMovimiento
{
    Timestamp = DateTime.UtcNow,
    TipoOperacion = "ELIMINAR",
    SKUAfectado = sku,
    Descripcion = $"Producto con SKU {sku} eliminado."
});
            return;
        }

        NodoProducto actual = cabeza;

        while (actual.Siguiente != null)
        {
            if (actual.Siguiente.Data.SKU == sku)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                auditoria.RegistrarEvento(new LogMovimiento
{
    Timestamp = DateTime.UtcNow,
    TipoOperacion = "ELIMINAR",
    SKUAfectado = sku,
    Descripcion = $"Producto con SKU {sku} eliminado."
});
                return;
            }

            actual = actual.Siguiente;
        }
    }

    /// <summary>
    /// Muestra todos los productos.
    /// </summary>
    public void MostrarProductos()
    {
        NodoProducto? actual = cabeza;

        while (actual != null)
        {
            Console.WriteLine(actual.Data);

            actual = actual.Siguiente;
        }
    }
    public AuditoriaService ObtenerAuditoria()
{
    return auditoria;
}
}