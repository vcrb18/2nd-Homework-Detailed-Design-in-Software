using System.Runtime.InteropServices;

namespace Servidor;

public class Juego
{
    private const int NumJugadores = 2;
    private static Jugadores _jugadores;
    private static int _idJugadorTurno = 1;
    private int _idUltimoJugadorEnLlevarseLasCartas;
    private static int _idJugadorRepartidor = 0;
    private int _idJugadorPartidor = 1;
    private static MazoCartas _mazoCartas;
    private static CartasEnMesa _cartasEnMesa;
    private static Vista _vista;
    private bool _vistaSocket = true;

    private static ControladorDeJugadasEnTurno _controladorDeJugadasEnTurno;
    private ControladorJuegaTurno _controladorJuegaTurno;
    private GanadorDelJuego _ganadorDelJuego;

    public Juego(Vista vista)
    {
        _vista = vista;
        CrearJugadores();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
        CrearSegundoControlador();
        CrearTercerControlador();
        CreaGanadorJuego();
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
        _controladorDeJugadasEnTurno = 
            new ControladorDeJugadasEnTurno(_cartasEnMesa, _vista, _jugadores);

    private void CrearTercerControlador() =>
        _controladorJuegaTurno =
            new ControladorJuegaTurno(_cartasEnMesa, _vista, _jugadores, _controladorDeJugadasEnTurno);

    private void CreaGanadorJuego() =>
        _ganadorDelJuego =
            new GanadorDelJuego(_jugadores, _vista);

    public void Jugar()
    {
        while (!EsFinJuego())
        {
            _controladorDeJugadasEnTurno.RevisarSiCartasMesaInicialesSumanQuince();
            _vista.MostrarInfoInicial(_idJugadorRepartidor, _idJugadorPartidor);
            while (!EsFinMazoYManos())
            {
                SiNoTienenCartasSeReparte();
                _controladorJuegaTurno.JugarTurno();
            }
            FinalRonda();
        }
        _ganadorDelJuego.RevisaQuienGanoJuego();
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
    
    private void SiNoTienenCartasSeReparte()
    {
        if (_jugadores.AmbosJugadoresTienenManosVacias())
        {
            RepartirCartas(); 
            _vista.SeVuelvenARepartirCartas();
        }
    }

    private void FinalRonda()
    {
        UltimaJugadaDelMazo();
        CalculaPuntos();
        NuevoJuego();
    }
    
    private static void UltimaJugadaDelMazo()
    {
        int idUltimoJugadorEnLlevarseLasCartas = _controladorDeJugadasEnTurno.IdUltimoJugadorEnLlevarseLasCartas;
        Jugador jugadorEnLlevarseLasCartas = _jugadores.ObtenerJugador(idUltimoJugadorEnLlevarseLasCartas);
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
    
    private void NuevoJuego()
    {
        _jugadores.ReiniciarListaJugadas();
        CambiarRepartidorYJugador();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
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

}