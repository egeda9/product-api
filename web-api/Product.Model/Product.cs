using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Product.Model
{
    public class Product
    {
        [JsonIgnore]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonIgnore]
        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [XmlIgnore]
        [JsonIgnore]
        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("stockQuantity")]
        public int StockQuantity { get; set; }

        [JsonProperty("manufacturer")]
        public string Manufacturer { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("releaseDate")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("isAvailable")]
        public bool IsAvailable { get; set; }

        [JsonIgnore]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}