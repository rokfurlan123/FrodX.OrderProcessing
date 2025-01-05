using FrodX.OrderProcessing.EFCore.Data;
using Newtonsoft.Json;

namespace FrodX.OrderProcessing.Infrastructure
{
    public static class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<Order>?> FetchApiData(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); 
                string content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Order>>(content);
            }
            catch
            {
                return null;
            }
        }
    }
}
