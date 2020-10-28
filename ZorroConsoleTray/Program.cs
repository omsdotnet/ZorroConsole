using System;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace ZorroConsoleTray
{
  static class Program
  {
    private const string StartWord = "ICDB START";

    private static GlobalKeyboardHook _globalKeyboardHook;
    private static KeboardPressKeyProcessor _keboardPressKeyProcessor;
    private static InputSimulator _inputSimulator;
    private static Thread _thread;

    private static string response = string.Empty;

    [STAThread]
    static void Main()
    {
      _thread = new Thread(ExecuteInForeground);
      _thread.Start();

      _inputSimulator = new InputSimulator();

      _keboardPressKeyProcessor = new KeboardPressKeyProcessor();

      _globalKeyboardHook = new GlobalKeyboardHook();
      _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TryApplicationContext());

      _globalKeyboardHook.Dispose();

      _thread.Abort();
    }

    private static void ExecuteInForeground()
    {
      while (true)
      {
        Thread.Sleep(500);

        if (!string.IsNullOrEmpty(response))
        {
          Thread.Sleep(500);

          response = string.Empty;
          
          _inputSimulator.Keyboard.TextEntry("icdb starting......................");
          _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
          _inputSimulator.Keyboard.TextEntry("icdb started");
          _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
        }
      }
    }

    private static void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs keyboardEvent)
    {
      if (keyboardEvent.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
      {
        if (keyboardEvent.KeyboardData.Key == Keys.Enter)
        {
          if (_keboardPressKeyProcessor.IsWord(StartWord))
          {
            response = "go";
          }
        }
        else
        {
          _keboardPressKeyProcessor.Save(keyboardEvent.KeyboardData.VirtualCode);
        }
      }
    }
  }
}
