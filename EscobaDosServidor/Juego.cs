using System.Runtime.InteropServices;

namespace Servidor;

public class Juego
{
    private const int NumJugadores = 2;
    private static Jugadores _jugadores;
    private static int _idJugadorTurno = 1;
    private static int _idUltimoJugadorEnLlevarseLasCartas;
    private static int _idJugadorRepartidor = 0;
    private int _idJugadorPartidor = 1;
    private static MazoCartas _mazoCartas;
    private static CartasEnMesa _cartasEnMesa;
    private static Vista _vista;
    private bool _vistaSocket = true;

    private ControladorDeJugadasEnJuego _controladorDeJugadasEnJuego;

    public Juego(Vista vista)
    {
        _vista = vista;
        CrearJugadores();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
        CrearSegundoControlador();
    }

    public static Juego Crear()
    {
        VistaPreviaJuego.MostrarManerasDeJugarJuego();
        int idModo = VistaPreviaJuego.EscogerModoLocalOServidor();
        if (idModo == 0)
        {
            return new Juego(new VistaConsola());
        }
        else
        {
            return new Juego(new VistaSocket());
        }
    }
    
    public static Juego CrearConJugadorAleatorio() => new Juego(new VistaJugadorAleatorio());

    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores);
    private void CrearMazo() => _mazoCartas = new MazoCartas();
    private void BarajarMazo() => _mazoCartas.AlgoritmoParaBarajarCartas();
    private void RepartirCartas() => _jugadores.RepartirCartas(_mazoCartas);
    private void PonerMesa() => _cartasEnMesa = new CartasEnMesa(_mazoCartas);

    private void CrearSegundoControlador() =>
        _controladorDeJugadasEnJuego = new ControladorDeJugadasEnJuego(_cartasEnMesa, _vista, _jugadores);

    public void Jugar()
    {
        while (!EsFinJuego())
        {
            _controladorDeJugadasEnJuego.RevisarSiCartasMesaInicialesSumanQuince();
            _vista.MostrarInfoInicial(_idJugadorRepartidor, _idJugadorPartidor);
            while (!EsFinMazoYManos())
            {
                Console.WriteLine(_mazoCartas.CuantasCartasQuedan());
                JugarTurno();
            }
            FinalRonda();
        }
        RevisaQuienGanoJuego();
    }
    
    private bool EsFinJuego()
    {
        bool esFinJuego = false;
        if (AlgunJugadorGanoElJuego())
        {
            esFinJuego = true;
            _vista.FinalDePartida();
        }

        return esFinJuego;
    }
    
    private bool AlgunJugadorGanoElJuego()
    {
        Jugador jugadorUno = _jugadores.ObtenerJugador(0);
        Jugador jugadorDos = _jugadores.ObtenerJugador(1);
        bool algunJugadorAlcanzoLosQuincePuntos = false;
        if (jugadorUno.Puntaje >= 15 || jugadorDos.Puntaje >= 15) { algunJugadorAlcanzoLosQuincePuntos = true; }

        return algunJugadorAlcanzoLosQuincePuntos;
    }
    
    private bool EsFinMazoYManos()
    {
        bool esFinMazoYManos = false;
        if (_mazoCartas.SeAcabaronLasCartas() && _jugadores.AmbosJugadoresTienenManosVacias())
        {
            esFinMazoYManos = true;
        }

        return esFinMazoYManos;
    }
    
    private void JugarTurno()
    {
        SiNoTienenCartasSeReparte();
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        _vista.MostrarQuienJuega(jugador);
        _vista.MostrarMesaActual(_cartasEnMesa);
        
        _vista.MostrarManoJugador(jugador);
        int idJugada = _vista.PedirCarta(1, jugador.Mano.Count);
        
        Carta cartaAJugar = jugador.Mano[idJugada - 1];
        _controladorDeJugadasEnJuego.ResetearJugadas();
        JugarTurnoJugador(cartaAJugar);
        CambiarTurno();
    }
    
    private void SiNoTienenCartasSeReparte()
    {
        if (_jugadores.AmbosJugadoresTienenManosVacias())
        {
            RepartirCartas(); 
            _vista.SeVuelvenARepartirCartas();
        }
    }
    
    private void JugarTurnoJugador(Carta cartaAJugar)
    {
        CalcularJugadas(cartaAJugar);
        BajarCarta(cartaAJugar);
        RevisarJugadasDisponibles();
    }
    
    private void CalcularJugadas(Carta cartaAJugar)
    {
        
        _controladorDeJugadasEnJuego.CalculaQueCartasSumanQuince(CartasQuePuedenSumarQuince(cartaAJugar), cartaAJugar, true);
    }
    
    // Ojito con esta
    private List<Carta> CartasQuePuedenSumarQuince(Carta cartaAJugar)
    {
        List<Carta> cartasQuePuedenSumarQuince = new List<Carta>();
        cartasQuePuedenSumarQuince.Add(cartaAJugar);
        foreach (var carta in _cartasEnMesa.CartasDeLaMesa)
        {
            cartasQuePuedenSumarQuince.Add(carta);
        }
        return cartasQuePuedenSumarQuince;
    }
    
    // Ojito con esta
    private void BajarCarta(Carta carta)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        jugador.SacarCartaDeMano(carta);
        _cartasEnMesa.AgregarCarta(carta);
    }

    private void RevisarJugadasDisponibles()
    {
        if (! _controladorDeJugadasEnJuego.HayAlMenosUnaJugadaDisponible())
        {
            _vista.NoHayJugadaDisponible();
        }
        else
        {
            Jugada jugadaJugador = _controladorDeJugadasEnJuego.ObtieneJugadaJugador();
            JugarJugada(jugadaJugador);
        }
    }

    private void JugarJugada(Jugada jugada)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        _controladorDeJugadasEnJuego.AgregarJugadaAlJugador(jugada, jugador);
        // AgregarJugadaAlJugador(jugada, jugador);
    }

    private static void CambiarTurno()
    {
        if (_idJugadorTurno == 0)
        {
            _idJugadorTurno = 1;
        }
        else
        {
            _idJugadorTurno = 0;
        }
        _vista.CambiaIdJugadorTurno(_idJugadorTurno);
    }

    private void FinalRonda()
    {
        UltimaJugadaDelMazo();
        CalculaPuntos();
        NuevoJuego();
    }
    
    // Tmb deberia sacarlo de aca
    private static void UltimaJugadaDelMazo()
    {
        Jugador jugadorEnLlevarseLasCartas = _jugadores.ObtenerJugador(_idUltimoJugadorEnLlevarseLasCartas);
        List<Carta> cartasMesa = _cartasEnMesa.CartasDeLaMesa;
        Jugada cartasSobrantes = new Jugada(cartasMesa, false);
        jugadorEnLlevarseLasCartas.AgregarJugada(cartasSobrantes);
        _vista.SeLlevaLasUltimasCartas(jugadorEnLlevarseLasCartas, cartasSobrantes);
        _cartasEnMesa.SacarCartasDeLaMesa(cartasSobrantes.CartasQueFormanJugada);
    }

    private void CalculaPuntos()
    {
        _jugadores.CalcularPuntajes();
        _vista.CartasGanadasEnEstaRonda(_jugadores);
        _vista.TotalPuntosGanadosJugadores(_jugadores);
    }
    
    private void CambiarRepartidorYJugador()
    {
        CambiarRepartidor();
        CambiarJugador();
    }

    private static void CambiarRepartidor()
    {
        if (_idJugadorRepartidor == 0)
        {
            _idJugadorRepartidor = 1;
        }
        else
        {
            _idJugadorRepartidor = 0;
        }
    }

    private void CambiarJugador()
    {
        if (_idJugadorPartidor == 0)
        {
            _idJugadorPartidor = 1;
            _idJugadorTurno = 1;
        }
        else
        {
            _idJugadorPartidor = 0;
            _idJugadorTurno = 0;
        }
    }
    
    private void NuevoJuego()
    {
        _jugadores.ReiniciarListaJugadas();
        CambiarRepartidorYJugador();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
    }
    
    private void RevisaQuienGanoJuego()
    {
        List<Jugador> listaConGanadorOGanadores = _jugadores.GanadorOGanadoresDelJuego();
        if (listaConGanadorOGanadores.Count == 1) { GanoUnJugador(listaConGanadorOGanadores); }
        else if (listaConGanadorOGanadores.Count == 2) { HuboUnEmpate(listaConGanadorOGanadores); }
    }
    
    private void GanoUnJugador(List<Jugador> listaConJugadorGanador)
    {
        int idJugadorGanador = _jugadores.ObtenerIdPrimerGanador(listaConJugadorGanador);
        Jugador jugadorGanador = _jugadores.ObtenerJugador(idJugadorGanador);
        _vista.GanaUnJugador(jugadorGanador);
    }

    private void HuboUnEmpate(List<Jugador> listaConJugadoresGanadores)
    {
        int idJugadorGanadorUno = _jugadores.ObtenerIdPrimerGanador(listaConJugadoresGanadores);
        Jugador jugadorGanadorUno = _jugadores.ObtenerJugador(idJugadorGanadorUno);
        int idJugadorGanadorDos = _jugadores.ObtenerIdSegundoGanador(listaConJugadoresGanadores);
        Jugador jugadorGanadorDos = _jugadores.ObtenerJugador(idJugadorGanadorDos);
        _vista.HuboUnEmpate(jugadorGanadorUno, jugadorGanadorDos);
    }

    public void ActualizarCartasEnMesa(CartasEnMesa cartasEnMesa)
    {
        _cartasEnMesa = cartasEnMesa;
    }
}