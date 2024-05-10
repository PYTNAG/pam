using System.Threading;
using System.Threading.Tasks;

namespace PAM.Core.App.RequestResolver
{
    public interface IRequestHandler { }
    
    public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
    {
        Task<TResponse> Handle(TRequest req, CancellationToken cancellationToken);
    }

    public interface IRequestHandler<in TRequest> : IRequestHandler<TRequest, Void>  { }
}