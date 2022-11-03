using Servidor;

namespace EscobaDos.Tests;

public class CartasEnMesaTests
{
    [Fact]
    public void AgregarCarta_CartasMesaAumentaEnUno()
    {
        Carta cartaParaAgregar = new Carta("Oro", "1");
        MazoCartas mazoCartas = new MazoCartas();
        CartasEnMesa cartasEnMesa = new CartasEnMesa(mazoCartas);
        int numeroCartasMesa = cartasEnMesa.CartasDeLaMesa.Count;
        
        cartasEnMesa.AgregarCarta(cartaParaAgregar);
        Carta cartaEntregada = cartasEnMesa.CartasDeLaMesa[cartasEnMesa.CartasDeLaMesa.Count - 1];
        int nuevoNumeroCartasMesa = cartasEnMesa.CartasDeLaMesa.Count;
        
        Assert.Equal(cartaParaAgregar, cartaEntregada);
        Assert.Equal(nuevoNumeroCartasMesa, numeroCartasMesa + 1);
    }

    [Fact]
    public void SacarCartas_CartasMesaSeDescartan()
    {
        List<Carta> cartasADescartar = new List<Carta>();
        MazoCartas mazoCartas = new MazoCartas();
        CartasEnMesa cartasEnMesa = new CartasEnMesa(mazoCartas);
        List<Carta> cartasEnMesaAntesDeSacar = cartasEnMesa.CartasDeLaMesa;
        int numeroCartasEnMesaAntesDeSacar = cartasEnMesaAntesDeSacar.Count;
        Carta primeraCartaADescartar = cartasEnMesa.CartasDeLaMesa[0];
        Carta segundaCartaADescartar = cartasEnMesa.CartasDeLaMesa[1];
        cartasADescartar.Add(primeraCartaADescartar);
        cartasADescartar.Add(segundaCartaADescartar);
        
        cartasEnMesa.SacarCartas(cartasADescartar);
        List<Carta> cartasEnMesaDespuesDeSacar = cartasEnMesa.CartasDeLaMesa;
        int numeroCartasEnMesaDespuesDeSacar = cartasEnMesaDespuesDeSacar.Count;

        Assert.Equal(numeroCartasEnMesaAntesDeSacar - cartasADescartar.Count, numeroCartasEnMesaDespuesDeSacar);
        Assert.NotEqual(cartasEnMesaAntesDeSacar, cartasEnMesaDespuesDeSacar);
    }
}