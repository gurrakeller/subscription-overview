using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
using backend.Featurees.Auth;
using Common.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    namespace backend.Features.Auth;

    public class AuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _config;

        public AuthService(UserManager<AppUser> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }

        public async Task<string?> RegisterAsync(RegisterDto dto)
        {
            var user = new AppUser { UserName = dto.Email, Email = dto.Email, FirstName = "", LastName = "" };
            var result = await _userManager.CreateAsync(user, dto.Password);
            return result.Succeeded ? GenerateJwt(user) : null;
        }

        public async Task<string?> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return null;

            return GenerateJwt(user);
        }

        private string GenerateJwt(AppUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }