using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Model { get; set; }
        [MaxLength(50)]
        public string Brand { get; set; }
        [MaxLength(50)]
        public string Color { get; set; }
        [MaxLength(100)]
        public string Price { get; set; }
        [MaxLength(100)]
        public string DiscountPrice { get; set; }
   
        public DateTime? DiscountDate { get; set; }
        [MaxLength(100)]
        public string Discountfaiz { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [ForeignKey("SubCategory")]
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }


    }
}
