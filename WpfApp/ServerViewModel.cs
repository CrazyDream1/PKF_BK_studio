using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp
{
    class ServerViewModel : INotifyPropertyChanged
    {
        public ServerViewModel() {
            _server = new Server();
            SendQuestionCommand = new DelegateCommand<int?>(index =>
            {
                if (index.HasValue) _server.SendTest(index.Value);
            });
            _server.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        }
        
        public int NumberOfWrongAnswers => _server.NumberOfWrongAnswers;
        public int NumberOfRightAnswers => _server.NumberOfRightAnswers;

        public ReadOnlyObservableCollection<Tuple<string, string>> Questions => _server.PublicQuestions;
        public bool IsConnected => _server.IsConnected;

        public DelegateCommand<int?> SendQuestionCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Server _server;
    }
}
