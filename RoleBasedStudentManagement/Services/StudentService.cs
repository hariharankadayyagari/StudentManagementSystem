using RoleBasedStudentManagement.DTOs.Student;
using RoleBasedStudentManagement.Services.Interfaces;
using RoleBasedStudentManagement.Repositories.Interfaces;

namespace RoleBasedStudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public List<StudentResponseDTO> GetAll()
        {
            var students = _studentRepository.GetAll();
            return students.Select(s=> new StudentResponseDTO
            {
                Id = s.Id,
                UserId = s.UserId,
                Name = s.User.Name,
                Email = s.User.Email,
                Course = s.Course,
                Year = s.Year,
            }).ToList();
        }

        public StudentResponseDTO? GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            if(student == null)
            {
                return null;
            }
            return new StudentResponseDTO
            {
                Id = student.Id,
                UserId = student.UserId,
                Name = student.User.Name,
                Email = student.User.Email,
                Course = student.Course,
                Year = student.Year
            };
        }

        public StudentResponseDTO Update(int id , UpdateStudentDTO updateStudentDTO)
        {
            var student = _studentRepository.GetById(id);
            if(student == null)
            {
                return null;
            }
            student.User.Name = updateStudentDTO.Name;
            student.User.Email = updateStudentDTO.Email;
            student.Course = updateStudentDTO.Course;
            student.Year = updateStudentDTO.Year;

            _studentRepository.Update(student);

            return new StudentResponseDTO
            {
                Id = student.Id,
                UserId = student.UserId,
                Name = student.User.Name,
                Email = student.User.Email,
                Course = student.Course,
                Year = student.Year
            };

        }


        public StudentResponseDTO? GetByUserId(int userId)
        {
            var student = _studentRepository.GetByUserId(userId);

            if (student == null)
            {
                return null;
            }

            return new StudentResponseDTO
            {
                Id = student.Id,
                UserId = student.UserId,
                Name = student.User.Name,
                Email = student.User.Email,
                Course = student.Course,
                Year = student.Year
            };
        }
    }
}
