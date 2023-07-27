using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace WpfAppClient
{
    public class ClientLogic : INotifyPropertyChanged
    {
        public ClientLogic()
        {
            StartClient();
            GetQuestion();
        }

        public string CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged("CurrentQuestion");
            }
        }

        public void StartClient()
        {
            try
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, Port);

                _socket = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    _socket.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        _socket.RemoteEndPoint.ToString());
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void GetQuestion()
        {
            byte[] bytes = new byte[1024];
            int bytesRec;
            string question = "";

            while (true)
            {
                bytesRec = _socket.Receive(bytes);
                question += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (question.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            CurrentQuestion = question.Substring(0, question.Length - 5);
        }

        public void SendAnswer(string answer)
        {
            CurrentQuestion = "";
            try
            {
                byte[] msg = Encoding.ASCII.GetBytes(answer + "<EOF>");
                _socket.Send(msg);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            GetQuestion();
        }

        public void CloseConnection()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private const int Port = 11000;
        private Socket _socket;
        private string _currentQuestion = "";
    }
}