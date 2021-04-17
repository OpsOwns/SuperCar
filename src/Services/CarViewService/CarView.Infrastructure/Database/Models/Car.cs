using System;

namespace CarView.Infrastructure.Database.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Make { get; set; }
        public DateTime ProductionYear { get; set; }
        public string Color { get; set; }
        public string Engine { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public string Fuel { get; set; }
        public string ImageLink { get; set; }
        public string Body { get; set; }
        public int Doors { get; set; }
        public int Seats { get; set; }
        public bool Trunk { get; set; }
    }
}
