using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using InputSimulator = ZorroConsoleTray.WindowsInput.InputSimulator;
using VirtualKeyCode = ZorroConsoleTray.WindowsInput.Native.VirtualKeyCode;

namespace ZorroConsoleTray
{
  internal static class Program
  {
    public static string StartWord = "APPTEST START";
    public static string StopWord = "APPTEST STOP";

    public static string ServiceUrl =
#if DEBUG
     "https://localhost:44370/appconsole/";
#else
     "https://zorrobackend.azurewebsites.net/appconsole/";
#endif

    private static GlobalKeyboardHook _globalKeyboardHook;
    private static KeboardPressKeyProcessor _keboardPressKeyProcessor;
    private static InputSimulator _inputSimulator;
    private static Thread _thread;
    private static WindowConsoleProcessor _windowConsoleProcessor;
    private static BusinessServiceClient _serviceClient;

    private static string request = string.Empty;
    private static WindowConsoleSessionId curerentSessionId;
    private static bool responseWork = false;


    [STAThread]
    static void Main()
    {
      LoadSettings();
      
      _serviceClient = new BusinessServiceClient();
      
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

      _serviceClient.Dispose();
    }

    public static void SaveSettings()
    {
      var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

      if (0 == ConfigurationManager.AppSettings.Count)
      {
        config.AppSettings.Settings.Add("StartWord", StartWord);
        config.AppSettings.Settings.Add("StopWord", StopWord);
        config.AppSettings.Settings.Add("ServiceUrl", ServiceUrl);

        config.Save(ConfigurationSaveMode.Full);
      }
      else
      {
        config.AppSettings.Settings["StartWord"].Value = StartWord;
        config.AppSettings.Settings["StopWord"].Value = StopWord;
        config.AppSettings.Settings["ServiceUrl"].Value = ServiceUrl;

        config.Save(ConfigurationSaveMode.Modified);
      }

      ConfigurationManager.RefreshSection("appSettings");
    }


    private static void LoadSettings()
    {
      var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

      if (3 <= ConfigurationManager.AppSettings.Count)
      {
        StartWord = ConfigurationManager.AppSettings["StartWord"];
        StopWord = ConfigurationManager.AppSettings["StopWord"];
        ServiceUrl = ConfigurationManager.AppSettings["ServiceUrl"];
      }
    }

    private static void ExecuteInForeground()
    {
      while (true)
      {
        Thread.Sleep(50);

        if (!string.IsNullOrEmpty(request))
        {
          var serviceRequest = string.Copy(request);
          request = string.Empty;

          Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {curerentSessionId} > {serviceRequest}");
          var response = _serviceClient.GetAnswer(ServiceUrl, serviceRequest);
          Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {curerentSessionId} < {response}");

          if (!string.IsNullOrEmpty(response))
          {
            responseWork = true;
            _inputSimulator.Keyboard.TextEntry(response);
            _inputSimulator.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            responseWork = false;

            //for terminal mode
            //_windowConsoleProcessor.SetSessionText(curerentSessionId, response);
          }

          if (serviceRequest.EndsWith(StopWord))
          {
            curerentSessionId = new WindowConsoleSessionId();
          }
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

            Debug.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff")} - {sessionId} ({curerentSessionId}) : {typedWord}");

            if (sessionId.Equals(curerentSessionId))
            {
              request = typedWord;
            }
            else
            {
              if (typedWord.EndsWith(StartWord))
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
