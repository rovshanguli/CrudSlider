using LessonMigration.Data;
using LessonMigration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LessonMigration.ViewComponents
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;
        public BlogViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogs = await _context.Blogs.ToListAsync();

            return (await Task.FromResult(View(blogs)));
        }

    }
}
