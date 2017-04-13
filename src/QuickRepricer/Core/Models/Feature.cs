using System;

namespace QuickRepricer.Core.Models
{
    public class Feature
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int PlanId { get; set; }

        //[MaxLength(100)]
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedAt { get; set; }

        //[MaxLength(50)]
        public string CreatedBy { get; set; }
        public DateTime ModifiedAt { get; set; }

        //[MaxLength(50)]
        public string ModifiedBy { get; set; }

        public virtual Plan Plan { get; set; }
    }
}