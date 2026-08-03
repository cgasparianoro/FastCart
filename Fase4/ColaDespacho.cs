using System.Collections.Generic;
using FastCart.Fase2;
using FastCart.Fase3;
using FastCart.Fase3.Models;
using FastCart.Fase4.Models;

namespace FastCart.Fase4;

public class ColaDespacho
{
    private NodoCola? frente;

    private NodoCola? final;

    private int cantidad;

    public ColaDespacho()
    {
        frente = null;
        final = null;
        cantidad = 0;
    }

    public void EncolarPedido(Pedido pedido)
    {
        NodoCola nuevoNodo = new NodoCola(pedido);

        if (final != null)
        {
            final.Siguiente = nuevoNodo;
        }
        else
        {
            frente = nuevoNodo;
        }

        final = nuevoNodo;
        cantidad++;
    }

    public Pedido? DespacharPedido(
        InventarioLista inventario,
        AuditoriaService auditoria)
    {
        if (frente == null)
        {
            return null;
        }

        if (!int.TryParse(frente.Dato.SKU, out int skuNumerico))
        {
            auditoria.RegistrarEvento(new LogMovimiento
            {
                Timestamp = DateTime.UtcNow,
                TipoOperacion = "DESPACHO_FALLIDO",
                SKUAfectado = 0,
                Descripcion = $"El SKU {frente.Dato.SKU} no es válido."
            });

            return null;
        }

        Producto producto;

        try
        {
            producto = inventario.BuscarPorSKU(skuNumerico);
        }
        catch (KeyNotFoundException)
        {
            auditoria.RegistrarEvento(new LogMovimiento
            {
                Timestamp = DateTime.UtcNow,
                TipoOperacion = "DESPACHO_FALLIDO",
                SKUAfectado = skuNumerico,
                Descripcion = $"El SKU {skuNumerico} no existe en el inventario."
            });

            return null;
        }

        if (producto.Stock < frente.Dato.Cantidad)
        {
            auditoria.RegistrarEvento(new LogMovimiento
            {
                Timestamp = DateTime.UtcNow,
                TipoOperacion = "STOCK_INSUFICIENTE",
                SKUAfectado = skuNumerico,
                Descripcion =
                    $"Stock insuficiente. Disponible: {producto.Stock}, " +
                    $"solicitado: {frente.Dato.Cantidad}."
            });

            return null;
        }

        Pedido pedidoDespachado = frente.Dato;

        producto.Stock -= pedidoDespachado.Cantidad;

        frente = frente.Siguiente;

        cantidad--;

        if (frente == null)
        {
            final = null;
        }

        auditoria.RegistrarEvento(new LogMovimiento
        {
            Timestamp = DateTime.UtcNow,
            TipoOperacion = "DESPACHO_EXITOSO",
            SKUAfectado = skuNumerico,
            Descripcion =
                $"Pedido #{pedidoDespachado.IdPedido} despachado. " +
                $"Cliente: {pedidoDespachado.Cliente}. " +
                $"Cantidad: {pedidoDespachado.Cantidad}. " +
                $"Stock restante: {producto.Stock}."
        });

        return pedidoDespachado;
    }

    public bool EstaVacia()
    {
        return frente == null;
    }

    public int TotalEncolados()
    {
        return cantidad;
    }
}