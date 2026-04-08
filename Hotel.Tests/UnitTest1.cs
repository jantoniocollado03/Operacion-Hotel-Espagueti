using HotelApp;

namespace Tarifas.Tests;

public class DescuentosTests
{
    [Fact]
    public void Procesar_ClienteEsVIP_AplicaQuincePorCientoDescuento()
    {
        // Arrange
        var reserva = new h1 { t = 2, d = 2, v = true, b = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(127.5m, resultado);
    }

    [Fact]
    public void Procesar_EstanciaLarga_AplicaCincoPorCientoExtra()
    {
        // Arrange
        var reserva = new h1 { t = 1, d = 10, v = false, b = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(475m, resultado);
    }

    [Fact]
    public void Procesar_VIPyEstanciaLarga_AplicaAmbosDescuentos()
    {
        // Arrange
        var reserva = new h1 { t = 1, d = 10, v = true, b = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(403.75m, resultado);
    }
}