using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FileData;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdultController : ControllerBase
    {
        private readonly IAdultService adultService;

        public AdultController(IAdultService adultService)
        {
            this.adultService = adultService;
        }
        
        [HttpGet]
        [Route("/adults")]
        public async Task<ActionResult<IList<Adult>>>
            GetAdultsAsync()
        {
            try
            {
                IList<Adult> adults = await adultService.GetAdultsAsync();
                return Ok(adults);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> GetAdultAsync([FromRoute] int? id)
        {
            try
            {
                Adult adult = await adultService.GetAdultAsync(id);
                return Ok(adult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> RemoveAdultAsync([FromRoute] int id)
        {
            try
            {
                await adultService.RemoveAdultAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Adult>> AddAdultAsync([FromBody] Adult adult)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Adult added = await adultService.AddAdultAsync(adult);
                return Created($"/{added.Id}", added); // return newly added to-do, to get the auto generated id
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Adult>> UpdateAdultAsync([FromBody] Adult adult)
        {
            try
            {
                Adult updatedAdult = await adultService.UpdateAdultAsync(adult);
                return Ok(updatedAdult);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        /*[HttpGet]
        [Route("/filter")]
        public List<Adult> SearchFilter(string searchByName, string filter, string filter2)
        {
            var adultsToShow = FileContext.Adults.Where(t =>
                (!searchByName.Equals("") && (t.FirstName.Contains(searchByName, StringComparison.OrdinalIgnoreCase) ||
                                              t.LastName.Contains(searchByName, StringComparison.OrdinalIgnoreCase)) ||
                 searchByName.Equals("")) &&
                (!filter2.Equals("") &&
                 (t.Sex.Equals(filter2) || (t.EyeColor.Equals(filter2) && filter.Equals("EyeColor")) ||
                  (t.HairColor.Equals(filter2) && filter.Equals("HairColor")) || t.JobTitle.JobTitle.Equals(filter2)) ||
                 filter2.Equals("")
                )
            ).ToList();
            var ordered = adultsToShow.OrderBy(t => t.Id).ToList();
            return ordered;
        }*/
    }
}