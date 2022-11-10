using Servidor;

namespace EscobaDos.Tests;

public class JuegoTests
{
    [Fact]
    public void Jugar_DebeLograrCompletarUnJuegoSinCaerse()
    {
        Juego juego = Juego.CrearConJugadorAleatorio();
        
        juego.Jugar();
        
    }
    
}