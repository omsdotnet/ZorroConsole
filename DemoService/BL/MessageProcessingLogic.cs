using System;
using DemoService.Controllers;
using DemoService.Properties;
using Microsoft.Extensions.Logging;

namespace DemoService.BL
{
  public class MessageProcessingLogic : IMessageProcessingLogic
  {
    private const string AppName = "APPTEST";

    private readonly ILogger<AppConsole> _logger;

    public MessageProcessingLogic(ILogger<AppConsole> logger)
    {
      _logger = logger;
    }


    public string Process(string request)
    {
      _logger.LogInformation($"Web: \"{request}\"");

      if (!string.IsNullOrEmpty(request))
      {
        if (request.ToUpper() == $"{AppName} START")
        {
          return string.Format(Resources.BorderedMessage, DateTime.Now.ToLongDateString());
        }

        if (request.ToUpper() == $"GIVE IMAGE")
        {
          return Resources.ImageMessage;
        }

        if (request.ToUpper() == $"{AppName} STOP")
        {
          return "Пока";
        }
        
        return "Не понял...";
      }

      return string.Empty;
    }
  }
}
