using System.ComponentModel.DataAnnotations;

namespace Servidor;

public class MazoCartas
{
    private List<Carta> _cartas;
    private GeneradorNumerosAleatorios _rnd;

    public MazoCartas()
    {
        GenerarCartas();
    }

    private void GenerarCartas()
    {
        _cartas = new List<Carta>();
        List<string> valoresCartas = Valores();
        foreach (var pinta in Enum.GetValues(typeof(Pintas)))
        {
            foreach (string valor in valoresCartas)
            {
                _cartas.Add(new Carta(pinta.ToString(), valor));
            }
        }
    }
    
    public List<Carta> Cartas
    {
        get { return _cartas; }
    }

    private List<string> Valores()
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

    public void BarajarCartas()
    {
        GeneradorNumerosAleatorios generadorRandom = new GeneradorNumerosAleatorios();
        int numCartas = _cartas.Count;
        while (numCartas > 1)
        {
            numCartas--;
            int k = generadorRandom.Generar(numCartas);
            Carta value = _cartas[k];
            _cartas[k] = _cartas[numCartas];
            _cartas[numCartas] = value;
        }
    }

    public void DarCartasIniciales(Jugador jugador)
    {
        for (int i = 0; i < 3; i++)
        {
            jugador.AgregarCartaAMano(SacarCartaDeArriba());
        }
    }

    // Por ahora se asumira que es la primera carta de la lista
    public Carta SacarCartaDeArriba()
    {
        Carta cartaDeArriba = _cartas[0];
        _cartas.Remove(cartaDeArriba);
        return cartaDeArriba;
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

    public int CuantasCartasQuedan()
    {
        return _cartas.Count;
    }
}