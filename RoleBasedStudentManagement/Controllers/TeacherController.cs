
using RoleBasedStudentManagement.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using RoleBasedStudentManagement.DTOs.Teacher;
using System.Security.Claims;


namespace RoleBasedStudentManagement.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var teachers = _teacherService.GetAll();
            return Ok(teachers);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var teacher = _teacherService.GetById(id);
            if(teacher == null)
            {
                return NotFound("No teacher");
            }
            return Ok(teacher);
        }


        [Authorize(Roles = "Admin,Teacher")]
        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateTeacherDTO updateTeacherDTO)
        {
            var teacher = _teacherService.GetById(id);

            if(teacher == null)
            {
                return NotFound("no teacher is there");
            }

            var loggedInUser = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);
            if (User.IsInRole("Student"))
            {
                return Forbid();
            }

            if (User.IsInRole("Teacher"))
            {
                if(teacher.UserId!= loggedInUser)
                {
                    return Forbid();
                }
            }
            var updatedTeacher = _teacherService.Update(id, updateTeacherDTO);
            return Ok(updatedTeacher);
        }

      


        [HttpGet("me")]
        public IActionResult MyProfile()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var teacher = _teacherService.GetByUserId(userId);
            if(teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
                
        }
    }
}
