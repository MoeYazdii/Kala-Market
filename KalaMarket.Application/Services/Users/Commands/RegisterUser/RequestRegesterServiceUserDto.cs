using System.Collections.Generic;

namespace KalaMarket.Application.Services.Users.Commands.RegisterUser
{
    public class RequestRegisterUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePasword { get; set; }
        public List<RoleInRegesterUserDto> roles { get; set; }
    }

}
