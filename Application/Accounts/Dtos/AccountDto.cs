using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Accounts.Dtos
{
   public class AccountDto
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IQueryable<string> Groups { get; set; }
    }
}
