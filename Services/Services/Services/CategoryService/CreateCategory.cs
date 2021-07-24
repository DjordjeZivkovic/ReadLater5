using AutoMapper;
using MediatR;
using ReadLater5.Application.Inputs.Commands.CategoryCommands;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.CategoryService
{
    public class CreateCategory
    {
        public class Handler : IRequestHandler<CategoryCreateCommand>
        {
            private readonly IDataContext _context;
            public readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
            {
                var category = _mapper.Map<Category>(request.Category);

                _context.Categories.Add(category);

                var success = await _context.SaveChangesAsync() > 0;

                if (!success)
                    throw new Exception(Errors.ProblemSavingChanges);

                return Unit.Value;
            }
        }
    }
}
