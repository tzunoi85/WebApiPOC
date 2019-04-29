using Application.Accounts.Messages.Commands;
using Application.Accounts.Messages.Queries;
using Infrastructure.Context;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> GetAccountsAsync()
        {
            return Ok(await _mediator.Send(new GeAccountsQuery()));
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAccount([FromBody]CreateAccountCommand command)
        {
            await _mediator.Send(command);
            return Created("", command);
        }
    }
}
