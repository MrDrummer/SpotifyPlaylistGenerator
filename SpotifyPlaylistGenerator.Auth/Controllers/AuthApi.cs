using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace SpotifyPlaylistGenerator.Auth.Controllers;

[ApiController]
[Route("api/auth")]
public class AutoApi : Controller
{
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        Console.WriteLine("Logged out user!");
        await HttpContext.SignOutAsync();
        return SignOut();
    }
}