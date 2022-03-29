using LessonMigration.Data;
using LessonMigration.Services;
using LessonMigration.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LessonMigration.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly LayoutService _layoutService;
        public HeaderViewComponent(LayoutService layoutService)
        {
            _layoutService = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int productCount = 0;
            if (Request.Cookies["basket"] != null)
            {
                List<BasketVM> basket = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                productCount = basket.Sum(m => m.Count);
            }
            else
            {
                productCount = 0;
            }

            ViewBag.count = productCount;
            
            Dictionary<string, string> settings = _layoutService.GetSettings();

            return (await Task.FromResult(View(settings)));
        }
    }
}
