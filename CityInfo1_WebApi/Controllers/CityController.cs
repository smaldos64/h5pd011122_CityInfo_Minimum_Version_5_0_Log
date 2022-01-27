using CityInfo1_Data.DTO;
using CityInfo1_Data.Interfaces;
using CityInfo1_Data.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using h3pd040121_Projekt1_WebApi.Extensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo1_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CityController(IRepositoryWrapper repositoryWrapper)
        {
            this._repositoryWrapper = repositoryWrapper;
        }

        // GET: CityController
        [HttpGet]
        public async Task<IActionResult> GetCities(bool includeRelations = true,
                                                   bool UseLazyLoading = true,
                                                   string UserName = "No Name")
        {
            var CityList = await _repositoryWrapper.CityRepositoryWrapper.FindAll();
          
            if ((false == includeRelations) || (false == UseLazyLoading))
            {
                _repositoryWrapper.CityRepositoryWrapper.DisableLazyLoading();
            }
            else  // true == includeRelations && true == UseLazyLoading 
            {
                _repositoryWrapper.CityRepositoryWrapper.EnableLazyLoading();
            }

            if (true == UseLazyLoading)
            {
                CityList = await _repositoryWrapper.CityRepositoryWrapper.FindAll();
            }
            else
            {
                CityList = await _repositoryWrapper.CityRepositoryWrapper.GetAllCities(includeRelations) as IEnumerable<City>; //as IQueryable<City>;
            }

            // Koden herunder tjener ingen praktisk formål, men er medtaget for at vise, hvor let det hele bliver her på 
            // controller niveau. Som det ses, kan man tilgå alle wrappere fra alle controllers, da vi jo netop laver en
            // dependency injection af vores IRepositoryWrapper i alle controllers. Vi opnår også alle frihedsgrader i
            // forhold til, hvordan de data vi arbejder med skal behandles. I princippet kan de komme alle steder fra 
            // f.eks. en *.xml fil eller alt muligt andet. Det har vi ikke noget kendskab til her på controller niveau.
#if USE_OTHER_REPOSITORY_IN_CONTROLLER
            if ((false == includeRelations) || (false == UseLazyLoading))
            {
                _repositoryWrapper.CountryRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.CountryRepositoryWrapper.EnableLazyLoading();
            }
            var CountryList = await _repositoryWrapper.CountryRepositoryWrapper.FindAll();
#endif

            List<CityDto> CityDtos;

            CityDtos = CityList.Adapt<CityDto[]>().ToList();

            return Ok(CityDtos);
        }

        [HttpGet("{CityId}", Name = "GetCity")]
        public async Task<IActionResult> GetCity(int CityId,
                                                 bool includeRelations = true,
                                                 string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.CityRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.CityRepositoryWrapper.EnableLazyLoading();
            }

            var City_Object = await _repositoryWrapper.CityRepositoryWrapper.FindOne(CityId);

            if (null == City_Object)
            {
                return NotFound();
            }
            else
            {
                CityDto CityDto_Object = City_Object.Adapt<CityDto>();
                return Ok(CityDto_Object);
            }
        }

        // Metoden herunder er "kun" medtaget for test formål. Den bruges til at vise
        // hvordan data fra controlleren kan formatteres på forskellig måde.
        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetCitiesWithCountryID(int CountryID,
                                                               bool includeRelations = true,
                                                               string UserName = "No Name")
        {
            if (false == includeRelations)
            {
                _repositoryWrapper.CityRepositoryWrapper.DisableLazyLoading();
            }
            else
            {
                _repositoryWrapper.CityRepositoryWrapper.EnableLazyLoading();
            }

            var CityList = await _repositoryWrapper.CityRepositoryWrapper.GetCitiesWithCountryID(CountryID);

            List<CityDto> CityDtos;

            CityDtos = CityList.Adapt<CityDto[]>().ToList();

            return Ok(CityDtos);
        }

        // POST: api/City
        [HttpPost]
        public async Task<IActionResult> CreateCity([FromBody] CityForSaveWithCountryDto CityDto_Object,
                                                    string UserName = "No Name")
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            City City_Object = CityDto_Object.Adapt<City>();
            await _repositoryWrapper.CityRepositoryWrapper.Create(City_Object);

            return Ok(City_Object.CityId);
        }

        // PUT api/<CityController>/5
        [HttpPut("{CityId}")]
        public async Task<IActionResult> UpdateCity(int CityId,
                                                    [FromBody] CityForUpdateDto CityDto_Object,
                                                    string UserName = "No Name")
        {
            if (CityId != CityDto_Object.CityId)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cityFromRepo = await _repositoryWrapper.CityRepositoryWrapper.FindOne(CityId);

            if (null == cityFromRepo)
            {
                return NotFound();
            }

            // Dur ikke med en Mapster Adapt i tilfældet med en update !!!
            // Derfor har jeg lavet min egen statiske metode CloneData til at kopiere 
            // data mellem 2 (generiske) objeter. Denne meode er lavet som en statisk metode i
            // en statisk klasse og kan derfor kaldes som en extension metode.
            // Metoden kan findes i filen Extensions/MyMapster.cs

            //var cityFromRepo1 = CityDto_Object.Adapt<City>();

            if (cityFromRepo.CloneData<City>(CityDto_Object))
            {
                await _repositoryWrapper.CityRepositoryWrapper.Update(cityFromRepo);
            }

            return NoContent();
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{CityId}")]
        public async Task<IActionResult> DeleteCity(int CityId,
                                                    string UserName = "No Name")
        {
            _repositoryWrapper.CityRepositoryWrapper.DisableLazyLoading();

            var cityFromRepo = await _repositoryWrapper.CityRepositoryWrapper.FindOne(CityId);

            if (null == cityFromRepo)
            {
                return NotFound();
            }

            await _repositoryWrapper.CityRepositoryWrapper.Delete(cityFromRepo);

            return NoContent();
        }
    }
}
