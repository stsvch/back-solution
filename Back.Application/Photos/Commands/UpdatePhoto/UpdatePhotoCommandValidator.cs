using FluentValidation;

namespace Back.Application.Photos.Commands.UpdatePhoto
{
    public class UpdatePhotoCommandValidator
        : AbstractValidator<UpdatePhotoCommand>
    {
        public UpdatePhotoCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id не должен быть пустым");

            RuleFor(x => x.Path)
                .NotEmpty().WithMessage("Path не должен быть пустым")
                .MaximumLength(500).WithMessage("Path слишком длинный");
        }
    }
}
