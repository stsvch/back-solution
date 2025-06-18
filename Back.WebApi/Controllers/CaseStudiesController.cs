using Back.Application.CaseStudies.Commands.CreateCaseStudy;
using Back.Application.CaseStudies.Commands.DeleteCaseStudy;
using Back.Application.CaseStudies.Commands.UpdateCaseStudy;
using Back.Application.CaseStudies.Queries.GetById;
using Back.Application.CaseStudies.Queries.GetPaged;
using Back.Application.Common;
using Back.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseStudiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CaseStudiesController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<PagedList<CaseStudyDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var paged = await _mediator.Send(
                new GetCaseStudiesPagedQuery(pageNumber, pageSize), ct);
            return Ok(paged);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CaseStudyDto>> GetById(
            Guid id,
            CancellationToken ct = default)
        {
            var dto = await _mediator.Send(
                new GetCaseStudyByIdQuery(id), ct);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateCaseStudyCommand cmd,
            CancellationToken ct = default)
        {
            var newId = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdateCaseStudyCommand cmd,
            CancellationToken ct = default)
        {
            if (id != cmd.Id)
                return BadRequest("Id mismatch");
            await _mediator.Send(cmd, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            Guid id,
            CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteCaseStudyCommand(id), ct);
            return NoContent();
        }
    }
}
