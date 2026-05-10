public class Spotted
{
    public int Id { get; set; }
    public int WantedPersonId { get; set; }
    public string Name { get; set; }

    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public DateTime DateSpotted { get; set; }

    public string Notes { get; set; }   // ✅ NEW
    public string MediaPath { get; set; }
    public string MediaType { get; set; }
}