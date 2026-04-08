namespace HotelApp;

public class Habitacion
{
    const decimal DESCUENTOVIP = 0.15m;
    const decimal DESCUENTOESTADIALARGA = 0.05m;
    private const decimal PRECIOPORDESAYUNO = 12m;

    // Atributos publicos
    public int tipo { get; set; } // tipo habitacion (1: Individual, 2: Doble, 3: Suite)
    public int dias { get; set; } // dias de estancia
    public bool vip { get; set; } // es cliente VIP
    public int desayuno { get; set; } // incluye desayuno (1: si, 0: no)

    // Metodo para procesar y calcular el precio
    public decimal procesar()
    {
        if (dias < 1) throw new Exception("No puedes poner dias menores que 1");
        
        decimal resultado = CalcularTarifaBase();

        if (desayuno == 1) resultado += dias * PRECIOPORDESAYUNO;
        if (vip == true) resultado -= resultado * DESCUENTOVIP;
        if (dias > 7)  resultado -= resultado * DESCUENTOESTADIALARGA;
        return resultado;

    }


    private decimal CalcularTarifaBase()
    {
        decimal precioPorDia = tipo switch
        {
            1 => 50m,
            2 => 75m,
            3 => 150m,
            _ => throw new Exception("Tipo invalido")
        };
        return dias * precioPorDia;
    }
}

