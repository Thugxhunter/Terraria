// Decompiled with JetBrains decompiler
// Type: Terraria.Audio.ProjectileAudioTracker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Audio
{
  public class ProjectileAudioTracker
  {
    private int _expectedType;
    private int _expectedIndex;

    public ProjectileAudioTracker(Projectile proj)
    {
      this._expectedIndex = proj.whoAmI;
      this._expectedType = proj.type;
    }

    public bool IsActiveAndInGame()
    {
      if (Main.gameMenu)
        return false;
      Projectile projectile = Main.projectile[this._expectedIndex];
      return projectile.active && projectile.type == this._expectedType;
    }
  }
}
