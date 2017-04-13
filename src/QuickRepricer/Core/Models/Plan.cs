using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuickRepricer.Core.Models
{
    public class Plan
    {
        [NotMapped]
        public string Name { get; set; }

        [NotMapped]
        public int AmountInCents { get; set; }

        [NotMapped]
        public string Currency { get; set; }

        [NotMapped]
        public string Interval { get; set; }

        [NotMapped]
        public int? TrialPeriodDays { get; set; }
        
        public int AmountInDollars
        {
            get
            {
                
                return (int)Math.Floor((decimal)AmountInCents / 100);
            }
        }

        [NotMapped]
        public string Info { get; set; }
        
        public int Id { get; set; }

        [MaxLength(50)]
        public string ExternalId { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        [MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        [MaxLength(50)]
        public string ModifiedBy { get; set; }

        public virtual ICollection<Feature> Features { get; set; }
    }
}
