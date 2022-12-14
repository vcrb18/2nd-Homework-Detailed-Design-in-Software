namespace Servidor;

public abstract class Vista
{
    protected abstract void Escribir(string mensaje);
    protected abstract string LeerLinea();
    public virtual void Cerrar() {}

    public virtual void CambiaIdJugadorTurno(int idJugadorTurno) {}

    protected void EscribirLinea(string mensaje) => Escribir(mensaje + "\n");
    protected void EscribirLinea() => EscribirLinea("");

    protected virtual void EscribirManoJugador(string mensaje) => Escribir(mensaje);
    protected virtual void EscribirManoJugadorLinea(string mensaje) => EscribirManoJugador(mensaje + "\n");
    public void EscribeLineasSeparadoras()
    {
        Escribir("-----------------------------------" + "\n");
    }
    public void MostrarInfoInicial(int repartidor, int partidor)
    {
        EscribeLineasSeparadoras();
        EscribirLinea($"El jugador {repartidor} comienza repartiendo cartas y el {partidor} parte jugando.");
    }

    public void MostrarQuienJuega(Jugador jugador)
    {
        EscribeLineasSeparadoras();
        EscribirLinea($"Juega Jugador {jugador.Id}");
    }

    public void MostrarMesaActual(CartasEnMesa cartasEnMesa)
    {
        List<Carta> listaDeCartasEnLaMesa = cartasEnMesa.CartasDeLaMesa; 
        Escribir("Mesa actual: ");
        for (int i = 1; i < listaDeCartasEnLaMesa.Count + 1; i++)
        {
            Carta carta = listaDeCartasEnLaMesa[i - 1];
            Escribir($" ({i}) {carta} ,");
        }
    }

    public void MostrarManoJugador(Jugador jugador)
    {
        EscribirManoJugador("\nMano Jugador: " );
        for (int i = 1; i < jugador.Mano.Count + 1; i++)
        {
            Carta carta = jugador.Mano[i - 1];
            EscribirManoJugador($" ({i}) {carta} ,");
        }
        EscribirManoJugadorLinea("\n¿Qué carta quieres bajar?");
        EscribirManoJugadorLinea($"(Ingresa un número entre 1 y {jugador.Mano.Count})");
    }
    
    public int PedirJugada(List<Jugada> jugadas)
    {
        EscribirLinea($"Hay {jugadas.Count} jugadas en la mesa:");
        for (int i = 1; i < jugadas.Count + 1; i++)
        {
            EscribirLinea($"{i}- {jugadas[i - 1]}");
        }
        int idJugada = PedirNumeroValido(1, jugadas.Count);
        
        return idJugada - 1;
    }

    public int PedirCarta(int minValue, int maxValue)
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
            string? inputUsuario = LeerLinea();
            fuePosibleTransformarElString = int.TryParse(inputUsuario, out numero);
        } while (!fuePosibleTransformarElString || numero < minValue || numero > maxValue);

        return numero;
    }
    
    public void NoHayJugadaDisponible()
    {
        Escribir($"Lamentablemente, no existe una combinación de cartas en la mesa que, sumada a la carta bajada, suman 15.");
    }

    public void SeVuelvenARepartirCartas()
    {
        EscribeLineasSeparadoras();
        EscribirLinea("Los jugadores se quedaron sin cartas");
        EscribirLinea("Se vuelven a repatir 3 cartas a cada uno");
    }

    public void SeLlevaLasUltimasCartas(Jugador jugador, Jugada jugada)
    {
        EscribeLineasSeparadoras();
        EscribirLinea($"Se jugaron todas las cartas de la baraja");
        EscribirLinea($"Las cartas sobrantes en la mesa se las lleva el último jugador que haya logrado llevarse las cartas en su turno");
        EscribirLinea($"Este es el jugador {jugador.Id}!");
        JugadorSeLlevaLasCartas(jugador, jugada);
    }
    
    public void JugadorSeLlevaLasCartas(Jugador jugador ,Jugada jugada)
    {
        EscribirLinea($"Jugador {jugador.Id} se lleva las siguientes cartas: {jugada}");
        if (jugada.EsEscoba)
        {
            MostrarEscoba(jugador);
        }
    }
    
    public void MostrarEscoba(Jugador jugador)
    {
        Escribir($"ESCOBA!************************************************** JUGADOR {jugador.Id}");
    }

    public void CartasGanadasEnEstaRonda(Jugadores jugadores)
    {
        EscribeLineasSeparadoras();
        EscribirLinea("Cartas ganadas en esta ronda:");
        foreach (var jugador in jugadores.ObtenerJugadores)
        {
            EscribeJugador(jugador);
            foreach (var jugada in jugador.ListaDeJugadas)
            {
                MostrarJugada(jugada);
            }
            EscribirLinea();
        }
    }
    public void EscribeJugador(Jugador jugador)
    {
        Escribir($"Jugador {jugador.Id}: ");
    }
    
    public void MostrarJugada(Jugada jugada)
    {
        Escribir($"{jugada}, ");
    }

    
    public void TotalPuntosGanadosJugadores(Jugadores jugadores)
    {
        EscribeLineasSeparadoras();
        EscribirLinea("Total puntos ganados");
        foreach (var jugador in jugadores.ObtenerJugadores)
        {
            EscribirLinea($"    Jugador {jugador.Id}: {jugador.Puntaje}");
        }
    }

    public void HayUnaODosEscobasAlComienzo()
    {
        EscribeLineasSeparadoras();
        EscribirLinea("Las cuatro cartas depositadas sobre la mesa suman exactamente uno o dos grupos de 15");
        EscribirLinea("Por lo tanto el jugador que reparte las cartas se lleva las cartas para sí");
    }

    public void FinalDePartida()
    {
        EscribeLineasSeparadoras();
        EscribirLinea("Ha llegado el final de la partida!");
    }

    public void GanaUnJugador(Jugador jugador)
    {
        Escribir($"El jugador {jugador.Id} GANA LA PARTIDA CON {jugador.Puntaje} PUNTOS");
    }

    public void HuboUnEmpate(Jugador ganadorUno, Jugador ganadorDos)
    {
        Escribir($"El jugador {ganadorUno.Id} EMPATÓ con el jugador {ganadorDos.Id} con un total de {ganadorUno.Puntaje} Puntos.");
    }

}