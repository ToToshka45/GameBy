using Microsoft.AspNetCore.Mvc;

namespace GamerProfileService.Controllers;

[ApiController]
[Route( "api/v1/[controller]" )]
public class GameLibraryController
{
    private readonly ILogger<GameLibraryController> _logger;

    public GameLibraryController( ILogger<GameLibraryController> logger )
    {
        _logger = logger;
    }

    [HttpGet( "library" )]
    public string GetLibrary()
    {
        return DateTime.Now.ToString();
    }

    [HttpGet( "library/count" )]
    [ResponseCache( Duration = 10 )]
    public string GetLibraryCount()
    {
        return DateTime.Now.ToString();
    }
}
