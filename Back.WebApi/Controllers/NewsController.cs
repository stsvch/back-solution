using Back.Application.Common;
using Back.Application.DTOs;
using Back.Application.Interfaces;
using Back.Application.News.Commands.AddPhotoToNews;
using Back.Application.News.Commands.CreateNews;
using Back.Application.News.Commands.DeleteNews;
using Back.Application.News.Commands.RemovePhotoFromNews;
using Back.Application.News.Commands.UpdateNews;
using Back.Application.News.Queries.GetById;
using Back.Application.News.Queries.GetPaged;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileStorageService _storage;

        public NewsController(
            IMediator mediator,
            IFileStorageService storage)
        {
            _mediator = mediator;
            _storage = storage;
        }

        // POST api/news/{id}/photos
        [HttpPost("{id:guid}/photos")]
        public async Task<IActionResult> UploadAndAddPhoto(
            Guid id,
            [FromForm] IFormFile file,
            CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не прикреплён");

            var path = await _storage.SaveFileAsync(file);

            var photoId = await _mediator.Send(
                new AddPhotoToNewsCommand(id, path), ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id },
                new { photoId, path }
            );
        }

        // DELETE api/news/{id}/photos/{photoId}
        [HttpDelete("{id:guid}/photos/{photoId:guid}")]
        public async Task<IActionResult> RemovePhoto(
            Guid id,
            Guid photoId,
            CancellationToken ct = default)
        {
            await _mediator.Send(
                new RemovePhotoFromNewsCommand(id, photoId),
                ct);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<NewsDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var result = await _mediator.Send(
                new GetNewsPagedQuery(pageNumber, pageSize), ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NewsDto>> GetById(
            Guid id,
            CancellationToken ct = default)
        {
            var dto = await _mediator.Send(
                new GetNewsByIdQuery(id), ct);
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateNewsCommand cmd,
            CancellationToken ct = default)
        {
            var newId = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdateNewsCommand cmd,
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
            await _mediator.Send(new DeleteNewsCommand(id), ct);
            return NoContent();
        }
    }
}
