using System.Net;
using LS.ServiceClient;
using PromptApi.AI;
using PromptApi.Models;

namespace PromptApi.ServiceClients;

public interface IAiModelApiClient
{
    public Task<AiResponseModel> GetPromptResponse(IAiModel model, object prompt);
}

public class AiModelApiClient(
    IAccessTokenProvider accessTokenProvider,
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
    : BaseRestServiceClient(accessTokenProvider, httpClientFactory), IAiModelApiClient
{
    protected override string ServiceUrl { get; set; } = String.Empty;

    public async Task<AiResponseModel> GetPromptResponse(IAiModel model, object prompt)
    {
        ServiceUrl = configuration[model.AiModelName.ToString()];
        if (!String.IsNullOrEmpty(ServiceUrl))
        {
            var response = await PostWithResponse<AiResponseModel>(model.ApiUrl, prompt);

            return response.Status == HttpStatusCode.OK ? response.Result : null;
        }

        return null;
    }
    
}