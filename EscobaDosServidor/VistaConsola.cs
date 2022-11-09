namespace Servidor;

public class VistaConsola : Vista
{
    // public override void CambiaIdJugadorTurno()
    // {
    //     throw new NotImplementedException();
    // }

    protected override void Escribir(string mensaje) => Console.Write(mensaje);
    

    protected override string LeerLinea() => Console.ReadLine();
}