using SkyscannerHoenApi.Models;
using System.Text.Json;

namespace SkyscannerHoenApi.Data
{
    public class DataService
    {
        private readonly List<Hotel> hotels;
        private readonly List<RentalCar> rentalCars;

        public DataService()
        {
            // Get project root path
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var projectPath = Path.Combine(basePath, "..", "..", ".."); // go 3 levels up to project root

            // Build full paths
            var hotelPath = Path.Combine(projectPath, "DataFiles", "hotels.json");
            var rentalCarPath = Path.Combine(projectPath, "DataFiles", "rentalcars.json");

            // Load data
            hotels = LoadData<Hotel>(hotelPath);
            rentalCars = LoadData<RentalCar>(rentalCarPath);
        }

        // Generic method to load JSON data
        private List<T> LoadData<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"❌ File not found: {filePath}");
                return new List<T>();
            }

            var json = File.ReadAllText(filePath);
            Console.WriteLine($"✅ Loaded {typeof(T).Name} data from {filePath}");
            return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
        }

        // Hotels search by city (case-insensitive)
        public List<Hotel> GetHotelsByCity(string city) =>
            hotels.Where(h => h.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();

        // Rental cars search by city (case-insensitive)
        public List<RentalCar> GetCarsByCity(string city) =>
            rentalCars.Where(c => c.City.Equals(city, StringComparison.OrdinalIgnoreCase)).ToList();
    }
}
