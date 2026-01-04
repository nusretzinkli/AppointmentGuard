using AppointmentGuard.Core.DTOs;
using AppointmentGuard.Core.Entities;
using AppointmentGuard.Core.Enums;
using AppointmentGuard.Data.IRepositories;
using AppointmentGuard.Service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net; 

namespace AppointmentGuard.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public AuthService(IPatientRepository patientRepository, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<Patient> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await _patientRepository.GetByEmailAsync(registerDto.Email);
            if (existingUser != null)
                throw new Exception("Bu e-posta adresi zaten kayıtlı.");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var patient = new Patient
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                TcNo = registerDto.TcNo,
                Phone = registerDto.Phone,
                DateOfBirth = registerDto.DateOfBirth,
                Gender = (Gender)registerDto.Gender,
                CreatedDate = DateTime.Now,
                IsBlocked = false,
                IsDeleted = false,
                PasswordHash = passwordHash
            };

            await _patientRepository.AddAsync(patient);
            await _unitOfWork.CommitAsync();

            return patient;
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _patientRepository.GetByEmailAsync(loginDto.Email);
            if (user == null)
                return null;

            bool isValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isValid)
                return null;

            return CreateToken(user);
        }

        private TokenDto CreateToken(Patient user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
        }
    }
}