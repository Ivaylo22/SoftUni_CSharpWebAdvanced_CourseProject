namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static PizzaRestaurant.Common.EntityValidationsConstants.Product;
    public class Product
    {
        public Product()
        {
            this.PizzaProducts = new HashSet<PizzaProduct>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; } = null!;

        public ICollection<PizzaProduct> PizzaProducts { get; set; }
    }
}
