using FastCart.Fase4.Models;

namespace FastCart.Fase4;

public class PilaDevoluciones
{
    private NodoPila? cima;

    private int cantidad;

    public PilaDevoluciones()
    {
        cima = null;
        cantidad = 0;
    }

    public void Apilar(Devolucion devolucion)
    {
        NodoPila nuevoNodo = new NodoPila(devolucion);

        nuevoNodo.Siguiente = cima;

        cima = nuevoNodo;

        cantidad++;
    }

    public Devolucion? Desapilar()
    {
        if (cima == null)
        {
            return null;
        }

        Devolucion devolucion = cima.Dato;

        cima = cima.Siguiente;

        cantidad--;

        return devolucion;
    }

    public bool EstaVacia()
    {
        return cima == null;
    }

    public int TotalApilados()
    {
        return cantidad;
    }
}