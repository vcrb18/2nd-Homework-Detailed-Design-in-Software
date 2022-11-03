using Servidor;

namespace EscobaDos.Tests;

public class JuegoTests
{
    // [Fact]
    // public void Jugar_DebeLograrCompletarUnJuegoSinCaerse()
    // {
    //     // Arrange
    //     Juego juego = Juego.CrearConJugadorAleatorio();
    //     
    //     // Act
    //     juego.Jugar();
    //     
    //     // Assert
    //
    // }

    [Fact]
    public void CambiarTurno_CambiaElIdJugadorTurno()
    {
        int idJugadorTurno = Juego.idJugadorTurno;

        Juego.CambiarTurno();
        int nuevoIdJugadorTurno = Juego.idJugadorTurno;

        Assert.NotEqual(nuevoIdJugadorTurno, idJugadorTurno);
        
    }
    
    [Fact]
    public void CambiarTurno_CambiarDosVecesVuelveAlOriginal()
    {
        int idJugadorTurno = Juego.idJugadorTurno;

        Juego.CambiarTurno();
        Juego.CambiarTurno();
        int nuevoIdJugadorTurno = Juego.idJugadorTurno;

        Assert.Equal(nuevoIdJugadorTurno, idJugadorTurno);
        
    }

    [Fact]
    public void GuardarUltimoJugadorEnLlevarseCartas_GuardaIdCorrectamente()
    {
        Jugador jugador = new Jugador(0);
        int idJugador = jugador._id;
        
        Juego.GuardarUltimoJugadorEnLlevarseCartas(jugador);
        int idJugadorEnLlevarseLasCartas = Juego.IdUltimoJugadorEnLlevarseLasCartas;
        
        Assert.Equal(idJugador, idJugadorEnLlevarseLasCartas);
    }

    [Fact]
    public void CambiarRepartidor_IdCambiaDeCeroAUno()
    {
        int idJugadorRepartidor = Juego.idJugadorRepartidor;
        
        Juego.CambiarRepartidor();
        int nuevoIdJugadorRepartidor = Juego.idJugadorRepartidor;
        
        Assert.NotEqual(idJugadorRepartidor, nuevoIdJugadorRepartidor);
    }
    
    [Fact]
    public void CambiarRepartidor_CambiarDosVecesVuelveAlMismoJugador()
    {
        int idJugadorRepartidor = Juego.idJugadorRepartidor;
        
        Juego.CambiarRepartidor();
        Juego.CambiarRepartidor();

        int nuevoIdJugadorRepartidor = Juego.idJugadorRepartidor;
        
        Assert.Equal(idJugadorRepartidor, nuevoIdJugadorRepartidor);
    }

    [Fact]
    public void AlgunJugadorGanoElJuego_InicialmenteNadieHaGanado()
    {
        Juego juego = new Juego();
        bool algunJugadorGanoElJuego = juego.AlgunJugadorGanoElJuego();
        
        Assert.False(algunJugadorGanoElJuego);
    }
    
    
}