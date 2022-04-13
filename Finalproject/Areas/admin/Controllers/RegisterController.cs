using Finalproject.Data;
using Finalproject.Models;
using Finalproject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Finalproject.Areas.admin.Controllers
{
    [Area("admin")]

    public class RegisterController : Controller
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;
            private readonly AppDbContext _context;

        public RegisterController(AppDbContext context,UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            {
            _context = context;
                _userManager = userManager;
                _signInManager = signInManager;
            }
            public IActionResult Register()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Register(VmRegister model)
            {
                if (ModelState.IsValid)
                {
                    CustomUser user = new CustomUser()
                    {
                        Name = model.Name,
                        Surname = model.Surname,
                        UserName=model.Email,
                        Email = model.Email,
                        IsAdmin=model.IsAdmin
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, false);
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);

                        }
                    }
                    return View(model);
                }
                return View(model);
            }
    
            public IActionResult Login()
            {
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Login(VmLogin model)
            {
                if (ModelState.IsValid)
                {
                var find = _context.CustomUsers.Where(m => m.Email == model.Email).Select(n => n.IsAdmin).FirstOrDefault();
                if (find==true)
                {

                    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email or Password is not Valid");
                        return View(model);
                    }
                }

                }
                return View(model);

            }

            public async Task<IActionResult> Logout()
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("login");
            }

        }






    
}
