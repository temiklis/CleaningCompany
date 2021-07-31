using CleaningCompany.Application.UseCases.Materials.Commands;
using CleaningCompany.Application.UseCases.Materials.DTOs;
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
    }
}
