using System;
using System.Collections.Generic;
using System.Linq;
using DemoService.Controllers;
using DemoService.Properties;
using Microsoft.Extensions.Logging;

namespace DemoService.BL
{
  internal class MessageProcessingLogic : IMessageProcessingLogic
  {
    private const string AppName = "APPTEST";

    private readonly ILogger<AppConsole> _logger;

    private readonly string[] _animation = 
    {
      Resources.AnimationMessage0,
      Resources.AnimationMessage1,
      Resources.AnimationMessage2,
      Resources.AnimationMessage3,
      Resources.AnimationMessage4,
      Resources.AnimationMessage5,
      Resources.AnimationMessage6,
      Resources.AnimationMessage7,
      Resources.AnimationMessage8,
      Resources.AnimationMessage9,
    };

    public MessageProcessingLogic(ILogger<AppConsole> logger)
    {
      _logger = logger;
    }


    public ServiceMessage Process(ServiceMessage request)
    {
      _logger.LogInformation($"Web: \"{request}\"");

      if (request.Text.ToUpper() == $"{AppName} START")
      {
        var resp = string.Format(Resources.BorderedMessage, DateTime.Now.ToLongDateString());

        return new ServiceMessage(resp, new Dictionary<string, IEnumerable<string>>());
      }

      if (request.Text.ToUpper() == $"GIVE IMAGE")
      {
        return new ServiceMessage(Resources.ImageMessage, new Dictionary<string, IEnumerable<string>>());
      }

      if (request.Text.ToUpper() == $"GIVE TABLE")
      {
        return new ServiceMessage(Resources.TableMessage, new Dictionary<string, IEnumerable<string>>());
      }

      if (request.Text.ToUpper() == $"GIVE ANIMATION")
      {
        var headers = new Dictionary<string, IEnumerable<string>>()
        {
          {"pooling", new List<string>() {"1"}},
          {"clrscr", new List<string>() {"-"}},
          {"write", new List<string>() {"-"}},
          {"count", new List<string>() {"0"}},
        };

        return new ServiceMessage(_animation[0], headers);
      }

      if (request.Context.ContainsKey("pooling"))
      {
        var linesRequest = Convert.ToInt32(request.Context["count"].First());

        linesRequest++;

        if (linesRequest == _animation.Length)
        {
          return new ServiceMessage(string.Empty, new Dictionary<string, IEnumerable<string>>());
        }

        var headers = new Dictionary<string, IEnumerable<string>>()
        {
          {"pooling", new List<string>() {"1000"}},
          {"clrscr", new List<string>() {"-"}},
          {"write", new List<string>() {"-"}},
          {"count", new List<string>() { linesRequest.ToString() }},
        };

        return new ServiceMessage(_animation[linesRequest], headers);
      }

      if (request.Text.ToUpper() == $"GIVE FORMATED")
      {
        var text =
          "Launch the site with [darkcyan]https://localhost:5200[/darkcyan] and press [yellow]Ctrl-c[/yellow] to exit.";

        var headers = new Dictionary<string, IEnumerable<string>>()
        {
          {"formated", new List<string>() {"-"}}
        };

        return new ServiceMessage(text, headers);
      }

      if (request.Text.ToUpper() == $"{AppName} STOP")
      {
        return new ServiceMessage("By...", new Dictionary<string, IEnumerable<string>>());
      }
      
      return new ServiceMessage("Didn't understand your command...", new Dictionary<string, IEnumerable<string>>());
    }
  }
}
