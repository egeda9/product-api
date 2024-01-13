using Newtonsoft.Json;

namespace Product.Model.Dto
{
    public class LoginUserDto
    {
        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
