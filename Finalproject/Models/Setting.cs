using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Logo { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(80)]
        public string Location { get; set; }
        [MaxLength(50)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Poweredby { get; set; }
        [MaxLength(100)]
        public string StoreName { get; set; }
        [MaxLength(50)]
        public string Fax { get; set; }
        [MaxLength(50)]
        public string OpeninTime { get; set; }
        [MaxLength(500)]
        public string Comments { get; set; }
        [MaxLength(1000)]
        public string Map { get; set; }
    } 
}
