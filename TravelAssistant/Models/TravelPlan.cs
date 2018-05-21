using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelAssistant.Models
{
    public class TravelPlan
    {
        public int TravelPlanId { get; set; }
        public string Name { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Position> Positions { get; set; }
    }
}