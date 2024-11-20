using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace GRPC.Handlers
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorization = Request.Headers.Authorization.ToString();

            if (authorization != null && authorization.StartsWith("Basic ")) 
            {
                var token = authorization.Replace("Basic ", "");
                var bytes = Convert.FromBase64String(token);
                var credentialsText = Encoding.UTF8.GetString(bytes);
                var credentials = credentialsText.Split(":");

                var userName = credentials[0];
                var password = credentials[1];

                if (userName == "ibrahim" && password == "Pa$$w0rd")
                {
                    var claims = new[]
                    {
                        new Claim(ClaimTypes.Name, "ibrahim")
                    };
                    var identity = new ClaimsIdentity(claims, Scheme.Name);
                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
                }
            }

            return AuthenticateResult.NoResult();
        }
    }
}
