using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Ecommerce.Application.Entities;
using Ecommerce.Application.Repositories.Interfaces;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<UserEntity> _userRepository;

        public ProfileService(IRepository<UserEntity> userRepository)
        {
            _userRepository = userRepository;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = _userRepository.GetById(context.Subject.Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)
                ?.Value);
            if (user != null)
            {
                context.IssuedClaims.Add(new Claim(ClaimTypes.Name, user.UserName));
            }

            return Task.FromResult(0);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var user = _userRepository.GetById(context.Subject.Claims
                .FirstOrDefault(x => x.Type == "sub")
                ?.Value);
            if (user != null)
            {
                context.IsActive = !user.IsDeleted;
            }
            else
            {
                context.IsActive = false;
            }

            return Task.FromResult(0);
        }
    }
}