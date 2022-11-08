namespace Servidor;

public class CalculadorPuntajeJugadorPorRonda
{
    private int _puntaje = 0;
    private Jugador _jugador;

    public CalculadorPuntajeJugadorPorRonda(Jugador jugador)
    {
        _jugador = jugador;
    }

    public int Puntaje
    {
        get { return _puntaje;  }
    }

    public void ReiniciaPuntajeACero()
    {
        _puntaje = 0;
    }

    public void CalculaPuntaje()
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
    
    private int NumeroDeEscobas()
    {
        int numeroDeEscobas = 0;
        foreach (var jugada in _jugador.ListaDeJugadas)
        {
            if (jugada.EsEscoba)
            {
                numeroDeEscobas++;
            }
        }

        return numeroDeEscobas;
    }

    private void PuntajePorSieteDeOro()
    {
        if (TieneSieteDeOro())
        {
            _puntaje++;
        }
    }

    private bool TieneSieteDeOro()
    {
        bool tieneSieteDeOro = false;
        foreach (var jugada in _jugador.ListaDeJugadas)
        {
            if (jugada.TieneSieteDeOro())
            {
                tieneSieteDeOro = true;
            }
        }

        return tieneSieteDeOro;
    }
    private void PuntajePorMayoriaDeSietes()
    {
        if (TieneMayoriaDeSietes())
        {
            _puntaje++;
        }

    }
    private bool TieneMayoriaDeSietes()
    {
        int numeroDeSietes = NumeroDeSietesQueTieneJugador();
        bool tieneMayoriaDeSietes = TieneDosOMasSietes(numeroDeSietes);
        return tieneMayoriaDeSietes;
    }

    private int NumeroDeSietesQueTieneJugador()
    {
        int numeroDeSietes = 0;
        foreach (var jugada in _jugador.ListaDeJugadas)
        {
            int numeroDeSietesEnJugada = jugada.NumeroDeSietesEnJugada();
            numeroDeSietes += numeroDeSietesEnJugada;
        }

        return numeroDeSietes;
    }
    
    private bool TieneDosOMasSietes(int numeroDeSietes)
    {
        if (numeroDeSietes >= 2) { return true; }
        else { return false; }
    }
    
    private void PuntajePorMayoriaDeCartas()
    {
        if (TieneMayoriaDeCartas())
        {
            _puntaje++;
        }
    }
    
    private bool TieneMayoriaDeCartas()
    {
        int numeroDeCartasEnJugadas = NumeroDeCartasEnJugadas();
        bool tieneMayoriaDeCartas = TieneMasDeVeinteCartas(numeroDeCartasEnJugadas);
        return tieneMayoriaDeCartas;
    }

    private int NumeroDeCartasEnJugadas()
    {
        int numCartas = 0;
        foreach (var jugada in _jugador.ListaDeJugadas)
        {
            numCartas += jugada.NumeroDeCartasDeJugada;
        }

        return numCartas;
    }
    
    private bool TieneMasDeVeinteCartas(int numCartas)
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
    
    private void PuntajePorMayoriaDeOros()
    {
        if (TieneMayoriaDeOros())
        {
            _puntaje++;
        }

    }
    
    private bool TieneMayoriaDeOros()
    {
        int numeroDeOrosEnJugadas = NumeroDeOrosEnJugadas();
        bool tieneMayoriaDeOros = TieneCincoOMasOros(numeroDeOrosEnJugadas);
        return tieneMayoriaDeOros;
    }

    private int NumeroDeOrosEnJugadas()
    {
        int numeroDeOrosEnJugadas = 0;
        foreach (var jugada in _jugador.ListaDeJugadas)
        {
            numeroDeOrosEnJugadas += jugada.NumeroDeOrosEnJugada();
        }

        return numeroDeOrosEnJugadas;
    }

    private bool TieneCincoOMasOros(int numeroOros)
    {
        if (numeroOros >= 5)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
}