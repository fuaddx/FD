using Pustok2.Models;

namespace Pustok2.ViewModel.BlogVM
{
    public class BloglistCreate
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UptadedAt { get; set; }
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
    }
}
