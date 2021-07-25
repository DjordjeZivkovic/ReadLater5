using Microsoft.AspNetCore.Mvc;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Inputs.Queries.BookmarkQueries;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadLater5.Presentation.Controllers
{
    public class BookmarksController : BaseController
    {
        public IActionResult Index() => View();

        [HttpGet]
        public async Task<IActionResult> GetBookmarks(int? start, int? length, string search, IEnumerable<ColumnDto> columns) =>
            Ok(await Mediator.Send(new BookmarksQuery { Start = start, Length = length, Search = search, Columns = columns }));

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(BookmarkCreateCommand command)
        {
            await Mediator.Send(command);

            ViewBag.Success = Successes.SuccessfullyCreated;

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var bookmark = await Mediator.Send(new BookmarkQuery { Id = id });

            return View(new BookmarkUpdateCommand { Bookmark = bookmark });
        }

        [HttpPost]
        public async Task<IActionResult> Update(BookmarkUpdateCommand command)
        {
            command.Bookmark = await Mediator.Send(command);

            ViewBag.Success = Successes.SuccessfullyUpdated;

            return View(command);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id) =>
            Ok(await Mediator.Send(new BookmarkDeleteCommand { Id = id }));
    }
}
