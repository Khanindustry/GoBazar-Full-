using Finalproject.Data;
using Finalproject.Models;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Subcategory(int? id)
        {
            VmHome model = new VmHome()
            {
                SubCategories = _context.SubCategories.Where(m => m.CategoryId == id).ToList(),
                Products = _context.Products.ToList(),
                Advertisings = _context.Advertisings.ToList()
     
     

            };

            return View(model);

        }
        public IActionResult Product(string searchData, int? id)
        {
            VmHome model = new VmHome()
            {
                Products = _context.Products.Where(m => m.SubCategoryId == id).ToList(),
                 Advertisings = _context.Advertisings.ToList()
            };
            string cart1 = Request.Cookies["cart1"];
            if (!string.IsNullOrEmpty(cart1))
            {
                model.Cart2 = cart1.Split("-").ToList();
            }


            return View(model);

        }

        public IActionResult ProductDetails(int? id)
        {

            return View(_context.Products.Find(id));

        }



        public IActionResult AddToCart(int id)
        {
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1)
            };

            string oldData = Request.Cookies["cart1"];
            string newData = null;
            //int basketCount = 0;

            if (!string.IsNullOrEmpty(oldData))
            {
                List<string> dataList = oldData.Split("-").ToList();

                if (!dataList.Any(c => c == id.ToString()))
                {

                    newData = oldData + "-" + id;
                    //basketCount = dataList.Count + 1;
                }
                else
                {
                    dataList.Remove(id.ToString());
                    newData = string.Join("-", dataList);
                    //basketCount = dataList.Count;
                }
            }
            else
            {
                newData = id.ToString();
                //basketCount = 1;
            }

            Response.Cookies.Append("cart1", newData, options);
            //TempData["BasketCount"]  = basketCount;

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Cart()

        {
            string cart = Request.Cookies["cart1"];
            List<Product> products = new List<Product>();
            if (!string.IsNullOrEmpty(cart))
            {
                List<string> cartList = cart.Split("-").ToList();
                products = _context.Products.Where(sp => cartList.Any(cl => cl == sp.Id.ToString())).ToList();

            }

          return View(products);
        }

        public IActionResult Search(string searchData, int? id)
        {
            VmHome model = new VmHome()
            {
                Products = _context.Products.Where(p => (searchData != null ? p.Model.Contains(searchData) : true)).ToList(),
                Advertisings = _context.Advertisings.ToList()
            };
            return View(model);
        }






    }
}
