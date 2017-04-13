using QuickRepricer.Mws.Workflows.Spec;

namespace QuickRepricer.Mws.Workflows
{
    public class UnsubscribeWorkflow : IWorkflow
    {
        private readonly string _userName;
      
        public UnsubscribeWorkflow(string userName)
        {
            _userName = userName;
        }

        public void Run()
        {
            UnsubscribedAsync();
            SendNotificationEvent();
        }

        private void UnsubscribedAsync()
        {

        }


        private void SendNotificationEvent()
        {

        }
        
    }
}
