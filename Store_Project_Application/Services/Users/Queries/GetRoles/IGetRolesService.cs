using Store_Project.Common.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Project_Application.Services.Users.Queries.GetRoles
{
    
        public interface IGetRolesService
        {
            ResultDTO<List<RolesDTO>> Execute();
        }
    
}
