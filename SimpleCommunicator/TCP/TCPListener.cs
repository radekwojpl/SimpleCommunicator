using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCommunicator.TCP
{
    class TCPListener
    {

        private  TcpListener server = null;
        private NetworkStream stream;
        

        public  void Stop()
        {
            server.Stop();
            stream.Dispose();
            server = null;
            Console.WriteLine("Server Stop");
            
        }
        public  void Start(string Address, int port)
        {
            
            IPAddress localAddres = IPAddress.Parse(Address);
            server = new TcpListener(localAddres, port);

            Byte[] bytes = new Byte[256];
            String data = null;

            server.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also user server.AcceptSocket() here.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                 stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }
            }



        }
    }
}
