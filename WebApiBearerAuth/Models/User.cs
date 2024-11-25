namespace WebApiBearerAuth.Models;

public class User
{
    public string UserName { get; set; } = String.Empty;

    public string[] Roles { get; set; } = [];
}
