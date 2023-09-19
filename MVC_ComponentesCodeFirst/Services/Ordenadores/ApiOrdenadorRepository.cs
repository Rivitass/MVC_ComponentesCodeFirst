using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Ordenadores;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.Ordenadores;

public class ApiOrdenadorRepository : IOrdenadorRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerManager _logger;

    private readonly string _apiUrl;

    public ApiOrdenadorRepository(IHttpClientFactory httpClientFactory, ILoggerManager logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;

        _apiUrl = "https://rodrigo-webapicomponentes.azurewebsites.net/api";
    }

    public async Task<List<OrdenadorDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de ordenadores");

        var ordenadores = new List<OrdenadorDto>();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Ordenadores"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                ordenadores = JsonConvert.DeserializeObject<List<OrdenadorDto>>(apiResponse);
            }
        }

        return ordenadores;
    }

    public async Task<OrdenadorDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el ordenador con id = {id}");

        var ordenador = new OrdenadorDto();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Ordenadores/{id}"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return ordenador;

                string apiResponse = await response.Content.ReadAsStringAsync();
                ordenador = JsonConvert.DeserializeObject<OrdenadorDto>(apiResponse);
            }
        }

        return ordenador;
    }

    public async Task AddAsync(OrdenadorDto ordenador)
    {
        _logger.LogInfo($"Ordenador con id = {ordenador.Id} añadido");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ordenador), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{_apiUrl}/Ordenadores", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task UpdateAsync(OrdenadorDto ordenador)
    {
        _logger.LogInfo($"Ordenador con id = {ordenador.Id} actualizado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(ordenador), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{_apiUrl}/Ordenadores", content))
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Ordenador con id = {id} eliminado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.DeleteAsync($"{_apiUrl}/Ordenadores/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
