using RoleBasedStudentManagement.DTOs.Teacher;
using RoleBasedStudentManagement.Repositories.Implementations;
using RoleBasedStudentManagement.Repositories.Interfaces;
using RoleBasedStudentManagement.Services.Interfaces;


namespace RoleBasedStudentManagement.Services
{
    public class TeacherService: ITeacherService
    {
        private readonly ITeacherRepository _teacherrepository;
        public TeacherService(ITeacherRepository teacherrepository)
        {
            _teacherrepository = teacherrepository;
        }

        public List<TeacherResponseDTO> GetAll()
        {
            var teachers = _teacherrepository.GetAll();
            return teachers.Select(t=> new TeacherResponseDTO
            {
                Id = t.Id,
                UserId = t.UserId,
                Name = t.User.Name,
                Email = t.User.Email,
                Subject = t.Subject,
                Department = t.Department
            }).ToList();
        }

        public TeacherResponseDTO? GetById(int id)
        {
            var teacher = _teacherrepository.GetById(id);
            if(teacher == null)
            {
                return null;
            }
            return new TeacherResponseDTO
            {
                Id = id,
                UserId = teacher.UserId,
                Name = teacher.User.Name,
                Email = teacher.User.Email,
                Subject = teacher.Subject,
                Department = teacher.Department
            };
        }

        public TeacherResponseDTO Update(int id , UpdateTeacherDTO updateTeacherDTO)
        {
            var teacher = _teacherrepository.GetById(id);
            if(teacher == null)
            {
                return null;
            }
            teacher.User.Name = updateTeacherDTO.Name;
            teacher.User.Email = updateTeacherDTO.Email;
            teacher.Subject = updateTeacherDTO.Subject;
            teacher.Department = updateTeacherDTO.Department;

            _teacherrepository.Update(teacher);

            return new TeacherResponseDTO
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                Name = teacher.User.Name,
                Email = teacher.User.Email,
                Subject = teacher.Subject,
                Department = teacher.Department
            };
        }

        public TeacherResponseDTO? GetByUserId(int userId)
        {
            var teacher = _teacherrepository.GetByUserId(userId);
            if(teacher == null)
            {
                return null;
            }
            return new TeacherResponseDTO
            {
                Id = teacher.Id,
                UserId = teacher.UserId,
                Email = teacher.User.Email,
                Name = teacher.User.Name,
                Department = teacher.Department,
                Subject = teacher.Subject
            };
        }

        
    }
}
