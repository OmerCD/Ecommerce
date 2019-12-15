using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Repositories.Interfaces;
using IdentityServer4.Validation;

namespace IdentityServer
{
    public class TokenValidator : ICustomTokenRequestValidator
    {
        private readonly IRepository<UserEntity> _userRepository;

        public TokenValidator(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(CustomTokenRequestValidationContext context)
        {
            if (context.Result.ValidatedRequest.Subject.Identity is ClaimsIdentity identity)
            {
                var id = identity.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
                var user = _userRepository.GetById(id);
                context.Result.ValidatedRequest.ClientClaims.Add(new Claim(ClaimTypes.Name, user?.UserName));
            }
            else
            {
                context.Result.IsError = true;
            }
            return Task.FromResult(0);
        }
    }
}