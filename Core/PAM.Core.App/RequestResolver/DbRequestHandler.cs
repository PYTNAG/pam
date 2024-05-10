using System.Threading;
using System.Threading.Tasks;
using PAM.Core.App.Validation;

namespace PAM.Core.App.RequestResolver
{
    public abstract class DbRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    {
        protected readonly IPersonalAccountsDbContext DbContext;

        protected DbRequestHandler(IPersonalAccountsDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public abstract Task<TResponse> Handle(TRequest req, CancellationToken cancellationToken);
    }

    public abstract class DbRequestHandler<TRequest> : DbRequestHandler<TRequest, Void>
    {
        protected DbRequestHandler(IPersonalAccountsDbContext dbContext) : base(dbContext) { }
    }
}