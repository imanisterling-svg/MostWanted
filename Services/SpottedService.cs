using System.Diagnostics;
using System.Text;
using System.Text.Json;

public class SpottedService
{
    private readonly HttpClient _httpClient;

    public SpottedService()
    {
        _httpClient = new HttpClient();
    }

    public async Task SaveSpottedAsync(Spotted spotted)
    {
        // Save to API (MySQL via PHP)
        Debug.WriteLine($" Spotted Service Running : {spotted.Name}");
        var json = JsonSerializer.Serialize(spotted);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        //Debug.WriteLine(JsonSerializer.Serialize(spotted));

        var response = await _httpClient.PostAsync("https://mostwanted.ikgtsapp.com/api/spotted.php", content);

       // var body = await response.Content.ReadAsStringAsync();
       // Debug.WriteLine(body);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception("Failed to save to server");
        }
    }
}