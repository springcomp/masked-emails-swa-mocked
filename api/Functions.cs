using System.Security.Claims;
using System.Text;

public class Functions
{
  private readonly IConfiguration configuration_;

  public Functions(IConfiguration configuration)
  {
    configuration_ = configuration;
  }

  [FunctionName("HttpTrigger")]
  public async Task<HttpResponseMessage> Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "claims")] HttpRequest req,
      [UserInfo] ClaimsPrincipal identity,
      ILogger log,
      CancellationToken cancellationToken)
  {
    using var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, req.HttpContext.RequestAborted);

    log.LogInformation("C# HTTP trigger function processed a request.");

    // use custom typed HTTP client
    // log to Application Insights

    log.LogCustom(
        $"An event has occurred"
    );

    var dictionary = new Dictionary<string, object>();

    foreach (var claim in identity.Claims)
    {
      log.LogTrace($"{claim.Type}: {claim.Value}");
      if (dictionary.ContainsKey(claim.Type))
      {
        // dictionary[claim.Type] is either a List<string> or a string.

        var items = dictionary[claim.Type] as List<string>;
        if (items == null)
          items = new List<string>(new[] { (string)dictionary[claim.Type] });
        items.Add(claim.Value);
        dictionary[claim.Type] = items;
      }
      else
      {
        dictionary.Add(claim.Type, claim.Value);
      }
    }

    dictionary.Add("Variable", configuration_["AppSetting"] ?? "No app setting found.");

    string output;
    using (var stream = new MemoryStream())
    {
      await System.Text.Json.JsonSerializer.SerializeAsync(stream, dictionary);
      output = Encoding.UTF8.GetString(stream.ToArray());
      log.LogTrace(output);
    }

    var response = new HttpResponseMessage();
    response.Content = new StringContent(output, Encoding.UTF8, "application/json");
    return response;
  }
}
