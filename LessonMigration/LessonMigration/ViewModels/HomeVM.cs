using LessonMigration.Models;
using System.Collections.Generic;

namespace LessonMigration.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public List<Category> Categories { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public SliderDetail Detail { get; set; }
    }
}
