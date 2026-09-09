using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Store_Project_Application.Services.Users.Queries.GetUsers
{
    public interface IGetUsersService
    {
        ResultGetUserDTO Execute(RequestGetUserDTO request);

    }
}
