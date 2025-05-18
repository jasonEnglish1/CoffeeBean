using System.Text.Json.Serialization;

namespace TombolaTest.Models
{    
    public class CoffeeBean
    {
        [JsonPropertyName("_id")]
        public required string Id { get; set; }
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("isBOTD")]
        public bool IsBOTD { get; set; }
        public string Cost { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        [JsonPropertyName("colour")]
        public string Colour { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
    }

}
