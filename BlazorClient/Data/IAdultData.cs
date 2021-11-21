using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorClient.Models;


namespace BlazorClient.Data
{
    public interface IAdultData
    {
        Task<IList<Adult>> GetAdultsAsync();
        Task AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
        Task UpdateAdultAsync(Adult adult);
        Task<Adult> GetAdultAsync(int id);
        //Task<List<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2);
    }
}