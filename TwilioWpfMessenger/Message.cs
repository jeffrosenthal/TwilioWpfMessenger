using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WpfMessenger
{
    public enum Direction
    {
        Inbound, 
        Outbound
    }
    public class Message: INotifyPropertyChanged
    {
        private string _from;
        private string _messageBody;
        private Direction _direction;

        public String From
        {
            get => _from;
            set
            {
                _from = value;
                NotifyPropertyChanged();
            }
        }
        public String MessageBody
        {
            get => _messageBody;
            set
            {
                _messageBody = value;
                NotifyPropertyChanged();
            }
        }
        public Direction Direction
        {
            get => _direction;
            set
            {
                _direction = value;
                NotifyPropertyChanged();
            }
        }
        public Message(string from, string content, Direction direction)
        {
            From = from;
            MessageBody = content;
            Direction = direction;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}