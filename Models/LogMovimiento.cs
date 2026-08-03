namespace FastCart.Fase3.Models;

/// <summary>
/// Representa un movimiento registrado en la bitácora de auditoría.
/// </summary>
public struct LogMovimiento
{
    // Marca temporal con precisión UTC.
    public DateTime Timestamp;

    // Categoría del evento:
    // INSERT, UPDATE, DELETE, RESTOCK o PRICE_CHANGE.
    public string TipoOperacion;

    // Identificador del producto afectado.
    public int SKUAfectado;

    // Descripción legible del cambio realizado.
    public string Descripcion;
}