﻿namespace PizzaRestaurant.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    public class Cart
    {
        public Cart()
        {
            this.CartsPizzas = new HashSet<CartPizza>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        public ICollection<CartPizza> CartsPizzas { get; set; }

        [Required]
        public decimal FinalPrice { get; set; }
    }
}
