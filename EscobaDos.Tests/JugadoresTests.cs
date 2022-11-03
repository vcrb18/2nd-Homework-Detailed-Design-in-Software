using Servidor;

namespace EscobaDos.Tests;

public class JugadoresTests
{
    [Fact]
    public void ManosVacias_TrueSiNoHayCartasEnMano()
    {
        Jugadores jugadores = new Jugadores(2);
    
        bool manosVacias = jugadores.ManosVacias();
        bool manoVaciaJugadorUno = jugadores.ObtenerJugador(0).ManoVacia();
        bool manoVaciaJugadorDos = jugadores.ObtenerJugador(1).ManoVacia();
        
        Assert.Equal(manoVaciaJugadorUno, manosVacias);
        Assert.Equal(manoVaciaJugadorDos, manosVacias);
    }
    
    [Fact]
    public void ManosVacias_FalseSiHayCartasEnMano()
    {
        Jugadores jugadores = new Jugadores(2);
        MazoCartas mazoCartas = new MazoCartas();
        jugadores.RepartirCartas(mazoCartas);

        bool manosVacias = jugadores.ManosVacias();

        Assert.False(manosVacias);
    }

    [Fact]
    public void ObtenerListaJugadoresGanadores_PuntajeIgualRetornaAmbosJugadores()
    {
        Jugadores jugadores = new Jugadores(2);

        List<Jugador> jugadoresGanadores = jugadores.ObtenerListaJugadoresGanadores();
        
        Assert.Equal(2, jugadoresGanadores.Count);
    }


}