using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Accounts.Messages.Commands
{
    public class CreateAccountCommand
        : IRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
