using Back.Application.Common;
using Back.Application.Developments.Commands.AddPhotoToDevelopment;
using Back.Application.Developments.Commands.CreateDevelopment;
using Back.Application.Developments.Commands.DeleteDevelopment;
using Back.Application.Developments.Commands.UpdateDevelopment;
using Back.Application.Developments.Queries.GetById;
using Back.Application.Developments.Queries.GetPaged;
using Back.Application.DTOs;
using Back.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Back.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopmentsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFileStorageService _storage;

        public DevelopmentsController(IMediator mediator, IFileStorageService storage)
        {
            _mediator = mediator;
            _storage = storage;
        }

        // POST api/developments/{id}/photos
        [HttpPost("{id:guid}/photos")]
        public async Task<IActionResult> UploadAndAddPhoto(
            Guid id,
            [FromForm] IFormFile file,
            CancellationToken ct = default)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Файл не прикреплён");

            // 1) Сохраняем файл на диск (или в облако) и получаем URL/путь
            var path = await _storage.SaveFileAsync(file);

            // 2) Вызываем команду, которая подхватит сохранённый путь
            var photoId = await _mediator.Send(
                new AddPhotoToDevelopmentCommand(id, path),
                ct
            );

            // 3) Возвращаем и Id новой фотографии, и путь
            return CreatedAtAction(
                nameof(GetById),
                new { id = id },
                new { PhotoId = photoId, Path = path }
            );
        }

        // GET api/developments?pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<PagedList<DevelopmentDto>>> GetAll(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken ct = default)
        {
            var query = new GetDevelopmentsPagedQuery(pageNumber, pageSize);
            var paged = await _mediator.Send(query, ct);
            return Ok(paged);
        }

        // GET api/developments/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<DevelopmentDto>> GetById(
            Guid id,
            CancellationToken ct = default)
        {
            var dto = await _mediator.Send(new GetDevelopmentByIdQuery(id), ct);
            return Ok(dto);
        }

        // POST api/developments
        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CreateDevelopmentCommand command,
            CancellationToken ct = default)
        {
            var newId = await _mediator.Send(command, ct);
            return CreatedAtAction(
              nameof(GetById),
              new { id = newId },
              new { id = newId }         // <-- вот это добавили
            );
        }

        // PUT api/developments/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(
            Guid id,
            [FromBody] UpdateDevelopmentCommand command,
            CancellationToken ct = default)
        {
            if (id != command.Id)
                return BadRequest("Id в URL и теле не совпадают");

            await _mediator.Send(command, ct);
            return NoContent();
        }

        // DELETE api/developments/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(
            Guid id,
            CancellationToken ct = default)
        {
            await _mediator.Send(new DeleteDevelopmentCommand(id), ct);
            return NoContent();
        }
    }
}
