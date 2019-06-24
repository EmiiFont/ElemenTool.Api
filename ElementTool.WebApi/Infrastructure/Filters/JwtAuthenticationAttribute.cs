using ElementTool.WebApi.Infrastructure.DbStorage;
using ElementTool.WebApi.Infrastructure.Validator;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ElementTool.WebApi.Infrastructure.Filters
{
    public class JwtAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public string Realm { get; set; }
        public bool AllowMultiple => false;


        public JwtAuthenticationAttribute()
        {
        }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var request = context.Request;
            var authorization = request.Headers.Authorization;

            if (authorization == null || authorization.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("Unauthorize Access", request);
                return;
            }

            if (string.IsNullOrEmpty(authorization.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing Jwt Token", request);
                return;
            }

            var token = authorization.Parameter;
            var principal = await AuthenticateJwtToken(token);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid token", request);
            }
            else
            {
                context.Principal = principal;
            }
        }

        private static bool ValidateToken(string token, out ElementoolClaim elementoolClaim)
        {
            elementoolClaim = new ElementoolClaim();
            
            var simplePrinciple = JwtManager.GetPrincipal(token);

            if (simplePrinciple == null)
                return false;

            var identity = simplePrinciple.Identity as ClaimsIdentity;

            if (identity == null)
                return false;

            if (!identity.IsAuthenticated)
                return false;

            var usernameClaim = identity.FindFirst(ClaimTypes.Name);
            var userAccountClaim = identity.FindFirst(ClaimTypes.GroupSid);
            var passwordClaim = identity.FindFirst(ClaimTypes.UserData);


            elementoolClaim.Username = usernameClaim?.Value;
            elementoolClaim.AccountName = userAccountClaim?.Value;
            elementoolClaim.Password = passwordClaim?.Value;

            if (string.IsNullOrEmpty(elementoolClaim.Username) && string.IsNullOrEmpty(elementoolClaim.AccountName))
                return false;

            //var _context = new FirebaseStorage();

            //var existing = _context.GetAccountItem(elementoolClaim.AccountName, elementoolClaim.Username);

           // if (existing == null)
                //return false;

            return true;
        }

        protected Task<IPrincipal> AuthenticateJwtToken(string token)
        {
            ElementoolClaim elementoolClaim;

            if (ValidateToken(token, out elementoolClaim))
            {
                // based on username to get more information from database in order to build local identity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, elementoolClaim.Username),
                    new Claim(ClaimTypes.GroupSid, elementoolClaim.AccountName),
                    new Claim(ClaimTypes.UserData, elementoolClaim.Password),
                };

                var identity = new ClaimsIdentity(claims, "Jwt");
                IPrincipal user = new ClaimsPrincipal(identity);

                return Task.FromResult(user);
            }

            return Task.FromResult<IPrincipal>(null);
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            Challenge(context);
            return Task.FromResult(0);
        }

        private void Challenge(HttpAuthenticationChallengeContext context)
        {
            string parameter = null;

            if (!string.IsNullOrEmpty(Realm))
                parameter = "realm=\"" + Realm + "\"";

            context.ChallengeWith("Bearer", parameter);
        }
    }

    public class ElementoolClaim
    {
        public string Username { get; set; }
        public string AccountName { get; set; }
        public string Password { get; set; }
    }
}
