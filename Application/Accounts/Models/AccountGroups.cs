using System;
namespace Application.Accounts.Models
{
    public class AccountGroups
    {
        public int AccountId { get; set; }

        public int GroupId { get; set; }

        public Account Account { get; set; }

        public Group Group { get; set; }
    }
}
