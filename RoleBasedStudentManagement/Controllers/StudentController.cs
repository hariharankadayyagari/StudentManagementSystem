using RoleBasedStudentManagement.Services.Interfaces;
using RoleBasedStudentManagement.DTOs.Student;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace RoleBasedStudentManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var students = _studentService.GetAll();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _studentService.GetById(id);
            if (student == null)
            {
                return NotFound("NOt present");
            }
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, UpdateStudentDTO updateStudentDTO)
        {
            var loggerInUserId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            if (User.IsInRole("Student"))
            {
                var student = _studentService.GetById(id);

                if (student == null)
                {
                    return NotFound("student not there");
                }
                if (student.UserId != loggerInUserId)
                {
                    return Forbid("Not access");
                }
            }
            var updateStudent = _studentService.Update(id, updateStudentDTO);
            if (updateStudent == null)
            {
                return NotFound("student not there");
            }
            return Ok(updateStudent);
        }



        [HttpGet("me")]
        public IActionResult MyProfile()
        {
            var loggedInUserId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value);

            var student = _studentService.GetByUserId(loggedInUserId);

            if (student == null)
            {
                return NotFound("Student profile not found");
            }

            return Ok(student);
        }

    }
}
