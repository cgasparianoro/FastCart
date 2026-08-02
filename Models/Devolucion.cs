namespace FastCart.Fase4.Models;

public class Devolucion
{
    public int IdDevolucion { get; set; }

    public string SKU { get; set; }

    public int Cantidad { get; set; }

    public string Cliente { get; set; }

    public string Motivo { get; set; }

    public DateTime Timestamp { get; set; }

    public Devolucion(
        int id,
        string sku,
        int cantidad,
        string cliente,
        string motivo)
    {
        IdDevolucion = id;
        SKU = sku;
        Cantidad = cantidad;
        Cliente = cliente;
        Motivo = motivo;
        Timestamp = DateTime.Now;
    }
}