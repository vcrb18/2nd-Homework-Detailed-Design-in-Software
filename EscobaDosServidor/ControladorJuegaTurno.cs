namespace Servidor;

public class ControladorJuegaTurno
{
    private Jugadores _jugadores;
    private static int _idJugadorTurno = 1;
    private static Vista _vista;
    private CartasEnMesa _cartasEnMesa;
    private ControladorDeJugadasEnTurno _controladorDeJugadasEnTurno;
    

    public ControladorJuegaTurno(CartasEnMesa cartasEnMesa, Vista vista, Jugadores jugadores, ControladorDeJugadasEnTurno controladorDeJugadasEnTurno)
    {
        _cartasEnMesa = cartasEnMesa;
        _vista = vista;
        _jugadores = jugadores;
        _controladorDeJugadasEnTurno = controladorDeJugadasEnTurno;
    }
    
    public void JugarTurno()
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        _vista.MostrarQuienJuega(jugador);
        _vista.MostrarMesaActual(_cartasEnMesa);
        
        _vista.MostrarManoJugador(jugador);
        int idJugada = _vista.PedirCarta(1, jugador.Mano.Count);
        
        Carta cartaAJugar = jugador.Mano[idJugada - 1];
        _controladorDeJugadasEnTurno.ResetearJugadas();
        JugarTurnoJugador(cartaAJugar);
        CambiarTurno();
    }
    
    private void JugarTurnoJugador(Carta cartaAJugar)
    {
        CalcularJugadas(cartaAJugar);
        BajarCarta(cartaAJugar);
        RevisarJugadasDisponibles();
    }
    
    private void CalcularJugadas(Carta cartaAJugar)
    {
        
        _controladorDeJugadasEnTurno.CalculaQueCartasSumanQuince(CartasQuePuedenSumarQuince(cartaAJugar), cartaAJugar, true);
    }
    
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
    
    private void BajarCarta(Carta carta)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        jugador.SacarCartaDeMano(carta);
        _cartasEnMesa.AgregarCarta(carta);
    }

    private void RevisarJugadasDisponibles()
    {
        if (! _controladorDeJugadasEnTurno.HayAlMenosUnaJugadaDisponible())
        {
            _vista.NoHayJugadaDisponible();
        }
        else
        {
            Jugada jugadaJugador = _controladorDeJugadasEnTurno.ObtieneJugadaJugador();
            JugarJugada(jugadaJugador);
        }
    }

    private void JugarJugada(Jugada jugada)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        _controladorDeJugadasEnTurno.AgregarJugadaAlJugador(jugada, jugador);
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
}