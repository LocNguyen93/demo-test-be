namespace RF.Web.Api.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using RF.Web.Api.Models;
    using RF.Web.Api.Services.RequestModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Web.Api.Services;

    [Route("product")]
    public class ProductController : RFBaseController
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductController(IMapper mapper, IProductService productService)
        {
            this.mapper = mapper;
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await productService.GetProducts());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            return Ok(await productService.GetProductById(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductModel createProductModel)
        {
            var requestModel = mapper.Map<ProductRequestModel>(createProductModel);
            var result = await productService.CreateProduct(requestModel);
            return Result(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductModel updateProductModel)
        {
            var requestModel = mapper.Map<ProductRequestModel>(updateProductModel);
            var result = await productService.UpdateProduct(id, requestModel);
            return Result(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await productService.DeleteProduct(id);
            return Result(result);
        }

        [HttpGet("shops")]
        public async Task<IActionResult> GetProductsByShops(string shopIds)
        {
            List<int> shopIdsArray = !String.IsNullOrEmpty(shopIds) ? shopIds.Split(',').Select(int.Parse).ToList() : new List<int>();
            return Ok(await productService.GetProductsByShops(shopIdsArray));
        }
    }
}
