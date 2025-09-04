using KalaMarket.Application.Interfaces.Contexts;
using KalaMarket.Common;
using KalaMarket.Common.Dto;
using System.Collections.Generic;
using System.Linq;

namespace KalaMarket.Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultGetUserDto> Execute(RequestGetUserDto request)
        {
            int rowsCount = 0;
            var query = _context.Users
                .OrderByDescending(u => u.Id)
                .AsQueryable();

            //search function
            if (!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                query = query.Where(u => u.FullName.Contains(request.SearchKey)
                && u.Email.Contains(request.SearchKey));
            }

            var userList = query.Select(u => new GetUsersDto
            {
                Email = u.Email,
                FullName = u.FullName,
                Id = u.Id,
                IsActive = u.IsActive
            }).ToPaged(request.Page, request.PageSize, out rowsCount)
                .ToList();

            return new ResultDto<ResultGetUserDto>
            {
                Data = new ResultGetUserDto
                {
                    Users = userList,
                    RowCount = rowsCount,
                    PageSize = request.PageSize,
                    CurrentPage = request.Page
                }
            };
        }
    }
}
