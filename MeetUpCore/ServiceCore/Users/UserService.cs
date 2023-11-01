using AutoMapper;
using MeetUpCore.Entities;
using MeetUpCore.Interface;
using MeetUpCore.Models;
using MeetUpCore.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using MeetUpCore.JWTSetting;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MeetUpCore.ServiceCore.Users
{
    public class UserService:IUserManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IOptions<JWTSettings> _jwtOptions;

        public UserService(
            UserManager<User> UserManager, 
            IUserRepository UserRepository, 
            IMapper Mapper, 
            IOptions<JWTSettings> JwtOptions) 
        {
            _userManager = UserManager;
            _userRepository = UserRepository;
            _mapper = Mapper;
            _jwtOptions = JwtOptions;
        }

        public List<Claim> GetClaims(User user, IList<string> UserRoles)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            foreach (string role in UserRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        public JwtSecurityToken GetNewToken(List<Claim> Claims)
        {
            return new JwtSecurityToken(
                issuer: _jwtOptions.Value.Issuer,
                audience: _jwtOptions.Value.Audience,
                notBefore: DateTime.UtcNow,
                claims: Claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(_jwtOptions.Value.LifetimeHours)),
                signingCredentials: new SigningCredentials(_jwtOptions.Value.SecretKey, SecurityAlgorithms.HmacSha256));
        }

        public async Task<string> LoginUserAsync(UserAuthentificationModel model) 
        {
            User user = await _userManager.FindByEmailAsync(model.Email.Normalize());

            if (user == null) { throw new KeyNotFoundException("User is not found"); }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isPasswordCorrect)
            {
                throw new ArgumentException("Password is incorrect"); 
            }

            var UserRoles = await _userManager.GetRolesAsync(user);
            var Claims = GetClaims(user, UserRoles);
            var JwtToken = GetNewToken(Claims);
            var AccessToken = new JwtSecurityTokenHandler().WriteToken(JwtToken);

            return AccessToken ;
        }

        public async Task RegisterAsync(UserCreationModel model)
        {
            User MappedUser = _mapper.Map<User>(model);
            var result = await _userManager.CreateAsync(MappedUser, model.Password);

            if (!result.Succeeded) 
            {
                throw new ArgumentException("Error when creating user");
            }

            await _userManager.AddToRoleAsync(MappedUser, RoleNames.USER);

        }
    }
}
