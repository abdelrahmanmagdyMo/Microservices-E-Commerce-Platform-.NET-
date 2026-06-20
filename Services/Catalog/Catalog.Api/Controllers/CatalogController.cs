using Catalog.Application.Commands;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{

    public class CatalogController(IMediator _mediator) : BaseApiController
    {
        [HttpGet]
        [Route("[action]/{id}", Name = "GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDto>> GetProductById(string id)
        {
            var query = new GetProductByIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]/{name}", Name = "GetProductsByProductName")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetProductsByProductName(string name)
        {
            var query = new GetAllProductsByNameQuery(name);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("[action]/{brandName}", Name = "GetProductsByBrandName")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetProductsByBrandName(string brandName)
        {
            var query = new GetAllProductsByBrandQuery(brandName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(typeof(IList<ProductResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<ProductResponseDto>>> GetAllProducts()
        {
            var query = new GetAllProductsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllTypes")]
        [ProducesResponseType(typeof(IList<TypeResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<TypeResponseDto>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<BrandResponseDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IList<BrandResponseDto>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDto), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ProductResponseDto>> CreateProduct([FromBody] CreateProductCommand createProduct)
        {
            var result = await _mediator.Send<ProductResponseDto>(createProduct);
            return Ok(result);
        }
        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommand updateProduct)
        {
            var result = await _mediator.Send<bool>(updateProduct);
            return Ok(result);
        }
        [HttpDelete]
        [Route("{id}", Name = "DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> DeleteProduct(string id)
        {
            var command = new DeleteProductCommand(id);
            var result = await _mediator.Send<bool>(command);
            return Ok(result);
        }
    }
}
