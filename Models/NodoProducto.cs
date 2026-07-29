namespace FastCart.Fase2;

/// <summary>
/// Representa un nodo autoreferenciado de la lista de productos.
/// </summary>
public class NodoProducto
{
    public Producto Data { get; set; }

    public NodoProducto? Siguiente { get; set; }

    /// <summary>
    /// Crea un nodo que almacena un producto.
    /// </summary>
    public NodoProducto(Producto producto)
    {
        this.Data = producto;
        this.Siguiente = null; // apunta a nada al crearse
    }
}