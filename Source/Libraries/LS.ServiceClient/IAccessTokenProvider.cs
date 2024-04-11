using System.Threading.Tasks;

namespace LS.ServiceClient;

public interface IAccessTokenProvider
{
    Task<string> Get();
}

public interface IClientCredentialsAccessTokenProvider : IAccessTokenProvider
{
    void ChangeRequestedScopes(string scopes);
}