namespace Servidor;

public class CartasEnMesa
{
    private const int NumCartasMesaInicial = 4;
    private List<Carta> _cartasEnMesa = new List<Carta>();

    public CartasEnMesa(MazoCartas mazoCartas)
    {
        for (int i = 0; i < NumCartasMesaInicial; i++)
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

    public void SacarCartas(List<Carta> cartas)
    {
        List<Carta> result = _cartasEnMesa.Except(cartas).ToList();
        _cartasEnMesa = result;
    }
}