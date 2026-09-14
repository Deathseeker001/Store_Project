using Store_Project.Common;
using Store_Project_Application.Interfaces.Contexts;


namespace Store_Project_Application.Services.Users.Queries.GetUsers
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultGetUserDTO Execute(RequestGetUserDTO request )
        {
            var users = _context.Users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Searchkey))
            {
                users = users.Where(p => p.FullName.Contains(request.Searchkey) && p.Email.Contains(request.Searchkey));
            }
            int rowsCount = 0;
            var userList = users.ToPaged(request.Page, 20, out rowsCount).Select(p => new GetUsersDTO
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsActive = p.IsActive
            }).ToList();

            return new ResultGetUserDTO
            {
                Rows = rowsCount,
                Users = userList
            };



        }
    }
}
