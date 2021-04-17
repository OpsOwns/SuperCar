using System;

namespace SuperCar.Car.API.Contract
{
    public record RegisterVehicleRequest(string VehicleType, string Make, DateTime ProductionYear, string Color,
        string Engine, string Model, string Country, string Fuel, string ImageLink, string Body, int Doors, int Seats,
        bool Trunk);
    public record UpdateDetailsRequest(string Fuel, string ImageLink, string Body, int Doors,
        int Seats, bool Trunk);
}
