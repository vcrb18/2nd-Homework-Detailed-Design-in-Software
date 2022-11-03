public class Carta
{
    private string _pinta;
    private string _valor;

    public Carta(string pinta, string valor)
    {
        _pinta = pinta;
        _valor = valor;
    }

    public override string ToString()
    {
        string valorToString = _valor.ToString();
        string descripcionCarta = valorToString + "_" + _pinta;
        return descripcionCarta;
    }

    public string Pinta
    {
        get { return _pinta; }
    }

    public string Valor
    {
        get { return _valor; }
    }
    
    public int ConvierteValorAInt()
    {
        Dictionary<string, int> dictionaryStringToInt = hashValorCarta();
        return dictionaryStringToInt[_valor];
    }

    public static Dictionary<string, int> hashValorCarta()
    {
        Dictionary<string, int> dictionaryStringToInt = new Dictionary<string, int>();
        dictionaryStringToInt.Add("1", 1);
        dictionaryStringToInt.Add("2", 2);
        dictionaryStringToInt.Add("3", 3);
        dictionaryStringToInt.Add("4", 4);
        dictionaryStringToInt.Add("5", 5);
        dictionaryStringToInt.Add("6", 6);
        dictionaryStringToInt.Add("7", 7);
        dictionaryStringToInt.Add("Sota", 8);
        dictionaryStringToInt.Add("Caballo", 9);
        dictionaryStringToInt.Add("Rey", 10);
        return dictionaryStringToInt;
    }
    
}