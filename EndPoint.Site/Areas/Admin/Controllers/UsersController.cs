using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store_Project_Application.Services.Users.Queries.GetRoles;
using Store_Project_Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        public UsersController(IGetUsersService getUsersService, IGetRolesService getRolesService )
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
        }
       


        public IActionResult Index(string searchkey, int page = 1)
        {
            return View(_getUsersService.Execute(new RequestGetUserDTO
            {
                Page = page,
                Searchkey = searchkey
            }));

        }
        
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }
    }
}
