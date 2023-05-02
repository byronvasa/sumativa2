using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Solicitar IP del servidor y puerto al que conectarse
            Console.Write("Ingrese la IP del Servidor: ");
            string ip = Console.ReadLine();
            Console.Write("Ingrese el puerto: ");
            int port = int.Parse(Console.ReadLine());

            // Conectar con el servidor
            TcpClient client = new TcpClient(ip, port);
            Console.WriteLine("Conectado al Servidor...");

            // Obtener el stream de entrada y salida de datos del servidor
            NetworkStream stream = client.GetStream();

            while (true)
            {
                // Enviar mensaje al servidor
                Console.Write("Cliente dice: ");
                string mensaje = Console.ReadLine();
                byte[] data = Encoding.ASCII.GetBytes(mensaje);
                stream.Write(data, 0, data.Length);


                if (mensaje == "chao")
                {
                    Console.WriteLine("Desconectado del Servidor...");
                    stream.Close();
                    client.Close();
                    break;
                }
                byte[] buffer = new byte[1024];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                mensaje = Encoding.ASCII.GetString(buffer, 0, bytes);
                Console.WriteLine("Servidor dice: " + mensaje);
            }
        }
    }
}