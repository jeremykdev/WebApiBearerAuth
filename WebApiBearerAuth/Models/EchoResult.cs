namespace WebApiBearerAuth.Models;

public class EchoResult
{
    public string Echo { get; set; } = String.Empty;

    public List<string> AdditionalInformation { get; set; } = [];

}
