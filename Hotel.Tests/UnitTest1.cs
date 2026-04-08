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
    
    [Theory]
    [InlineData(1, 50)]  // Individual: 50€
    [InlineData(2, 75)]  // Doble: 75€
    [InlineData(3, 150)] // Suite: 150€
    public void Procesar_TiposHabitacion_CalculaTarifaBaseCorrecta(int tipo, decimal precioEsperado)
    {
        // Arrange: 1 día, sin extras ni descuentos
        var reserva = new h1 { t = tipo, d = 1, v = false, b = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(precioEsperado, resultado);
    }

    [Fact]
    public void Procesar_ConDesayuno_SumaDoceEurosPorDia()
    {
        // Arrange: Individual (50€) + Desayuno (12€) = 62€ por día.
        // 2 días = 124€
        var reserva = new h1 { t = 1, d = 2, v = false, b = 1 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(124m, resultado);
    }

    [Fact]
    public void Procesar_TipoInvalido_LanzaExcepcion()
    {
        // Arrange: Tipo de habitación 5 (no existe)
        var reserva = new h1 { t = 5, d = 1, v = false, b = 0 };

        // Act & Assert: Verifica que lanza la excepción con el mensaje exacto
        var ex = Assert.Throws<Exception>(() => reserva.procesar());
        Assert.Equal("Tipo invalido", ex.Message);
    }
}