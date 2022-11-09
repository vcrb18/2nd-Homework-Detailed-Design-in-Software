using System.Net;
using System.Net.Sockets;

TcpClient client = new TcpClient ();
client.Connect(IPAddress.Loopback, 8001);

NetworkStream ns = client.GetStream();
StreamWriter writer = new StreamWriter(ns);
StreamReader reader = new StreamReader(ns);
int myId = Convert.ToInt32(reader.ReadLine());
int opponentId;
Console.WriteLine($"Eres el jugador {myId}");
if (myId == 0) { opponentId = 1; }
else { opponentId = 0; }

string message = "";
while (message != "[FIN JUEGO]")
{
    message = reader.ReadLine();
    if (message == $"[INGRESE INPUT JUGADOR {myId}]")
    {
        string input = Console.ReadLine();
        writer.WriteLine(input);
        writer.Flush();
    }
    else if (message != "[FIN JUEGO]")
        if (message != $"[INGRESE INPUT JUGADOR {opponentId}]")
        Console.WriteLine(message);
}


client.Close();