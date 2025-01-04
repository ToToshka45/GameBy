using Application.Dto;
using DataAccess.Abstractions;
using Domain;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    //ToDo AddRole DeleteRole
    public class AuthenticantionService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;

        private readonly string _issuer = "GamesByAuth"; 
        private readonly string _audience = "GamesByMictoservices";


        private readonly UserTokenService _userService;

        public AuthenticantionService(IRepository<User> userRepository,IRepository<Role> roleRepository,
            UserTokenService userService)
        {
            _userRepository = userRepository;
            
            _userService = userService;
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

        public async Task<AuthResultDto> AuthUser(string userPassword, string userLogin,string userEmail
             )
         {
            AuthResultDto result = new AuthResultDto();

            User user=null;

            if (!string.IsNullOrEmpty(userLogin)) {
                user= (await _userRepository.Search(x => x.Login.Name==userLogin)).FirstOrDefault();
            }
            else if(!string.IsNullOrEmpty(userEmail)) 
            {
                user = (await _userRepository.Search(x => x.Email.Email == userEmail)).FirstOrDefault();
            }

            if (user == null) { 
                result.IsSuccess = false;
                result.ErrorMessage = "Пользователь не найден";
                return result;
            }

            if(user.Password.Password!=userPassword)
            {
                result.IsSuccess = false;
                result.ErrorMessage = "Неверный логин или пароль";
                return result;
            }

            
            result.AccessToken = GenerateTokens(user.Id,user.Roles.Select(x=>x.Role.RoleName).ToList());
            result.RefreshToken = GenerateTokens(user.Id, user.Roles.Select(x => x.Role.RoleName).ToList(), true);
            result.IsSuccess=true;

            _userService.AddUserToken(new UserToken() { RefreshToken=result.RefreshToken,
            ExpirationDate=DateTime.Now.AddDays(7),UserId=user.Id,UserRoles= user.Roles.
            Select(x => x.Role.RoleName).ToList()
            });
            return result;
        }


        public async Task<RefreshAccessTokenDto> RefreshToken(string refreshToken)
        {
            RefreshAccessTokenDto res=new RefreshAccessTokenDto();

            UserToken userToken = _userService.FindUserByRefreshToken(refreshToken);
            if (userToken == null)
            {
                res.ErrorMessage = "Invalid Token";
                res.IsSuccess = false;
                return res;
            }

            string PreviousToken = userToken.RefreshToken;
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
            res.IsSuccess=true;

            userToken.RefreshToken = res.RefreshToken;
            _userService.UpdateUserToken(userToken, PreviousToken);
            

            return res;
        }

        private string GenerateTokens(Guid userId,List<string> userRoleNames,
            bool IsRefresh=false)
        {
            string res = string.Empty;


            List<Claim> claims = new List<Claim>();
            foreach (string RoleName in userRoleNames) {
                claims.Add(new Claim(ClaimTypes.Role, RoleName));
            }
            claims.Add(new Claim(ClaimTypes.Name, userId.ToString()));
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretsecretsecretkey!123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            DateTime expiresAt= DateTime.UtcNow;
            if (IsRefresh)
            {
                expiresAt = expiresAt.AddDays(7);
            }
            else expiresAt = expiresAt.AddMinutes(30);

            var token = new JwtSecurityToken(issuer:_issuer,
                audience:_audience,
                claims,
                expires: expiresAt,
                signingCredentials: creds);
             
            res = new JwtSecurityTokenHandler().WriteToken(token);
            return res;
        }

        private ClaimsPrincipal GetPrincipalFromTokens(string tokenStr)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysupersecret_secretsecretsecretkey!123")),
                ValidateLifetime = true
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(tokenStr, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
             
            return principal;
        }


    }
}
