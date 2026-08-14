using System;
using System.Collections.Generic;
using FastCart.Fase2;
using Xunit;

namespace FastCart.Tests;

public class UnitTest1
{
    [Fact]
    public void InsertarOrdenado_ProductoValido_SePuedeBuscar()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();
        Producto producto = new Producto(
            2001,
            "Producto Prueba",
            25.00m,
            10
        );

        // Act
        inventario.InsertarOrdenado(producto);
        Producto resultado = inventario.BuscarPorSKU(2001);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(2001, resultado.SKU);
        Assert.Equal("Producto Prueba", resultado.Nombre);
    }

    [Fact]
    public void BuscarPorSKU_SKUExistente_RetornaProductoCorrecto()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();

        inventario.InsertarOrdenado(
            new Producto(1001, "Arroz", 45.50m, 30)
        );

        inventario.InsertarOrdenado(
            new Producto(1006, "Café", 120.00m, 15)
        );

        // Act
        Producto resultado = inventario.BuscarPorSKU(1006);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(1006, resultado.SKU);
        Assert.Equal("Café", resultado.Nombre);
        Assert.Equal(120.00m, resultado.Precio);
        Assert.Equal(15, resultado.Stock);
    }

    [Fact]
    public void BuscarPorSKU_SKUInexistente_LanzaKeyNotFoundException()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();

        inventario.InsertarOrdenado(
            new Producto(1001, "Arroz", 45.50m, 30)
        );

        // Act + Assert
        Assert.Throws<KeyNotFoundException>(
            () => inventario.BuscarPorSKU(9999)
        );
    }

    [Fact]
    public void EliminarPorSKU_SKUExistente_ProductoYaNoExiste()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();

        inventario.InsertarOrdenado(
            new Producto(1007, "Huevos", 75.00m, 36)
        );

        // Act
        inventario.EliminarPorSKU(1007);

        // Assert
        Assert.Throws<KeyNotFoundException>(
            () => inventario.BuscarPorSKU(1007)
        );
    }

    [Fact]
    public void InsertarOrdenado_VariosProductos_TodosPuedenBuscarse()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();

        // Act
        inventario.InsertarOrdenado(
            new Producto(3001, "Producto A", 50.00m, 10)
        );

        inventario.InsertarOrdenado(
            new Producto(3002, "Producto B", 10.00m, 20)
        );

        inventario.InsertarOrdenado(
            new Producto(3003, "Producto C", 30.00m, 30)
        );

        // Assert
        Assert.Equal(
            3001,
            inventario.BuscarPorSKU(3001).SKU
        );

        Assert.Equal(
            3002,
            inventario.BuscarPorSKU(3002).SKU
        );

        Assert.Equal(
            3003,
            inventario.BuscarPorSKU(3003).SKU
        );
    }

    [Fact]
    public void EliminarPorSKU_ListaVacia_NoGeneraError()
    {
        // Arrange
        InventarioLista inventario = new InventarioLista();

        // Act
        Exception? excepcion = Record.Exception(
            () => inventario.EliminarPorSKU(9999)
        );

        // Assert
        Assert.Null(excepcion);
    }
}