using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Extensions.Configuration;
using ServiceStack;
using Twilio;
using Twilio.Rest.Api.V2010.Account;


//dotnet user-secrets set "authToken" "6338aXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX" --project "fullpath tp csproj file"
//    https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows




namespace WpfMessenger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ViewModel _viewModel;
        private readonly MessageSender _messageSender;
        /// <summary>
        /// MainWindow ctor - pass in IConfigurationRoot so user secrets can be accessed
        /// </summary>
        /// <param name="conf"></param>
        public MainWindow(IConfigurationRoot conf, ViewModel vm, MessageSender ms)
        {
            InitializeComponent();
            _viewModel = vm;
            _messageSender = ms;
            DataContext = _viewModel;
            var accountSid = conf["accountSid"];
        }

        //The selected item changed in the conversations (left panel)
        private void lbConversations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = ((ViewModel)DataContext);
            vm.Filter = (string)e.AddedItems[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Gather the pieces that will be required
            var body = NewMessageTextBox.Text;
            var number = (string)lbConversations.SelectedValue;

            //Add a new Outbound message to Messages
            _viewModel.AddMessage(number, body, Direction.Outbound);
            NewMessageTextBox.Text = String.Empty;

            //Send the Sms message
            _messageSender.SendSms(number, body);
        }
    }

    public class MessageSender
    {
        const string accountSid = "";
        const string authToken = "";

        public MessageSender()
        {
            TwilioClient.Init(accountSid, authToken);
        }
        public void SendSms(string toNumber, string body)
        {
            //Add the country code to the phone number if it is not there
            if (!toNumber.StartsWith("+1"))
                toNumber = $"+1{toNumber}";

            var message = MessageResource.Create(
                body: body,
                from: new Twilio.Types.PhoneNumber("+19704322501"),
                to: new Twilio.Types.PhoneNumber(toNumber)
            );
            Console.WriteLine(message.Sid);
        }
    }
    
}
