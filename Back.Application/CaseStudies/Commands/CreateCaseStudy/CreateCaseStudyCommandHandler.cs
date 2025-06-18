using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.CreateCaseStudy
{
    public class CreateCaseStudyCommandHandler
        : IRequestHandler<CreateCaseStudyCommand, Guid>
    {
        private readonly IRepository<CaseStudy> _repo;
        public CreateCaseStudyCommandHandler(IRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Guid> Handle(
            CreateCaseStudyCommand request,
            CancellationToken cancellationToken)
        {
            var cs = new CaseStudy(request.Title, request.Description);
            foreach (var path in request.PhotoPaths)
                cs.AddPhoto(path);
            await _repo.AddAsync(cs, cancellationToken);
            return cs.Id;
        }
    }
}
