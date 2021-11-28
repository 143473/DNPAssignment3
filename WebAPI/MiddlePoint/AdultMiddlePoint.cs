using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.MiddlePoint
{
    public class AdultMiddlePoint : IAdultMiddlePoint
    {
        private IList<Adult> adults;
        private IAdultService adultService;
        private List<string> filterList;
        private List<string> filterList2;

        public AdultMiddlePoint(IAdultService adultService)
        {
            this.adultService = adultService;
            adults = adultService.GetAdultsAsync().Result;
            filterList = new() {"HairColor", "EyeColor", "Sex", "JobTitle"};
        }
        public async Task<List<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2)
        {
            var adultsToShow = adults.Where(t =>
                (searchByName != null && (t.FirstName.Contains(searchByName, StringComparison.OrdinalIgnoreCase) ||
                                              t.LastName.Contains(searchByName, StringComparison.OrdinalIgnoreCase)) ||
                 searchByName == null) &&
                (filter2!= null &&
                 (t.Sex.Equals(filter2) || (t.EyeColor.Equals(filter2) && filter.Equals("EyeColor")) ||
                  (t.HairColor.Equals(filter2) && filter.Equals("HairColor")) || t.JobTitle.JobTitle.Equals(filter2)) ||
                 filter2==null
                )
            ).ToList();
            var ordered = adultsToShow.OrderBy(t => t.Id).ToList();
            return ordered;
        }

        public async Task<IList<string>> GetFilterCategories()
        {
            return filterList;
        }
        
        public async Task<IList<string>> GetFilterList(string category)
        {
            filterList2 = null;
            try
            {
                switch (category)
                {
                    case "HairColor":
                        filterList2 = adults.Select(x => x.HairColor).Distinct().ToList();
                        break;
                    case "EyeColor":
                        filterList2 = adults.Select(x => x.EyeColor).Distinct().ToList();
                        break;
                    case "Sex":
                        filterList2 = adults.Select(x => x.Sex).Distinct().ToList();
                        break;
                    case "JobTitle":
                        filterList2 = adults.Select(x => x.JobTitle.JobTitle).Distinct().ToList();
                        break;
                    default :
                        filterList2 = null;
                        break;
                }
            }
            catch (Exception)
            {
            }
            return filterList2;
        }
    }
}