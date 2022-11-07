namespace EscobaDos.Tests;

public class CartaTests
{
    [Theory]
    [InlineData("1", 1)]
    [InlineData("2", 2)]
    [InlineData("3", 3)]
    [InlineData("4", 4)]
    [InlineData("5", 5)]
    [InlineData("6", 6)]
    [InlineData("7", 7)]
    [InlineData("Sota", 8)]
    [InlineData("Caballo", 9)]
    [InlineData("Rey", 10)]
    public void ConvierteStringValorAInt_StringDelValorDebeConvertirse(string valor, int expected)
    {
        // Arrange
        Carta carta = new Carta("oro", valor);
        int valorEsperado = expected;
        // Act
        int valorNuevo = carta.ConvierteStringValorAInt();
        // Assert
        Assert.Equal(valorNuevo, valorEsperado);
    }

    [Fact]
    public void ToString_ContieneValorYPintaDeLaCarta()
    {
        Carta carta = new Carta("Oro", "1");

        string cartaToString = carta.ToString();
        string valorCarta = carta.Valor;
        string pintaCarta = carta.Pinta;
        bool cartaToStringContieneValorCarta = cartaToString.Contains(valorCarta);
        bool cartaToStringContienePintaCarta = cartaToString.Contains(pintaCarta);
        
        Assert.True(cartaToStringContieneValorCarta);
        Assert.True(cartaToStringContienePintaCarta);

    }

    
}