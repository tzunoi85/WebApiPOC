using System;
using System.Collections.Generic;

namespace Application.Accounts.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AccountGroups> AccountGroups { get; set; }
    }
}
