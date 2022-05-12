using System.Collections.Generic;
namespace BackEnd.Models.ViewModels
{
    public class AccountDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class AccountListDto
    {
        public IEnumerable<AccountDto> Accounts { get; set; }
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }
    }
}