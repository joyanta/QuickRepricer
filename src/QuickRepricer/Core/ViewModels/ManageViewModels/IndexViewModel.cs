using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace QuickRepricer.Core.ViewModels.ManageViewModels
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public string CustomerId { get; set; }

        public string SubscriptionId {get; set;}

        public string CurrentSubscribedPlan { get; set; }

        public string UserName { get; set; }
    }
}
