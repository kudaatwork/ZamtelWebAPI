using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ZamtelWebAPI.Models;

namespace ZamtelWebAPI.Handlers
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly ZamtelContext _context;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            ZamtelContext context)
            : base(options, logger, encoder, clock)
        {
            _context = context;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Authorization header was not found");
            }

            try
            {
                var authenticationHeaderValue = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

                var bytes = Convert.FromBase64String(authenticationHeaderValue.Parameter);

                string[] credentials = Encoding.UTF8.GetString(bytes).Split(":");

                string username = credentials[0];
                string password = credentials[1];

                var user = _context.Users.Where(x => x.Email == username || x.Username == username && x.Password == password).FirstOrDefault();

                if (user == null)
                {
                    return AuthenticateResult.Fail("Need to implement");
                }
                else
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user.Email) };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var principal = new ClaimsPrincipal(identity);
                    var ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }                
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail("Error has occured");
            }
        }
    }
}
