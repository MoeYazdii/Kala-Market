using KalaMarket.Common.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KalaMarket.Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultDto<ResultGetUserDto> Execute(RequestGetUserDto request);
    }
}
