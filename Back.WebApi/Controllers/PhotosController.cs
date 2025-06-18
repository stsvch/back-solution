using Back.Application.Common;
using Back.Application.DTOs;
using Back.Application.Interfaces;
using Back.Application.Photos.Commands.CreatePhoto;
using Back.Application.Photos.Commands.DeletePhoto;
using Back.Application.Photos.Commands.UpdatePhoto;
using Back.Application.Photos.Queries.GetById;
using Back.Application.Photos.Queries.GetPhotosPaged;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : ControllerBase
    {
        private readonly IFileStorageService _storage;
        private readonly IMediator _mediator;
        public PhotosController(IFileStorageService storage, IMediator mediator)
        {
            _storage = storage;
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не прикреплён");

            var url = await _storage.SaveFileAsync(file);
            return Ok(new { Url = url });
        }

        [HttpGet]
        public async Task<ActionResult<PagedList<PhotoDto>>> GetAll(
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 10,
           CancellationToken ct = default)
        {
            var paged = await _mediator.Send(
                new GetPhotosPagedQuery(pageNumber, pageSize), ct);
            return Ok(paged);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PhotoDto>> GetById(Guid id, CancellationToken ct = default)
            => Ok(await _mediator.Send(new GetPhotoByIdQuery(id), ct));

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreatePhotoCommand cmd, CancellationToken ct = default)
        {
            var newId = await _mediator.Send(cmd, ct);
            return CreatedAtAction(nameof(GetById), new { id = newId }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdatePhotoCommand cmd,
            CancellationToken ct = default)
        {
            if (id != cmd.Id) return BadRequest();
            await _mediator.Send(cmd, ct);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            Guid id,
            CancellationToken ct = default)
        {
            await _mediator.Send(new DeletePhotoCommand(id), ct);
            return NoContent();
        }
    }
}
