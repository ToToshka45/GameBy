using Application.Dto;
using DataAccess.Abstractions;
using Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        private readonly ILogger<AuthenticantionService> _logger;

        public AuthenticantionService(IRepository<User> userRepository, IRepository<Role> roleRepository,
            UserTokenService userService, IOptions<JwtSettings> options, ILogger<AuthenticantionService> logger)
        {
            _userRepository = userRepository;
            _userService = userService;
            _jwtSettings = options.Value;
            _logger = logger;
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

        public async Task<AuthResultDto> AuthUserById(int userId)
        {
            AuthResultDto result = new();
            User? user = null;

            user = await _userRepository.GetByIdAsync(userId);

            result.AccessToken = GenerateToken(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList());
            result.RefreshToken = GenerateToken(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList(), true);

            SetCachedRefreshToken(result.RefreshToken, user.Id, user.Roles);

            return result;
        }

        public async Task<AuthResultDto?> AuthUser(string userPassword, string userLogin, string userEmail)
        {
            AuthResultDto? result = new();
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
                return null;
            }

            if (user.Password.Value != userPassword)
            {
                return null;
            }

            result.Id = user.Id;
            result.Username = user.Login.Name;
            result.Email = user.Email.Value;
            result.AccessToken = GenerateToken(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList());
            result.RefreshToken = GenerateToken(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList(), true);

            SetCachedRefreshToken(result.RefreshToken, user.Id, user.Roles);

            return result;
        }

        public async Task<RefreshAccessTokenDto?> RefreshToken(string refreshToken)
        {
            UserToken? userToken = _userService.FindUserByRefreshToken(refreshToken);
            if (userToken == null)
            {
                return null;
            }

            RefreshAccessTokenDto res = new();
            var userData = await _userRepository.GetByIdAsync(userToken.UserId);
            string previousToken = userToken.RefreshToken;
            res.UserId = userToken.UserId;
            res.Username = userData.Login.Name;
            res.Email = userData.Email.Value;
            res.AccessToken = GenerateToken(userToken.UserId, userToken.UserRoles);
            res.RefreshToken = GenerateToken(userToken.UserId, userToken.UserRoles, true);

            userToken.RefreshToken = res.RefreshToken;
            await _userService.UpdateUserToken(userToken, previousToken);

            return res;
        }

        private string GenerateToken(int userId, List<string> userRoleNames, bool isRefresh = false)
        {
            string res = string.Empty;

            List<Claim> claims =
                [
                   new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                   new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
                ];

            foreach (string roleName in userRoleNames)
            {
                claims.Add(new Claim("roles", roleName));
            }
            //claims.Add(new Claim(ClaimTypes.Name, userId.ToString()));

            var key = new SymmetricSecurityKey(GetSecretKey());
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                                             audience: _jwtSettings.Audience,
                                             claims,
                                             expires: DateTime.Now.AddMinutes(isRefresh ? JwtSettings.RefreshTokenExpiresInMinutes : JwtSettings.AccessTokenExpiresInMinutes),
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
                return Convert.ToInt32(principal.Identity.Name);
            }
            catch (SecurityTokenException e)
            {
                _logger.LogError(e, "Security token validation failed for access token: {AccessToken}", accessToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while validating the access token: {AccessToken}", accessToken);
            }
            return null;
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

        private void SetCachedRefreshToken(string refreshToken, int userId, IEnumerable<UserRole> roles)
        {
            _userService.AddUserToken(new UserToken()
            {
                RefreshToken = refreshToken,
                ExpirationDate = DateTime.Now.AddMinutes(10),
                UserId = userId,
                UserRoles = roles.Select(x => x.Role.RoleName).ToList()
            });
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
