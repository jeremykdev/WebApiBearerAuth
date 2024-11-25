using WebApiBearerAuth.Models;

namespace WebApiBearerAuth.Services;

public interface ITokenService
{
    string GetToken(User user);
}
