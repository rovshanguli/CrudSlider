using LessonMigration.Data;
using LessonMigration.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LessonMigration.Utilities.File;
using LessonMigration.Utilities.Helpers;

namespace LessonMigration.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            List<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
            return View(sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();

            if (!slider.Photo.CheckFileType("image/"))
            {
                ModelState.AddModelError("Photo", "Image type is wrong");
                return View();
            }

            if(!slider.Photo.CheckFileSize(200))
            {
                ModelState.AddModelError("Photo", "Image size is wrong");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }

            slider.Image = fileName;
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Slider slider = await GetSliderById(id);

            if (slider == null) return NotFound();

            string path = Helper.GetFilePath(_env.WebRootPath, "img", slider.Image);
            Helper.DeleteFile(path);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            Slider slider = await GetSliderById(id);
            return View(slider);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id,Slider slider)
        {
            //Delete Old File From Folder

            Slider dbSlider = await GetSliderById(id);
            if (dbSlider == null) return NotFound();
            string oldPath = Helper.GetFilePath(_env.WebRootPath, "img", dbSlider.Image);
            dbSlider.Image = slider.Image;
            Helper.DeleteFile(oldPath);
            


            //Add New File To Folder

            string fileName = Guid.NewGuid().ToString() + "_" + slider.Photo.FileName;

            string path = Helper.GetFilePath(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await slider.Photo.CopyToAsync(stream);
            }



            //Replace Image Name in DB
            dbSlider.Image = fileName;
            _context.Sliders.Update(dbSlider);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<Slider> GetSliderById(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }
    }
}
