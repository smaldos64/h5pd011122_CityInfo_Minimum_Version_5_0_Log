using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CityInfo1_Data.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [Required]
        [MaxLength(50)]
        public string CountryName { get; set; }

        [Required]
        [MaxLength(200)]
        public string CountryDescription { get; set; }

        public virtual ICollection<City> Cities { get; set; }
               = new List<City>();
    }
}
