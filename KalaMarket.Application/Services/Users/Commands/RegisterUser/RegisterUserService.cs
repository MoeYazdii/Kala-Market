using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Common.Dto;
using KalaMarket.Domain.Entites.Users;
using System.Collections.Generic;

namespace KalaMarket.Application.Services.Users.Commands.RegisterUser
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;
        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto<ResultRegisterUserDto> Execute(RequestRegesterServiceUserDto request)
        {
            User user = new User()
            {
                Email = request.Email,
                FullName = request.FullName,

            };
            List<UserInRole> userInRole = new List<UserInRole>();

            foreach (var item in request.roles)
            {
                var roles = _context.Roles.Find(item.Id);
                userInRole.Add(new UserInRole
                {
                    Role = roles,
                    RoleId = roles.Id,
                    User = user,
                    UserId = user.Id,
                });
            }
            user.UserInRoles = userInRole;
            _context.Users.Add(user);
            _context.SaveChanges();

            return new ResultDto<ResultRegisterUserDto>()
            {
                Data = new ResultRegisterUserDto()
                {
                    UserId = user.Id,
                },
                IsSuccess = true,
                Message = "ثبت نام کاربر انجام شد"
            };
        }
    }

}
