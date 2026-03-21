using FleetManager.Models;
using ReactiveUI;
using System.Reactive;
 
namespace FleetManager.ViewModels;
 
public class VehicleItemViewModel : ViewModelBase
{
    private readonly Vehicle _vehicle;
    public ReactiveCommand<Unit, Unit> RefuelCommand { get; }
    public ReactiveCommand<Unit, Unit> StartTripCommand { get; }
    public ReactiveCommand<Unit, Unit> RepairCommand { get; }
 
    public VehicleItemViewModel(Vehicle vehicle)
    {
        _vehicle = vehicle;
        
        RefuelCommand = ReactiveCommand.Create(
            Refuel,
            this.WhenAnyValue(x => x.CanRefuel)
        );
        
        StartTripCommand = ReactiveCommand.Create(
            StartTrip,
            this.WhenAnyValue(x => x.CanStartTrip)
        );
        
        RepairCommand = ReactiveCommand.Create(
            Repair,
            this.WhenAnyValue(x => x.CanRepair)
        );
    }
 
    public string Name => _vehicle.Name;
    public string LicensePlate => _vehicle.LicensePlate;
    public double FuelLevel => _vehicle.FuelLevel;
    public bool CanRefuel => _vehicle.Status == VehicleStatus.Available || _vehicle.Status == VehicleStatus.Service;
    public bool CanStartTrip => _vehicle.FuelLevel >= 15 && _vehicle.Status != VehicleStatus.Service;
    public bool CanRepair => _vehicle.Status == VehicleStatus.Service;
    

 
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
    
    private void Refuel()
    {
        _vehicle.FuelLevel = 100;

        this.RaisePropertyChanged(nameof(FuelLevel));
        this.RaisePropertyChanged(nameof(FuelColor));
        this.RaisePropertyChanged(nameof(CanStartTrip)); 

    }
    
    private void StartTrip()
    {
        _vehicle.Status = VehicleStatus.InRoute;

        this.RaisePropertyChanged(nameof(StatusText));
        this.RaisePropertyChanged(nameof(StatusColor));
        this.RaisePropertyChanged(nameof(CanRefuel));
        this.RaisePropertyChanged(nameof(CanStartTrip));
    }
    
    private void Repair()
    {
        _vehicle.Status = VehicleStatus.Available;

        this.RaisePropertyChanged(nameof(StatusText));
        this.RaisePropertyChanged(nameof(StatusColor));
        this.RaisePropertyChanged(nameof(CanRepair));
        this.RaisePropertyChanged(nameof(CanRefuel));
        this.RaisePropertyChanged(nameof(CanStartTrip));
    }
}