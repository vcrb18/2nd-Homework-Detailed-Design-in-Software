namespace Servidor;

public class ControladorDeJugadasEnJuego
{
    // private List<Jugada> _listaDeJugadasPosibles = new List<Jugada>();
    private CartasEnMesa _cartasEnMesa;
    private int _target = 15;
    private Vista _vista;
    private Jugadores _jugadores;
    private int _idJugadorRepartidor = 0;
    private static int _idUltimoJugadorEnLlevarseLasCartas;

    private AlgoritmoQueGuardaJugadasPosibles _algoritmoQueGuardaJugadasPosibles;

    public ControladorDeJugadasEnJuego(CartasEnMesa cartasEnMesa, Vista vista, Jugadores jugadores)
    {
        _cartasEnMesa = cartasEnMesa;
        _vista = vista;
        _jugadores = jugadores;
        _algoritmoQueGuardaJugadasPosibles = new AlgoritmoQueGuardaJugadasPosibles(_cartasEnMesa);
    }
    public void RevisarSiCartasMesaInicialesSumanQuince()
    {
        // _algoritmoQueGuardaJugadasPosibles.CalculaQueCartasSumanQuince(_cartasEnMesa.CartasDeLaMesa, null, false);
        // _listaDeJugadasPosibles = _algoritmoQueGuardaJugadasPosibles.ListaDeJugadasPosibles;
        CalculaQueCartasSumanQuince(_cartasEnMesa.CartasDeLaMesa, null, false);
        if (CuatroCartasMesaFormanUnaJugada()) { JugarJugadaAlComienzoDeMano(_algoritmoQueGuardaJugadasPosibles._listaDeJugadasPosibles[0]); }
        else if (CuatroCartasMesaFormanDosJugadasDeDosCartas()) { SeJueganDosJugadasAlComienzoDeMano(); }
        ResetearJugadas();
    }

    public void CalculaQueCartasSumanQuince(List<Carta> cartasQuePuedenSumarQuince, Carta cartaQueDebePertenecerAJugada,
        bool debeIncluirCarta)
    {
        _algoritmoQueGuardaJugadasPosibles.CalculaQueCartasSumanQuince(cartasQuePuedenSumarQuince,
            cartaQueDebePertenecerAJugada, debeIncluirCarta);
        _listaDeJugadasPosibles = _algoritmoQueGuardaJugadasPosibles.ListaDeJugadasPosibles;
    }
    
    // Desde aqui
    // public void CalculaQueCartasSumanQuince(List<Carta> cartasQuePuedenSumarQuince, Carta cartaQueDebePertenecerAJugada, bool debeIncluirCarta)
    // {
    //     CalculaQueCartasSumanQuinceRecursivo(cartasQuePuedenSumarQuince,new List<Carta>(), cartaQueDebePertenecerAJugada, debeIncluirCarta);
    // }
    //
    // private void CalculaQueCartasSumanQuinceRecursivo(List<Carta> numbers, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    // {
    //     int sumaValoresCarta = 0;
    //     foreach (Carta carta in cartasCandidatasParaSumarQuince) sumaValoresCarta += carta.ConvierteStringValorAInt();
    //     GuardaJugadaSiSeCumplenLasCondiciones(sumaValoresCarta, cartasCandidatasParaSumarQuince, cartaObligatoria, debeIncluirCarta);
    //     if (sumaValoresCarta >= _target)
    //         return;
    //     RealizaRecursionConCartasRestantes(numbers, cartasCandidatasParaSumarQuince, cartaObligatoria, debeIncluirCarta);
    // }
    //
    // private void GuardaJugadaSiSeCumplenLasCondiciones(int sumaValoresCarta, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    // {
    //     if (debeIncluirCarta)
    //     {
    //         if (sumaValoresCarta == _target && cartasCandidatasParaSumarQuince.Contains(cartaObligatoria))
    //         {
    //             GuardaJugada(cartasCandidatasParaSumarQuince);
    //         }
    //     }
    //     else
    //     {
    //         if (sumaValoresCarta == _target)
    //         {
    //             GuardaJugada(cartasCandidatasParaSumarQuince);
    //         }
    //     }
    // }
    //
    // private void GuardaJugada(List<Carta> cartasQueSumanQuince)
    // {
    //     bool laJugadaEsUnaEscoba = _cartasEnMesa.LaJugadaEsUnaEscoba(cartasQueSumanQuince);
    //     Jugada jugada = new Jugada(cartasQueSumanQuince, laJugadaEsUnaEscoba);
    //     _listaDeJugadasPosibles.Add(jugada);
    // }
    //
    // private void RealizaRecursionConCartasRestantes(List<Carta> numbers, List<Carta> cartasCandidatasParaSumarQuince, Carta cartaObligatoria, bool debeIncluirCarta)
    // {
    //     for (int i = 0; i < numbers.Count; i++)
    //     {
    //         List<Carta> cartasRestantes = new List<Carta>();
    //         Carta n = numbers[i];
    //         for (int j = i + 1; j < numbers.Count; j++) cartasRestantes.Add(numbers[j]);
    //
    //         List<Carta> cartasCandidatasParaSumarQuinceRecursivo = new List<Carta>(cartasCandidatasParaSumarQuince);
    //         cartasCandidatasParaSumarQuinceRecursivo.Add(n);
    //         CalculaQueCartasSumanQuinceRecursivo(cartasRestantes, cartasCandidatasParaSumarQuinceRecursivo, cartaObligatoria, debeIncluirCarta);
    //     }
    // }
    
    // Hasta aqui
    
    private bool CuatroCartasMesaFormanUnaJugada()
    {
        if (_listaDeJugadasPosibles.Count == 1 && _listaDeJugadasPosibles[0].HayCuatroCartasEnJugada())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void JugarJugadaAlComienzoDeMano(Jugada jugada)
    {
        _vista.HayUnaODosEscobasAlComienzo();
        jugada.TransformaJugadaAEscobaEnCasoInicial();
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorRepartidor);
        AgregarJugadaAlJugador(jugada, jugador);

    }
    
    public void AgregarJugadaAlJugador(Jugada jugada, Jugador jugador)
    {
        GuardarUltimoJugadorEnLlevarseCartas(jugador);
        jugador.AgregarJugada(jugada);
        _cartasEnMesa.SacarCartasDeLaMesa(jugada.CartasQueFormanJugada);
        _vista.JugadorSeLlevaLasCartas(jugador, jugada);
    }
    
    public static void GuardarUltimoJugadorEnLlevarseCartas(Jugador jugador)
    {
        _idUltimoJugadorEnLlevarseLasCartas = jugador.Id;
    }
    
    private bool CuatroCartasMesaFormanDosJugadasDeDosCartas()
    {
        if (_listaDeJugadasPosibles.Count == 2 && AmbasJugadasSonDeDosCartas(_listaDeJugadasPosibles))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private bool AmbasJugadasSonDeDosCartas(List<Jugada> jugadas)
    {
        if ((jugadas[0].HayDosCartasEnJugada()) && (jugadas[1].HayDosCartasEnJugada()))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    private void SeJueganDosJugadasAlComienzoDeMano()
    {
        foreach (var jugada in _listaDeJugadasPosibles)
        {
            JugarJugadaAlComienzoDeMano(jugada);
        }
    }
    
    public void ResetearJugadas()
    {
        // _listaDeJugadasPosibles = new List<Jugada>();
        _algoritmoQueGuardaJugadasPosibles.ResetearJugadas();
    }

    public bool HayAlMenosUnaJugadaDisponible()
    {
        if (_listaDeJugadasPosibles.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public Jugada ObtieneJugadaJugador()
    {
        Jugada jugada;
        int idJugada = ObtieneIdJugada();
        jugada = _listaDeJugadasPosibles[idJugada];
        return jugada;
    }
    
    private int ObtieneIdJugada()
    {
        int idJugada;
        if (_listaDeJugadasPosibles.Count == 1) { idJugada = 0; }
        else { idJugada = _vista.PedirJugada(_listaDeJugadasPosibles); }

        return idJugada;
    }

    public int IdUltimoJugadorEnLlevarseLasCartas
    {
        get { return _idUltimoJugadorEnLlevarseLasCartas;  }
    }
}