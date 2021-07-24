using MediatR;
using Microsoft.AspNetCore.Identity;
using ReadLater5.Application.Inputs.Queries.AuthQueries;
using ReadLater5.Application.Interfaces;
using ReadLater5.Domain.Constants;
using ReadLater5.Domain.Dtos;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ReadLater5.Application.Services.AuthService
{
    public class Login
    {
        public class Handler : IRequestHandler<LoginQuery, AuthUserDto>
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly SignInManager<IdentityUser> _signInManager;
            private readonly IJwtGenerator _jwtGenerator;

            public Handler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _signInManager = signInManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<AuthUserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, Errors.WrongEmailOrPassword);

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                    throw new RestException(HttpStatusCode.Unauthorized, Errors.WrongEmailOrPassword);

                await _userManager.UpdateAsync(user);

                return new AuthUserDto
                {
                    Token = _jwtGenerator.CreateToken(user),
                };
            }
        }
    }
}
