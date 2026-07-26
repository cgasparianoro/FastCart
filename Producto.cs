namespace FastCart.Fase2;

/// <summary>
/// Representa un producto del catálogo maestro de FastCart.
/// </summary>
public class Producto
{
    public int SKU { get; set; }

    public string Nombre { get; set; }

    public decimal Precio { get; set; }

    public int Stock { get; set; }

    /// <summary>
    /// Inicializa un producto con SKU, nombre, precio y stock.
    /// </summary>
    public Producto(int sku, string nombre, decimal precio, int stock)
    {
        SKU = sku;
        Nombre = nombre;
        Precio = precio;
        Stock = stock;
    }

    /// <summary>
    /// Devuelve la información completa del producto.
    /// </summary>
    public override string ToString()
    {
        return $"SKU: {SKU} | Nombre: {Nombre} | Precio: {Precio:C2} | Stock: {Stock}";
    }
}