using Ecommerce.Application.Attributes;
using System;
using System.Text;
using Ecommerce.Application.Utilities;

namespace Ecommerce.Application.Entities
{
    public class UserEntity : MongoEntity
    {
        private const int SaltLength = 32;
        [UniqueField] public string UserName { get; set; }
        [UniqueField] public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public bool IsDeleted { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] Salt { get; set; }

        public void SetPassword(string password)
        {
            var bytePassword = Encoding.UTF8.GetBytes(password);
            Salt = HashAndSalt.GetSalt(SaltLength);
            HashedPassword = HashAndSalt.GenerateSaltedHash(bytePassword, Salt);
        }

        public bool CheckPassword(string password)
        {
            var bytePassword = Encoding.UTF8.GetBytes(password);
            var newHashedPassword = HashAndSalt.GenerateSaltedHash(bytePassword, Salt);
            return newHashedPassword.CompareByteArrays(HashedPassword);
        }
    }
}