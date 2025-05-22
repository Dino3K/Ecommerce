

using System.Diagnostics;
using System.Text.Json;

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
            Debug.WriteLine("");
            
        }
    }
}
