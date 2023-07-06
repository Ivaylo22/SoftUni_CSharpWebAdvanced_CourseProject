﻿using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PizzaRestaurant.Data.Configurations
{
    public static class EntityConfigurationHelper
    {
        public static void ApplyEntityConfigurations(ModelBuilder modelBuilder)
        {
            var configurationTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)));

            foreach (var configurationType in configurationTypes)
            {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                dynamic configuration = Activator.CreateInstance(configurationType);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                modelBuilder.ApplyConfiguration(configuration);
            }
        }
    }
}
