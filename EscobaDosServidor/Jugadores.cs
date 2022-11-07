namespace Servidor;

public class Jugadores
{
    private List<Jugador> _jugadores;

    public Jugadores(int numJugadores)
    {
        _jugadores= new List<Jugador>();
        for (int i = 0; i < numJugadores; i++)
        {
            _jugadores.Add(new Jugador(i));
        }
    }

    public bool ManosVacias()
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
    
    public Jugador ObtenerJugador(int idJugador) => _jugadores[idJugador];

    public void RepartirCartas(MazoCartas mazoCartas)
    {
        foreach (var jugador in _jugadores)
        {
            mazoCartas.DarCartasIniciales(jugador);
        }   
    }

    public void MostrarCartasGanadas()
    {
        foreach (var jugador in _jugadores)
        {
            Vista.EscribeJugador(jugador);
            jugador.MostrarCartasGanadas();
        }
    }

    public List<Jugador> ObtenerJugadores
    {
        get { return _jugadores; }
    }

    public void CalcularPuntajes()
    {
        foreach (var jugador in _jugadores)
        {
            jugador.CalcularPuntaje();
        }
    }

    public List<Jugador> ObtenerListaJugadoresGanadores()
    {
        List<Jugador> jugadorGanador = new List<Jugador>();
        if (_jugadores[0].Puntaje > _jugadores[1].Puntaje)
        {
            jugadorGanador.Add(_jugadores[0]);
        }
        else if (_jugadores[0].Puntaje < _jugadores[1].Puntaje)
        {
            jugadorGanador.Add(_jugadores[1]);
        }
        else if (_jugadores[0].Puntaje == _jugadores[1].Puntaje)
        {
            jugadorGanador.Add(_jugadores[0]);
            jugadorGanador.Add(_jugadores[1]);
        }

        return jugadorGanador;
    }

    public int ObtenerIdGanador(List<Jugador> listaJugadorGanador)
    {
        return listaJugadorGanador[0]._id;
    }
    
    public int ObtenerIdGanadorDos(List<Jugador> listaJugadorGanador)
    {
        return listaJugadorGanador[1]._id;
    }
    
    
}