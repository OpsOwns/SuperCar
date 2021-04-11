using SuperCar.CarService.Domain.Enums;
using SuperCar.Shared.Domain.Abstraction;
using System;
using System.Collections.Generic;

namespace SuperCar.CarService.Domain.ValueObjects
{
    public class VehicleDescription : ValueObject
    {
        public VehicleType Type { get; }
        public string Make { get; }
        public DateTime ProductionYear { get; }
        public string Color { get; }
        public string Engine { get; }
        public string Model { get; }
        public string Country { get; }
        private VehicleDescription(VehicleType type, string make, DateTime productionYear, string color, string engine, string model, string country)
        {
            Type = type;
            Make = make;
            ProductionYear = productionYear;
            Color = color;
            Engine = engine;
            Model = model;
            Country = country;
        }
        //TODO Validation
        public static VehicleDescription Create(VehicleType type, string make, DateTime productionYear, string color, string engine,
            string model, string country)
        {

            return new VehicleDescription(type, make, productionYear, color, engine, model, country);
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Type;
            yield return Make;
            yield return ProductionYear;
            yield return Color;
            yield return Engine;
            yield return Model;
            yield return Country;
        }
    }
}
