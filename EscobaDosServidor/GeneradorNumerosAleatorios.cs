namespace Servidor;

public class GeneradorNumerosAleatorios
{
    private const int RandomSeed = 21;
    private static Random rng = new Random(RandomSeed);

    public int Generar(int rango) => rng.Next(rango);
}