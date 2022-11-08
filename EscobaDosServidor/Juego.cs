namespace Servidor;

public class Juego
{
    private const int NumJugadores = 2;
    private static int _target = 15;
    private static Jugadores _jugadores;
    private static int _idJugadorTurno = 0;
    private static int _idUltimoJugadorEnLlevarseLasCartas;
    private static int _idJugadorRepartidor = 0;
    private int _idJugadorPartidor = 1;
    private static MazoCartas _mazoCartas;
    private static CartasEnMesa _cartasEnMesa;
    private static Vista _vista;
    private List<Jugada> _listaDeJugadasPosibles = new List<Jugada>();

    public Juego(Vista vista)
    {
        _vista = vista;
        CrearJugadores();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
    }
    
    public static Juego Crear() => new Juego(new Vista());

    public static Juego CrearConJugadorAleatorio() => new Juego(new VistaJugadorAleatorio());

    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores);
    private void CrearMazo() => _mazoCartas = new MazoCartas();
    private void BarajarMazo() => _mazoCartas.AlgoritmoParaBarajarCartas();
    private void RepartirCartas() => _jugadores.RepartirCartas(_mazoCartas);
    private void PonerMesa() => _cartasEnMesa = new CartasEnMesa(_mazoCartas);

    public static int idJugadorTurno
    {
        get { return _idJugadorTurno;  }
    }
    
    public static int idJugadorRepartidor
    {
        get { return _idJugadorRepartidor;  }
    }
    public static int IdUltimoJugadorEnLlevarseLasCartas
    {
        get { return _idUltimoJugadorEnLlevarseLasCartas;  }
    }

    public void Jugar()
    {
        while (!EsFinJuego())
        {
            
            RevisarSiCartasMesaInicialesSumanQuince();
            _vista.MostrarInfoInicial(_idJugadorRepartidor, _idJugadorPartidor);
            while (!EsFinMazoYManos())
            {
                Console.WriteLine(_mazoCartas.CuantasCartasQuedan());
                JugarTurno();
                CambiarTurno();
            }
            if (_mazoCartas.SeAcabaronLasCartas() && _jugadores.AmbosJugadoresTienenManosVacias())
            {
                NuevoJuego();
            }
        }
    }

    private void RevisarSiCartasMesaInicialesSumanQuince()
    {
        sum_up_casoBorde(_cartasEnMesa.CartasDeLaMesa, _target);
        if (_listaDeJugadasPosibles.Count == 1)
        {
            if ((_listaDeJugadasPosibles[0].HayCuatroCartasEnJugada()))
            {
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[0]);
                ResetearJugadas();
            }
        }
        else if (_listaDeJugadasPosibles.Count == 2)
        {
            if (AmbasJugadasSonDeDosCartas(_listaDeJugadasPosibles))
            {
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[0]);
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[1]);
                ResetearJugadas();
            }
        }
    }
    
    // Siguientes dos metodos pasarlos a Jugada.cs
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

    private void JugarJugadaCasoBorde(Jugada jugada)
    {
        _vista.HayUnaODosEscobasAlComienzo();   
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorRepartidor);
        GuardarUltimoJugadorEnLlevarseCartas(jugador);
        jugador.AgregarJugada(jugada);
        _cartasEnMesa.SacarCartasDeLaMesa(jugada.CartasQueFormanJugada);
        _vista.JugadorSeLlevaLasCartas(jugador, jugada);
        _vista.MostrarEscoba(jugador);
    }

    public bool EsFinJuego()
    {
        bool esFinJuego = false;
        if (AlgunJugadorGanoElJuego())
        {
            esFinJuego = true;
            _vista.FinalDePartida();
            RevisaQuienGanoJuego();
        }

        return esFinJuego;
    }
    
    private bool AlgunJugadorGanoElJuego()
    {
        Jugador jugadorUno = _jugadores.ObtenerJugador(0);
        Jugador jugadorDos = _jugadores.ObtenerJugador(1);
        if (jugadorUno.Puntaje >= _target || jugadorDos.Puntaje >= _target)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void RevisaQuienGanoJuego()
    {
        List<Jugador> listaConGanadorOGanadores = _jugadores.GanadorOGanadoresDelJuego();
        if (listaConGanadorOGanadores.Count == 1) { GanoUnJugador(listaConGanadorOGanadores); }
        else if (listaConGanadorOGanadores.Count == 2) { HuboUnEmpate(listaConGanadorOGanadores); }
    }

    private void GanoUnJugador(List<Jugador> listaConJugadorGanador)
    {
        int idJugadorGanador = _jugadores.ObtenerIdPrimerGanador(listaConJugadorGanador);
        Jugador jugadorGanador = _jugadores.ObtenerJugador(idJugadorGanador);
        _vista.GanaUnJugador(jugadorGanador);
    }

    private void HuboUnEmpate(List<Jugador> listaConJugadoresGanadores)
    {
        int idJugadorGanadorUno = _jugadores.ObtenerIdPrimerGanador(listaConJugadoresGanadores);
        Jugador jugadorGanadorUno = _jugadores.ObtenerJugador(idJugadorGanadorUno);
        int idJugadorGanadorDos = _jugadores.ObtenerIdSegundoGanador(listaConJugadoresGanadores);
        Jugador jugadorGanadorDos = _jugadores.ObtenerJugador(idJugadorGanadorDos);
        _vista.HuboUnEmpate(jugadorGanadorUno, jugadorGanadorDos);
    }
    
    public bool EsFinMazoYManos()
    {
        bool esFinMazoYManos = false;
        if (_mazoCartas.SeAcabaronLasCartas() && _jugadores.AmbosJugadoresTienenManosVacias())
        {
            esFinMazoYManos = true;
            UltimaJugadaDelMazo();
            CalculaPuntos();
        }

        return esFinMazoYManos;
    }

    private void CalculaPuntos()
    {
        _jugadores.CalcularPuntajes();
        _vista.CartasGanadasEnEstaRonda(_jugadores);
        _vista.TotalPuntosGanadosJugadores(_jugadores);
    }

    public void NuevoJuego()
    {
        _jugadores.ReiniciarListaJugadas();
        CambiarRepartidorYJugador();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
    }

    public void CambiarRepartidorYJugador()
    {
        CambiarRepartidor();
        CambiarJugador();
    }

    public static void CambiarRepartidor()
    {
        if (_idJugadorRepartidor == 0)
        {
            _idJugadorRepartidor = 1;
        }
        else
        {
            _idJugadorRepartidor = 0;
        }
    }

    public void CambiarJugador()
    {
        if (_idJugadorPartidor == 0)
        {
            _idJugadorPartidor = 1;
            _idJugadorTurno = 1;
        }
        else
        {
            _idJugadorPartidor = 0;
            _idJugadorTurno = 0;
        }
    }
    
    
    private void JugarTurno()
    {
        SiNoTienenCartasSeReparte();
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        _vista.MostrarQuienJuega(jugador);
        _vista.MostrarMesaActual(_cartasEnMesa);
        _vista.MostrarManoJugador(jugador);
        int idJugada = _vista.PedirCarta(1, jugador.Mano.Count);
        Carta cartaAJugar = jugador.Mano[idJugada - 1];
        ResetearJugadas();
        JugarTurnoJugador(cartaAJugar);
    }
    
    private void SiNoTienenCartasSeReparte()
    {
        if (_jugadores.AmbosJugadoresTienenManosVacias())
        {
            if (_mazoCartas.SeAcabaronLasCartas())
            {
                // NUNCA VA A ENTRAR ACAF
                
                // UltimaJugadaDelMazo();
                // NuevoJuego();
            }
            else
            {
                RepartirCartas();
                _vista.SeVuelvenARepartirCartas();
            }
        }
    }

    private void ResetearJugadas()
    {
        _listaDeJugadasPosibles = new List<Jugada>();
    }

    private void JugarTurnoJugador(Carta cartaAJugar)
    {
        CalcularJugadas(cartaAJugar);
        BajarCarta(cartaAJugar);
        RevisarJugadasDisponibles();
    }
    
    private void CalcularJugadas(Carta cartaAJugar)
    {
        List<Jugada> jugadasPosibles = new List<Jugada>();
        sum_up(CartasQuePuedenSumarQuince(cartaAJugar), _target, cartaAJugar);
    }
    
    private List<Carta> CartasQuePuedenSumarQuince(Carta cartaAJugar)
    {
        List<Carta> cartasQuePuedenSumarQuince = new List<Carta>();
        cartasQuePuedenSumarQuince.Add(cartaAJugar);
        foreach (var carta in _cartasEnMesa.CartasDeLaMesa)
        {
            cartasQuePuedenSumarQuince.Add(carta);
        }
        return cartasQuePuedenSumarQuince;
    }
    
    private void sum_up(List<Carta> numbers, int target, Carta cartaObligatoria)
    {
        sum_up_recursive(numbers, target, new List<Carta>(), cartaObligatoria);
    }

    private void sum_up_recursive(List<Carta> numbers, int target, List<Carta> partial, Carta cartaObligatoria)
    {
        int s = 0;
        foreach (Carta x in partial) s += x.ConvierteStringValorAInt();

        if (s == target)
            if (partial.Contains(cartaObligatoria))
            {
                GuardaJugada(partial);
            }
        if (s >= target)
            return;
        for (int i = 0; i < numbers.Count; i++)
        {
            List<Carta> remaining = new List<Carta>();
            Carta n = numbers[i];
            for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

            List<Carta> partial_rec = new List<Carta>(partial);
            partial_rec.Add(n);
            sum_up_recursive(remaining, target, partial_rec, cartaObligatoria);
        }
    }
    
    private void GuardaJugada(List<Carta> cartasQueSumanQuince)
    {
        bool laJugadaEsUnaEscoba = _cartasEnMesa.LaJugadaEsUnaEscoba(cartasQueSumanQuince);
        Jugada jugada = new Jugada(cartasQueSumanQuince, laJugadaEsUnaEscoba);
        _listaDeJugadasPosibles.Add(jugada);
    }

    private void BajarCarta(Carta carta)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        jugador.SacarCartaDeMano(carta);
        _cartasEnMesa.AgregarCarta(carta);
    }

    private void RevisarJugadasDisponibles()
    {
        if (!HayAlMenosUnaJugadaDisponible())
        {
            _vista.NoHayJugadaDisponible();
        }
        else
        {
            JugarJugada(ObtieneJugadaJugador());
        }
    }

    private bool HayAlMenosUnaJugadaDisponible()
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

    private Jugada ObtieneJugadaJugador()
    {
        Jugada jugada;
        if (_listaDeJugadasPosibles.Count == 1)
        {
            jugada = _listaDeJugadasPosibles[0];
        }
        else
        {
            int idJugada = _vista.PedirJugada(_listaDeJugadasPosibles);
            jugada = _listaDeJugadasPosibles[idJugada];
        }

        return jugada;
    }
    
    private void JugarJugada(Jugada jugada)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        GuardarUltimoJugadorEnLlevarseCartas(jugador);
        jugador.AgregarJugada(jugada);
        _cartasEnMesa.SacarCartasDeLaMesa(jugada.CartasQueFormanJugada);
        _vista.JugadorSeLlevaLasCartas(jugador, jugada);
    }
    
    private void sum_up_casoBorde(List<Carta> numbers, int target)
    {
        sum_up_recursive_casoBorde(numbers, target, new List<Carta>());
    }

    private void sum_up_recursive_casoBorde(List<Carta> numbers, int target, List<Carta> partial)
    {
        int s = 0;
        foreach (Carta x in partial) s += x.ConvierteStringValorAInt();

        if (s == target)
            GuardaJugada(partial);

        if (s >= target)
            return;

        for (int i = 0; i < numbers.Count; i++)
        {
            List<Carta> remaining = new List<Carta>();
            Carta n = numbers[i];
            for (int j = i + 1; j < numbers.Count; j++) remaining.Add(numbers[j]);

            List<Carta> partial_rec = new List<Carta>(partial);
            partial_rec.Add(n);
            sum_up_recursive_casoBorde(remaining, target, partial_rec);
        }
    }
    








    private static void UltimaJugadaDelMazo()
    {
        Jugador jugadorEnLlevarseLasCartas = _jugadores.ObtenerJugador(_idUltimoJugadorEnLlevarseLasCartas);
        List<Carta> cartasMesa = _cartasEnMesa.CartasDeLaMesa;
        
        Jugada cartasSobrantes = new Jugada(cartasMesa, false);

        jugadorEnLlevarseLasCartas.AgregarJugada(cartasSobrantes);
        _vista.SeLlevaLasUltimasCartas(jugadorEnLlevarseLasCartas, cartasSobrantes);
        _cartasEnMesa.SacarCartasDeLaMesa(cartasSobrantes.CartasQueFormanJugada);
    }
    
    public static void CambiarTurno()
    {
        if (_idJugadorTurno == 0)
        {
            _idJugadorTurno = 1;
        }
        else
        {
            _idJugadorTurno = 0;
        }
    }

    public static void GuardarUltimoJugadorEnLlevarseCartas(Jugador jugador)
    {
        _idUltimoJugadorEnLlevarseLasCartas = jugador.Id;
    }
    
}