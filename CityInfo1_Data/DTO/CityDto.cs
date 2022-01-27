using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using CityInfo1_Data.Models;

namespace CityInfo1_Data.DTO
{
    public class CityForSaveDto
    {
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string CityName { get; set; }

        [MaxLength(200)]
        public string CityDescription { get; set; }
    }

    public class CityForSaveWithCountryDto : CityForSaveDto
    {
        public virtual int CountryID { get; set; }
    }

    public class CityForUpdateDto : CityForSaveWithCountryDto
    {
        public int CityId { get; set; }
    }

    public class CityDto : CityForUpdateDto
    {
        public CountryDtoNoCity Country { get; set; }
    }

    public class CityDtoNoCountry : CityForUpdateDto
    {

    }
}
