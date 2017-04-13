namespace QuickRepricer.Messages.Commands
{
    public class UnsubscribeCommand
    {
        public string UserName { get; private set; }

        public UnsubscribeCommand(string userName)
        {
            UserName = userName;
        }
    }
}
