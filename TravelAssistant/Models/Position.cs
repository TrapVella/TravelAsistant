using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAssistant.Models
{
    public enum PositionType
    {
        Start = 0,
        End = 1,
        WayPoint = 2
    }
    public class Position
    {
        public int PositionId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public PositionType PositionType { get; set; }
        [ForeignKey("TravelPlan")]
        public int TravelPlanId { get; set; }
        public TravelPlan TravelPlan { get; set; }
    }
}