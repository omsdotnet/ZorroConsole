namespace ZorroConsoleTray
{
  internal struct WindowConsoleSessionId
  {
    public uint ThreadId { get; }

    public int WindowHandle { get; }

    public WindowConsoleSessionId(uint threadId, int windowHandle)
    {
      WindowHandle = windowHandle;
      ThreadId = threadId;
    }

    public override string ToString()
    {
      return $"{ThreadId}_{WindowHandle}";
    }
  }
}
