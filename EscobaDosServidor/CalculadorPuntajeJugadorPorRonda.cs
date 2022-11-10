namespace Servidor;

public class CalculadorPuntajeJugadorPorRonda
{
    private int _puntaje = 0;
    private Jugador _jugador;
    private RecuentoPuntosJugador _recuentoPuntosJugador;

    public CalculadorPuntajeJugadorPorRonda(Jugador jugador)
    {
        _jugador = jugador;
        _recuentoPuntosJugador = new RecuentoPuntosJugador(_jugador);
    }

    public int Puntaje
    {
        get { return _puntaje;  }
    }

    public void ReiniciaPuntajeACero()
    {
        _puntaje = 0;
    }

    public void CalculaPuntaje()
    {
        PuntajePorEscoba();
        PuntajePorSieteDeOro();
        PuntajePorMayoriaDeSietes();
        PuntajePorMayoriaDeCartas();
        PuntajePorMayoriaDeOros();
    }
    private void PuntajePorEscoba()
    {
        _puntaje += _recuentoPuntosJugador.NumeroDeEscobas();
    }
     
    private void PuntajePorSieteDeOro()
    {
        if (_recuentoPuntosJugador.TieneSieteDeOro())
        {
            _puntaje++;
        }
    }
    
    private void PuntajePorMayoriaDeSietes()
    {
        if (_recuentoPuntosJugador.TieneDosOMasSietes())
        {
            _puntaje++;
        }

    }

    private void PuntajePorMayoriaDeCartas()
    {
        if (_recuentoPuntosJugador.TieneVeinteOMasCartas())
        {
            _puntaje++;
        }
    }

    private void PuntajePorMayoriaDeOros()
    {
        if (_recuentoPuntosJugador.TieneCincoOMasOros())
        {
            _puntaje++;
        }
    }
    
}