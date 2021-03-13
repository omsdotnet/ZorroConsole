namespace DemoService.BL
{
  public interface IMessageProcessingLogic
  {
    ServiceMessage Process(ServiceMessage request);
  }
}