
namespace LessonMigration.Models
{
    public class ProductImage:BaseEntity
    {
        public string Image { get; set; }
        public int ProductId { get; set; }
        public bool IsMain { get; set; } = false;
        public Product Product { get; set; }
    }
}
