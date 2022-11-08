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
            if (carta.EsSieteDeOro()) { tieneSieteDeOro = true; }
        }

        return tieneSieteDeOro;
    }

    public int NumeroDeSietesEnJugada()
    {
        int numeroDeSietes = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (carta.ValorCartaEsSiete()) { numeroDeSietes += 1; }
        }
        
        return numeroDeSietes;
    }

    public int NumeroDeOrosEnJugada()
    {
        int numeroDeOros = 0;
        foreach (var carta in _cartasQueFormanJugada)
        {
            if (carta.PintaCartaEsOro()) { numeroDeOros += 1; }
        }

        return numeroDeOros;
    }
    
}