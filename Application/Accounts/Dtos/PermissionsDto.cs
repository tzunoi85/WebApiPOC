using System;
namespace Application.Accounts.Dtos
{
    public class PermissionsDto
    {
        public int AccountId { get; set; }

        public string Email { get; set; }

        public int GroupId { get; set; }

        public string Group { get; set; }
    }
}
