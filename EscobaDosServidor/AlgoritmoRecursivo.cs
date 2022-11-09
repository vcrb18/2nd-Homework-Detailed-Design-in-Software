namespace Servidor;

public class AlgoritmoQueGuardaJugadasPosibles
{
    private List<Jugada> _listaDeJugadasPosibles = new List<Jugada>();
    private CartasEnMesa _cartasEnMesa;

    public AlgoritmoQueGuardaJugadasPosibles(CartasEnMesa cartasEnMesa)
    {
        _cartasEnMesa = cartasEnMesa;
    }

    public void CalculaQueCartasSumanQuince(List<Carta> cartasQuePuedenSumarQuince, Carta cartaQueDebePertenecerAJugada, bool debeIncluirCarta)
    {
        CalculaQueCartasSumanQuinceRecursivo(cartasQuePuedenSumarQuince,new List<Carta>(), cartaQueDebePertenecerAJugada, debeIncluirCarta);
    }
    
    private void CalculaQueCartasSumanQuinceRecursivo(List<Carta> numbers, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    {
        int sumaValoresCarta = 0;
        foreach (Carta carta in cartasCandidatasParaSumarQuince) sumaValoresCarta += carta.ConvierteStringValorAInt();
        GuardaJugadaSiSeCumplenLasCondiciones(sumaValoresCarta, cartasCandidatasParaSumarQuince, cartaObligatoria, debeIncluirCarta);
        if (sumaValoresCarta >= 15)
            return;
        RealizaRecursionConCartasRestantes(numbers, cartasCandidatasParaSumarQuince, cartaObligatoria, debeIncluirCarta);
    }
    
    
    private void GuardaJugadaSiSeCumplenLasCondiciones(int sumaValoresCarta, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    {
        if (debeIncluirCarta)
        {
            if (sumaValoresCarta == 15 && cartasCandidatasParaSumarQuince.Contains(cartaObligatoria))
            {
                GuardaJugada(cartasCandidatasParaSumarQuince);
            }
        }
        else
        {
            if (sumaValoresCarta == 15)
            {
                GuardaJugada(cartasCandidatasParaSumarQuince);
            }
        }
    }

    private void GuardaJugada(List<Carta> cartasQueSumanQuince)
    {
        bool laJugadaEsUnaEscoba = _cartasEnMesa.LaJugadaEsUnaEscoba(cartasQueSumanQuince);
        Jugada jugada = new Jugada(cartasQueSumanQuince, laJugadaEsUnaEscoba);
        _listaDeJugadasPosibles.Add(jugada);
    }
    
    private void RealizaRecursionConCartasRestantes(List<Carta> numbers, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    {
        for (int i = 0; i < numbers.Count; i++)
        {
            List<Carta> cartasRestantes = new List<Carta>();
            Carta n = numbers[i];
            for (int j = i + 1; j < numbers.Count; j++) cartasRestantes.Add(numbers[j]);

            List<Carta> cartasCandidatasParaSumarQuinceRecursivo = new List<Carta>(cartasCandidatasParaSumarQuince);
            cartasCandidatasParaSumarQuinceRecursivo.Add(n);
            CalculaQueCartasSumanQuinceRecursivo(cartasRestantes, cartasCandidatasParaSumarQuinceRecursivo, cartaObligatoria, debeIncluirCarta);
        }
    }

    public void ResetearJugadas()
    {
        _listaDeJugadasPosibles = new List<Jugada>();

    }
    
    public List<Jugada> ListaDeJugadasPosibles
    {
        get { return _listaDeJugadasPosibles; }
    }
}