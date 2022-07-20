using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using LEX_SubscriptionService.Dtos;
using LEX_SubscriptionService.Models.Authenticate;
using System.Net.Http.Headers;
using LEX_SubscriptionService.Models;

namespace LEX_SubscriptionService.SyncDataServices.Http;

public class HttpCommandDataClient : ICommandDataClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }
    public async Task<HttpResponseMessage> SendSourcesToRequestProcess(string token, IEnumerable<Source> sourceItem)
    {
        var httpContent = new StringContent(JsonSerializer.Serialize(sourceItem),
            Encoding.UTF8, "application/json");
        
        _httpClient.DefaultRequestHeaders.Add("ServiceKey", _configuration["ServiceKey"]);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.PostAsync($"{_configuration["RequestProcessService"]}", httpContent);
        Console.WriteLine($"--> SendSourcesToRequestProcess -- Response: {response.Content.ReadAsStringAsync().Result}");

        if(response.IsSuccessStatusCode)
        {
            
            Console.WriteLine("--> Sync POST to RequestProcess Service was OK!");
            return response;
        }
        else
        {
            Console.WriteLine("--> Sync POST to RequestProcess Service was NOT OK!");
            return null;
        }
    } 
}