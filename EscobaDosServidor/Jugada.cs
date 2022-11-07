namespace Servidor;

public class Jugada
{
    private List<Carta> _cartasQueFormanJugada;
    private bool _esEscoba;

    public Jugada(List<Carta> cartasQueFormanJugada, bool esEscoba)
    {
        _cartasQueFormanJugada = cartasQueFormanJugada;
        _esEscoba = esEscoba;
    }

    public List<Carta> CartasQueFormanJugada
    {
        get { return _cartasQueFormanJugada; }
    }
    
    public int NumeroDeCartasDeJugada
    {
        get { return _cartasQueFormanJugada.Count; }
    }
    
    public bool EsEscoba
    {
        get { return _esEscoba; }
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
            if (EsSieteDeOro(carta)) { tieneSieteDeOro = true; }
        }

        return tieneSieteDeOro;
    }

    private bool EsSieteDeOro(Carta carta)
    {
        bool esSieteDeOro = false;
        if (carta.Pinta == "Oro" && carta.Valor == "7") { esSieteDeOro = true; }

        return esSieteDeOro;
    }

    public int NumeroDeSietesEnJugada()
    {
        int numeroDeSietes = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (ValorCartaEsSiete(carta)) { numeroDeSietes += 1; }
        }
        
        return numeroDeSietes;
    }

    private bool ValorCartaEsSiete(Carta carta)
    {
        if (carta.Valor == "7")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public int NumeroDeOrosEnJugada()
    {
        int numeroDeOros = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (PintaCartaEsOro(carta)) { numeroDeOros += 1; }
        }

        return numeroDeOros;
    }
    
    private bool PintaCartaEsOro(Carta carta)
    {
        if (carta.Pinta == "Oro")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    
}