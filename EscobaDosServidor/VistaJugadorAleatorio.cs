namespace Servidor;

public class VistaJugadorAleatorio : Vista
{
    private Random _rnd = new Random();
    protected override int PedirNumeroValido(int minValue, int maxValue) 
        => _rnd.Next(minValue, maxValue + 1);
    protected override void Escribir(string mensaje) => Console.Write(mensaje);
    protected override string LeerLinea() => Console.ReadLine();
}