using System.Text.Json.Serialization;

public class WantedPersonDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("imagePath")]
    public string ImagePath { get; set; }
}

public class OnlineResponseDto
{
    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("data")]
    public List<WantedPersonDto> Data { get; set; }
}
