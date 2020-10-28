using System;
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

    [STAThread]
    static void Main()
    {
      _inputSimulator = new InputSimulator();

      _keboardPressKeyProcessor = new KeboardPressKeyProcessor();

      _globalKeyboardHook = new GlobalKeyboardHook();
      _globalKeyboardHook.KeyboardPressed += OnKeyPressed;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new TryApplicationContext());

      _globalKeyboardHook.Dispose();
    }

    private static void OnKeyPressed(object sender, GlobalKeyboardHookEventArgs keyboardEvent)
    {
      if (keyboardEvent.KeyboardState == GlobalKeyboardHook.KeyboardState.KeyUp)
      {
        if (keyboardEvent.KeyboardData.Key == Keys.Enter)
        {
          if (_keboardPressKeyProcessor.IsWord(StartWord))
          {
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            _inputSimulator.Keyboard.TextEntry("icdb starting......................");
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            _inputSimulator.Keyboard.TextEntry("icdb started");
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
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
