using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

namespace ZorroConsoleTray
{
  internal static class Program
  {
    private const string StartWord = "ICDB START";

    private static GlobalKeyboardHook _globalKeyboardHook;
    private static KeboardPressKeyProcessor _keboardPressKeyProcessor;
    private static InputSimulator _inputSimulator;
    private static Thread _thread;
    private static WindowConsoleProcessor _windowConsoleProcessor;

    private static string request = string.Empty;
    private static WindowConsoleSessionId curerentSessionId;
    private static bool responseWork = false;


    [STAThread]
    static void Main()
    {
      _windowConsoleProcessor = new WindowConsoleProcessor();

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
        Thread.Sleep(50);

        if (!string.IsNullOrEmpty(request))
        {
          Thread.Sleep(50);

          Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {curerentSessionId} > {request}");

          var response = "HELLO!!!";

          responseWork = true;
          _inputSimulator.Keyboard.TextEntry(response);
          _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
          responseWork = false;

          //_windowConsoleProcessor.SetSessionText(curerentSessionId, response);

          Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {curerentSessionId} < {response}");

          request = string.Empty;
        }
      }
    }

    private static void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs keyboardEvent)
    {
      if (!responseWork)
      {
        if (keyboardEvent.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
        {
          if (keyboardEvent.KeyboardData.Key == Keys.Enter)
          {
            var sessionId = _windowConsoleProcessor.CatchUserSession();
            var typedWord = _keboardPressKeyProcessor.GetTypedWord();

            Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {sessionId} : {typedWord}");

            if (sessionId.Equals(curerentSessionId))
            {
              request = typedWord;
            }
            else
            {
              var strLenDiff = typedWord.Length - StartWord.Length;
              if (strLenDiff > 0)
              {
                typedWord = typedWord.Substring(strLenDiff);
              }

              if (typedWord == StartWord)
              {
                curerentSessionId = sessionId;
                request = typedWord;
              }
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
}
