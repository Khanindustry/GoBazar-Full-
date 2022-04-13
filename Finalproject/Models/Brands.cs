using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Brands
    {

        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Uptitle { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
     




    }
}
