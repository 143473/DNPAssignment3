using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorClient.Models;
using Microsoft.AspNetCore.Components;


namespace BlazorClient.Data
{
    public interface IAdultData
    {
        Task AddAdultAsync(Adult adult);
        Task RemoveAdultAsync(int adultId);
        Task UpdateAdultAsync(Adult adult);
        Task<Adult> GetAdultAsync(int id);
        Task<IList<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2);
        Task<List<string>> GetFilterCategories();
        Task<List<string>> GetFilterList(string category);
    }
}