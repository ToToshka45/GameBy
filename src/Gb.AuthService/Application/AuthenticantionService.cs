using Application.Dto;
using DataAccess.Abstractions;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application
{
    //ToDo AddRole DeleteRole
    public class AuthenticantionService
    {
        private readonly IRepository<User> _userRepository;
        //private readonly IRepository<Role> _roleRepository;
        private readonly UserTokenService _userService;

        private readonly JwtSettings _jwtSettings;

        public AuthenticantionService(IRepository<User> userRepository, IRepository<Role> roleRepository,
            UserTokenService userService, IOptions<JwtSettings> options)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtSettings = options.Value;
        }

        /*
        //ToDo No UserFound or Role Exist
        public async Task<RefreshAccessTokenDto> AddRole(Guid userguid,string Role)
        {
            RefreshAccessTokenDto res = new RefreshAccessTokenDto();
            User user = await _userRepository.GetByIdAsync(userguid);
            user.Roles.Add(new UserRole() { RoleId=,UserId=userguid});
            _userRepository.UpdateAsync();
            return res;
        }

        public async Task<RefreshAccessTokenDto> RevokeRole(Guid userguid, string Role)
        {
            RefreshAccessTokenDto res = new RefreshAccessTokenDto();
            return res;
        }*/

        public async Task<AuthResultDto> AuthUserById(int UserId
             )
        {
            AuthResultDto result = new AuthResultDto();

            User? user = null;

            user = await _userRepository.GetByIdAsync(UserId);

            result.AccessToken = GenerateTokens(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList());
            result.RefreshToken = GenerateTokens(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList(), true);
            result.IsSuccess = true;

            _userService.AddUserToken(new UserToken()
            {
                RefreshToken = result.RefreshToken,
                ExpirationDate = DateTime.Now.AddDays(7),
                UserId = user.Id,
                UserRoles = user.Roles.Select(x => x.Role.RoleName).ToList()
            });

            return result;
        }

        public async Task<AuthResultDto> AuthUser(string userPassword, string userLogin, string userEmail)
        {
            AuthResultDto result = new();

            User? user = null;

            if (!string.IsNullOrEmpty(userLogin))
            {
                user = (await _userRepository.Search(x => x.Login.Name == userLogin)).FirstOrDefault();
            }
            else if (!string.IsNullOrEmpty(userEmail))
            {
                user = (await _userRepository.Search(x => x.Email.Value == userEmail)).FirstOrDefault();
            }

            if (user == null)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Пользователь не найден";
                return result;
            }

            if (user.Password.Value != userPassword)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Неверный логин или пароль";
                return result;
            }

            result.Id = user.Id;
            result.Username = user.Login.Name;
            result.Email = user.Email.Value;
            result.AccessToken = GenerateTokens(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList());
            result.RefreshToken = GenerateTokens(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList(), true);
            result.IsSuccess = true;

            _userService.AddUserToken(new UserToken()
            {
                RefreshToken = result.RefreshToken,
                ExpirationDate = DateTime.Now.AddSeconds(20),
                UserId = user.Id,
                UserRoles = user.Roles.Select(x => x.Role.RoleName).ToList()
            });

            return result;
        }

        public async Task<RefreshAccessTokenDto> RefreshToken(string refreshToken)
        {
            RefreshAccessTokenDto res = new RefreshAccessTokenDto();

            UserToken userToken = _userService.FindUserByRefreshToken(refreshToken);
            if (userToken == null)
            {
                res.ErrorMessage = "Invalid Token";
                res.IsSuccess = false;
                return res;
            }

            string previousToken = userToken.RefreshToken;
            /*
            ClaimsPrincipal principal = null;
            try {
               principal= GetPrincipalFromTokens(refreshToken); 
            }
            catch(SecurityTokenException e) {
                res.ErrorMessage = "Invalid Token";
                res.IsSuccess = false;
                return res;
            }
            string useridStr = principal.Identity.Name;

            var test=await _userRepository.GetAllAsync();

            var user=await _userRepository.GetByIdAsync(Guid.Parse(useridStr));

            if (user == null)
            {
                res.ErrorMessage = "Invalid Token";
                res.IsSuccess = false;
                return res;
            }

            res.AccessToken = GenerateTokens(Guid.Parse(useridStr), user.Roles.Select(x=>x.Role.RoleName).ToList()
                );
            res.RefreshToken = GenerateTokens(Guid.Parse(useridStr), user.Roles.Select(x => x.Role.RoleName).ToList(),
                true);
            */
            res.AccessToken = GenerateTokens(userToken.UserId, userToken.UserRoles
                );
            res.RefreshToken = GenerateTokens(userToken.UserId, userToken.UserRoles,
                true);
            res.IsSuccess = true;

            userToken.RefreshToken = res.RefreshToken;
            await _userService.UpdateUserToken(userToken, previousToken);

            return res;
        }

        private string GenerateTokens(int userId, List<string> userRoleNames, bool isRefresh = false)
        {
            string res = string.Empty;

            List<Claim> claims = 
                [
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
                ];

            foreach (string roleName in userRoleNames)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleName));
            }
            //claims.Add(new Claim(ClaimTypes.Name, userId.ToString()));

            var key = new SymmetricSecurityKey(GetSecretKey());
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiresAt = DateTime.UtcNow;
            if (isRefresh)
            {
                expiresAt = expiresAt.AddMinutes(10);
            }
            else
            {
                expiresAt = expiresAt.AddSeconds(30);
            }

            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims,
                                             expires: expiresAt,
                                             signingCredentials: creds);

            res = new JwtSecurityTokenHandler().WriteToken(token);
            return res;
        }

        public int? GetTokenInfo(string accessToken)
        {
            ClaimsPrincipal? principal = null;
            try
            {
                principal = GetPrincipalFromTokens(accessToken);
            }
            catch (SecurityTokenException e)
            {
                return null;
            }
            return Convert.ToInt32(principal.Identity.Name);
        }

        private ClaimsPrincipal GetPrincipalFromTokens(string tokenStr)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(GetSecretKey()),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(tokenStr, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        private byte[] GetSecretKey()
        {
            if (_jwtSettings.SecretKey is not null)
                return Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);
            else
                return RandomNumberGenerator.GetBytes(32);
        }
    }
}
