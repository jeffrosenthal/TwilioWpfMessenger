using System;
using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace WpfMessenger
{
    public class MessageSender
    {
        private readonly string _phonenumber;

        public MessageSender(IConfigurationRoot conf)
        {
            var accountSid = conf["accountSid"];
            var authToken = conf["authToken"]; 
            
            _phonenumber = conf["phonenumber"];

            TwilioClient.Init(accountSid, authToken);
        }
        public void SendSms(string toNumber, string body)
        {
            try
            {
                //Add the country code to the phone number if it is not there
                if (!toNumber.StartsWith("+1"))
                    toNumber = $"+1{toNumber}";

                var message = MessageResource.Create(
                    body: body,
                    @from: new PhoneNumber(_phonenumber),
                    to: new PhoneNumber(toNumber)
                );
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}