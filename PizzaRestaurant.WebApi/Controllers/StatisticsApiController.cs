namespace PizzaRestaurant.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using PizzaRestaurant.Services.Data.Interfaces;
    using PizzaRestaurant.Services.Data.Models;

    [Route("api/statistics")]
    [ApiController]
    public class StatisticsApiController : ControllerBase
    {
        private readonly IMenuService menuService;

        public StatisticsApiController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(200, Type = typeof(StatisticsServiceModel))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                StatisticsServiceModel serviceModel =
                    await menuService.GetStatisticsAsync();

                return Ok(serviceModel);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
