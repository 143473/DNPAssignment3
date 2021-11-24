using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using FileData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;


namespace WebAPI.Data
{
    public class AdultService : IAdultService
    {
        private FileContext FileContext;
        private AdultsContext adultsContext;

        public AdultService(AdultsContext adultsContext)
        {
            FileContext = new FileContext();
            this.adultsContext = adultsContext;
            foreach (var adult in FileContext.Adults)
            {
                adultsContext.Adults.AddAsync(adult);
            }

            adultsContext.SaveChangesAsync();
        }

        public async Task<IList<Adult>>  GetAdultsAsync()
        {
            //return FileContext.Adults;
            return await adultsContext.Adults.ToListAsync();
        }

        public async Task<Adult> AddAdultAsync(Adult newAdult)
        {
            // var nextId = FileContext.Adults.Max(adult => adult.Id);
            // newAdult.Id = ++nextId;
            // FileContext.Adults.Add(newAdult);
            // FileContext.SaveChanges();
            // return newAdult;
            EntityEntry<Adult> newlyAdded = await adultsContext.Adults.AddAsync(newAdult);
            await adultsContext.SaveChangesAsync();
            return newlyAdded.Entity;

        }

        public async Task RemoveAdultAsync(int adultId)
        {
            // Adult adultToRemove = FileContext.Adults.First(t => t.Id == adultId);
            // FileContext.Adults.Remove(adultToRemove);
            // FileContext.SaveChanges();

            Adult toDelete = await adultsContext.Adults.FirstOrDefaultAsync(t => t.Id == adultId);
            if (toDelete != null)
            {
                adultsContext.Adults.Remove(toDelete);
                await adultsContext.SaveChangesAsync();
            }
        }

        public async Task<Adult> UpdateAdultAsync(Adult adult)
        {
            // Adult toUpdate = FileContext.Adults.First(t => t.Id == adult.Id);
            // FileContext.Adults.Remove(toUpdate);
            // FileContext.Adults.Add(adult);
            // FileContext.SaveChanges();
            // return adult;

            try
            {
                Adult toUpdate = await adultsContext.Adults.FirstAsync(t => t.Id == adult.Id);
                adultsContext.Update(toUpdate);
                await adultsContext.SaveChangesAsync();
                return toUpdate;
            }
            catch (Exception e)
            {
                throw new Exception($"Did not find adult with id {adult.Id}");
            }
        }

        public async Task<Adult> GetAdultAsync(int? id)
        {
           // return FileContext.Adults.FirstOrDefault(t => t.Id == id);
           return await adultsContext.Adults.FirstOrDefaultAsync(t => t.Id == id);
        }

        // public async Task<List<Adult>> SearchFilterAsync(string searchByName, string filter, string filter2)
        // {
        //     var adultsToShow = FileContext.Adults.Where(t =>
        //         (!searchByName.Equals("") && (t.FirstName.Contains(searchByName, StringComparison.OrdinalIgnoreCase) ||
        //                                       t.LastName.Contains(searchByName, StringComparison.OrdinalIgnoreCase)) ||
        //          searchByName.Equals("")) &&
        //         (!filter2.Equals("") &&
        //          (t.Sex.Equals(filter2) || (t.EyeColor.Equals(filter2) && filter.Equals("EyeColor")) ||
        //           (t.HairColor.Equals(filter2) && filter.Equals("HairColor")) || t.JobTitle.JobTitle.Equals(filter2)) ||
        //          filter2.Equals("")
        //         )
        //     ).ToList();
        //     var ordered = adultsToShow.OrderBy(t => t.Id).ToList();
        //     return ordered;
        // }
    }
}