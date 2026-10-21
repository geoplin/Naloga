using Naloga.Aplication.Interfaces;
using System.Diagnostics;
using System.Net;

namespace Naloga.Aplication.Services
{
    public class DataFetchService : IDataFetchService
    {
        private readonly ILogger _logger;
        private int retryCount = 0;

        public DataFetchService(ILogger logger)
        {
            this._logger = logger;
        }
        
        public async Task GetDataAsync(HttpClient httpClient, bool periodic = true)
        {
            CancellationTokenSource cts = new();
            PeriodicTimer timer = new(TimeSpan.FromMilliseconds(1)); ;

            while (await timer.WaitForNextTickAsync(cts.Token))
            {

                using HttpResponseMessage response = await httpClient.GetAsync("/posts");

                if (!response.EnsureSuccessStatusCode().Equals(HttpStatusCode.OK) && retryCount < 5)
                {
                    _logger.LogError("Failed to fetch data from json placeholder");
                    retryCount++;
                    continue;
                }

                var jsonResponse = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Fetched data from json placeholder: {jsonResponse}");

                if (timer.Period == TimeSpan.FromMilliseconds(1))
                {
                    timer = new(TimeSpan.FromSeconds(900));
                }
            }
        }
    }
}
