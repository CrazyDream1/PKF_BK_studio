using Prism.Commands;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfAppClient
{
    class ClientViewModel : INotifyPropertyChanged
    {
        public ClientViewModel() 
        {
            _clientLogic = new ClientLogic();

            SendAnswerCommand = new DelegateCommand<string>(str =>
            {
                _clientLogic.SendAnswer(str);
            });
            _clientLogic.PropertyChanged += (s, e) => { OnPropertyChanged(e.PropertyName); };
        }

        public string CurrentQuestion => _clientLogic.CurrentQuestion;

        public DelegateCommand<string> SendAnswerCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private ClientLogic _clientLogic;
    }
}
