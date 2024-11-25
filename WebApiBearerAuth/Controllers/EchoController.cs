using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiBearerAuth.Models;
using WebApiBearerAuth.Services;
using System.Web;
using System.Security.Claims;

namespace WebApiBearerAuth.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class EchoController : ControllerBase
{
    private readonly IEchoService _echoService;

    public EchoController(IEchoService echoService)
    {
        this._echoService = echoService;
    }


    [HttpGet("Echo")]
    public EchoResult GetEchoResult(string? input)
    {
        var result = this._echoService.GetEchoResult(input);
        return result;
    }

    [HttpGet("EchoExt1")]
    public EchoResult GetEchoResultExt1(string? input)
    {
        var result = this._echoService.GetEchoResult(input);

        // add information about authenticated user
        //string? username = this.HttpContext.User?.Identity?.Name;
        string? username = null;

        var identity = this.HttpContext.User.Identity as ClaimsIdentity;
        if (identity != null)
        {
            username = identity.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier )?.FirstOrDefault()?.Value;
            // username = identity.Claims.Where(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.FirstOrDefault()?.Value;
        }
        
        bool isAuthenticated = this.HttpContext.User?.Identity?.IsAuthenticated ?? false;

        result.AdditionalInformation.Add($"User: {username}");
        result.AdditionalInformation.Add($"Is auhtenticated: {isAuthenticated}");


        return result;
    }
}
