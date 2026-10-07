using RoleBasedStudentManagement.DTOs.Admin;
using RoleBasedStudentManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace RoleBasedStudentManagement.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    [Authorize(Roles ="Admin")]

    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }


        [HttpPut("users/{userId}/role")]
        public IActionResult ChangeRole(int userId,ChangeRoleDTO changeRoleDTO)
        {
            _adminService.ChangeRole(userId, changeRoleDTO);

            return Ok("User role is changed");
        }


        [HttpGet("users")]
        public IActionResult GetAllUsers()
        {
            var users = _adminService.GetAllUsers();
            return Ok(users);
        }

    }
}
