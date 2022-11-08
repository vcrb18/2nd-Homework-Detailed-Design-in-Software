using Servidor;

namespace EscobaDos.Tests;

public class MazoCartasTests
{
    [Fact]
    public void GenerarCartas_SeCreaElMazoCompleto()
    {
        MazoCartas mazoCartas = new MazoCartas();

        int numeroCartasMazo = mazoCartas.Cartas.Count;
        
        Assert.Equal(40, numeroCartasMazo);
    }

    [Fact]
    public void BarajarCartas_ListaInicialDistintaABarajada()
    {
        MazoCartas mazoCartas = new MazoCartas();
        List<Carta> cartasSinBarajar = CreaListaCartasMazo(mazoCartas);
        
        mazoCartas.AlgoritmoParaBarajarCartas();
        List<Carta> cartasDelMazoBarajadas = mazoCartas.Cartas;

        Assert.NotEqual(cartasSinBarajar, cartasDelMazoBarajadas);
    }

    [Fact]
    public void DarCartasIniciales_SeEntregaTresCartas()
    {
        MazoCartas mazoCartas = new MazoCartas();
        Jugador[] jugadores = new Jugador[2];
        for (int i = 0; i < 2; i++)
        {
            jugadores[i] = new Jugador(i);
        }

        foreach (var jugador in jugadores)
        {
            mazoCartas.DarCartasIniciales(jugador);
        }

        Assert.Equal(3, jugadores[0].Mano.Count);
        Assert.Equal(3, jugadores[1].Mano.Count);
    }

    [Fact]
    public void SeAcabaronLasCartas_MazoInicialNoVacio()
    {
        MazoCartas mazoCartas = new MazoCartas();

        bool seAcabaronLasCartas = mazoCartas.SeAcabaronLasCartas();
        
        Assert.False(seAcabaronLasCartas);
    }
    
    [Fact]
    public void SeAcabaronLasCartas_MazoFinalVacio()
    {
        MazoCartas mazoCartas = new MazoCartas();
        int cartasEnElMazo = mazoCartas.CuantasCartasQuedan();
        for (int i = 0; i < cartasEnElMazo; i++)
        {
            mazoCartas.SacarCartaSuperiorMazo();
        }

        bool seAcabaronLasCartas = mazoCartas.SeAcabaronLasCartas();
        
        Assert.True(seAcabaronLasCartas);
    }
    
    public List<Carta> CreaListaCartasMazo(MazoCartas mazoCartas)
    {
        List<Carta> cartasSinBarajar = new List<Carta>();
        for(int i = 0; i < mazoCartas.Cartas.Count; i++)
        {
            cartasSinBarajar.Add(mazoCartas.Cartas[i]);
        }

        return cartasSinBarajar;
    }
    
}