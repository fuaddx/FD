using System.ComponentModel.DataAnnotations;

namespace Pustok2.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [MaxLength(64)]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UptadedAt { get; set; }
        public Author? Author { get; set; }
        public Author? AuthorId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
