﻿using PizzaRestaurant.Web.ViewModels.Pizza;

namespace PizzaRestaurant.Services.Data.Interfaces
{
    public interface IPizzaService
    {
        Task<IEnumerable<PizzasForMenuViewModel>> GetAllPizzasAsync(int id);
    }
}
