namespace Servidor;

public class Vista
{
    public void EscribeLineas()
    {
        Console.WriteLine("-----------------------------------");
    }
    public void MostrarInfoInicial(int repartidor, int partidor)
    {
        EscribeLineas();
        Console.WriteLine($"El jugador {repartidor} comienza repartiendo cartas y el {partidor} parte jugando.");
    }

    public void MostrarQuienJuega(Jugador jugador)
    {
        EscribeLineas();
        Console.WriteLine($"Juega Jugador {jugador.Id}");
    }

    public void MostrarMesaActual(CartasEnMesa cartasEnMesa)
    {
        List<Carta> listaDeCartasEnLaMesa = cartasEnMesa.CartasDeLaMesa; 
        Console.WriteLine("Mesa actual:");
        for (int i = 1; i < listaDeCartasEnLaMesa.Count + 1; i++)
        {
            Carta carta = listaDeCartasEnLaMesa[i - 1];
            Console.WriteLine($"({i}) {carta.Valor}_{carta.Pinta}");
        }
    }

    // Arreglar!
    public Carta MostrarManoJugador(Jugador jugador)
    {
        Console.WriteLine("\nMano Jugador:");
        List<Carta> manoJugador = jugador.Mano;
        for (int i = 1; i < manoJugador.Count + 1; i++)
        {
            Carta carta = manoJugador[i - 1];
            Console.WriteLine($"({i}) {carta.Valor}_{carta.Pinta}");
        }
        Console.WriteLine("¿Qué carta quieres bajar?");
        Console.WriteLine($"(Ingresa un número entre 1 y {manoJugador.Count}");
        int nrCartaEscogida = PedirJugada(1, manoJugador.Count);
        return manoJugador[nrCartaEscogida - 1];
    }

    public int PedirJugada(int minValue, int maxValue)
    {
        int nrCartaEscogida = PedirNumeroValido(minValue, maxValue);
        return nrCartaEscogida;
    }

    protected virtual int PedirNumeroValido(int minValue, int maxValue)
    {
        int numero;
        bool fuePosibleTransformarElString;
        do
        {
            string? inputUsuario = Console.ReadLine();
            fuePosibleTransformarElString = int.TryParse(inputUsuario, out numero);
        } while (!fuePosibleTransformarElString || numero < minValue || numero > maxValue);

        return numero;
    }

    public int PedirJugada(List<Jugada> jugadas)
    {
        Console.WriteLine($"Hay {jugadas.Count} jugadas en la mesa:");
        for (int i = 1; i < jugadas.Count + 1; i++)
        {
            Console.WriteLine($"{i}- {jugadas[i - 1].ToString()}");
        }

        int idJugada = PedirNumeroValido(1, jugadas.Count);
        return idJugada - 1;
    }


    public void JugadorSeLlevaLasCartas(Jugador jugador ,Jugada jugada)
    {
        // Console.WriteLine($"Jugador {jugador._id} se lleva las siguientes cartas: {MostrarEscoba(jugada.Escoba())}");
        Console.WriteLine($"Jugador {jugador.Id} se lleva las siguientes cartas: {jugada.ToString()}");
        if (jugada.EsEscoba)
        {
            MostrarEscoba(jugador);
        }
    }

    public void MostrarEscoba(Jugador jugador)
    {
        Console.WriteLine($"ESCOBA!************************************************** JUGADOR {jugador.Id}");
    }

    public void NoHayEscoba()
    {
        Console.WriteLine($"Lamentablemente, no existe una combinación de cartas en la mesa que, sumada a la carta bajada, suman 15.");
    }

    public void SeVuelvenARepartirCartas()
    {
        EscribeLineas();
        Console.WriteLine("Los jugadores se quedaron sin cartas");
        Console.WriteLine("Se vuelven a repatir 3 cartas a cada uno");
    }

    public void SeLlevaLasUltimasCartas(Jugador jugador, Jugada jugada)
    {
        EscribeLineas();
        Console.WriteLine($"Se jugaron todas las cartas de la baraja");
        Console.WriteLine($"Las cartas sobrantes en la mesa se las lleva el último jugador que haya logrado llevarse las cartas en su turno");
        Console.WriteLine($"Este es el jugador {jugador.Id}!");
        JugadorSeLlevaLasCartas(jugador, jugada);
    }

    public void CartasGanadasEnEstaRonda(Jugadores jugadores)
    {
        EscribeLineas();
        Console.WriteLine("Cartas ganadas en esta ronda:");
        jugadores.MostrarCartasGanadas();
    }
    
    public void TotalPuntosGanadosJugadores(Jugadores jugadores)
    {
        EscribeLineas();
        Console.WriteLine("Total puntos ganados");
        foreach (var jugador in jugadores.ObtenerJugadores)
        {
            Console.WriteLine($"    Jugador {jugador.Id}: {jugador.Puntaje}");
        }
    }

    public void HayUnaODosEscobasAlComienzo()
    {
        EscribeLineas();
        Console.WriteLine("Las cuatro cartas depositadas sobre la mesa suman exactamente uno o dos grupos de 15");
        Console.WriteLine("Por lo tanto el jugador que reparte las cartas se lleva las cartas para sí");
    }

    public void FinalDePartida()
    {
        EscribeLineas();
        Console.WriteLine("Ha llegado el final de la partida!");
    }

    public void GanaUnJugador(Jugador jugador)
    {
        Console.WriteLine($"El jugador {jugador.Id} GANA LA PARTIDA CON {jugador.Puntaje} PUNTOS");
    }

    public void HuboUnEmpate(Jugador ganadorUno, Jugador ganadorDos)
    {
        Console.WriteLine($"El jugador {ganadorUno.Id} EMPATÓ con el jugador {ganadorDos.Id} con un total de {ganadorUno.Puntaje} Puntos.");
    }

    public static void MostrarJugada(Jugador jugador, Jugada jugada)
    {
        // Console.WriteLine($"    Jugador {jugador._id}:");
        Console.WriteLine($"{jugada}. Es Escoba: {jugada.EsEscoba}");
    }

    public static void EscribeJugador(Jugador jugador)
    {
        Console.WriteLine($"Jugador {jugador.Id}:");
    }
}