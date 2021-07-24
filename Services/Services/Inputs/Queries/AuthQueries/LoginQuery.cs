using MediatR;
using ReadLater5.Domain.Dtos;

namespace ReadLater5.Application.Inputs.Queries.AuthQueries
{
    public class LoginQuery : IRequest<AuthUserDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
