using MostWanted.Model;

namespace MostWantedApp.Services
{
    public interface IWantedPersonService
    {
        Task<List<WantedPerson>> GetWantedPersons();
        Task SavePersonsAsync(List<WantedPerson> persons);
    }
}
