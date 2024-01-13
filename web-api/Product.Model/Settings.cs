using Newtonsoft.Json;

namespace Product.Model
{
    public class Settings
    {
        public const string Root = "Settings";

        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; } = string.Empty;
    }
}
