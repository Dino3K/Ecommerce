

namespace Ecommerce.Mobile.Services
{
    public static class ConfigService
    {
        public static string GetConfigService()
        {
#if ANDROID
            return "https://10.0.2.2:7039/api/countries";

#else

            return "https://10.0.2.2:7039/api/countries";

#endif
        }
    }
}
