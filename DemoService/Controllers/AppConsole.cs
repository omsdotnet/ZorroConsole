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

    [HttpPost]
    public string ProcessMessages([FromBody]string request)
    {
      return _bLogic.Process(request);
    }
  }
}
