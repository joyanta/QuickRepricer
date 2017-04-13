using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace QuickRepricer.Core.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //[MaxLength(500)]
        public string StripeCustomerId { get; set; }

        public string StripeSubscriptionId { get; set; }

        public DateTime ActiveUntil { get; set; }

        public DateTime? CreditCardExpires { get; set; }
        
        public int? PlanId { get; set; }

        public int? MerchantId { get; set; }
    }
}
