using WebApiBearerAuth.Models;

namespace WebApiBearerAuth.Services;

public interface IEchoService
{
    public EchoResult GetEchoResult(string inputToEcho, IEnumerable<string>? additionalInfo = null);
}
