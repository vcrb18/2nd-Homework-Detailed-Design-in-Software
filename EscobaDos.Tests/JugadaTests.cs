using Servidor;

namespace EscobaDos.Tests;

public class JugadaTests
{
    [Fact]
    public void HayDosCartasEnJugada_SiHayDosCartasRetornaTrueSiNoFalse()
    {
        Jugada jugadaConDosCartas = creaJugadaConDosCartas();
        Jugada jugadaConUnaCarta = creaJugadaConUnaCarta();
        
        bool hayDosCartasEnJugadaConDosCartas = jugadaConDosCartas.HayDosCartasEnJugada();
        bool hayDosCartasEnJugadaConUnaCarta = jugadaConUnaCarta.HayDosCartasEnJugada();

        Assert.True(hayDosCartasEnJugadaConDosCartas);
        Assert.False(hayDosCartasEnJugadaConUnaCarta);
    }

    private Jugada creaJugadaConDosCartas()
    {
        List<Carta> cartas = creaListaConDosCartas();
        Jugada jugada = new Jugada(cartas, false);
        return jugada;
    }
    
    private Jugada creaJugadaConUnaCarta()
    {
        List<Carta> cartas = creaListaConUnaCarta();
        Jugada jugada = new Jugada(cartas, false);
        return jugada;
    }

    private List<Carta> creaListaConDosCartas()
    {
        List<Carta> cartas = new List<Carta>();
        Carta cartaUno = new Carta("Oro", "1");
        Carta cartaDos = new Carta("Oro", "2");
        cartas.Add(cartaUno);
        cartas.Add(cartaDos);
        return cartas;
    }
    
    private List<Carta> creaListaConUnaCarta()
    {
        List<Carta> cartas = new List<Carta>();
        Carta cartaUno = new Carta("Espada", "Rey");
        cartas.Add(cartaUno);
        return cartas;
    }
    
}