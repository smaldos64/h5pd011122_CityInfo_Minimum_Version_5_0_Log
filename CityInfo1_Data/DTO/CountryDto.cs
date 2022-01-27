using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CityInfo1_Data.DTO
{
    public class CountryForSaveDto
    {
        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }

        [MaxLength(200)]
        public string CountryDescription { get; set; }
    }

    public class CountryForUpdateDto : CountryForSaveDto
    {
        public int CountryID { get; set; }
    }

    public class CountryDtoNoCity : CountryForUpdateDto
    {

    }

    public class CountryDto : CountryForUpdateDto
    {
        public List<CityDtoNoCountry> Cities { get; set; }
    }
}
