using Application.Accounts.Messages.Commands;
using Application.Accounts.Messages.Queries;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Accounts.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController
        : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpGet]
        [EnableQuery(AllowedQueryOptions = Microsoft.AspNet.OData.Query.AllowedQueryOptions.All)]
        [Route("")]
        //[Authorize(Roles = "admin")]
        // Bearer followed by token
        public async Task<IActionResult> GetAccountsAsync()
        {
            return Ok(await _mediator.Send(new GeAccountsQuery()));
        }

        [HttpGet]
        [EnableQuery]
        [Route("Permissions")]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            return Ok(await _mediator.Send(new GePermissionsQuery()));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountCommand command)
        {
            await _mediator.Send(command);
            return Created("", command);
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MySecretKeyMySecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, 1.ToString()),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
           return Ok( tokenHandler.WriteToken(token));
        }

    }
}
