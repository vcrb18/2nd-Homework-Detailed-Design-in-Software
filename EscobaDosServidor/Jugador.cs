using System.Runtime.InteropServices;

namespace Servidor;

public class Jugador
{
    private List<Carta> _mano = new List<Carta>();
    private int _puntaje = 0;
    private int _id;
    private List<Jugada> _listaDeJugadas = new List<Jugada>();
    private CalculadorPuntajeJugadorPorRonda _calculadorPuntajeJugadorPorRonda;

    public Jugador(int id)
    {
        _id = id;
        _calculadorPuntajeJugadorPorRonda = new CalculadorPuntajeJugadorPorRonda(this);
    }
    public List<Carta> Mano
    {
        get { return _mano; }
    }
    public int Puntaje
    {
        get { return _puntaje;  }
    }

    public int Id
    {
        get { return _id;  }
    }
    public List<Jugada> ListaDeJugadas
    {
        get { return _listaDeJugadas;  }
    }
    public void AgregarCartaAMano(Carta carta)
    {
        _mano.Add(carta);
    }
    
    public void AgregarJugada(Jugada jugada)
    {
        _listaDeJugadas.Add(jugada);
    }

    public void SacarCartaDeMano(Carta carta)
    {
        _mano.Remove(carta);
    }

    public bool ManoVacia()
    {
        if (_mano.Count == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }

    public void MostrarCartasGanadas()
    {
        foreach (var jugada in _listaDeJugadas)
        {
            Vista.MostrarJugada(jugada);
        }
    }

    public void CalcularPuntaje()
    {
        _calculadorPuntajeJugadorPorRonda.ReiniciaPuntajeACero();
        _calculadorPuntajeJugadorPorRonda.CalculaPuntaje();
        _puntaje += _calculadorPuntajeJugadorPorRonda.Puntaje;
    }

    public void ReiniciarListaJugadas()
    {
        _listaDeJugadas = new List<Jugada>();
    }
    
}