using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sumativa2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Establecer el socket TCP en el puerto 2050
            TcpListener servidor = new TcpListener(IPAddress.Any, 2050);
            servidor.Start();
            Console.WriteLine("Servidor iniciado...");

            while (true)
            {
                // Esperar una conexión entrante
                TcpClient client = servidor.AcceptTcpClient();
                Console.WriteLine("Cliente conectado...");

                // Obtener el stream de entrada y salida de datos del cliente
                NetworkStream stream = client.GetStream();

                while (true)
                {
                    // Recibir el mensaje enviado por el cliente
                    byte[] buffer = new byte[1024];
                    int bytes = stream.Read(buffer, 0, buffer.Length);
                    string mensaje = Encoding.ASCII.GetString(buffer, 0, bytes);
                    Console.WriteLine("Cliente dice: " + mensaje);

                    // Si el mensaje es "chao", cerrar la conexión con el cliente
                    if (mensaje == "chao")
                    {
                        Console.WriteLine("Cliente se desconectó...");
                        stream.Close();
                        client.Close();
                        break;
                    }

                    // Enviar mensaje de vuelta al cliente
                    Console.Write("Servidor dice: ");
                    mensaje = Console.ReadLine();
                    byte[] data = Encoding.ASCII.GetBytes(mensaje);
                    stream.Write(data, 0, data.Length);
                }
            }
        }
    }
}