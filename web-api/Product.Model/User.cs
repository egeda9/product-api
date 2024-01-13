using Newtonsoft.Json;
using System.Xml.Serialization;

namespace Product.Model
{
    public class User
    {
        [JsonIgnore]
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("passwordHash")]
        public string PasswordHash { get; set; }

        [JsonProperty("passwordSalt")]
        public string PasswordSalt { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}
