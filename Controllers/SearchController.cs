using Microsoft.AspNetCore.Mvc;
using SkyscannerHoenApi.Data;

namespace SkyscannerHoenApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly DataService _dataService;

        public SearchController()
        {
            _dataService = new DataService();
        }

        // GET: api/search?city=Lilycove
        [HttpGet]
        public IActionResult Search([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city))
                return BadRequest("City parameter is required.");

            var hotels = _dataService.GetHotelsByCity(city);
            var cars = _dataService.GetCarsByCity(city);

            return Ok(new
            {
                Hotels = hotels,
                RentalCars = cars
            });
        }
    }
}
