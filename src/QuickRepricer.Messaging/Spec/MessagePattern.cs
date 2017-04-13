using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRepricer.Messaging.Spec
{
    public enum MessagePattern
    {
        FireAndForget,
        RequestResponse,
        PublishSubscribe
    }
}
