using System;
using System.Threading.Tasks;
using ThanachartTest.Application.DTOs;
using ThanachartTest.Domain.AggregatesModel.UserAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.JwtAggregate;
using ThanachartTest.Domain.AggregatesModel.CommonAggregate.Interfaces;
using ThanachartTest.Domain.Entities;

namespace ThanachartTest.Application.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IUserRepository userRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _unitOfWork = unitOfWork;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
                return null;

            // Verify password (simple comparison for demo - in production use proper password hashing)
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtService.GenerateToken(user.Id, user.Username);

            return new LoginResponse
            {
                Token = token,
                Username = user.Username,
                Email = user.Email,
                FullName = user.FullName
            };
        }

        public async Task<bool> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.UsernameExistsAsync(request.Username))
                return false;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Email = request.Email,
                FullName = request.FullName,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "System"
            };

            await _userRepository.AddUserAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
