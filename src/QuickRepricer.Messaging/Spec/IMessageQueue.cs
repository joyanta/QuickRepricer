using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Spec
{
    public interface IMessageQueue : IDisposable
    {
        string Name { get; }

        string Address { get; }

        bool IsTemporary { get; }

        Dictionary<string, string> Properties { get; }

        void InitialiseOutbound(string address, MessagePattern pattern, bool isTemporary);

        void InitialiseInbound(string address, MessagePattern pattern, bool isTemporary);

        void Send(Message message);

        void Listen(Action<Message> onMessageReceived, CancellationToken cancellationToken);

        void Receive(Action<Message> onMessageReceived, int maximumWaitMilliseconds = 0);

        string GetAddress(string name);

        IMessageQueue GetResponseQueue();

        IMessageQueue GetReplyQueue(Message message);

        void DeleteQueue();
    }
}
