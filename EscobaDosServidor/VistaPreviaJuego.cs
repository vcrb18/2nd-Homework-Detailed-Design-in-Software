namespace Servidor;

public static class VistaPreviaJuego
{
    private static void Escribir(string mensaje) => Console.Write(mensaje);
    private static void EscribirLinea(string mensaje) => Escribir(mensaje + "\n");
    private static string LeerLinea() => Console.ReadLine();

    public static void MostrarManerasDeJugarJuego()
    {
        EscribirLinea("Existen dos maneras de jugar a la escoba");
        Escribir("Modo Local o Servidor:");
        Escribir(" (0) Local,");
        EscribirLinea(" (1) Servidor");
        EscribirLinea("¿En qué modo quieres jugar?");
        EscribirLinea("(Ingresa 0 para jugar en modo Local o 1 en el otro caso)");
    }

    public static int EscogerModoLocalOServidor()
    {
        int idModo = PedirNumeroValido(0, 1);
        return idModo;
    }
    
    private static int PedirNumeroValido(int minValue, int maxValue)
    {
        int numero;
        bool fuePosibleTransformarElString;
        do
        {
            string? inputUsuario = LeerLinea();
            fuePosibleTransformarElString = int.TryParse(inputUsuario, out numero);
        } while (!fuePosibleTransformarElString || numero < minValue || numero > maxValue);

        return numero;
    }
}