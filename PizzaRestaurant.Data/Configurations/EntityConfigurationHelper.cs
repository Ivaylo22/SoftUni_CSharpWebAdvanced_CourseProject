using Microsoft.EntityFrameworkCore;
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
                dynamic configuration = Activator.CreateInstance(configurationType);
                modelBuilder.ApplyConfiguration(configuration);
            }
        }
    }
}
