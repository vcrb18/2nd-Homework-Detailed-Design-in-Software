namespace Servidor;

public class Jugada
{
    private List<Carta> _cartasQueFormanJugada;
    private bool _esEscoba;

    public Jugada(List<Carta> cartasQueFormanEscoba, bool esEscoba)
    {
        _cartasQueFormanJugada = cartasQueFormanEscoba;
        _esEscoba = esEscoba;
    }

    public List<Carta> CartasQueFormanJugada
    {
        get { return _cartasQueFormanJugada; }
    }

    public override string ToString()
    {
        string s = "";
        foreach (var carta in _cartasQueFormanJugada)
        {
            s += $"{carta.ToString()}, ";
        }

        return s;
    }

    public bool EsEscoba
    {
        get { return _esEscoba; }
    }
    
    public int NumeroDeCartasDeJugada
    {
        get { return _cartasQueFormanJugada.Count; }
    }

    public bool HayDosCartasEnJugada()
    {
        if (_cartasQueFormanJugada.Count == 2)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool TieneSieteDeOro()
    {
        bool tieneSieteDeOro = false;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (carta.Pinta == "Oro" && carta.Valor == "7")
            {
                tieneSieteDeOro = true;
            }
        }

        return tieneSieteDeOro;
    }

    public int TieneMayoriaDeSietes()
    {
        bool tieneMayoriaDeSietes = false;
        int numeroDeSietes = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (carta.Valor == "7")
            {
                numeroDeSietes += 1;
            }
        }

        if (numeroDeSietes >= 2)
        {
            tieneMayoriaDeSietes = true;
        }

        return numeroDeSietes;
    }

    public int NumeroDeOrosEnJugada()
    {
        int numeroDeOros = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (carta.Pinta == "Oro")
            {
                numeroDeOros += 1;
            }
        }

        return numeroDeOros;
    }
    
    
    
}