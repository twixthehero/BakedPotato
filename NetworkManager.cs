using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BakedPotato
{
    class NetworkManager
    {
        private static NetworkManager instance;

        private const string ADDRESS = "potato.redcubed.net";
        private const int PORT = 42424;

        // Signaling object used to notify when an asynchronous operation is completed
        private static ManualResetEvent _clientDone = new ManualResetEvent(false);

        const int TIMEOUT_MILLISECONDS = 5000;

        public bool IsConnected { get; set; } = false;

        private Socket socket;

        public static NetworkManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new NetworkManager();

                return instance;
            }
        }

        public static void DestroyInstance()
        {
            if (instance != null)
            {

            }
        }

        private NetworkManager()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        /*
        public bool IsConnecting
        {
            get
            {
                return _clientDone.
            }
        }
        */
        public string Connect()
        {
            string result = "";

            try
            {
                SocketAsyncEventArgs args = new SocketAsyncEventArgs();
                args.RemoteEndPoint = new DnsEndPoint(ADDRESS, PORT);
                args.Completed += (s, e) =>
                {
                    result = e.SocketError.ToString();

                    
                    _clientDone.Set();
                };

                _clientDone.Reset();

                socket.ConnectAsync(args);

                _clientDone.WaitOne(TIMEOUT_MILLISECONDS);
            }
            catch (Exception e)
            {

            }

            return result;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        public void Send(string text)
        {
            Send(Encoding.UTF8.GetBytes(text));
        }

        public void Send(byte[] data)
        {
            if (!IsConnected) return;

            try
            {

            }
            catch (Exception e)
            {

            }
        }
    }
}
