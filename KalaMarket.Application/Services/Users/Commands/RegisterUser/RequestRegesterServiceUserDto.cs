using System.Collections.Generic;

namespace KalaMarket.Application.Services.Users.Commands.RegisterUser
{
    public class RequestRegesterServiceUserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<RoleInRegesterServiceUserDto> roles { get; set; }
    }

}
