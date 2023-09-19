using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Componentes;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.Componentes;

public class ApiComponenteRepository : IComponenteRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerManager _logger;

    private readonly string _apiUrl;

    public ApiComponenteRepository(IHttpClientFactory httpClientFactory, ILoggerManager logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;

        _apiUrl = "https://rodrigo-webapicomponentes.azurewebsites.net/api";
    }

    public async Task<List<ComponenteDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de componentes");

        var componentes = new List<ComponenteDto>();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Componentes"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                componentes = JsonConvert.DeserializeObject<List<ComponenteDto>>(apiResponse);
            }
        }

        return componentes;
    }

    public async Task<ComponenteDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el componente con id = {id}");

        var componente = new ComponenteDto();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Componentes/{ id }"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return componente;

                string apiResponse = await response.Content.ReadAsStringAsync();
                componente = JsonConvert.DeserializeObject<ComponenteDto>(apiResponse);
            }
        }

        return componente;
    }

    public async Task AddAsync(ComponenteDto componente)
    {
        _logger.LogInfo($"Componente con id = {componente.Id} añadido");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(componente), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{_apiUrl}/Componentes", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task UpdateAsync(ComponenteDto componente)
    {
        _logger.LogInfo($"Componente con id = {componente.Id} actualizado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(componente), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{_apiUrl}/Componentes", content))
            {
                
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Componente con id = {id} eliminado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.DeleteAsync($"{_apiUrl}/Componentes/{ id }"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
