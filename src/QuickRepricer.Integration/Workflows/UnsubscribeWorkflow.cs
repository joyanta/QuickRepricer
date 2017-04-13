using QuickRepricer.Integration.Workflows.Spec;
using System.Threading.Tasks;

namespace QuickRepricer.Integration.Workflows
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
