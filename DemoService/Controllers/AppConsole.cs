using DemoService.BL;
using Microsoft.AspNetCore.Mvc;

namespace DemoService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AppConsole : ControllerBase
  {
    private readonly IMessageProcessingLogic _bLogic;

    public AppConsole(IMessageProcessingLogic bLogic)
    {
      _bLogic = bLogic;
    }

    /// <summary>
    /// Microservice Console Controller
    /// </summary>
    /// <param name="request">For the request text to be correctly recognized, specify it in quotes, for example: "GIVE IMAGE"</param>
    /// <returns>response text</returns>
    [HttpPost]
    public string ProcessMessages([FromBody]string request)
    {
      return _bLogic.Process(request);
    }
  }
}
