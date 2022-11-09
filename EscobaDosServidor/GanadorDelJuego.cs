namespace Servidor;

public class GanadorDelJuego
{
    private Jugadores _jugadores;
    private Vista _vista;

    public GanadorDelJuego(Jugadores jugadores, Vista vista)
    {
        _jugadores = jugadores;
        _vista = vista;
    }
    
    public void RevisaQuienGanoJuego()
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
}