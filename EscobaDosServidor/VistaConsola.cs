namespace Servidor;

public class VistaConsola : Vista
{
    protected override void Escribir(string mensaje) => Console.Write(mensaje);
    protected override string LeerLinea() => Console.ReadLine();
}