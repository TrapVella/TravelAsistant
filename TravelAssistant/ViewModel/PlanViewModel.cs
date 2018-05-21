using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelAssistant.Models;

namespace TravelAssistant.ViewModel
{
    public class PlanViewModel
    {
        [Required]
        public string StartingPointLat { get; set; }
        [Required]
        public string StartingPointLon { get; set; }
        [Required]
        public string EndingPointLat { get; set; }
        [Required]
        public string EndingPointLon { get; set; }
        public List<string> WaypointsLat { get; set; }
        public List<string> WaypointsLon { get; set; }
        public int TravelPlanId { get; set; }
    }
}