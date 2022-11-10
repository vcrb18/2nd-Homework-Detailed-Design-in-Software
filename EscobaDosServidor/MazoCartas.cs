using System.ComponentModel.DataAnnotations;

namespace Servidor;

public class MazoCartas
{
    private List<Carta> _cartas;
    private GeneradorNumerosAleatorios generadorRandom = new GeneradorNumerosAleatorios();

    public MazoCartas()
    {
        GenerarCartas();
    }

    private void GenerarCartas()
    {
        _cartas = new List<Carta>();
        List<string> valoresDeCartas = ValoresDeCartas();
        foreach (var pinta in Enum.GetValues(typeof(Pintas)))
        {
            foreach (string valor in valoresDeCartas)
            {
                _cartas.Add(new Carta(pinta.ToString(), valor));
            }
        }
    }
    
    private List<string> ValoresDeCartas()
    {
        List<string> valoresCartas = new List<string>();
        valoresCartas.AddRange(new List<string>
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "Sota",
            "Caballo",
            "Rey"
        });
        return valoresCartas;
    }

    private enum Pintas
    {
        Oro,
        Espada,
        Copa,
        Bastos
    }
    
    public List<Carta> Cartas
    {
        get { return _cartas; }
    }


    public void AlgoritmoParaBarajarCartas()
    {
        int numeroCartasMazo = _cartas.Count;
        while (numeroCartasMazo > 1)
        {
            numeroCartasMazo--;
            int numeroAleatorio = generadorRandom.Generar(numeroCartasMazo);
            Carta cartaSeleccionadaRandom = _cartas[numeroAleatorio];
            _cartas[numeroAleatorio] = _cartas[numeroCartasMazo];
            _cartas[numeroCartasMazo] = cartaSeleccionadaRandom;
        }
    }
    
    public void DarCartasIniciales(Jugador jugador)
    {
        for (int i = 0; i < 3; i++)
        {
            Carta cartaSuperiorMazo = CartaSuperiorMazo();
            SacarCartaSuperiorMazo();
            jugador.AgregarCartaAMano(cartaSuperiorMazo);
        }
    }
    
    public Carta CartaSuperiorMazo()
    {
        Carta cartaDeArriba = _cartas[0];
        return cartaDeArriba;
    }

    public void SacarCartaSuperiorMazo()
    {
        _cartas.Remove(CartaSuperiorMazo());
    }
    
    public bool SeAcabaronLasCartas()
    {
        if (_cartas.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}