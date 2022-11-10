namespace Servidor;

public class ControladorDeJugadasEnTurno
{
    private CartasEnMesa _cartasEnMesa;
    private int _target = 15;
    private Vista _vista;
    private Jugadores _jugadores;
    private int _idJugadorRepartidor = 0;
    private static int _idUltimoJugadorEnLlevarseLasCartas;

    private AlgoritmoQueGuardaJugadasPosibles _algoritmoQueGuardaJugadasPosibles;

    public ControladorDeJugadasEnTurno(CartasEnMesa cartasEnMesa, Vista vista, Jugadores jugadores)
    {
        _cartasEnMesa = cartasEnMesa;
        _vista = vista;
        _jugadores = jugadores;
        _algoritmoQueGuardaJugadasPosibles = new AlgoritmoQueGuardaJugadasPosibles(_cartasEnMesa);
    }
    
    public int IdUltimoJugadorEnLlevarseLasCartas
    {
        get { return _idUltimoJugadorEnLlevarseLasCartas;  }
    }
    
    private List<Jugada> ListaJugadasPosibles()
    {
        return _algoritmoQueGuardaJugadasPosibles.ListaDeJugadasPosibles;
    }
    
    public void RevisarSiCartasMesaInicialesSumanQuince()
    {
        CalculaQueCartasSumanQuince(_cartasEnMesa.CartasDeLaMesa, null, false);
        if (CuatroCartasMesaFormanUnaJugada()) { JugarJugadaAlComienzoDeMano(ListaJugadasPosibles()[0]); }
        else if (CuatroCartasMesaFormanDosJugadasDeDosCartas()) { SeJueganDosJugadasAlComienzoDeMano(); }
        ResetearJugadas();
    }

    public void CalculaQueCartasSumanQuince(List<Carta> cartasQuePuedenSumarQuince, Carta cartaQueDebePertenecerAJugada,
        bool debeIncluirCarta)
    {
        _algoritmoQueGuardaJugadasPosibles.CalculaQueCartasSumanQuince(cartasQuePuedenSumarQuince,
            cartaQueDebePertenecerAJugada, debeIncluirCarta);
    }
    
    private bool CuatroCartasMesaFormanUnaJugada()
    {
        if (ListaJugadasPosibles().Count == 1 && ListaJugadasPosibles()[0].HayCuatroCartasEnJugada())
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
    
    private static void GuardarUltimoJugadorEnLlevarseCartas(Jugador jugador)
    {
        _idUltimoJugadorEnLlevarseLasCartas = jugador.Id;
    }
    
    private bool CuatroCartasMesaFormanDosJugadasDeDosCartas()
    {
        if (ListaJugadasPosibles().Count == 2 && AmbasJugadasSonDeDosCartas(ListaJugadasPosibles()))
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
        foreach (var jugada in ListaJugadasPosibles())
        {
            JugarJugadaAlComienzoDeMano(jugada);
        }
    }
    
    public void ResetearJugadas()
    {
        _algoritmoQueGuardaJugadasPosibles.ResetearJugadas();
    }

    public bool HayAlMenosUnaJugadaDisponible()
    {
        if (ListaJugadasPosibles().Count == 0)
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
        jugada = ListaJugadasPosibles()[idJugada];
        return jugada;
    }
    
    private int ObtieneIdJugada()
    {
        int idJugada;
        if (ListaJugadasPosibles().Count == 1) { idJugada = 0; }
        else { idJugada = _vista.PedirJugada(ListaJugadasPosibles()); }

        return idJugada;
    }
    

}