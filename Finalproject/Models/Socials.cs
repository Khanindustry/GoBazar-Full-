using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Socials
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Icon { get; set; }
  
        [MaxLength(250)]
        public string Link { get; set; }



    }
}
