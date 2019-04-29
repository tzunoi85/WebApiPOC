
using System.Collections.Generic;

namespace Application.Accounts.Models
{
   public class Account
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<AccountGroups> AccountGroups { get; set; } 
    }
}
