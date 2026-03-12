using FleetManager.Models;
 
namespace FleetManager.ViewModels;
 
public class VehicleItemViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;
 
    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
    }
 
    public string Name         => _vehicle.Name;
    public string LicensePlate => _vehicle.LicensePlate;
    public double FuelLevel    => _vehicle.FuelLevel;
 
    public string StatusText
    {
        get
        {
            if (_vehicle.Status == VehicleStatus.Available)
                return "Dostępny";
            else if (_vehicle.Status == VehicleStatus.InRoute)
                return "W trasie";
            else if (_vehicle.Status == VehicleStatus.Service)
                return "Serwis";
            else
                return "Nieznany";
        }
    }
 
    public string StatusColor
    {
        get
        {
            if (_vehicle.Status == VehicleStatus.Available)
                return "#4CAF50";
            else if (_vehicle.Status == VehicleStatus.InRoute)
                return "#2196F3";
            else if (_vehicle.Status == VehicleStatus.Service)
                return "#F44336";
            else
                return "#888888";
        }
    }
 
    public string FuelColor
    {
        get
        {
            if (_vehicle.FuelLevel <= 15)
                return "#F44336";
            else if (_vehicle.FuelLevel <= 40)
                return "#FF9800";
            else
                return "#4CAF50";
        }
    }
}