using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WpfMessenger
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        private ObservableCollection<string> _conversations = new ObservableCollection<string>();
        
        private string _filter;
        
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Messages Property
        /// </summary>
        public ObservableCollection<Message> Messages
        {
            get
            {
                var results = from message in _messages.ToArray()
                    where message.From == (string.IsNullOrEmpty(Filter) ? message.From : Filter)
                    select message;
                ObservableCollection<Message> filteredMessages = new ObservableCollection<Message>(results);

                return filteredMessages;
            }
        }
        /// <summary>
        /// Conversations Property
        /// </summary>
        public ObservableCollection<string> Conversations
        {
            get
            {
                var results = from conversation in _conversations.ToArray()
                    select conversation;
                return new ObservableCollection<string>(results);
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public ViewModel()
        {
            //Add an empty conversation - used to select all in the filtering
            _conversations.Add($"aaaa");
        }

        public void AddMessage(string from, string body, Direction direction)
        {
            //Add a message
            _messages.Add(new Message($"{from}", body, direction));
            //Add a conversation but do not allow duplicate entries
            if(!Conversations.Contains(from))
                _conversations.Add($"{from}");

            //Notify everyone of the changes
            NotifyPropertyChanged("Messages");
            NotifyPropertyChanged("Conversations");
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}