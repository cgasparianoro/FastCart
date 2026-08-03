namespace FastCart.Fase4.Models;

/// <summary>
/// Representa un pedido que será colocado en la cola de despacho.
/// </summary>
public class Pedido
{
    public int IdPedido { get; set; }

    public string SKU { get; set; }

    public int Cantidad { get; set; }

    public string Cliente { get; set; }

    public DateTime Timestamp { get; set; }

    public Pedido(int id, string sku, int cantidad, string cliente)
    {
        IdPedido = id;
        SKU = sku;
        Cantidad = cantidad;
        Cliente = cliente;
        Timestamp = DateTime.Now;
    }
}