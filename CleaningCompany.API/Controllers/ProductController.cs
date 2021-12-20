using CleaningCompany.Application.UseCases.Products;
using CleaningCompany.Application.UseCases.Products.Commands;
using CleaningCompany.Application.UseCases.Products.DTOs;
using CleaningCompany.Application.UseCases.Products.Queries;
using CleaningCompany.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateProductDto createProductDto)
        {
            var result = await _mediator.Send(new CreateProductCommand()
            {
                Name = createProductDto.Name,
                Description = createProductDto.Description,
                BasePrice = createProductDto.BasePrice,
                Difficulty = createProductDto.Difficulty,
                MaterialsIds = createProductDto.Materials.ToList()
            });

            return CreateResponseFromResult<int>(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateProductDto updateProductDto)
        {
            var result = await _mediator.Send(new UpdateProductCommand()
            {
                Id = updateProductDto.Id,
                Name = updateProductDto.Name,
                Description = updateProductDto.Description,
                BasePrice = updateProductDto.BasePrice,
                Difficulty = updateProductDto.Difficulty,
                MaterialsIds = updateProductDto.Materials.ToList()
            });

            return CreateResponseFromResult<int>(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts([FromQuery] ProductParameters parameters)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<ProductDto>>(result);
        }

        [HttpGet("Cards")]
        public async Task<ActionResult<IEnumerable<ProductCardDto>>> GetProductCards()
        {
            var cards = await _mediator.Send(new GetProductCardsQuery());

            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductWithMaterialsDto>> GetProductById([FromRoute] int id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery()
            {
                Id = id
            });

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteProductCommand()
            {
                Id = id
            });

            return CreateResponseFromResult<int>(result);
        }
    }
}
