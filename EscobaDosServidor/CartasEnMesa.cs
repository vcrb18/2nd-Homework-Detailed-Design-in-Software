namespace Servidor;

public class CartasEnMesa
{
    private const int NumeroCartasMesaInicialmente = 4;
    private List<Carta> _cartasEnMesa = new List<Carta>();

    public CartasEnMesa(MazoCartas mazoCartas)
    {
        for (int i = 0; i < NumeroCartasMesaInicialmente; i++)
        {
            _cartasEnMesa.Add(mazoCartas.SacarCartaDeArriba());
        }
    }

    public List<Carta> CartasDeLaMesa
    {
        get { return _cartasEnMesa; }
    }

    public void AgregarCarta(Carta carta)
    {
        _cartasEnMesa.Add(carta);
    }

    public void SacarCartasDeLaMesa(List<Carta> cartas)
    {
        List<Carta> mesaConCartasYaRetiradas = _cartasEnMesa.Except(cartas).ToList();
        _cartasEnMesa = mesaConCartasYaRetiradas;
    }

    public bool LaJugadaEsUnaEscoba(List<Carta> cartasJugada)
    {
        bool laJugadaEsUnaEscoba = _cartasEnMesa.All(cartaMesa => cartasJugada.Contains(cartaMesa));
        return laJugadaEsUnaEscoba;
    }
}