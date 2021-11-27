using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
            this.adultsContext = adultsContext; 
            
                // FileContext = new FileContext();
                // foreach (var adult in FileContext.Adults)
                // {
                //     adultsContext.Adults.AddAsync(adult);
                // }
                // adultsContext.SaveChangesAsync();
                //

        }

        public async Task<IList<Adult>>  GetAdultsAsync()
        {
            return await adultsContext.Adults.Include("JobTitle").ToListAsync();
        }

        public async Task<Adult> AddAdultAsync(Adult newAdult)
        {
            EntityEntry<Adult> newlyAdded = await adultsContext.Adults.AddAsync(newAdult);
            await adultsContext.SaveChangesAsync();
            return newlyAdded.Entity;
        }

        public async Task RemoveAdultAsync(int adultId)
        {
            Adult toDelete = await adultsContext.Adults.FirstOrDefaultAsync(t => t.Id == adultId);
            if (toDelete != null)
            {
                adultsContext.Adults.Remove(toDelete);
                adultsContext.Jobs.Remove(toDelete.JobTitle);
                await adultsContext.SaveChangesAsync();
            }
        }

        public async Task<Adult> UpdateAdultAsync(Adult adult)
        {
            try
            {
                Adult toUpdate = await adultsContext.Adults.Include("JobTitle").FirstOrDefaultAsync(t => t.Id == adult.Id);
     
                toUpdate.FirstName = adult.FirstName;
                toUpdate.LastName = adult.LastName;
                toUpdate.Age = adult.Age;
                toUpdate.Height = adult.Height;
                toUpdate.Sex = adult.Sex;
                toUpdate.JobTitle.Salary = adult.JobTitle.Salary;
                toUpdate.JobTitle.JobTitle = adult.JobTitle.JobTitle;
                toUpdate.Weight = adult.Weight;
                toUpdate.EyeColor = adult.EyeColor;
                toUpdate.HairColor = adult.HairColor;

                adultsContext.Adults.Update(toUpdate);
                
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
            Adult adult =  await adultsContext.Adults.Include("JobTitle").FirstOrDefaultAsync(t => t.Id == id);
           return adult;
        }


    }
}