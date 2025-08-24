using BP.Core.Users;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BP.Application
{
    /// <summary>
    /// Класс для генерации токеа
    /// </summary>
    public class JwtOperations
    {
        private readonly ILogger<JwtOperations> logger;
        private readonly IOptions<JwtOptions> options;

        public JwtOperations(ILogger<JwtOperations> logger, IOptions<JwtOptions> options)
        {
            this.logger = logger;
            this.options = options;
        }

        public string Generate(User user)
        {
            try
            {
                logger.LogInformation("Generate token...");

                Claim[] claims = [new("userid", user.Id.ToString())];

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.KEY)),
                    SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: signingCredentials,
                    expires: DateTime.UtcNow.AddHours(options.Value.EXPIRESHOURS),
                    issuer: options.Value.ISSUER,
                    audience: options.Value.AUDIENCE);

                var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

                return tokenValue;
            }
            catch (Exception ex)
            {
                logger.LogInformation("Generate token error");
                throw;
            }
        }
    }
}
