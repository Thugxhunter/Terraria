// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.ResourceSets.CommonResourceBarMethods
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.UI.ResourceSets
{
  public class CommonResourceBarMethods
  {
    public static void DrawLifeMouseOver()
    {
      if (Main.mouseText)
        return;
      Player localPlayer = Main.LocalPlayer;
      localPlayer.cursorItemIconEnabled = false;
      string text = localPlayer.statLife.ToString() + "/" + (object) localPlayer.statLifeMax2;
      Main.instance.MouseTextHackZoom(text);
      Main.mouseText = true;
    }

    public static void DrawManaMouseOver()
    {
      if (Main.mouseText)
        return;
      Player localPlayer = Main.LocalPlayer;
      localPlayer.cursorItemIconEnabled = false;
      string text = localPlayer.statMana.ToString() + "/" + (object) localPlayer.statManaMax2;
      Main.instance.MouseTextHackZoom(text);
      Main.mouseText = true;
    }
  }
}
