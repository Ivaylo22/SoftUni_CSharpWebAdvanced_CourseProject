namespace PizzaRestaurant.Web.ViewModels.Pizza
{
    using PizzaRestaurant.Web.ViewModels.Dough;
    using PizzaRestaurant.Web.ViewModels.Products;
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Pizza;

    public class AddPizzaViewModel
    {
        public AddPizzaViewModel()
        {
            this.AvailableProducts = new HashSet<ProductsForPizzaViewModel>();
            this.Doughs = new HashSet<DoughForAddPizzaViewModel>();
            this.ProductsId = new HashSet<int>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal InitialPrice { get; set; }

        [Required]
        [MaxLength(ImageMaxLength)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public int DoughId { get; set; }

        public IEnumerable<DoughForAddPizzaViewModel> Doughs { get; set; }

        public IEnumerable<ProductsForPizzaViewModel> AvailableProducts { get; set; }

        public IEnumerable<int> ProductsId { get; set; }


    }
}
