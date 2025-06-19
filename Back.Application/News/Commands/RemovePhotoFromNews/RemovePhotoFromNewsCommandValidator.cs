using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.News.Commands.RemovePhotoFromNews
{
    public class RemovePhotoFromNewsCommandValidator
            : AbstractValidator<RemovePhotoFromNewsCommand>
    {
        public RemovePhotoFromNewsCommandValidator()
        {
            RuleFor(x => x.NewsId).NotEmpty();
            RuleFor(x => x.PhotoId).NotEmpty();
        }
    }
}
