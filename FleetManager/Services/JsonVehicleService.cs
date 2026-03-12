using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using FleetManager.Models;

namespace FleetManager.Services
{
    public class VehicleService : IVehicleService
    {
        private const string FileName = "vehicles.json";
        private readonly JsonSerializerOptions _options = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public async Task<List<Vehicle>> LoadVehiclesAsync()
        {
            try
            {
                if (!File.Exists(FileName))
                {
                    return new List<Vehicle>();
                }

                using FileStream openStream = File.OpenRead(FileName);
                var result = await JsonSerializer.DeserializeAsync<List<Vehicle>>(openStream, _options);
                return result ?? new List<Vehicle>();
            }
            catch
            {
                return new List<Vehicle>();
            }
        }

        public async Task SaveVehiclesAsync(IEnumerable<Vehicle> vehicles)
        {
            try
            {
                using FileStream createStream = File.Create(FileName);
                await JsonSerializer.SerializeAsync(createStream, vehicles, _options);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}