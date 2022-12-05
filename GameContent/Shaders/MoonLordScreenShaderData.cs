// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.MoonLordScreenShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class MoonLordScreenShaderData : ScreenShaderData
  {
    private int _moonLordIndex = -1;
    private bool _aimAtPlayer;

    public MoonLordScreenShaderData(string passName, bool aimAtPlayer)
      : base(passName)
    {
      this._aimAtPlayer = aimAtPlayer;
    }

    private void UpdateMoonLordIndex()
    {
      if (this._aimAtPlayer || this._moonLordIndex >= 0 && Main.npc[this._moonLordIndex].active && Main.npc[this._moonLordIndex].type == 398)
        return;
      int num = -1;
      for (int index = 0; index < Main.npc.Length; ++index)
      {
        if (Main.npc[index].active && Main.npc[index].type == 398)
        {
          num = index;
          break;
        }
      }
      this._moonLordIndex = num;
    }

    public override void Apply()
    {
      this.UpdateMoonLordIndex();
      if (this._aimAtPlayer)
        this.UseTargetPosition(Main.LocalPlayer.Center);
      else if (this._moonLordIndex != -1)
        this.UseTargetPosition(Main.npc[this._moonLordIndex].Center);
      base.Apply();
    }
  }
}
