using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.DeleteCaseStudy
{
    public class DeleteCaseStudyCommandHandler
        : IRequestHandler<DeleteCaseStudyCommand, Unit>
    {
        private readonly IRepository<CaseStudy> _repo;
        public DeleteCaseStudyCommandHandler(IRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            DeleteCaseStudyCommand request,
            CancellationToken cancellationToken)
        {
            await _repo.DeleteAsync(request.Id, cancellationToken);
            return Unit.Value;
        }
    }
}
