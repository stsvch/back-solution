using Back.Application.CaseStudies.Commands.AddPhotoToCaseStudy;
using Back.Application.CaseStudies.Commands.CreateCaseStudy;
using Back.Application.CaseStudies.Commands.DeleteCaseStudy;
using Back.Application.CaseStudies.Commands.RemovePhotoByPathFromCaseStudy;
using Back.Application.CaseStudies.Commands.RemovePhotoFromCaseStudy;
using Back.Application.CaseStudies.Commands.UpdateCaseStudy;
using Back.Application.CaseStudies.Queries.GetById;
using Back.Application.CaseStudies.Queries.GetPaged;
using Back.Application.Common;
using Back.Application.DTOs;
using Back.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaseStudiesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileStorageService _storage;
        public CaseStudiesController(
            IMediator mediator,
            IFileStorageService storage)
        {
            _mediator = mediator;
            _storage = storage;
        }

        // POST api/casestudies/{id}/photos
        [HttpPost("{id:guid}/photos")]
        public async Task<IActionResult> UploadAndAddPhoto(
            Guid id,
            [FromForm] IFormFile file,
            CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не прикреплён");

            // сохраняем файл и получаем путь
            var relativePath = await _storage.SaveFileAsync(file);

            // создаём фото в агрегате
            var photoId = await _mediator.Send(
                new AddPhotoToCaseStudyCommand(id, relativePath), ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { photoId, relativePath }
            );
        }

        // DELETE api/casestudies/{id}/photos/{photoId}
        [HttpDelete("{id:guid}/photos/{photoId:guid}")]
        public async Task<IActionResult> RemovePhoto(
            Guid id,
            Guid photoId,
            CancellationToken ct = default)
        {
            await _mediator.Send(
                new RemovePhotoFromCaseStudyCommand(id, photoId),
                ct);
            return NoContent();
        }

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

        // DELETE api/casestudies/{id}/photos?path=...
        [HttpDelete("{id:guid}/photos")]
        public async Task<IActionResult> RemovePhotoByPath(
            Guid id,
            [FromQuery] string path,
            CancellationToken ct = default)
        {
            await _mediator.Send(
                new RemovePhotoByPathFromCaseStudyCommand(id, path),
                ct);
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
