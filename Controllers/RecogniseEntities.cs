using System;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace globalai23_mumbai_demo.Controllers;

[ApiController]
[Route("[controller]")]
public class RecogniseEntityController : ControllerBase
{
    private readonly ILogger<RecogniseEntityController> _logger;
    private readonly IConfiguration _configuration;

    public RecogniseEntityController(ILogger<RecogniseEntityController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet()]
    public CategorizedEntityCollection Get(string text)
    {
        string? languageServiceURI = _configuration.GetValue<string>("languageserviceendpoint");
        string? languageServiceKey = _configuration.GetValue<string>("languageservicekey");

        AzureKeyCredential credentials = new AzureKeyCredential(languageServiceKey);
        Uri endpoint = new Uri(languageServiceURI);

        var client = new TextAnalyticsClient(endpoint, credentials);
        var response = client.RecognizeEntities(text);
        //use the response value and proceed with booking
        return response.Value;
    }
}
