namespace PizzaRestaurant.Web.ViewModels.Dough
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Dough;

    public class AddDoughViewModel
        {
        [Required]
        [MinLength(NameMinLength)]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

    }
}
