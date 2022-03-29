using LessonMigration.Data;
using LessonMigration.Models;
using LessonMigration.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMigration.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.ProductCount = _context.Products.Where(p => p.IsDeleted == false).Count();
            List<Product> products = await _context.Products.Where(p => p.IsDeleted == false)
               .Include(m => m.Category)
               .Include(m => m.Images)
               .OrderByDescending(m => m.Id)
               .Take(8)
               .ToListAsync();

            return View(products);
        }

        public IActionResult LoadMore(int skip)
        {
            List<Product> products = _context.Products.Where(p => p.IsDeleted == false)
              .Include(m => m.Category)
              .Include(m => m.Images)
              .OrderByDescending(m => m.Id)
              .Skip(skip)
              .Take(4)
              .ToList();

            return PartialView("_ProductsPartial", products);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBasket(int? id)
        {
            if (id is null) return NotFound();

            Product dbProduct = await GetProductById(id);

            if (dbProduct == null) return BadRequest();

            List<BasketVM> basket = GetBasket();

            UpdateBasket(basket, dbProduct);

            return RedirectToAction("Index", "Home");

        }

        private async Task<Product> GetProductById(int? id)
        {
            return await _context.Products.FindAsync(id);
        }

        private void UpdateBasket(List<BasketVM> basket, Product product)
        {
            var existProduct = basket.Find(m => m.Id == product.Id);

            if (existProduct == null)
            {
                basket.Add(new BasketVM
                {
                    Id = product.Id,
                    Count = 1
                });
            }
            else
            {
                existProduct.Count++;
            }

            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basket), new CookieOptions { MaxAge = TimeSpan.FromMinutes(100) });
        }

        private List<BasketVM> GetBasket()
        {
            List<BasketVM> basket;

            if (Request.Cookies["basket"] != null)
            {
                basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            }
            else
            {
                basket = new List<BasketVM>();
            }

            return basket;
        }


        public async Task<IActionResult> Basket()
        {
            List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
            List<BasketDetailVM> basketDetailItems = new List<BasketDetailVM>();

            foreach (BasketVM item in basket)
            {
                Product product = await _context.Products.Include(m => m.Images).FirstOrDefaultAsync(m => m.Id == item.Id);

                BasketDetailVM basketDetail = new BasketDetailVM
                {
                    Id = item.Id,
                    Name = product.Name,
                    Image = product.Images.Where(m => m.IsMain).FirstOrDefault().Image,
                    Count = item.Count,
                    Price = product.Price * item.Count
                };

                basketDetailItems.Add(basketDetail);

            }
            return View(basketDetailItems);
           
        }
    }
}
