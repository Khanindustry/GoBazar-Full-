using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Uptitle { get; set; }
        [Column(TypeName = "nText")]
        public string Title { get; set; }
        public DateTime CreatedDate { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
 


    }
}
