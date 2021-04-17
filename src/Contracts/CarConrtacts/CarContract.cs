namespace SuperCar.Contracts
{
    public enum CarEvents
    {
        VehicleDetailsChanged,
        VehicleRegistered,
        VehicleRemoved
    }
    public class CarContract
    {
        public string Payload { get; set; }
        public string StreamId { get; set; }
        public CarEvents CarEvent { get; set; }
    }
}
