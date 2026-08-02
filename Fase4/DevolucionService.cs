using System.Collections.Generic;
using FastCart.Fase2;
using FastCart.Fase3;
using FastCart.Fase3.Models;
using FastCart.Fase4.Models;

namespace FastCart.Fase4;

public class DevolucionService
{
    private readonly PilaDevoluciones pila;

    private readonly InventarioLista inventario;

    private readonly AuditoriaService auditoria;

    public DevolucionService(
        PilaDevoluciones pila,
        InventarioLista inventario,
        AuditoriaService auditoria)
    {
        this.pila = pila;
        this.inventario = inventario;
        this.auditoria = auditoria;
    }

    public bool RegistrarDevolucion(Devolucion devolucion)
    {
        if (devolucion == null)
        {
            return false;
        }

        if (devolucion.Cantidad <= 0)
        {
            return false;
        }

        if (!int.TryParse(devolucion.SKU, out int sku))
        {
            return false;
        }

        Producto producto;

        try
        {
            producto = inventario.BuscarPorSKU(sku);
        }
        catch (KeyNotFoundException)
        {
            return false;
        }

        producto.Stock += devolucion.Cantidad;

        pila.Apilar(devolucion);

        auditoria.RegistrarEvento(new LogMovimiento
        {
            Timestamp = DateTime.UtcNow,
            TipoOperacion = "DEVOLUCION",
            SKUAfectado = sku,
            Descripcion =
                $"Se devolvieron {devolucion.Cantidad} unidades " +
                $"del producto {sku}."
        });

        return true;
    }
}