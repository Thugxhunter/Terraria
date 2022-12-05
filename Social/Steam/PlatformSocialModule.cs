// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.PlatformSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Steamworks;
using Terraria.GameInput;
using Terraria.UI.Gamepad;

namespace Terraria.Social.Steam
{
  public class PlatformSocialModule : Terraria.Social.Base.PlatformSocialModule
  {
    public override void Initialize()
    {
      if (Main.dedServ)
        return;
      int num;
      PlayerInput.UseSteamDeckIfPossible = (num = SteamUtils.IsSteamRunningOnSteamDeck() ? 1 : 0) != 0;
      if (num != 0)
      {
        PlayerInput.SettingsForUI.SetCursorMode(CursorMode.Gamepad);
        PlayerInput.CurrentInputMode = InputMode.XBoxGamepadUI;
        GamepadMainMenuHandler.MoveCursorOnNextRun = true;
        PlayerInput.PreventFirstMousePositionGrab = true;
      }
      if (num == 0)
        return;
      Main.graphics.PreferredBackBufferWidth = Main.screenWidth = 1280;
      Main.graphics.PreferredBackBufferHeight = Main.screenHeight = 800;
      Main.startFullscreen = true;
      Main.toggleFullscreen = true;
      Main.screenBorderless = false;
      Main.screenMaximized = false;
      Main.InitialMapScale = Main.MapScale = 0.73f;
      Main.UIScale = 1.07f;
    }

    public override void Shutdown()
    {
    }
  }
}
