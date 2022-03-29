using System;

namespace LessonMigration.Models
{
    public class Blog : BaseEntity
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
