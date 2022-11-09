namespace Servidor;

public class Jugadores
{
    private List<Jugador> _jugadores;

    public Jugadores(int numeroJugadores)
    {
        _jugadores= new List<Jugador>();
        for (int i = 0; i < numeroJugadores; i++)
        {
            _jugadores.Add(new Jugador(i));
        }
    }
    
    public List<Jugador> ObtenerJugadores
    {
        get { return _jugadores; }
    }

    public Jugador ObtenerJugador(int idJugador) => _jugadores[idJugador];
    
    public bool AmbosJugadoresTienenManosVacias()
    {
        return RevisarSiAmbosJugadoresTienenManosVacias();
    }

    private bool RevisarSiAmbosJugadoresTienenManosVacias()
    {
        if (_jugadores[0].ManoVacia() && _jugadores[1].ManoVacia())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RepartirCartas(MazoCartas mazoCartas)
    {
        foreach (var jugador in _jugadores)
        {
            mazoCartas.DarCartasIniciales(jugador);
        }   
    }

    // public void MostrarCartasGanadas()
    // {
    //     foreach (var jugador in _jugadores)
    //     {
    //         Vista.EscribeJugador(jugador);
    //         jugador.MostrarCartasGanadas();
    //     }
    // }
    
    public void CalcularPuntajes()
    {
        foreach (var jugador in _jugadores)
        {
            jugador.CalcularPuntaje();
        }
    }

    public List<Jugador> GanadorOGanadoresDelJuego()
    {
        if (HayEmpate())
        {
            return listaJugadoresEmpatados();
        }
        else
        {
            return ListaConJugadorGanador();
        }
    }
    
    private bool HayEmpate()
    {
        if (_jugadores[0].Puntaje == _jugadores[1].Puntaje)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private List<Jugador> listaJugadoresEmpatados()
    {
        List<Jugador> jugadoresGanadores = new List<Jugador>();
        jugadoresGanadores.Add(_jugadores[0]);
        jugadoresGanadores.Add(_jugadores[1]);
        return jugadoresGanadores;
    }

    private List<Jugador> ListaConJugadorGanador()
    {
        List<Jugador> jugadorGanador = new List<Jugador>();
        jugadorGanador.Add(ObtieneJugadorGanador());
        return jugadorGanador;
    }

    private Jugador ObtieneJugadorGanador()
    {
        if (_jugadores[0].Puntaje > _jugadores[1].Puntaje)
        {
            return _jugadores[0];
        }
        else
        {
            return _jugadores[1];
        }
    }
    
    public int ObtenerIdPrimerGanador(List<Jugador> listaJugadorGanador)
    {
        return listaJugadorGanador[0].Id;
    }
    
    public int ObtenerIdSegundoGanador(List<Jugador> listaJugadorGanador)
    {
        return listaJugadorGanador[1].Id;
    }

    public void ReiniciarListaJugadas()
    {
        foreach (var jugador in _jugadores)
        {
            jugador.ReiniciarListaJugadas();
        }
    }
    
}