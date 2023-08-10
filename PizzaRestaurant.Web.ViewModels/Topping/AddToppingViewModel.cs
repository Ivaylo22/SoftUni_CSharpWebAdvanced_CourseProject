namespace PizzaRestaurant.Web.ViewModels.Topping
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Topping;

    public class AddToppingViewModel
    {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
