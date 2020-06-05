using Infra.Context;
using Infra.Repositories.GenericRepository;

namespace Infra.Repositories.User
{
  public class UserRepository
      : GenericRepository<Domain.Entity.User>, IUserRepository
  {
    public UserRepository(MainContext dbContext) : base(dbContext)
    {
    }
  }
}
