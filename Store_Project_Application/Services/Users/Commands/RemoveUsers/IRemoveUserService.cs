using Store_Project.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Project_Application.Services.Users.Commands.RemoveUsers
{
    public interface IRemoveUserService
    {
        ResultDTO Execute(long UserId);
    }

}
