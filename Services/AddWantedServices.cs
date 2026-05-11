using MostWanted.Model;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

public class AddWantedService
{
    private readonly HttpClient _httpClient;

    public AddWantedService()
    {
        _httpClient = new HttpClient();
    }

    public async Task Save(WantedPerson wantedPerson)
    {
        // Save to API (MySQL via PHP)
        Debug.WriteLine($" Spotted Service Running : {wantedPerson.Name}");
        var json = JsonSerializer.Serialize(wantedPerson);
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