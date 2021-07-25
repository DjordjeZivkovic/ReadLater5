using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Application.Inputs.Commands.BookmarkCommands;
using ReadLater5.Application.Inputs.Queries.BookmarkQueries;
using ReadLater5.Domain.Dtos;
using ReadLater5.Domain.Envelopes;
using ReadLater5.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReadLater5.Presentation.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/bookmarks/")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiBookmarksController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<BookmarkEnvelope>> Get(int? start, int? length, string search, IEnumerable<ColumnDto> columns) =>
            await Mediator.Send(new BookmarksQuery { Start = start, Length = length, Search = search, Columns = columns });
    
        [HttpGet("{id}")]
        public async Task<ActionResult<BookmarkVM>> Get(int id) =>
            await Mediator.Send(new BookmarkQuery { Id = id });

        [HttpPost]
        public async Task<ActionResult<Unit>> Post(BookmarkCreateCommand command) => 
            await Mediator.Send(command);

        [HttpPut("{id}")]
        public async Task<ActionResult<BookmarkVM>> Put(BookmarkUpdateCommand command) => 
            await Mediator.Send(command);

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(int id) =>
            await Mediator.Send(new BookmarkDeleteCommand { Id = id });
    }
}
