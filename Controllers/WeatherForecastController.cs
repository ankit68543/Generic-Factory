using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IFactoryService<IService, string> _factoryService;

        public WeatherForecastController(IFactoryService<IService, string> factoryService)
        {
            _factoryService = factoryService;
        }

        [HttpGet]
        public string Get()
        {
            // Some Condition
            var key = nameof(ServiceA);

            var value = this._factoryService.Create(key).GetMessage();

            return value;
        }
    }
}
