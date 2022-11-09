namespace Servidor;

public abstract class VistaJugadorAleatorio : Vista
{
    private Random _rnd = new Random();
    
    // public override void Pausar() { }
    protected override int PedirNumeroValido(int minValue, int maxValue) 
        => _rnd.Next(minValue, maxValue + 1);
}