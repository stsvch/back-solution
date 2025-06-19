using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.AddPhotoToNews
{
    public class AddPhotoToNewsCommandValidator
        : AbstractValidator<AddPhotoToNewsCommand>
    {
        public AddPhotoToNewsCommandValidator()
        {
            RuleFor(x => x.NewsId).NotEmpty();
            RuleFor(x => x.PhotoPath)
                .NotEmpty().MaximumLength(500);
        }
    }
}
