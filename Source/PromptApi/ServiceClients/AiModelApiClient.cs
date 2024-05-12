﻿using System.Net;
using LS.ServiceClient;
using PromptApi.AI;
using PromptApi.Models;

namespace PromptApi.ServiceClients;

public interface IAiModelApiClient
{
    public Task<string> GetPromptResponse(IAiModel model, object prompt);
}

public class AiModelApiClient : BaseRestServiceClient, IAiModelApiClient
{
    protected override string ServiceName => "aiModel";
    
    public AiModelApiClient(IAccessTokenProvider accessTokenProvider, IHttpClientFactory httpClientFactory) : base(accessTokenProvider, httpClientFactory)
    {
    }

    public async Task<string> GetPromptResponse(IAiModel model, object prompt)
    {
        var response = await PostWithResponse<string>(model.ApiUrl, prompt);

        return response.Status == HttpStatusCode.OK ? response.Result! : String.Empty;
    }
    
}