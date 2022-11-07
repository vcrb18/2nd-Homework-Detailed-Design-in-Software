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


    // [Fact]
    // public void PuntajePorEscoba_SumaPuntajeEquivalenteANumeroDEeEscobas()
    // {
    //     Jugador jugador = creaJugadorConJugadasYDosEscobas();
    //     int puntajeAntesDeContar = jugador.Puntaje;
    //     int numeroDeEscobas = jugador.NumeroDeEscobas();
    //
    //     jugador.PuntajePorEscoba();
    //     int puntajeLuegoDeContar = jugador.Puntaje;
    //
    //     Assert.Equal(puntajeLuegoDeContar, numeroDeEscobas);
    //     Assert.NotEqual(puntajeAntesDeContar, puntajeLuegoDeContar);
    // }
    //
    // [Fact]
    // public void PuntajePorSieteDeOro_SumaUnoAlPuntajeSiHaySieteDeOro()
    // {
    //     Jugador jugadorConSieteDeOro = CreaJugadorConJugadasConSieteDeOro();
    //     Jugador jugadorSinSieteDeOro = CreaJugadorConJugadasSinSieteDeOro();
    //     int puntajeAntesDeContarJugadorConSieteDeOro = jugadorConSieteDeOro.Puntaje;
    //     int puntajeAntesDeContarJugadorSinSieteDeOro = jugadorSinSieteDeOro.Puntaje;
    //
    //     jugadorConSieteDeOro.PuntajePorSieteDeOro();
    //     jugadorSinSieteDeOro.PuntajePorSieteDeOro();
    //     int puntajeLuegoDeContarJugadorConSieteDeOro = jugadorConSieteDeOro.Puntaje;
    //     int puntajeLuegoDeContarJugadorSinSieteDeOro = jugadorSinSieteDeOro.Puntaje;
    //
    //     Assert.Equal(puntajeAntesDeContarJugadorConSieteDeOro + 1, puntajeLuegoDeContarJugadorConSieteDeOro);
    //     Assert.Equal(puntajeAntesDeContarJugadorSinSieteDeOro, puntajeLuegoDeContarJugadorSinSieteDeOro);
    // }
    //
    // [Fact]
    // public void PuntajePorMayoriaDeSietes_SumaPuntajeAlTenerMasDeUnSiete()
    // {
    //     Jugador jugadorSinSietes = CreaJugadorSinSietes();
    //     Jugador jugadorConUnSiete = CreaJugadorConUnSiete();
    //     Jugador jugadorConDosSiete = CreaJugadorConDosSiete();
    //     Jugador jugadorConTresSiete = CreaJugadorConTresSiete();
    //     Jugador jugadorConCuatroSiete = CreaJugadorConCuatroSiete();
    //     int puntajeAntesDeContarJugadorSinSietes = jugadorSinSietes.Puntaje;
    //     int puntajeAntesDeContarJugadorConUnSiete = jugadorConUnSiete.Puntaje;
    //     int puntajeAntesDeContarJugadorConDosSiete = jugadorConDosSiete.Puntaje;
    //     int puntajeAntesDeContarJugadorConTresSiete = jugadorConTresSiete.Puntaje;
    //     int puntajeAntesDeContarJugadorConCuatroSiete = jugadorConCuatroSiete.Puntaje;
    //
    //     jugadorSinSietes.PuntajePorMayoriaDeSietes();
    //     jugadorConUnSiete.PuntajePorMayoriaDeSietes();
    //     jugadorConDosSiete.PuntajePorMayoriaDeSietes();
    //     jugadorConTresSiete.PuntajePorMayoriaDeSietes();
    //     jugadorConCuatroSiete.PuntajePorMayoriaDeSietes();
    //     int puntajeLuegoDeContarJugadorSinSietes = jugadorSinSietes.Puntaje;
    //     int puntajeLuegoDeContarJugadorConUnSiete = jugadorConUnSiete.Puntaje;
    //     int puntajeLuegoDeContarJugadorConDosSiete = jugadorConDosSiete.Puntaje;
    //     int puntajeLuegoDeContarJugadorConTresSiete = jugadorConTresSiete.Puntaje;
    //     int puntajeLuegoDeContarJugadorConCuatroSiete = jugadorConCuatroSiete.Puntaje;
    //     
    //     Assert.Equal(puntajeAntesDeContarJugadorSinSietes, puntajeLuegoDeContarJugadorSinSietes);
    //     Assert.Equal(puntajeAntesDeContarJugadorConUnSiete, puntajeLuegoDeContarJugadorConUnSiete);
    //     Assert.Equal(puntajeAntesDeContarJugadorConDosSiete + 1, puntajeLuegoDeContarJugadorConDosSiete);
    //     Assert.Equal(puntajeAntesDeContarJugadorConTresSiete + 1, puntajeLuegoDeContarJugadorConTresSiete);
    //     Assert.Equal(puntajeAntesDeContarJugadorConCuatroSiete + 1, puntajeLuegoDeContarJugadorConCuatroSiete);
    // }
    //
    //


    //
    // private Jugador creaJugadorConJugadasYDosEscobas()
    // {
    //     Jugador jugador = new Jugador(0);
    //     List<Jugada> jugadas = CreaTresJugadasConDosEscobasSinSiete();
    //     AgregaTodasLasJugadasAlJugador(jugador, jugadas);
    //     return jugador;
    // }
    //
    // public List<Jugada> CreaTresJugadasConDosEscobasSinSiete()
    // {
    //     List<Jugada> listaJugadas = new List<Jugada>();
    //     List<Carta> listaCartas = CreaListaDeTresCartasSinSiete();
    //     Jugada jugadaUno = new Jugada(listaCartas, true);
    //     Jugada jugadaDos = new Jugada(listaCartas, false);
    //     Jugada jugadaTres = new Jugada(listaCartas, true);
    //
    //     listaJugadas.Add(jugadaUno);
    //     listaJugadas.Add(jugadaDos);
    //     listaJugadas.Add(jugadaTres);
    //
    //     return listaJugadas;
    // }
    //
    // public Jugador CreaJugadorConJugadasSinSieteDeOro()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasSinSiete());
    //     return jugador;
    // }
    //
    // public Jugador CreaJugadorConJugadasConSieteDeOro()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasConSieteDeOro());
    //     return jugador;
    // }
    // public Jugador CreaJugadorSinSietes()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasSinSiete());
    //     return jugador;
    // }
    //
    // public Jugador CreaJugadorConUnSiete()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasConUnSiete());
    //     return jugador;
    // }
    //
    // public Jugador CreaJugadorConDosSiete()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasConDosSiete());
    //     return jugador;
    // }
    //
    // public Jugador CreaJugadorConTresSiete()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasConTresSiete());
    //     return jugador;
    // }
    //
    // public Jugador CreaJugadorConCuatroSiete()
    // {
    //     Jugador jugador = CreaJugadorYAgregaJugadas(CreaListaJugadasConCuatroSiete());
    //     return jugador;
    // }
    //
    // private Jugador CreaJugadorYAgregaJugadas(List<Jugada> jugadas)
    // {
    //     Jugador jugador = new Jugador(0);
    //     AgregaTodasLasJugadasAlJugador(jugador, jugadas);
    //     return jugador;
    // }
    //
    // public List<Jugada> CreaListaJugadasConSieteDeOro()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugada = CreaJugadaConSieteDeOro();
    //     jugadas.Add(jugada);
    //     return jugadas;
    // }
    // public List<Jugada> CreaListaJugadasSinSiete()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugada = CreaJugadaSinSiete();
    //     jugadas.Add(jugada);
    //     return jugadas;
    // }
    //
    // public List<Jugada> CreaListaJugadasConUnSiete()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugadaUno = CreaJugadaSinSiete();
    //     Jugada jugadaDos = CreaJugadaConSieteDeOro();
    //     
    //     jugadas.Add(jugadaUno);
    //     jugadas.Add(jugadaDos);
    //
    //     return jugadas;
    // }
    //
    // public List<Jugada> CreaListaJugadasConDosSiete()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugadaUno = CreaJugadaSinSiete();
    //     Jugada jugadaDos = CreaJugadaConSieteDeOro();
    //     Jugada jugadaTres = CreaJugadaConSieteDeBastos();
    //     
    //     jugadas.Add(jugadaUno);
    //     jugadas.Add(jugadaDos);
    //     jugadas.Add(jugadaTres);
    //     
    //     return jugadas;
    // }
    //
    // public List<Jugada> CreaListaJugadasConTresSiete()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugadaUno = CreaJugadaSinSiete();
    //     Jugada jugadaDos = CreaJugadaConSieteDeOro();
    //     Jugada jugadaTres = CreaJugadaConSieteDeBastos();
    //     Jugada jugadaCuatro = CreaJugadaConSieteDeEspada();
    //
    //     jugadas.Add(jugadaUno);
    //     jugadas.Add(jugadaDos);
    //     jugadas.Add(jugadaTres);
    //     jugadas.Add(jugadaCuatro);
    //     
    //     return jugadas;
    // }
    //
    // public List<Jugada> CreaListaJugadasConCuatroSiete()
    // {
    //     List<Jugada> jugadas = new List<Jugada>();
    //     Jugada jugadaUno = CreaJugadaSinSiete();
    //     Jugada jugadaDos = CreaJugadaConSieteDeOro();
    //     Jugada jugadaTres = CreaJugadaConSieteDeBastos();
    //     Jugada jugadaCuatro = CreaJugadaConSieteDeEspada();
    //     Jugada jugadaCinco = CreaJugadaConSieteDeCopa();
    //
    //     jugadas.Add(jugadaUno);
    //     jugadas.Add(jugadaDos);
    //     jugadas.Add(jugadaTres);
    //     jugadas.Add(jugadaCuatro);
    //     jugadas.Add(jugadaCinco);
    //     
    //     return jugadas;
    // }
    //
    // public Jugada CreaJugadaSinSiete()
    // {
    //     Jugada jugada = new Jugada(CreaListaDeTresCartasSinSiete(), false);;
    //     return jugada;
    // }
    //
    // public Jugada CreaJugadaConSieteDeOro()
    // {
    //     Jugada jugada = new Jugada(CreaListaDeTresCartasConSieteDeOro(), false);;
    //     return jugada;
    // }
    //
    // public Jugada CreaJugadaConSieteDeBastos()
    // {
    //     Jugada jugada = new Jugada(CreaListaDeTresCartasConSieteDeBastos(), false);;
    //     return jugada;
    // }
    //
    // public Jugada CreaJugadaConSieteDeEspada()
    // {
    //     Jugada jugada = new Jugada(CreaListaDeTresCartasConSieteDeEspadas(), false);;
    //     return jugada;
    // }
    //
    // public Jugada CreaJugadaConSieteDeCopa()
    // {
    //     Jugada jugada = new Jugada(CreaListaDeTresCartasConSieteDeCopa(), false);;
    //     return jugada;
    // }
    //
    // private void AgregaTodasLasJugadasAlJugador(Jugador jugador, List<Jugada> jugadas)
    // {
    //     foreach (var jugada in jugadas)
    //     {
    //         jugador.AgregarJugada(jugada);
    //     }
    // }
    //
    // public List<Carta> CreaListaDeTresCartasConSieteDeOro()
    // {
    //     List<Carta> listaDeTresCartasConSieteDeOro = CreaListaDeTresCartasConSiete("Oro");
    //     return listaDeTresCartasConSieteDeOro;
    // }
    //
    // public List<Carta> CreaListaDeTresCartasConSieteDeBastos()
    // {
    //     List<Carta> listaDeTresCartasConSieteDeBastos = CreaListaDeTresCartasConSiete("Bastos");
    //     return listaDeTresCartasConSieteDeBastos;
    // }
    //
    // public List<Carta> CreaListaDeTresCartasConSieteDeEspadas()
    // {
    //     List<Carta> listaDeTresCartasConSieteDeEspadas = CreaListaDeTresCartasConSiete("Espadas");
    //     return listaDeTresCartasConSieteDeEspadas;
    // }
    //
    // public List<Carta> CreaListaDeTresCartasConSieteDeCopa()
    // {
    //     List<Carta> listaDeTresCartasConSieteDeCopa = CreaListaDeTresCartasConSiete("Copa");
    //     return listaDeTresCartasConSieteDeCopa;
    // }
    // public List<Carta> CreaListaDeTresCartasConSiete(string pinta)
    // {
    //     List<Carta> listaCartas = new List<Carta>();
    //     Carta cartaUno = new Carta(pinta, "5");
    //     Carta cartaDos = new Carta(pinta, "7");
    //     Carta cartaTres = new Carta(pinta, "3");
    //     listaCartas.Add(cartaUno);
    //     listaCartas.Add(cartaDos);
    //     listaCartas.Add(cartaTres);
    //     return listaCartas;
    // }

    //
    // public void creaJugadaUnoParaTieneMasDeVeinteCartas()
    // {
    //     List<Carta> cartasJugada = new List<Carta>();
    //     Carta cartaUno = new Carta("Oro", "4");
    //     Carta cartaDos = new Carta("Oro", "3");
    //     Carta cartaTres = new Carta("Rey", "4");
    //     Carta cartaCuatro = new Carta("Espada", "4");
    //     cartasJugada.Add(cartaUno);
    //     cartasJugada.Add(cartaDos);
    //     cartasJugada.Add(cartaTres);
    //     cartasJugada.Add(cartaCuatro);
    // }

}
