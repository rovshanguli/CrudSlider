using LessonMigration.Data;
using LessonMigration.Models;
using LessonMigration.Services;
using LessonMigration.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMigration.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly LayoutService _layoutService;
        private readonly ProductService _productService;
        public HomeController(AppDbContext context, LayoutService layoutService, ProductService productService)
        {
            _context = context;
            _layoutService = layoutService;
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {
            //HttpContext.Session.SetString("name", "Esger");
            //Response.Cookies.Append("surname", "Esgerov",new CookieOptions { MaxAge = TimeSpan.FromMinutes(5)});
            Dictionary<string, string> settings = _layoutService.GetSettings();

            int take = int.Parse(settings["HomeTake"]);

            List<Slider> sliders = await _context.Sliders.ToListAsync();
            SliderDetail detail = await _context.SliderDetails.FirstOrDefaultAsync();
            List<Category> categories = await _context.Categories.Where(c=>c.IsDeleted == false).ToListAsync();
            IEnumerable<Product> products = await _productService.GetProducts(take);

            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                Detail = detail,
                Categories = categories,
                Products = products
            };

            return View(homeVM);
        }

        //public IActionResult Test()
        //{
        //    var session = HttpContext.Session.GetString("name");
        //    var cookie = Request.Cookies["surname"];
        //    if (session == null) return NotFound();
        //    return Json("User name:" + session + "User surname:" + cookie);
        //}
    }
}
