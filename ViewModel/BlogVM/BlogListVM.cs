using Pustok2.Models;

namespace Pustok2.ViewModel.BlogVM
{
    public class BlogListVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UptadedAt { get; set; }
        public Author Author { get; set; }
    }
}