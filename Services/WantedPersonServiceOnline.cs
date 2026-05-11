using MostWanted.Model;
using SQLite;
using System.Text.Json;
using System.Diagnostics;
using System.Text;

public class WantedPersonServiceOnline
{
    private readonly SQLiteConnection conn;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _addUrl = "https://mostwanted.ikgtsapp.com/api/addwanted.php";

    public string StatusMessage { get; private set; }

    public WantedPersonServiceOnline(string dbPath)
    {
        conn = new SQLiteConnection(dbPath);
        conn.CreateTable<WantedPerson>();
    }

    public static class WantedPersonMapper
    {
        public static WantedPerson ToDomain(WantedPersonDto dto)
        {
            return new WantedPerson
            {
                Id = int.TryParse(dto.Id, out var parsedId) ? parsedId : 0,
                Name = dto.Name,
                Description = dto.Description,
                Type = dto.Type,
                ImagePath = dto.ImagePath
            };
        }
    }


  




    public async Task AddPersonAsync(WantedPerson wantedPerson)
    {
        try
        {
            HttpContent content;

            // If we have an image file, use multipart/form-data
            if (!string.IsNullOrEmpty(wantedPerson.ImagePath) && File.Exists(wantedPerson.ImagePath))
            {
                var form = new MultipartFormDataContent();

                // Add text fields
                form.Add(new StringContent(wantedPerson.Name), "name");
                form.Add(new StringContent(wantedPerson.Description), "description");
                form.Add(new StringContent(wantedPerson.Type), "type");

                // Add image file
                var fileStream = File.OpenRead(wantedPerson.ImagePath);
                var streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");
                form.Add(streamContent, "image", Path.GetFileName(wantedPerson.ImagePath));

                content = form;
            }
            else
            {
                // No image: send JSON
                var json = JsonSerializer.Serialize(new
                {
                    name = wantedPerson.Name,
                    description = wantedPerson.Description,
                    type = wantedPerson.Type,
                    imagePath = wantedPerson.ImagePath ?? string.Empty
                });

                content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.PostAsync(_addUrl, content);
            var body = await response.Content.ReadAsStringAsync();

            Debug.WriteLine($"[HTTP] {response.StatusCode} - {body}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var doc = JsonDocument.Parse(body);
                    string status = doc.RootElement.TryGetProperty("status", out var s) ? s.GetString() : "unknown";
                    string message = doc.RootElement.TryGetProperty("message", out var m) ? m.GetString() : body;
                    StatusMessage = $"Online insert successful. Server says: {status} - {message}";
                }
                catch (JsonException)
                {
                    StatusMessage = $"Online insert successful. Raw server reply: {body}";
                }
            }
            else
            {
                StatusMessage = $"Online insert failed: {response.StatusCode} - {body}";
            }

            // Always insert locally
            conn.Insert(wantedPerson);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Service] AddPerson failed: {ex.Message}");
            StatusMessage = $"Error: {ex.Message}";
        }
    }





    public async Task<List<WantedPerson>> GetWantedPersonsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://mostwanted.ikgtsapp.com/api/getwanted_person.php");
            var json = await response.Content.ReadAsStringAsync();

           Debug.WriteLine($"Raw JSON: {json}");



    
            var dtoResult = JsonSerializer.Deserialize<OnlineResponseDto>(json);

            if (dtoResult?.Status == "success" && dtoResult.Data != null)
            {
                return dtoResult.Data.Select(WantedPersonMapper.ToDomain).ToList();
            }









            var result = JsonSerializer.Deserialize<OnlineResponse>(json);

      

            if (result?.Status == "success" && result.Data != null)
            {
                return result.Data;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[Service] Fetch failed: {ex.Message}");
        }

        return new List<WantedPerson>();
    }

    public class OnlineResponse
    {
        public string Status { get; set; }
        public List<WantedPerson> Data { get; set; }
    }



    public async Task<WantedPerson?> GetWantedPersonInfoAsync(int id)
    {
        Debug.WriteLine($"Fetching online person with Id={id}");

        try
        {
            var response = await _httpClient.GetAsync($"https://mostwanted.ikgtsapp.com/api/getwanted_person.php?id={id}");
            var json = await response.Content.ReadAsStringAsync();

            var dtoResult = JsonSerializer.Deserialize<OnlineResponseDto>(json);

            if (dtoResult?.Status == "success" && dtoResult.Data != null)
            {

                var persons = dtoResult.Data.Select(WantedPersonMapper.ToDomain).ToList();
           
                return persons.FirstOrDefault(q => q.Id == id);

            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"[OnlineService] Failed to retrieve data: {ex.Message}");
        }

        return null;
    }




}




