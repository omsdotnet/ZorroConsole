using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using DemoService.BL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
      var headers = HttpContext.Request.Headers
        .ToDictionary(x => x.Key, y => (IEnumerable<string>)y.Value);

      var result = _bLogic.Process(new ServiceMessage(request, headers));

      foreach (var header in result.Context)
      {
        HttpContext.Response.Headers.Add(header.Key, new StringValues(header.Value.ToArray()));
      }

      return result.Text;
    }
  }
}
