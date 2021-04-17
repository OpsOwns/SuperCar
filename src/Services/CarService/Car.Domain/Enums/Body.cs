using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SuperCar.Car.Domain.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Body
    {
        Sedan,
        Couple,
        Sports,
        Hatchback,
        Minivan,
        Pickup
    }
}