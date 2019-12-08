using Ecommerce.Application.Attributes;
using System;

namespace Ecommerce.Application.Entities
{
    public class UserEntity : MongoEntity
    {
        public string UserName { get; set; }
        [UniqueField]
        public string EMail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDay { get; set; }
        public string HashedPassword { get; set; }
        public string Salt { get; set; }
    }
}
