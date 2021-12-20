using CleaningCompany.Application.UseCases.Materials;
using CleaningCompany.Application.UseCases.Materials.Commands;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Application.UseCases.Materials.Queries;
using CleaningCompany.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleaningCompany.API.Controllers
{
    [Route("api/[controller]")]
    public class MaterialController : BaseController
    {
        private readonly IMediator _mediator;

        public MaterialController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateMaterialDto createMaterialDto)
        {
            var result = await _mediator.Send(new CreateMaterialCommand()
            {
                Name = createMaterialDto.Name,
                Price = createMaterialDto.Price
            });

            return CreateResponseFromResult<int>(result);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateMaterialDto updateMaterialDto)
        {
            var result = await _mediator.Send(new UpdateMaterialCommand()
            {
                Id = updateMaterialDto.Id,
                Name = updateMaterialDto.Name,
                Price = updateMaterialDto.Price
            });

            return CreateResponseFromResult<int>(result);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialWithProductsStringDto>>> GetAllMaterials([FromQuery] MaterialParameters parameters)
        {
            var result = await _mediator.Send(new GetAllMaterialsQuery(parameters));

            AddPaginationHeader(result);

            return CreateResponseFromResult<PagedList<MaterialWithProductsStringDto>>(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialDto>> GetMaterialById([FromRoute] int id)
        {
            var material = await _mediator.Send(new GetMaterialByIdQuery()
            {
                Id = id
            });

            return Ok(material);
        }

        [HttpGet("ProductMaterials/{id}")]
        public async Task<ActionResult<IEnumerable<MaterialDto>>> GetMaterialsForProduct([FromRoute] int id)
        {
            var materials = await _mediator.Send(new GetMaterialsByProductIdQuery()
            {
                Id = id
            });

            return Ok(materials);
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<MaterialIdNameDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllIdNameMaterialsQuery());

            return CreateResponseFromResult<IEnumerable<MaterialIdNameDto>>(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete([FromRoute] int id)
        {
            var result = await _mediator.Send(new DeleteMaterialCommand()
            {
                Id = id
            });

            return CreateResponseFromResult<int>(result);
        }
    }
}
