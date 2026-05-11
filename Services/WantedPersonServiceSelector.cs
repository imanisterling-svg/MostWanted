
using Microsoft.Maui.Networking;
using MostWanted.Model;

namespace MostWanted.Services
{
    public class WantedPersonServiceSelector
    {
        private readonly WantedPersonServiceOnline _onlineService;
        private readonly WantedPersonService await_offlineService;

        public WantedPersonServiceSelector(
            WantedPersonServiceOnline onlineService,
            WantedPersonService offlineService)
        {
            _onlineService = onlineService;
            await_offlineService = offlineService;
        }








        public async Task<List<WantedPerson>> GetWantedPersonsAsync()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                // Online first
                var onlineData = await _onlineService.GetWantedPersonsAsync();
                if (onlineData?.Any() == true)
                {
                    // Optionally sync to local SQLite
                    //    await _offlineService.SavePersonsAsync(onlineData);
                    return onlineData;
                }
            }

            // Fallback to offline
            return await_offlineService.GetWantedPersonsAsync();
        }
    }













}