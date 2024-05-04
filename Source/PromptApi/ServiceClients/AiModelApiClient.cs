using System.Net;
using LS.ServiceClient;
using PromptApi.AI;

namespace PromptApi.ServiceClients;

public class AiModelApiClient : BaseRestServiceClient
{
    protected override string ServiceName => "aiModel";
    
    public AiModelApiClient(IAccessTokenProvider accessTokenProvider, IHttpClientFactory httpClientFactory) : base(accessTokenProvider, httpClientFactory)
    {
    }

    public async Task<string> GetPromptResponse(AiModel model)
    {
        var response = await PostWithResponse<string>(model.Path, model.Prompt);

        return response.Status == HttpStatusCode.OK ? response.Result! : String.Empty;
    }
    
}