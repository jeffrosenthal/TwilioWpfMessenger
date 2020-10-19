using System;
using ServiceStack;
using Twilio.TwiML;

namespace WpfMessenger
{
    
    [Route("/TwilioCallBack")]
    public class TwilioCallBack { }

    public class TwilioService : Service
    {
        private readonly ViewModel _viewModel;
        public TwilioService(ViewModel vm)
        {
            try
            {
                _viewModel = vm;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public object Any(TwilioCallBack request)
        {
            var body = base.Request.FormData["Body"];
            var msgTo = base.Request.FormData["to"];
            var msgFrom = base.Request.FormData["from"];
            
            _viewModel.AddMessage(msgFrom, body, Direction.Inbound);
            
            return new HttpResult(string.Empty, "text/plain");
        }
    }

    //Define the Web Services AppHost
    
}