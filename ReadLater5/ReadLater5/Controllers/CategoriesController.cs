using Microsoft.AspNetCore.Mvc;
using ReadLater5.Application.Inputs.Commands.CategoryCommands;
using ReadLater5.Application.Inputs.Queries.CategoryQueries;
using ReadLater5.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadLater5.Presentation.Controllers
{
    public class CategoriesController : BaseController
    {
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetCategories(int? start, int? length, string search, IEnumerable<ColumnDto> columns) =>
            Ok(await Mediator.Send(new CategoriesQuery { Start = start, Length = length, Search = search, Columns = columns }));

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateCommand command) =>
            Ok(await Mediator.Send(command));
    }
}
