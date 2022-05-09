namespace Shared.Clients
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class ClientRegisterDto : LoginDto
    {
        public string Email { get; set; }
    }

    // public class AdminRegisterDto : LoginDto
    // {
    //     public string Email { get; set; }
    //     public string Fullname { get; set; }
    // }
}