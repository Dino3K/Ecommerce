

using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Ecommerce.Shared.Entities;
using Microsoft.Maui.Controls.Platform.Compatibility;

namespace Ecommerce.Mobile.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseurl= "https://localhost:7039/api/countries";
        private readonly JsonSerializerOptions _serializerOptions;
        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, SslPolicyErrors) => true
            };
            _httpClient = new HttpClient(handler);
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
            _baseurl = ConfigService.GetConfigService();
            Debug.WriteLine($"URL Configurada: {_baseurl}");
            
        }
        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                Debug.WriteLine($"Realizando Solicitud de Get: {_baseurl}");

                var response = await _httpClient.GetAsync(_baseurl);
                Debug.WriteLine($"Codigo de respuesta: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"URL Configurada: {errorContent}");
                    await Application.Current.MainPage.DisplayAlert("Errir de Api", $"Error al obtener paises. Codigo: {response.StatusCode}", "OK");
                    return new List<Country>();
                }
                var content = await response.Content.ReadAsStringAsync();
                Debug.WriteLine($"Respuesta obtenida: {content}");

                try
                {
                    var result = JsonSerializer.Deserialize<List<Country>>(content);
                    return result ?? new List<Country>();
                }
                catch(JsonException jsonEx)
                {
                    Debug.WriteLine($"Error al Desrializar el Json: {jsonEx.Message}");

                    return new List<Country>();
                }
            }
            catch(HttpRequestException ex)
            {
                Debug.WriteLine($"Error HTTP al obtener paises: {ex.Message}");

                string errorMesage = "Error de conexcion a la API";
                if(ex.Message.Contains("Certificate") || ex.Message.Contains("SSL"))
                {
                    errorMesage = "Errorr de certifdicado SSL. Verificar la configuracion de desarrollo.";

                }
                else if (ex.Message.Contains("connection")) {

                    errorMesage = "NO se pudo conectaer al servidor. Verifica que la api este en ejecuciom";
                }
                await Application.Current.MainPage.DisplayAlert("Error de cioexxion", errorMesage, "OK");
                return new List<Country>();
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error general al obtener paises: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    $"Error inesperado: {ex.Message}",
                    "OK");
                return new List<Country>();


            }

        }
        public async Task <Country>GetCountryAsync (int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseurl}/{id}");
                response.EnsureSuccessStatusCode();
                var conten = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Country>( conten,_serializerOptions);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Error al obener el pais: {ex.Message}");
                return null;
            }
        }


        public async Task<bool> AddcountryAsync(Country country)
        {
            try
            {
                var json = JsonSerializer.Serialize(country, _serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync(_baseurl, content);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Tonto el que nol lo lea (Pais): {ex.Message}");
                return false;
            }
        }
        public async Task <bool> UpdateCountryAsync(Country country)
        {
            try
            {
                var json = JsonSerializer.Serialize(country,_serializerOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"{_baseurl}/{country.Id}", content);
                return response.IsSuccessStatusCode;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Tonto el que lo lea (Pais): {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseurl}/{id}");
                return response.IsSuccessStatusCode;
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al borrar el pais: {ex.Message}");
                return false;
            }
        }

    }
}
