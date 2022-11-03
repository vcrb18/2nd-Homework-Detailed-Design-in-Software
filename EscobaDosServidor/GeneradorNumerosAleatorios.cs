namespace Servidor;

//CAMBIADA! Revisar
public class GeneradorNumerosAleatorios
{
    private const int RandomSeed = 1;
    private static Random rng = new Random(RandomSeed);

    public int Generar(int rango) => rng.Next(rango);
}