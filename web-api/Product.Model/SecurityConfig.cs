using Newtonsoft.Json;

namespace Product.Model
{
    public class SecurityConfig
    {
        public const string Root = "Jwt";

        [JsonProperty("issuer")]
        public string Issuer { get; set; } = string.Empty;

        [JsonProperty("audience")]
        public string Audience { get; set; } = string.Empty;

        [JsonProperty("key")]
        public string Key { get; set; } = string.Empty;
    }
}
