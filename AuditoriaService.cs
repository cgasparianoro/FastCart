using FastCart.Fase3.Models;

namespace FastCart.Fase3;

public class AuditoriaService
{
    private NodoAuditoria? cabeza;

    private NodoAuditoria? cola;

    public AuditoriaService()
    {
        cabeza = null;
        cola = null;
    }

    public void RegistrarEvento(LogMovimiento movimiento)
    {
        NodoAuditoria nuevoNodo = new NodoAuditoria(movimiento);

        nuevoNodo.Siguiente = null;
        nuevoNodo.Anterior = cola;

        if (cola != null)
        {
            cola.Siguiente = nuevoNodo;
        }
        else
        {
            cabeza = nuevoNodo;
        }

        cola = nuevoNodo;
    }

    public void ImprimirHistorialCronologico()
    {
        NodoAuditoria? actual = cabeza;

        while (actual != null)
        {
            Console.WriteLine(
                $"[{actual.Dato.Timestamp:yyyy-MM-dd HH:mm:ss}] " +
                $"{actual.Dato.TipoOperacion} | " +
                $"SKU: {actual.Dato.SKUAfectado} | " +
                $"{actual.Dato.Descripcion}"
            );

            actual = actual.Siguiente;
        }
    }

    public void ImprimirHistorialInverso()
    {
        NodoAuditoria? actual = cola;

        while (actual != null)
        {
            Console.WriteLine(
                $"[{actual.Dato.Timestamp:yyyy-MM-dd HH:mm:ss}] " +
                $"{actual.Dato.TipoOperacion} | " +
                $"SKU: {actual.Dato.SKUAfectado} | " +
                $"{actual.Dato.Descripcion}"
            );

            actual = actual.Anterior;
        }
    }
}