namespace Naloga.Aplication.Interfaces
{
    public interface IDataFetchService
    {
        Task GetDataAsync(HttpClient httpClient, bool periodic = true);
    }
}
