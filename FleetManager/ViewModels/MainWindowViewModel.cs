using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FleetManager.Services;
 
namespace FleetManager.ViewModels;
 
public class MainWindowViewModel : ViewModelBase
{
    
    public ObservableCollection<VehicleItemViewModel> Vehicles { get; }
 
    public MainWindowViewModel()
    {
        Vehicles = new ObservableCollection<VehicleItemViewModel>();
        _ = LoadAsync();
    }
 
    private async Task LoadAsync()
    {
        VehicleService service = new VehicleService();
        var lista = await service.LoadVehiclesAsync();
 
        foreach (var pojazd in lista)
        {
            Vehicles.Add(new VehicleItemViewModel(pojazd));
        }
    }
    
}