using PizzaRestaurant.Web.ViewModels.Products;

namespace PizzaRestaurant.Web.ViewModels.Pizza
{
    public class PizzasForMenuViewModel
    {
        public PizzasForMenuViewModel()
        {
            this.Products = new HashSet<ProductsForPizzaViewModel>();
        }

        public string Name { get; set; } = null!;

        public decimal InitialPrice { get; set; }

        public string ImageUrl { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string DoughName { get; set; } = null!;

        public IEnumerable<ProductsForPizzaViewModel> Products { get; set; }
    }
}
