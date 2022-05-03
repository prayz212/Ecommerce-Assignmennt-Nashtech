using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace CustomerSite.Utils
{
    public static class DeserializeExtensions
    {
        private static JsonSerializerOptions serializerCamelCaseSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static async Task<T> DeserializeToCamelCaseAsync<T>(this Stream json)
        {
            return await JsonSerializer.DeserializeAsync<T>(json, serializerCamelCaseSettings);
        }
    }
}