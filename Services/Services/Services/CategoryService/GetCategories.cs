using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ReadLater5.Application.Inputs.Queries.CategoryQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Envelopes;
using ReadLater5.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.CategoryService
{
    public class GetCategories
    {
        public class Handler : IRequestHandler<CategoriesQuery, CategoryEnvelope>
        {
            private readonly IDataContext _context;
            private readonly IMapper _mapper;

            public Handler(IDataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<CategoryEnvelope> Handle(CategoriesQuery request, CancellationToken cancellationToken)
            {
                var queryable = _context.Categories.AsQueryable();

                if (!string.IsNullOrEmpty(request.Search))
                    queryable = queryable.Where(x => x.Name.Contains(request.Search));
                
                IEnumerable<Category> categories = await queryable
                    .Skip(request.Start ?? 0)
                    .Take(request.Length ?? 10)
                    .ToListAsync();

                return new CategoryEnvelope
                {
                    Data = _mapper.Map<List<CategoryDto>>(categories),
                    RecordsFiltered = queryable.Count(),
                    RecordsTotal = queryable.Count()
                };
            }
        }
    }
}
