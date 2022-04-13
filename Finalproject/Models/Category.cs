using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Icon { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<Product>Products{ get; set; }



    }
}
