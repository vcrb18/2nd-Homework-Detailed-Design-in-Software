using Servidor;

namespace EscobaDos.Tests;

public class JugadorTests
{
    [Fact]
    public void AgregarCartaAMano_SeAgregaUnaCartaALaMano()
    {
        Carta cartaParaAgregar = new Carta("1", "Oro");
        Jugador jugador = new Jugador(0);
        int numeroCartasEnManoAntesDeAgregar = jugador.Mano.Count;

        jugador.AgregarCartaAMano(cartaParaAgregar);
        int numeroCartasEnManoDespuesDeAgregar = jugador.Mano.Count;
        Carta cartaAgregada = jugador.Mano[jugador.Mano.Count - 1];

        Assert.Equal(numeroCartasEnManoAntesDeAgregar + 1, numeroCartasEnManoDespuesDeAgregar);
        Assert.Equal(cartaParaAgregar, cartaAgregada);
    }

    [Fact]
    public void AgregarJugada_SeAgregaUnaJugadaAlJugador()
    {
        List<Carta> cartasQueFormanJugada = new List<Carta>();
        Carta primeracartaDeJugada = new Carta("1", "Oro");
        Carta segundacartaDeJugada = new Carta("7", "Oro");
        cartasQueFormanJugada.Add(primeracartaDeJugada);
        cartasQueFormanJugada.Add(segundacartaDeJugada);
        Jugada jugadaParaAgregar = new Jugada(cartasQueFormanJugada, true);
        Jugador jugador = new Jugador(0);
        int numeroJugadasAntesDeAgregar = jugador.ListaDeJugadas.Count;

        jugador.AgregarJugada(jugadaParaAgregar);
        int numeroJugadasDespuesDeAgregar = jugador.ListaDeJugadas.Count;
        Jugada jugadaAgregada = jugador.ListaDeJugadas[jugador.ListaDeJugadas.Count - 1];

        Assert.Equal(numeroJugadasAntesDeAgregar + 1, numeroJugadasDespuesDeAgregar);
        Assert.Equal(jugadaParaAgregar, jugadaAgregada);
    }

    [Fact]
    public void SacarCartaDeMano_CartaYaNoEstaEnMano()
    {
        List<Carta> cartas = CreaListaDeTresCartasSinSiete();
        Jugador jugador = new Jugador(0);
        jugador.AgregarCartaAMano(cartas[0]);
        jugador.AgregarCartaAMano(cartas[1]);
        jugador.AgregarCartaAMano(cartas[2]);
        List<Carta> manoJugador = jugador.Mano;
        int numeroCartasEnManoAntesDeSacar = manoJugador.Count;

        jugador.SacarCartaDeMano(cartas[2]);
        int numeroCartasEnManoDespuesDeSacar = manoJugador.Count;

        Assert.Equal(numeroCartasEnManoAntesDeSacar - 1, numeroCartasEnManoDespuesDeSacar);
    }

    [Fact]
    public void ManoVacia_SinCartasDebeSerTrue()
    {
        Jugador jugadorConCartas = creaJugadorConCartas();
        Jugador jugadorSinCartas = creaJugadorSinCartas();

        bool manoVaciaJugadorConCartas = jugadorConCartas.ManoVacia();
        bool manoVaciaJugadorSinCartas = jugadorSinCartas.ManoVacia();

        Assert.NotEqual(manoVaciaJugadorConCartas, manoVaciaJugadorSinCartas);
        Assert.False(manoVaciaJugadorConCartas);
        Assert.True(manoVaciaJugadorSinCartas);
    }
    
    private Jugador creaJugadorConCartas()
    {
        Jugador jugador = new Jugador(1);
        List<Carta> listaCartas = CreaListaDeTresCartasSinSiete();
        foreach (var carta in listaCartas)
        {
            jugador.AgregarCartaAMano(carta);
        }
    
        return jugador;
    }
    
    public List<Carta> CreaListaDeTresCartasSinSiete()
    {
        List<Carta> listaCartas = new List<Carta>();
        Carta cartaUno = new Carta("Oro", "1");
        Carta cartaDos = new Carta("Oro", "Sota");
        Carta cartaTres = new Carta("Oro", "6");
        listaCartas.Add(cartaUno);
        listaCartas.Add(cartaDos);
        listaCartas.Add(cartaTres);
        return listaCartas;
    }
    
    private Jugador creaJugadorSinCartas()
    {
        Jugador jugador = new Jugador(0);
        return jugador;
    }

}
