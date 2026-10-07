using RoleBasedStudentManagement.Services.Interfaces;
using RoleBasedStudentManagement.DTOs.Auth;
using RoleBasedStudentManagement.Repositories.Interfaces;
using RoleBasedStudentManagement.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace RoleBasedStudentManagement.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        public AuthService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IConfiguration configuration, IStudentRepository studentRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
            _studentRepository = studentRepository;
        }

        public void Register(RegisterDTO registerDTO)
        {
            var existingUser = _userRepository.GetByEmail(registerDTO.Email);
            if (existingUser != null)
            {
                throw new Exception("Email already exists");
            }
            var user = new User
            {
                Name = registerDTO.Name,
                Email = registerDTO.Email,
                Role = "Student"
            };

            user.PasswordHash = _passwordHasher.HashPassword(user, registerDTO.Password);
            _userRepository.Create(user);
            var student = new Student
            {
                UserId = user.Id,
                Course = registerDTO.Course,
                Year = registerDTO.Year
            };

            _studentRepository.Create(student);
        }

        public string? Login(LoginDTO loginDTO)
        {
            var user = _userRepository.GetByEmail(loginDTO.Email);
            if (user == null)
            {
                return null;
            }
            var result = _passwordHasher.VerifyHashedPassword
                (
                user,
                user.PasswordHash,
                loginDTO.Password
                );
            if (result == PasswordVerificationResult.Failed)
            {
                return null;
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_configuration["Jwt:ExpiryMinutes"]!)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
