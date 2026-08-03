namespace FastCart.Fase3.Models;

public class NodoAuditoria
{
    // La carga útil: el registro del evento.
    public LogMovimiento Dato;

    // Referencia al nodo más reciente, hacia la Cola.
    public NodoAuditoria? Siguiente;

    // Referencia al nodo más antiguo, hacia la Cabeza.
    public NodoAuditoria? Anterior;

    public NodoAuditoria(LogMovimiento dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}