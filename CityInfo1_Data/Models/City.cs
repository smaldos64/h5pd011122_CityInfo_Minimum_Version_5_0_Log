using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo1_Data.Models
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        
        [Required]
        [MaxLength(50)]
        public string CityName { get; set; }

        [Required]
        [MaxLength(200)]
        public string CityDescription { get; set; }
        
        [ForeignKey("CountryID")]
        public int CountryID { get; set; }

        public virtual Country Country { get; set; }
    }
}
