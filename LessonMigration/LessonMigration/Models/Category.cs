using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LessonMigration.Models
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage ="Bu hisseni bosh buraxmayin")]
        [StringLength(20, ErrorMessage = "Uzunluq cox ola bilmez")]
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
