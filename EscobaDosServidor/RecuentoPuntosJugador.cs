namespace Servidor;

public class RecuentoPuntosJugador
{
    private Jugador _jugador;
    private List<Jugada> _listaDeJugadas;

    public RecuentoPuntosJugador(Jugador jugador)
    {
        _jugador = jugador;
        _listaDeJugadas = jugador.ListaDeJugadas;
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
    
    public bool TieneDosOMasSietes()
    {
        if (NumeroDeSietes() >= 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private int NumeroDeSietes()
    {
        int numeroDeSietes = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            int numeroDeSietesEnJugada = jugada.NumeroDeSietesEnJugada();
            numeroDeSietes += numeroDeSietesEnJugada;
        }

        return numeroDeSietes;
    }
    
    public bool TieneVeinteOMasCartas()
    {
        if (NumeroDeCartasEnJugadas() >= 20)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private int NumeroDeCartasEnJugadas()
    {
        int numCartas = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            numCartas += jugada.NumeroDeCartasDeJugada;
        }

        return numCartas;
    }
    
    public bool TieneCincoOMasOros()
    {
        if (NumeroDeOrosEnJugadas() >= 5)
        {
            return true; 
        }
        else
        {
            return false;
        }
    }
    
    private int NumeroDeOrosEnJugadas()
    {
        int numeroDeOrosEnJugadas = 0;
        foreach (var jugada in _listaDeJugadas)
        {
            numeroDeOrosEnJugadas += jugada.NumeroDeOrosEnJugada();
        }

        return numeroDeOrosEnJugadas;
    }
}