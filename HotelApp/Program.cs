
namespace HotelApp;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("--- SISTEMA DE RESERVAS HOTEL DESCANSO S.A. ---");
        Console.WriteLine("Iniciando prueba de cálculo legacy...\n");

        try
        {
            // Ejemplo 1: Habitación Doble, 3 días, No VIP, Con desayuno
            Habitacion reserva1 = new Habitacion();
            reserva1.tipo = 2; // Doble (75€/día)
            reserva1.dias = 3; // 3 días
            reserva1.vip = false; // No VIP
            reserva1.desayuno = 1; // Con desayuno (12€/día)

            decimal total1 = reserva1.procesar();
            Console.WriteLine($"Reserva 1 (Doble, 3 días, Desayuno): {total1} EUR");
            // Esperado: (75 * 3) + (12 * 3) = 225 + 36 = 261 EUR

            // Ejemplo 2: Suite, 10 días, VIP, Sin desayuno
            Habitacion reserva2 = new Habitacion();
            reserva2.tipo = 3; // Suite (150€/día)
            reserva2.dias = 10; // 10 días
            reserva2.vip = true; // VIP (-15%)
            reserva2.desayuno = 0; // Sin desayuno

            decimal total2 = reserva2.procesar();
            Console.WriteLine($"Reserva 2 (Suite, 10 días, VIP): {total2} EUR");
            // Esperado: (150 * 10) = 1500. VIP = 1500 * 0.85 = 1275. 
            // Larga estancia = 1275 - (1275 * 0.05) = 1211.25 EUR
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error durante el cálculo: {ex.Message}");
        }

        Console.WriteLine("\nPresiona cualquier tecla para salir...");
        Console.ReadKey();
    }
}
