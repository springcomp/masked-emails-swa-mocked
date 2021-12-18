using System.Security.Claims;

public class ApiControllerBase
{
    protected bool GetAuthenticatedUserId(ClaimsPrincipal identity, out string? identifier)
    {
        identifier = null;

        var claim = identity
            .Claims
            .Where(c => c.Type == ClaimTypes.Name)
            .FirstOrDefault()
            ;

        if (claim == null) return false;

        identifier = claim.Value;
        return true;
    }

    protected static IActionResult BadRequest()
    {
        return new StatusCodeResult(StatusCodes.Status400BadRequest);
    }
    protected static IActionResult Conflict()
    {
        return new StatusCodeResult(StatusCodes.Status409Conflict);
    }
    protected static IActionResult NoContent()
    {
        return new StatusCodeResult(StatusCodes.Status204NoContent);
    }
    protected static IActionResult Ok()
    {
        return new OkResult();
    }
    protected static IActionResult Ok<T>(T @object)
    {
        return new OkObjectResult(@object);
    }

    protected static T GetQueryParameter<T>(HttpRequest req, string name, T defaultValue)
    {
        return  GetQueryParameter<T>(req, name) ?? defaultValue;
    }
    protected static T? GetQueryParameter<T>(HttpRequest req, string name)
    {
        var text = req.Query[name].FirstOrDefault();
        if (text != null){
            if (typeof(T) == typeof(int)){
                if (Int32.TryParse(text, out int _number))
                    return (T)(object)_number;
            }
            return (T?)(object?)null;
        }

        return (T?)(object?)null;
    }
    protected static string? GetQueryParameter(HttpRequest req, string name)
    {
        return req.Query[name].FirstOrDefault();
    }
    protected static string GetQueryParameter(HttpRequest req, string name, string defaultValue)
    {
        return req.Query[name].FirstOrDefault() ?? defaultValue;
    }
}
