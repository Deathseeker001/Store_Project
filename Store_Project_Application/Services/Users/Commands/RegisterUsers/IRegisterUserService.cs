using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Store_Project.Common;
using Store_Project.Common.DTO;
using Store_Project.Domain.Entities.Users;
using Store_Project_Application.Interfaces.Contexts;

namespace Store_Project_Application.Services.Users.Commands.RegisterUsers
{
    public interface IRegisterUserService
    {
        ResultDTO<ResultRegisterUserDTO> Execute(RequestRegisterUserDTO request);
    }

    public class RegisterUserService : IRegisterUserService
    {
        private readonly IDataBaseContext _context;

        public RegisterUserService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDTO<ResultRegisterUserDTO> Execute(RequestRegisterUserDTO request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDTO<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "پست الکترونیک را وارد نمایید"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDTO<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "نام را وارد نمایید"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDTO<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور را وارد نمایید"
                    };
                }
                if (request.Password != request.RePasword)
                {
                    return new ResultDTO<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "رمز عبور و تکرار آن برابر نیست"
                    };
                }
                string emailRegex = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";

                var match = Regex.Match(request.Email, emailRegex, RegexOptions.IgnoreCase);
                if (!match.Success)
                {
                    return new ResultDTO<ResultRegisterUserDTO>()
                    {
                        Data = new ResultRegisterUserDTO()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "ایمیل خودرا به درستی وارد نمایید"
                    };
                }


                var passwordHasher = new PasswordHasher();
                var hashedPassword = passwordHasher.HashPassword(request.Password);

                User user = new User()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Password = hashedPassword,
                    IsActive = true,
                };

                List<UserInRole> userInRoles = new List<UserInRole>();

                foreach (var item in request.roles)
                {
                    var roles = _context.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id,
                    });
                }
                user.UserInRoles = userInRoles;

                _context.Users.Add(user);

                _context.SaveChanges();

                return new ResultDTO<ResultRegisterUserDTO>()
                {
                    Data = new ResultRegisterUserDTO()
                    {
                        UserId = user.Id,
                    },
                    IsSuccess = true,
                    Message = "ثبت نام کاربر انجام شد",
                };
            }
            catch (Exception)
            {
                return new ResultDTO<ResultRegisterUserDTO>()
                {
                    Data = new ResultRegisterUserDTO()
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "ثبت نام انجام نشد !"
                };
            }
        }
    }
    public class RequestRegisterUserDTO
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePasword { get; set; }
        public List<RolesInRegisterUserDTO> roles { get; set; }
    }

    public class RolesInRegisterUserDTO
    {
        public long Id { get; set; }
    }

    public class ResultRegisterUserDTO
    {
        public long UserId { get; set; }
    }

}
