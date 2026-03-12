namespace FleetManager.Models;

public enum VehicleStatus
{
    Available,
    InRoute,
    Service
}

public class Vehicle
{
    public string Name { get; set; } = string.Empty;
    public string LicensePlate { get; set; } = string.Empty;
    public double FuelLevel { get; set; }
    public VehicleStatus Status { get; set; }
}