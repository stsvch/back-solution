using Back.Domain.Entities;
using Back.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Back.Application.CaseStudies.Commands.UpdateCaseStudy
{
    public class UpdateCaseStudyCommandHandler
        : IRequestHandler<UpdateCaseStudyCommand, Unit>
    {
        private readonly IRepository<CaseStudy> _repo;
        public UpdateCaseStudyCommandHandler(IRepository<CaseStudy> repo)
            => _repo = repo;

        public async Task<Unit> Handle(
            UpdateCaseStudyCommand request,
            CancellationToken cancellationToken)
        {
            var cs = await _repo.GetByIdAsync(request.Id, cancellationToken)
                     ?? throw new KeyNotFoundException("CaseStudy not found");

            cs.ChangeTitle(request.Title);
            cs.ChangeDescription(request.Description);

            await _repo.UpdateAsync(cs, cancellationToken);
            return Unit.Value;
        }
    }
}
