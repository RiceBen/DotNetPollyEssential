using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CustomPolicyBuilder
{
    public class ThirdPartyHelper
    {
        private readonly IHttpClientFactory _clientFactory;

        public ThirdPartyHelper(IHttpClientFactory client)
        {
            _clientFactory = client;
        }

        public async Task<Classes> FetchOpenCityData()
        {
            var url = "https://opencity.openpa.opencontent.io/api/opendata/v2/classes";

            var client = _clientFactory.CreateClient("ThirdPartyHelper");

            var response = await client.GetStringAsync(url);

            var resultData = JsonSerializer.Deserialize<Classes>(response);

            return resultData;
        }
    }

    public class Classes
    {
        [JsonPropertyName("classes")]
        public OpenContent[] content { get; set; }
    }

    public class OpenContent
    {
        [JsonPropertyName("name")]
        public string name { get; set; }
        [JsonPropertyName("nameList")]
        public NameList nameList { get; set; }
        [JsonPropertyName("identifier")]
        public string identifier { get; set; }
        [JsonPropertyName("contents")]
        public int content { get; set; }
        [JsonPropertyName("link")]
        public string link { get; set; }
        [JsonPropertyName("seatch")]
        public string search { get; set; }
    }

    public class NameList
    {
        [JsonPropertyName("always-available")]
        public string always_available { get; set; }
        [JsonPropertyName("ita-IT")]
        public string ita_IT { get; set; }
    }
}