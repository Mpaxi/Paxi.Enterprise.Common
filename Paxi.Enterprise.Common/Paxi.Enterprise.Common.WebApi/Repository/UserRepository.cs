using Paxi.Enterprise.Common.Infra.Database.Generic;
using Paxi.Enterprise.Common.WebApi.Context;
using Paxi.Enterprise.Common.WebApi.Model;

namespace Paxi.Enterprise.Common.WebApi.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
