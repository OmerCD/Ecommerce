using System.Threading.Tasks;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Repositories.Interfaces;
using IdentityModel;
using IdentityServer4.Validation;

namespace IdentityServer
{
    public class PasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IRepository<UserEntity> _userRepository;

        public PasswordValidator(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = _userRepository.GetByFieldFirst(x => x.UserName, context.UserName);
            if (user.CheckPassword(context.Password))
            {
                context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
            }
            return Task.FromResult(0);
        }
    }
}