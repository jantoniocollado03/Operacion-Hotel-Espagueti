using HotelApp;

namespace Tarifas.Tests;

public class DescuentosTests
{
    /// <summary>
    /// Verifica que se aplique correctamente el descuento del 15% 
    /// cuando el atributo de cliente VIP es verdadero.
    /// </summary>
    [Fact]
    public void Procesar_ClienteEsVIP_AplicaQuincePorCientoDescuento()
    {
        // Arrange
        var reserva = new Habitacion() { tipo = 2, dias = 2, vip = true, desayuno = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(127.5m, resultado);
    }

    /// <summary>
    /// Comprueba que el sistema aplique un 5% de descuento adicional 
    /// si la estancia del cliente supera los 7 días.
    /// </summary>
    [Fact]
    public void Procesar_EstanciaLarga_AplicaCincoPorCientoExtra()
    {
        // Arrange
        var reserva = new Habitacion() { tipo = 1, dias = 10, vip = false, desayuno = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(475m, resultado);
    }

    /// <summary>
    /// Valida la acumulación de beneficios: descuento por cliente VIP 
    /// y descuento por larga estancia simultáneamente.
    /// </summary>
    [Fact]
    public void Procesar_VIPyEstanciaLarga_AplicaAmbosDescuentos()
    {
        // Arrange
        var reserva = new Habitacion() { tipo = 1, dias = 10, vip = true, desayuno = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(403.75m, resultado);
    }
    
    /// <summary>
    /// Valida que el precio base por noche sea correcto según el tipo de habitación:
    /// Individual (50), Doble (75) o Suite (150).
    /// </summary>
    /// <param name="tipo">Identificador del tipo de habitación.</param>
    /// <param name="precioEsperado">Precio por noche esperado para el cálculo.</param>
    [Theory]
    [InlineData(1, 50)]  // Individual: 50€
    [InlineData(2, 75)]  // Doble: 75€
    [InlineData(3, 150)] // Suite: 150€
    public void Procesar_TiposHabitacion_CalculaTarifaBaseCorrecta(int tipo, decimal precioEsperado)
    {
        // Arrange: 1 día, sin extras ni descuentos
        var reserva = new Habitacion() { tipo = tipo, dias = 1, vip = false, desayuno = 0 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(precioEsperado, resultado);
    }

    /// <summary>
    /// Verifica que el coste del desayuno (12 euros/día) se sume 
    /// correctamente al total de la reserva.
    /// </summary>
    [Fact]
    public void Procesar_ConDesayuno_SumaDoceEurosPorDia()
    {
        // Arrange: Individual (50€) + Desayuno (12€) = 62€ por día.
        // 2 días = 124€
        var reserva = new Habitacion() { tipo = 1, dias = 2, vip = false, desayuno = 1 };

        // Act
        decimal resultado = reserva.procesar();

        // Assert
        Assert.Equal(124m, resultado);
    }

    /// <summary>
    /// Asegura que el sistema lance una excepción controlada cuando 
    /// se introduce un código de habitación no reconocido.
    /// </summary>
    /// <exception cref="System.Exception">Lanzada cuando el tipo es inválido.</exception>
    [Fact]
    public void Procesar_TipoInvalido_LanzaExcepcion()
    {
        // Arrange: Tipo de habitación 5 (no existe)
        var reserva = new Habitacion() { tipo = 5, dias = 1, vip = false, desayuno = 0 };

        // Act & Assert: Verifica que lanza la excepción con el mensaje exacto
        var ex = Assert.Throws<Exception>(() => reserva.procesar());
        Assert.Equal("Tipo invalido", ex.Message);
    }
}
