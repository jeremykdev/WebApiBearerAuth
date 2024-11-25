using WebApiBearerAuth.Models;

namespace WebApiBearerAuth.Services;

public class EchoService : IEchoService
{
    public EchoResult GetEchoResult(string inputToEcho, IEnumerable<string>? additionalInfo = null)
    {
        EchoResult echoResult = new();


        if (String.IsNullOrEmpty(inputToEcho))
            echoResult.Echo = "Nothing to echo";
        else 
            echoResult.Echo = inputToEcho;
        
        echoResult.AdditionalInformation = additionalInfo?.ToList() ?? [];

        return echoResult;
    }
}
