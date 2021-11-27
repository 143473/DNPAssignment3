using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAPI.Models;

namespace WebAPI.MiddlePoint
{
    public interface IAdultMiddlePoint
    {
        Task<List<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2);
        Task<IList<string>> GetFilterCategories();
        Task<IList<string>> GetFilterList(string category);
    }
}