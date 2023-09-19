using MVC_ComponentesCodeFirst.CrossCuting.Logging;
using MVC_ComponentesCodeFirst.Models.Pedidos;
using Newtonsoft.Json;
using System.Text;

namespace MVC_ComponentesCodeFirst.Services.Pedidos;

public class ApiPedidoRepository : IPedidoRepository
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILoggerManager _logger;

    private readonly string _apiUrl;

    public ApiPedidoRepository(IHttpClientFactory httpClientFactory, ILoggerManager logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;

        _apiUrl = "https://rodrigo-webapicomponentes.azurewebsites.net/api";
    }

    public async Task<List<PedidoDto>> AllAsync()
    {
        _logger.LogInfo("Devolviendo la lista de pedidos");

        var pedidos = new List<PedidoDto>();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Pedidos"))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                pedidos = JsonConvert.DeserializeObject<List<PedidoDto>>(apiResponse);
            }
        }

        return pedidos;
    }

    public async Task<PedidoDto?> GetByIdAsync(int id)
    {
        _logger.LogInfo($"Devolviendo el pedido con id = {id}");

        var pedido = new PedidoDto();

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.GetAsync($"{_apiUrl}/Pedidos/{id}"))
            {
                if (response.StatusCode != System.Net.HttpStatusCode.OK) return pedido;

                string apiResponse = await response.Content.ReadAsStringAsync();
                pedido = JsonConvert.DeserializeObject<PedidoDto>(apiResponse);
            }
        }

        return pedido;
    }

    public async Task AddAsync(PedidoDto pedido)
    {
        _logger.LogInfo($"Pedido con id = {pedido.Id} añadido");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PostAsync($"{_apiUrl}/Pedidos", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task UpdateAsync(PedidoDto pedido)
    {
        _logger.LogInfo($"Pedido con id = {pedido.Id} actualizado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(pedido), Encoding.UTF8, "application/json");

            using (var response = await httpClient.PutAsync($"{_apiUrl}/Pedidos", content))
            {

                string apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        _logger.LogInfo($"Pedido con id = {id} eliminado");

        using (var httpClient = _httpClientFactory.CreateClient())
        {
            using (var response = await httpClient.DeleteAsync($"{_apiUrl}/Pedidos/{id}"))
            {
                var apiResponse = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
