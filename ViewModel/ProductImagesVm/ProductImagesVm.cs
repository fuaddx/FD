using Pustok2.Models;

namespace Pustok2.ViewModel.ProductImagesVm
{
    public class ProductImagesVm
    {
        public string ImagePath { get; set; }
        public Product Product { get; set; }
        public bool IsActive { get; set; }
    }
}
