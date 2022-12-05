// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.CoreSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using rail;
using ReLogic.OS;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Terraria.Social.WeGame
{
  public class CoreSocialModule : ISocialModule
  {
    private RailCallBackHelper _callbackHelper = new RailCallBackHelper();
    private static object _railTickLock = new object();
    private bool isRailValid;

    [DllImport("kernel32.dll")]
    private static extern uint GetCurrentThreadId();

    public static event Action OnTick;

    public void Initialize()
    {
      RailGameID railGameId = new RailGameID();
      ((RailComparableID) railGameId).id_ = 2000328UL;
      string[] strArray;
      if (Main.dedServ)
        strArray = Environment.GetCommandLineArgs();
      else
        strArray = new string[1]{ " " };
      if (rail_api.RailNeedRestartAppForCheckingEnvironment(railGameId, strArray.Length, strArray))
        Environment.Exit(1);
      if (!rail_api.RailInitialize())
        Environment.Exit(1);
      // ISSUE: method pointer
      this._callbackHelper.RegisterCallback((RAILEventID) 2, new RailEventCallBackHandler((object) null, __methodptr(RailEventCallBack)));
      this.isRailValid = true;
      ThreadPool.QueueUserWorkItem(new WaitCallback(this.TickThread), (object) null);
      Main.OnTickForThirdPartySoftwareOnly += new Action(CoreSocialModule.RailEventTick);
    }

    public static void RailEventTick()
    {
      rail_api.RailFireEvents();
      if (!Monitor.TryEnter(CoreSocialModule._railTickLock))
        return;
      Monitor.Pulse(CoreSocialModule._railTickLock);
      Monitor.Exit(CoreSocialModule._railTickLock);
    }

    private void TickThread(object context)
    {
      Monitor.Enter(CoreSocialModule._railTickLock);
      while (this.isRailValid)
      {
        if (CoreSocialModule.OnTick != null)
          CoreSocialModule.OnTick();
        Monitor.Wait(CoreSocialModule._railTickLock);
      }
      Monitor.Exit(CoreSocialModule._railTickLock);
    }

    public void Shutdown()
    {
      if (Platform.IsWindows)
      {
        Application.ApplicationExit += (EventHandler) ((obj, evt) => this.isRailValid = false);
      }
      else
      {
        this.isRailValid = false;
        AppDomain.CurrentDomain.ProcessExit += (EventHandler) ((obj, evt) => this.isRailValid = false);
      }
      this._callbackHelper.UnregisterAllCallback();
      rail_api.RailFinalize();
    }

    public static void RailEventCallBack(RAILEventID eventId, EventBase data)
    {
      if (eventId != 2)
        return;
      CoreSocialModule.ProcessRailSystemStateChange(((RailSystemStateChanged) data).state);
    }

    public static void SaveAndQuitCallBack() => Main.WeGameRequireExitGame();

    private static void ProcessRailSystemStateChange(RailSystemState state)
    {
      if (state != 2 && state != 3)
        return;
      int num = (int) MessageBox.Show("检测到WeGame异常，游戏将自动保存进度并退出游戏", "Terraria--WeGame Error");
      WorldGen.SaveAndQuit(new Action(CoreSocialModule.SaveAndQuitCallBack));
    }
  }
}
