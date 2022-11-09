using System.Net;
using System.Net.Sockets;

namespace Servidor;

public class VistaSocket : Vista
{
    private TcpListener _listener;
    private TcpClient _client;
    private StreamReader _reader;
    private StreamWriter _writer;
    private TcpClient _clientDos;
    private StreamReader _readerDos;
    private StreamWriter _writerDos;
    private int _idJugadorTurno = 1;
    private int _idJugadorUno = 0;
    private int _idJugadorDos = 1;
    
    public VistaSocket()
    {
        _listener = new TcpListener(IPAddress.Loopback, 8001);
        _listener.Start();
        
        AceptarPrimerCliente();
        AceptarSegundoCliente();
    }

    private void AceptarPrimerCliente()
    {
        _client = _listener.AcceptTcpClient();
        _reader = new StreamReader(_client.GetStream());
        _writer = new StreamWriter(_client.GetStream());
        _writer.WriteLine(_idJugadorUno);
        _writer.Flush();
    }

    private void AceptarSegundoCliente()
    {
        _clientDos = _listener.AcceptTcpClient();
        _readerDos = new StreamReader(_clientDos.GetStream());
        _writerDos = new StreamWriter(_clientDos.GetStream());
        _writerDos.WriteLine(_idJugadorDos);
        _writerDos.Flush();
    }

    public override void CambiaIdJugadorTurno(int idJugadorTurno)
    {
        _idJugadorTurno = idJugadorTurno;
    }


    protected override void Escribir(string mensaje)
    {
        _writer.WriteLine(mensaje);
        _writer.Flush();
        _writerDos.WriteLine(mensaje);
        _writerDos.Flush();
    }

    protected override string LeerLinea()
    {
        EscribirLinea($"[INGRESE INPUT JUGADOR {_idJugadorTurno}]");
        if (_idJugadorTurno == 0)
        {
            return _reader.ReadLine();
        }
        else
        {
            return _readerDos.ReadLine();
        }
    }

    public override void Cerrar()
    {
        EscribirLinea("[FIN JUEGO]");
        _client.Close();
        _clientDos.Close();
        _listener.Stop();
    }
}