using Servidor;

namespace EscobaDos.Tests;

public class JugadoresTests
{
    [Fact]
    public void AmbosJugadoresTienenManosVacias_TrueSiNoHayCartasEnMano()
    {
        Jugadores jugadores = new Jugadores(2);
    
        bool manosVacias = jugadores.AmbosJugadoresTienenManosVacias();
        bool manoVaciaJugadorUno = jugadores.ObtenerJugador(0).ManoVacia();
        bool manoVaciaJugadorDos = jugadores.ObtenerJugador(1).ManoVacia();
        
        Assert.Equal(manoVaciaJugadorUno, manosVacias);
        Assert.Equal(manoVaciaJugadorDos, manosVacias);
    }
    
    [Fact]
    public void AmbosJugadoresTienenManosVacias_FalseSiHayCartasEnMano()
    {
        Jugadores jugadores = new Jugadores(2);
        MazoCartas mazoCartas = new MazoCartas();
        jugadores.RepartirCartas(mazoCartas);

        bool manosVacias = jugadores.AmbosJugadoresTienenManosVacias();

        Assert.False(manosVacias);
    }

    [Fact]
    public void GanadorOGanadoresDelJuego_PuntajeIgualRetornaAmbosJugadores()
    {
        Jugadores jugadores = new Jugadores(2);

        List<Jugador> jugadoresGanadores = jugadores.GanadorOGanadoresDelJuego();
        
        Assert.Equal(2, jugadoresGanadores.Count);
    }


}