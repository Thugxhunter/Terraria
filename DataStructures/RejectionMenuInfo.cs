// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.RejectionMenuInfo
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.Audio;

namespace Terraria.DataStructures
{
  public class RejectionMenuInfo
  {
    public ReturnFromRejectionMenuAction ExitAction;
    public string TextToShow;

    public void DefaultExitAction()
    {
      SoundEngine.PlaySound(11);
      Main.menuMode = 0;
      Main.netMode = 0;
    }
  }
}
