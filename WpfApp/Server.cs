using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WpfApp
{

    public class Server : INotifyPropertyChanged
    {
        public readonly ReadOnlyObservableCollection<Tuple<string, string>> PublicQuestions;

        public int NumberOfRightAnswers
        {
            get { return _numberOfRightAnswers; }
            set
            {
                _numberOfRightAnswers = value;
                OnPropertyChanged("NumberOfRightAnswers");
            }
        }

        public int NumberOfWrongAnswers
        {
            get { return _numberOfWrongAnswers; }
            set
            {
                _numberOfWrongAnswers = value;
                OnPropertyChanged("NumberOfWrongAnswers");
            }
        }

        public Server()
        {
            PublicQuestions = new ReadOnlyObservableCollection<Tuple<string, string>>(_questions);
            _questions.Add(new Tuple<string, string>("2 + 2", "4"));
            _questions.Add(new Tuple<string, string>("2 * 2", "4"));
            _questions.Add(new Tuple<string, string>("2 - 2", "0"));
            _questions.Add(new Tuple<string, string>("2 / 2", "1"));

            CreateSocket();
            Communication();
        }

        public void Communication()
        {
            try
            {
                _handler = _socket.Accept();
                IsConnected = true;
                _isReady = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void CloseConnection()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
            IsConnected = false;
        }

        public void WaitForAnswer()
        {
            string answer = null;
            byte[] bytes = null;

            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = _handler.Receive(bytes);
                answer += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (answer.IndexOf("<EOF>") > -1)
                {
                    break;
                }
            }
            answer = answer.Substring(0, answer.Length - 5);
            if (answer.Equals(PublicQuestions[_idOfQuestion].Item2))
            {
                NumberOfRightAnswers++;
            }
            else
            {
                NumberOfWrongAnswers++;
            }
            _isReady = true;
        }

        public void SendTest(int index)
        {
            if (IsConnected && _isReady)
            {
                try
                {
                    byte[] msg = Encoding.ASCII.GetBytes(PublicQuestions[index].Item1 + "<EOF>");
                    _handler.Send(msg);
                    _idOfQuestion = index;
                    _isReady = false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            WaitForAnswer();
        }

        public bool IsConnected
        {
            get { return _isConnected; }
            set
            {
                _isConnected = value;
                OnPropertyChanged("IsConnected");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CreateSocket()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            try
            {
                _socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                _socket.Bind(localEndPoint);
                _socket.Listen(1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private const int Port = 11000;
        private bool _isConnected = false;
        private readonly ObservableCollection<Tuple<string, string>> _questions = new ObservableCollection<Tuple<string, string>>();
        private Socket _socket;
        private Socket _handler;
        private int _idOfQuestion = 0;
        private int _numberOfRightAnswers = 0;
        private int _numberOfWrongAnswers = 0;
        private bool _isReady = false;
    }

}