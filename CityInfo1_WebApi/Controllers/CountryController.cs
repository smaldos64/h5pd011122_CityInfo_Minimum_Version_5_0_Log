using CityInfo1_Data.DTO;
using CityInfo1_Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

using Mapster;
using System.Collections.Generic;
using System.Linq;
using CityInfo1_Data.Models;
using h3pd040121_Projekt1_WebApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo1_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CountryController(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        // GET: api/<CountryController>
        [HttpGet]
        public async Task<IActionResult> GetCountries(bool includeRelations = true,
                                                      string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.CountryRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.CountryRepositoryWrapper.EnableLazyLoading();
            }

            var CountryList = await _repositoryWrapper.CountryRepositoryWrapper.FindAll();

            // Use Mapster
            List<CountryDto> CountryDtos = CountryList.Adapt<CountryDto[]>().ToList();

            return Ok(CountryDtos);
        }

        // GET api/<CountryController>/5
        [HttpGet("{CountryId}", Name = "GetCountry")]
        public async Task<IActionResult> GetCountry(int CountryId,
                                                    bool includeRelations = true,
                                                    string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.CountryRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.CountryRepositoryWrapper.EnableLazyLoading();
            }

            var Country_Object = await _repositoryWrapper.CountryRepositoryWrapper.FindOne(CountryId);

            if (null == Country_Object)
            {
                return NotFound();
            }
            else
            {

                CountryDto CountryDto_Object = Country_Object.Adapt<CountryDto>();
                return Ok(CountryDto_Object);
            }
        }

        // POST api/<CountryController>
        [HttpPost]
        public async Task<IActionResult> CreateCountry([FromBody] CountryForSaveDto CountryDto_Object,
                                                       string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Country Country_Object = CountryDto_Object.Adapt<Country>();
            await _repositoryWrapper.CountryRepositoryWrapper.Create(Country_Object);

            return Ok(Country_Object.CountryID);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{CountryId}")]
        public async Task<IActionResult> UpdateCountry(int CountryId,
                                                       [FromBody] CountryDto CountryDto_Object,
                                                       string UserName = "No Name")
        {
            if (CountryId != CountryDto_Object.CountryID)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var CountryFromRepo = await _repositoryWrapper.CountryRepositoryWrapper.FindOne(CountryId);

            if (null == CountryFromRepo)
            {
                return NotFound();
            }

            if (CountryFromRepo.CloneData<Country>(CountryDto_Object))
            {
                await _repositoryWrapper.CountryRepositoryWrapper.Update(CountryFromRepo);
            }

            return NoContent();
        }

        // DELETE: api/<CountryController>/5
        [HttpDelete("{CountryId}")]
        public async Task<IActionResult> DeleteCountry(int CountryId,
                                                       string UserName = "No Name")
        {
            _repositoryWrapper.CountryRepositoryWrapper.DisableLazyLoading();

            var CountryFromRepo = await _repositoryWrapper.CountryRepositoryWrapper.FindOne(CountryId);

            if (null == CountryFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.CountryRepositoryWrapper.Delete(CountryFromRepo);

            return NoContent();
        }
    }
}
