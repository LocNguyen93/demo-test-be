namespace RF.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RF.Web.Api.Models;
    using RF.Web.Api.Services.RequestModels;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Api.Services;

    [Route("shop")]
    public class ShopController : RFBaseController
    {
        private readonly IShopService shopService;
        private readonly IMapper mapper;

        public ShopController(IMapper mapper, IShopService shopService)
        {
            this.mapper = mapper;
            this.shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> GetShops()
        {
            return Ok(await shopService.GetShops());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShopById([FromRoute] int id)
        {
            return Ok(await shopService.GetShopById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateShop([FromBody] CreateShopModel createShopModel)
        {
            var requestModel = mapper.Map<ShopRequestModel>(createShopModel);
            var result = await shopService.CreateShop(requestModel);
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShop([FromRoute] int id, [FromBody] UpdateShopModel updateShopModel)
        {
            var requestModel = mapper.Map<ShopRequestModel>(updateShopModel);
            var result = await shopService.UpdateShop(id, requestModel);
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShop([FromRoute] int id)
        {
            var result = await shopService.DeleteShop(id);
            return Result(result);
        }

        [HttpPut("{id}/add-product")]
        public async Task<IActionResult> UpdateShopProduct([FromRoute] int id, [FromBody] int[] productIds)
        {
            if (productIds.GroupBy(x => x).Any(x => x.Count() > 1))
                return ErrorMessage("duplicated_user", "There are duplicated user.");

            return Result(await shopService.UpdateShopProduct(id, productIds.ToList()));
        }
    }
}
