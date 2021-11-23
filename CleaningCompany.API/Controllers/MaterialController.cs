using CleaningCompany.Application.UseCases.Materials;
using CleaningCompany.Application.UseCases.Materials.Commands;
using CleaningCompany.Application.UseCases.Materials.DTOs;
using CleaningCompany.Application.UseCases.Materials.Queries;
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
            var id = await _mediator.Send(new CreateMaterialCommand()
            {
                Name = createMaterialDto.Name,
                Price = createMaterialDto.Price
            });

            return Ok(id);
        }

        [HttpPut]
        public async Task<ActionResult<int>> Update(UpdateMaterialDto updateMaterialDto)
        {
            var id = await _mediator.Send(new UpdateMaterialCommand()
            {
                Id = updateMaterialDto.Id,
                Name = updateMaterialDto.Name,
                Price = updateMaterialDto.Price
            });

            return Ok(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialWithProductsStringDto>>> Get([FromQuery] MaterialParameters parameters)
        {
            var materials = await _mediator.Send(new GetAllMaterialsQuery(parameters));

            return Ok(materials);
        }

        [HttpGet("id")]
        public async Task<ActionResult<MaterialDto>> Get(int id)
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

        [HttpDelete]
        public async Task<ActionResult<int>> Delete(DeleteMaterialDto deleteMaterialDto)
        {
            var id = await _mediator.Send(new DeleteMaterialCommand()
            {
                Id = deleteMaterialDto.Id
            });

            return Ok(id);
        }
    }
}
