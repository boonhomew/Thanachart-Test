using System.Security.Cryptography;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate;
using ThanachartTest.Domain.AggregatesModel.AuthenticationAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.CommonAggregate.Interfaces;
using ThanachartTest.Domain.AggregatesModel.EntityAggregate;
using ThanachartTest.Domain.AggregatesModel.UserAggregate.Interfaces;

namespace ThanachartTest.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(IUserRepository userRepository, IJwtService jwtService, IUnitOfWork unitOfWork)
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

        public JWKsResponse ReadJWKs()
        {
            // For HMAC-SHA256, we typically don't expose the symmetric key in JWKs
            // This is a placeholder implementation
            // In production, consider using RSA or ECDSA for asymmetric signing
            var jwk = new JWKResponse
            {
                Kid = "thanachart-key-1",
                Use = "sig",
                Kty = "oct", // Symmetric key type for HMAC
                Alg = "HS256",
                E = "",
                N = ""
            };

            return new JWKsResponse(jwk);
        }

        public TokenVerificationResponse VerifyToken(string token)
        {
            var userId = _jwtService.ValidateToken(token);

            if (userId == null)
            {
                return new TokenVerificationResponse
                {
                    IsValid = false,
                    Message = "Invalid or expired token"
                };
            }

            var user = _userRepository.GetByIdAsync(userId.Value).Result;

            return new TokenVerificationResponse
            {
                IsValid = true,
                UserId = userId.Value,
                Username = user?.Username,
                Message = "Token is valid"
            };
        }

    }

}
