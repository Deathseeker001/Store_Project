using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Store_Project_Application.Services.Users.Queries.GetRoles;
using Store_Project_Application.Services.Users.Queries.GetUsers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Store_Project_Application.Services.Users.Commands.RegisterUsers;
using Store_Project_Application.Services.Users.Commands.UserStatusChange;
using Store_Project_Application.Services.Users.Commands.EditUser;
using Store_Project_Application.Services.Users.Commands.RemoveUsers;


namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IUserSatusChangeService _userSatusChangeService;
        private readonly IEditUserService _editUserService;
        private readonly IRemoveUserService _removeUserService;
        public UsersController(
            IGetUsersService getUsersService,
            IGetRolesService getRolesService, 
            IRegisterUserService registerUserService,
            IUserSatusChangeService userSatusChangeService,
            IEditUserService editUserService,
            IRemoveUserService removeUserService
            )
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _registerUserService = registerUserService;
            _userSatusChangeService = userSatusChangeService;
            _editUserService = editUserService;
            _removeUserService = removeUserService;
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

        [HttpPost]
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _registerUserService.Execute(new RequestRegisterUserDTO
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInRegisterUserDTO>()
                   {
                        new RolesInRegisterUserDTO
                        {
                             Id= RoleId
                        }
                   },
                Password = Password,
                RePasword = RePassword,
            });
            return Json(result);
        }

        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_removeUserService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserSatusChange(long UserId)
        {
            return Json(_userSatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId, string Fullname)
        {
            return Json(_editUserService.Execute(new RequestEdituserDto
            {
                Fullname = Fullname,
                UserId = UserId,
            }));
        }
    }
}
