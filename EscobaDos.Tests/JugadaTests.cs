using Servidor;

namespace EscobaDos.Tests;

public class JugadaTests
{
    [Theory]
    [InlineData("Oro", "7", true)]
    public void TieneSieteDeOro(string pinta, string valor, bool expected)
    {
        Carta carta = new Carta(pinta, valor);
        bool valorEsperado = expected;
    }

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

    public Jugada creaJugadaConDosCartas()
    {
        List<Carta> cartas = creaListaConDosCartas();
        Jugada jugada = new Jugada(cartas, false);
        return jugada;
    }
    
    public Jugada creaJugadaConUnaCarta()
    {
        List<Carta> cartas = creaListaConUnaCarta();
        Jugada jugada = new Jugada(cartas, false);
        return jugada;
    }

    public List<Carta> creaListaConDosCartas()
    {
        List<Carta> cartas = new List<Carta>();
        Carta cartaUno = new Carta("Oro", "1");
        Carta cartaDos = new Carta("Oro", "2");
        cartas.Add(cartaUno);
        cartas.Add(cartaDos);
        return cartas;
    }
    
    public List<Carta> creaListaConUnaCarta()
    {
        List<Carta> cartas = new List<Carta>();
        Carta cartaUno = new Carta("Espada", "Rey");
        cartas.Add(cartaUno);
        return cartas;
    }
    
}