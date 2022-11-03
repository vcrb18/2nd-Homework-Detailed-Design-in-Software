namespace Servidor;

public class Juego
{
    private const int NumJugadores = 2;
    // private const int NumCartasMesaInicial = 4;

    private static int _target = 15;
    private static Jugadores _jugadores;
    private static int _idJugadorTurno = 0;
    private static int _idUltimoJugadorEnLlevarseLasCartas;
    private static int _idJugadorRepartidor = 0;
    private int _idJugadorPartidor = 1;
    private static MazoCartas _mazoCartas;
    private static CartasEnMesa _cartasEnMesa;
    private static Vista _vista = new Vista();
    private List<Jugada> _listaDeJugadasPosibles = new List<Jugada>();

    public Juego()
    {
        CrearJugadores();
        CrearMazo();
        BarajarMazo();
        RepartirCartas();
        PonerMesa();
    }
    
    public static Juego CrearConJugadorAleatorio() => new Juego();

    private void CrearJugadores() => _jugadores = new Jugadores(NumJugadores);
    private void CrearMazo() => _mazoCartas = new MazoCartas();
    private void BarajarMazo() => _mazoCartas.BarajarCartas();
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
            
            ChequeaCasoBorde();
            _vista.MostrarInfoInicial(_idJugadorRepartidor, _idJugadorPartidor);
            while (!EsFinMazoYManos())
            {
                _mazoCartas.CuantasCartasQuedan();
                JugarTurno();
                CambiarTurno();
            }
            // break;
            if (_mazoCartas.SeAcabaronLasCartas() && _jugadores.ManosVacias())
            {
                NuevoJuego();
            }
        }
    }

    private void ChequeaCasoBorde()
    {
        sum_up_casoBorde(_cartasEnMesa.CartasDeLaMesa, _target);
        if (_listaDeJugadasPosibles.Count == 1)
        {
            // Necesito que sean las 4 cartas
            if (HayCUatroCartasEnJugada(_listaDeJugadasPosibles[0]))
            {
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[0]);
                ResetearJugadas();
            }
        }
        else if (_listaDeJugadasPosibles.Count == 2)
        {
            // Necesito que cada jugada sean 2 cartas
            if (AmbasJugadasSonDeDosCartas(_listaDeJugadasPosibles))
            {
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[0]);
                JugarJugadaCasoBorde(_listaDeJugadasPosibles[1]);
                ResetearJugadas();

            }
        }

    }
    
    // Siguientes tres metodos pasarlos a Jugada.cs
    private bool HayCUatroCartasEnJugada(Jugada jugada)
    {
        if (jugada.NumeroDeCartasDeJugada == 4)
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

    private void JugarJugadaCasoBorde(Jugada jugada)
    {
        _vista.HayUnaODosEscobasAlComienzo();   
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorRepartidor);
        GuardarUltimoJugadorEnLlevarseCartas(jugador);
        jugador.AgregarJugada(jugada);
        _cartasEnMesa.SacarCartas(jugada.CartasQueFormanJugada);
        _vista.JugadorSeLlevaLasCartas(jugador, jugada);
        _vista.MostrarEscoba(jugador);
    }

    // Arreglar funcion
    public bool EsFinJuego()
    {
        if (AlgunJugadorGanoElJuego())
        {
            _vista.FinalDePartida();
            List<Jugador> listaJugadoresGanadores = _jugadores.ObtenerListaJugadoresGanadores();
            if (listaJugadoresGanadores.Count == 1)
            {
                int idJugadorGanador = _jugadores.ObtenerIdGanador(listaJugadoresGanadores);
                Jugador jugadorGanador = _jugadores.ObtenerJugador(idJugadorGanador);
                _vista.GanaUnJugador(jugadorGanador);
            }
            else if (listaJugadoresGanadores.Count == 2)
            {
                int idJugadorGanadorUno = _jugadores.ObtenerIdGanador(listaJugadoresGanadores);
                Jugador jugadorGanadorUno = _jugadores.ObtenerJugador(idJugadorGanadorUno);
                
                int idJugadorGanadorDos = _jugadores.ObtenerIdGanadorDos(listaJugadoresGanadores);
                Jugador jugadorGanadorDos = _jugadores.ObtenerJugador(idJugadorGanadorDos);
                
                _vista.HuboUnEmpate(jugadorGanadorUno, jugadorGanadorDos);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AlgunJugadorGanoElJuego()
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


    public bool EsFinMazoYManos()
    {
        if (_mazoCartas.SeAcabaronLasCartas())
        {
            if (_jugadores.ManosVacias())
            {
                // Jugador jugadorEnLlevarseLasCartas = _jugadores.ObtenerJugador(_idUltimoJugadorEnLlevarseLasCartas);
                // Console.WriteLine($"Jugadas pre Ultima jugada jugador {jugadorEnLlevarseLasCartas._id}: {jugadorEnLlevarseLasCartas.NumeroDeJugadas()}");
                // Console.WriteLine($"VEMOS LA ULTIMA JUGADA: {jugadorEnLlevarseLasCartas.ListaDeJugadas[jugadorEnLlevarseLasCartas.ListaDeJugadas.Count - 1]}");
                // Console.WriteLine($"Jugadas post Ultima jugada jugador {jugadorEnLlevarseLasCartas._id}: {jugadorEnLlevarseLasCartas.NumeroDeJugadas()}");
                // Console.WriteLine($"VEMOS LA ULTIMA JUGADA: {jugadorEnLlevarseLasCartas.ListaDeJugadas[jugadorEnLlevarseLasCartas.ListaDeJugadas.Count - 1]}");
                UltimaJugadaDelMazo();
                _vista.CartasGanadasEnEstaRonda(_jugadores);
                _jugadores.CalcularPuntajes();
                _vista.TotalPuntosGanadosJugadores(_jugadores);
                return true;
            }
            else
            {
                return false;
            }

        }
        else
        {
            return false;
        }
    }

    public void NuevoJuego()
    {
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
        Carta cartaAJugar = _vista.MostrarManoJugador(jugador);
        ResetearJugadas();
        JugarTurnoJugador(cartaAJugar);
        
    }


    private void JugarTurnoJugador(Carta cartaAJugar)
    {
        CalcularJugadas(cartaAJugar);
        if (_listaDeJugadasPosibles.Count == 0)
        {
            // No hay escoba
            BajarCarta(cartaAJugar);
            _vista.NoHayEscoba();
        }
        else if (_listaDeJugadasPosibles.Count == 1)
        {
            BajarCarta(cartaAJugar);
            JugarJugada(_listaDeJugadasPosibles[0]);
        }
        else
        {
            BajarCarta(cartaAJugar);
            int idJugada = _vista.PedirJugada(_listaDeJugadasPosibles);
            JugarJugada(_listaDeJugadasPosibles[idJugada]);
        }
    }

    private void BajarCarta(Carta carta)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        jugador.SacarCartaDeMano(carta);
        _cartasEnMesa.AgregarCarta(carta);
    }

    private void JugarJugada(Jugada jugada)
    {
        Jugador jugador = _jugadores.ObtenerJugador(_idJugadorTurno);
        GuardarUltimoJugadorEnLlevarseCartas(jugador);
        jugador.AgregarJugada(jugada);
        _cartasEnMesa.SacarCartas(jugada.CartasQueFormanJugada);
        _vista.JugadorSeLlevaLasCartas(jugador, jugada);
        _vista.MostrarEscoba(jugador);
    }

    private void CalcularJugadas(Carta cartaAJugar)
    {
        List<Jugada> jugadasPosibles = new List<Jugada>();
        sum_up(CartasQuePuedenSumarQuince(cartaAJugar), _target, cartaAJugar);
        
        // List<Carta> cartasQuePuedenSumarQuince = CartasQuePuedenSumarQuince(cartaAJugar);
        // List<int> C = new List<int>();
        // C.Add(3);
        // C.Add(2);
        // C.Add(5);
        // C.Add(7);
        // C.Add(8);
        // // sum_up(C, target);
        // // OBTENER CARTAS DE LAS LISTAS DE INTS QUE SUMAN 15
        // // CREAR JUGADAS
        
        // return jugadasPosibles;
    }

    private List<Carta> CartasQuePuedenSumarQuince(Carta cartaAJugar)
    {
        // int valorCartaAJugar = Convert.ToInt32(cartaAJugar.Valor);
        List<Carta> cartasQuePuedenSumarQuince = new List<Carta>();
        cartasQuePuedenSumarQuince.Add(cartaAJugar);
        foreach (var carta in _cartasEnMesa.CartasDeLaMesa)
        {
            cartasQuePuedenSumarQuince.Add(carta);
        }
        return cartasQuePuedenSumarQuince;
    }

    private void sum_up_casoBorde(List<Carta> numbers, int target)
    {
        sum_up_recursive_casoBorde(numbers, target, new List<Carta>());
    }

    private void sum_up_recursive_casoBorde(List<Carta> numbers, int target, List<Carta> partial)
    {
        int s = 0;
        foreach (Carta x in partial) s += x.ConvierteValorAInt();

        if (s == target)
            // Console.WriteLine(partial.ToArray());
            // Console.WriteLine("sum(" + string.Join(",", partial.ToArray()) + ")=" + target);
            // Console.WriteLine("sum(" + string.Join(",", partial) + ")=" + target);
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
    
    private void sum_up(List<Carta> numbers, int target, Carta cartaObligatoria)
    {
        sum_up_recursive(numbers, target, new List<Carta>(), cartaObligatoria);
    }

    private void sum_up_recursive(List<Carta> numbers, int target, List<Carta> partial, Carta cartaObligatoria)
    {
        int s = 0;
        foreach (Carta x in partial) s += x.ConvierteValorAInt();

        if (s == target)
            // Console.WriteLine(partial.ToArray());
            // Console.WriteLine("sum(" + string.Join(",", partial.ToArray()) + ")=" + target);
            // Console.WriteLine("sum(" + string.Join(",", partial) + ")=" + target);

            if (partial.Contains(cartaObligatoria))
            {
                // Console.WriteLine($"De antes habian {_listaDeJugadasPosibles.Count} Jugadas");
                // Console.WriteLine($"Jugada a guardar");
                // Console.WriteLine("sum(" + string.Join(",", partial) + ")=" + target);
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
        Jugada jugadaPosible = new Jugada(cartasQueSumanQuince, true);
        _listaDeJugadasPosibles.Add(jugadaPosible);
    }

    private void ResetearJugadas()
    {
        _listaDeJugadasPosibles = new List<Jugada>();
    }

    private void SiNoTienenCartasSeReparte()
    {
        if (_jugadores.ManosVacias())
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

    private static void UltimaJugadaDelMazo()
    {
        Jugador jugadorEnLlevarseLasCartas = _jugadores.ObtenerJugador(_idUltimoJugadorEnLlevarseLasCartas);
        List<Carta> cartasMesa = _cartasEnMesa.CartasDeLaMesa;
        
        Jugada cartasSobrantes = new Jugada(cartasMesa, false);

        jugadorEnLlevarseLasCartas.AgregarJugada(cartasSobrantes);
        _vista.SeLlevaLasUltimasCartas(jugadorEnLlevarseLasCartas, cartasSobrantes);
        _cartasEnMesa.SacarCartas(cartasSobrantes.CartasQueFormanJugada);
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
        _idUltimoJugadorEnLlevarseLasCartas = jugador._id;
    }
    
    
  
    
}