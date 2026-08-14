using System;
using FastCart.Fase3.Models;

namespace FastCart.Fase3;

/// <summary>
/// Administra la bitácora de auditoría mediante una lista doblemente enlazada.
/// Permite registrar eventos y recorrer el historial en ambos sentidos.
/// </summary>
public class AuditoriaService
{
    private NodoAuditoria? cabeza;
    private NodoAuditoria? cola;

    /// <summary>
    /// Inicializa una nueva bitácora de auditoría vacía.
    /// </summary>
    public AuditoriaService()
    {
        cabeza = null;
        cola = null;
    }

    /// <summary>
    /// Registra un nuevo movimiento al final de la bitácora de auditoría.
    /// </summary>
    /// <param name="movimiento">
    /// Movimiento que contiene la información de la operación realizada.
    /// </param>
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

    /// <summary>
    /// Imprime en consola el historial de auditoría
    /// en orden cronológico, desde el evento más antiguo
    /// hasta el más reciente.
    /// </summary>
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

    /// <summary>
    /// Imprime en consola el historial de auditoría
    /// en orden inverso, desde el evento más reciente
    /// hasta el más antiguo.
    /// </summary>
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