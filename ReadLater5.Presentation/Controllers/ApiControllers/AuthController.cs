using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReadLater5.Application.Inputs.Queries.AuthQueries;
using ReadLater5.Domain.Dtos;
using System.Threading.Tasks;

namespace ReadLater5.Presentation.Controllers.ApiControllers
{
    [ApiController]
    [Route("/api/auth/")]
    public class AuthController : BaseController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthUserDto>> Login(LoginQuery query) =>
            await Mediator.Send(query);
    }
}
