using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DemoService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AppConsole : ControllerBase
  {
    private readonly ILogger<AppConsole> _logger;

    public AppConsole(ILogger<AppConsole> logger)
    {
      _logger = logger;
    }

    [HttpPost]
    public string ProcessMessages(string request)
    {
      _logger.LogDebug(request);

      return "Hello";
    }
  }
}
