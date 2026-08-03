namespace FastCart.Fase4.Models;

/// <summary>
/// Representa un nodo de la cola dinámica de despacho.
/// </summary>
public class NodoCola
{
    public Pedido Dato { get; set; }

    public NodoCola? Siguiente { get; set; }

    public NodoCola(Pedido pedido)
    {
        Dato = pedido;
        Siguiente = null;
    }
}