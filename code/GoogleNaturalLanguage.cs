using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Cloud.Language.V1;
using Google.Api.Gax.Grpc;
using Grpc.Core;
using Google.Api.Gax.Grpc.Rest;
using Cysharp.Threading.Tasks;

public class GoogleNaturalLanguage
{
    private LanguageServiceClient client;
    public GoogleNaturalLanguage(string apiKey)
    {
        LanguageServiceSettings settings = new LanguageServiceSettings
        {
            CallSettings = CallSettings.FromHeader("X-Goog-Api-Key", apiKey)
        };
        client = new LanguageServiceClientBuilder
        {
            GrpcAdapter = RestGrpcAdapter.Default,
            ChannelCredentials = ChannelCredentials.SecureSsl,
            Settings = settings
        }.Build();
    }

    public AnalyzeSentimentResponse AnalyzeSentiment(Document document)
    {
        var response = client.AnalyzeSentiment(document);
        return response;
    }
}
