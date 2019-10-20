using Codex.SalarySurvey.Data.Infrastructure;
using Codex.SalarySurvey.Domain.Contracts.Repositories;
using Codex.SalarySurvey.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Codex.SalarySurvey.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public IEnumerable<User> GetUsers()
        {
            return DbContext.Users;
        }

        public User GetByPhone(string phone)
        {
            return DbContext.Users.FirstOrDefault(a => a.Phone == phone);
        }

        public User Login(string phone, string email)
        {
            return DbContext.Users.FirstOrDefault(a => a.Phone == phone && a.Email == email);
        }

        public bool IsPhoneUnique(string phone)
        {
            return !DbContext.Users.Any(a => a.Phone == phone && a.SmsCodePassedOn.HasValue);
        }

        public bool IsEmailUnique(string email)
        {
            return !DbContext.Users.Any(a => a.Email == email);
        }
    }
}
