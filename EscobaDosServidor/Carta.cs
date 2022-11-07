public class Carta
{
    private string _pinta;
    private string _valor;

    public Carta(string pinta, string valor)
    {
        _pinta = pinta;
        _valor = valor;
    }

    public string Pinta
    {
        get { return _pinta; }
    }

    public string Valor
    {
        get { return _valor; }
    }
    
    public override string ToString()
    {
        string descripcionCarta = _valor + "_" + _pinta;
        return descripcionCarta;
    }
    
    public int ConvierteStringValorAInt()
    {
        Dictionary<string, int> diccionarioDeValoresCarta = DiccionarioDeValoresCarta();
        return diccionarioDeValoresCarta[_valor];
    }

    public static Dictionary<string, int> DiccionarioDeValoresCarta()
    {
        Dictionary<string, int> diccionarioDeValoresCarta = new Dictionary<string, int>
        {
            {"1", 1},
            {"2", 2},
            {"3", 3},
            {"4", 4},
            {"5", 5},
            {"6", 6},
            {"7", 7},
            {"Sota", 8},
            {"Caballo", 9},
            {"Rey", 10},
        };
        return diccionarioDeValoresCarta;
    }
    
}