using System.Runtime.InteropServices;

namespace Servidor;

public class Jugador
{
    private List<Carta> _mano = new List<Carta>();
    private int _puntaje = 0;
    public int _id; // ARREGLAR
    private List<Jugada> _listaDeJugadas = new List<Jugada>();

    public Jugador(int id)
    {
        _id = id;
    }

    public void AgregarCartaAMano(Carta carta)
    {
        _mano.Add(carta);
    }

    public List<Jugada> ListaDeJugadas
    {
        get { return _listaDeJugadas;  }
    }
    public int Puntaje
    {
        get { return _puntaje;  }
    }

    public List<Carta> Mano
    {
        get { return _mano; }
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
            Vista.MostrarJugada(this, jugada);
        }
    }

    public void CalcularPuntaje()
    {
        PuntajePorEscoba();
        PuntajePorSieteDeOro();
        PuntajePorMayoriaDeSietes();
        PuntajePorMayoriaDeCartas();
        PuntajePorMayoriaDeOros();
    }

    public void PuntajePorEscoba()
    {
        _puntaje += NumeroDeEscobas();
    }

    public void PuntajePorSieteDeOro()
    {
        if (TieneSieteDeOro())
        {
            _puntaje++;
        }
    }

    public void PuntajePorMayoriaDeSietes()
    {
        if (TieneMayoriaDeSietes())
        {
            _puntaje++;
        }

    }
    private void PuntajePorMayoriaDeCartas()
    {
        if (TieneMayoriaDeCartas())
        {
            _puntaje++;
        }

    }
    
    private void PuntajePorMayoriaDeOros()
    {
        if (TieneMayoriaDeOros())
        {
            _puntaje++;
        }

    }
    
    public int NumeroDeEscobas()
    {
        int numeroDeEscobas = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            if (jugada.EsEscoba)
            {
                numeroDeEscobas++;
            }
        }

        return numeroDeEscobas;
    }

    public bool TieneSieteDeOro()
    {
        bool tieneSieteDeOro = false;
        foreach (var jugada in _listaDeJugadas)
        {
            if (jugada.TieneSieteDeOro())
            {
                tieneSieteDeOro = true;
            }
        }

        return tieneSieteDeOro;
    }

    public bool TieneMayoriaDeSietes()
    {
        bool tieneMayoriaDeSietes = false;
        int numeroDeSietes = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            int numeroDeSietesEnJugada = jugada.TieneMayoriaDeSietes();  // Retorna el numero de sietes en la jugada
            // if (jugada.TieneMayoriaDeSietes())
            // {
            //     tieneMayoriaDeSietes = true;
            // }
            numeroDeSietes += numeroDeSietesEnJugada;
        }

        if (numeroDeSietes >= 2)
        {
            tieneMayoriaDeSietes = true;
        }

        return tieneMayoriaDeSietes;
    }

    public bool TieneMayoriaDeCartas()
    {
        bool tieneMayoriaDeCartas = false;
        int numCartas = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            numCartas += jugada.NumeroDeCartasDeJugada;
        }

        if (TieneMasDeVeinteCartas(numCartas))
        {
            tieneMayoriaDeCartas = true;
        }
        return tieneMayoriaDeCartas;
    }

    public bool TieneMasDeVeinteCartas(int numCartas)
    {
        if (numCartas >= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TieneMayoriaDeOros()
    {
        bool tieneMayoriaDeOros = false;
        int numOros = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            numOros += jugada.NumeroDeOrosEnJugada();
        }

        if (TieneMasDeCincoOros(numOros))
        {
            tieneMayoriaDeOros = true;
        }

        return tieneMayoriaDeOros;
    }

    public bool TieneMasDeCincoOros(int numOros)
    {
        if (numOros >= 5)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}