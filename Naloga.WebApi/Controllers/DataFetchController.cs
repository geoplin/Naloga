using Microsoft.AspNetCore.Mvc;
using Naloga.Aplication.Interfaces;
using Naloga.Aplication.Services;

namespace Naloga.WebApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DataFetchController : ControllerBase
    {
        private readonly IDataFetchService _dataFetchService;

        public DataFetchController(IDataFetchService dataFetchService)
        {
            _dataFetchService = dataFetchService;
        }

        [HttpGet(Name = "GetDataJsonPlaceholder")]
        public async Task FetchData()
        {
            await _dataFetchService.GetDataAsync(new HttpClient
            {
                BaseAddress = new Uri("https://jsonplaceholder.typicode.com"),
            });
        }
    }
}
