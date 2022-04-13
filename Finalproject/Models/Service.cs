using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Service
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Icon { get; set; }
        [MaxLength(50)] 
        public string Title1 { get; set; }
        [MaxLength(50)]
        public string Title2 { get; set; }



    }
}
