using System;
using System.Collections.Generic;
using FastCart.Fase3;
using FastCart.Fase3.Models;

namespace FastCart.Fase2;

/// <summary>
/// Administra el catálogo de productos mediante una lista simplemente enlazada.
/// Permite insertar, buscar, eliminar y mostrar productos,
/// además de registrar las operaciones en la bitácora de auditoría.
/// </summary>
public class InventarioLista
{
    private NodoProducto? cabeza;
    private readonly AuditoriaService auditoria = new AuditoriaService();

    /// <summary>
    /// Inserta un producto al inicio de la lista.
    /// </summary>
    /// <param name="producto">Producto que se insertará en el inventario.</param>
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
    /// Inserta un producto manteniendo la lista ordenada
    /// de forma ascendente según el precio.
    /// </summary>
    /// <param name="producto">Producto que se insertará en el inventario.</param>
    public void InsertarOrdenado(Producto producto)
    {
        NodoProducto nuevo = new NodoProducto(producto);

        if (cabeza == null || producto.Precio < cabeza.Data.Precio)
        {
            nuevo.Siguiente = cabeza;
            cabeza = nuevo;
        }
        else
        {
            NodoProducto actual = cabeza;

            while (actual.Siguiente != null &&
                   actual.Siguiente.Data.Precio <= producto.Precio)
            {
                actual = actual.Siguiente;
            }

            nuevo.Siguiente = actual.Siguiente;
            actual.Siguiente = nuevo;
        }

        auditoria.RegistrarEvento(new LogMovimiento
        {
            Timestamp = DateTime.UtcNow,
            TipoOperacion = "INSERTAR_ORDENADO",
            SKUAfectado = producto.SKU,
            Descripcion = $"Producto {producto.Nombre} insertado ordenadamente."
        });
    }

    /// <summary>
    /// Busca un producto dentro del inventario mediante su SKU.
    /// </summary>
    /// <param name="sku">SKU del producto que se desea buscar.</param>
    /// <returns>El producto correspondiente al SKU indicado.</returns>
    /// <exception cref="KeyNotFoundException">
    /// Se produce cuando no existe un producto con el SKU especificado.
    /// </exception>
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
    /// Elimina del inventario el producto correspondiente al SKU indicado.
    /// Si el producto no existe, la lista permanece sin cambios.
    /// </summary>
    /// <param name="sku">SKU del producto que se desea eliminar.</param>
    public void EliminarPorSKU(int sku)
    {
        if (cabeza == null)
        {
            return;
        }

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
    /// Muestra en la consola todos los productos almacenados
    /// actualmente en el inventario.
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

    /// <summary>
    /// Obtiene el servicio de auditoría asociado al inventario.
    /// </summary>
    /// <returns>Servicio que contiene la bitácora de movimientos.</returns>
    public AuditoriaService ObtenerAuditoria()
    {
        return auditoria;
    }
}