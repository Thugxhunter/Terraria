// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Ambience.AmbientSkyDrawCache
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Ambience
{
  public class AmbientSkyDrawCache
  {
    public static AmbientSkyDrawCache Instance = new AmbientSkyDrawCache();
    public AmbientSkyDrawCache.UnderworldCache[] Underworld = new AmbientSkyDrawCache.UnderworldCache[5];
    public AmbientSkyDrawCache.OceanLineCache OceanLineInfo;

    public void SetUnderworldInfo(int drawIndex, float scale) => this.Underworld[drawIndex] = new AmbientSkyDrawCache.UnderworldCache()
    {
      Scale = scale
    };

    public void SetOceanLineInfo(float yScreenPosition, float oceanOpacity) => this.OceanLineInfo = new AmbientSkyDrawCache.OceanLineCache()
    {
      YScreenPosition = yScreenPosition,
      OceanOpacity = oceanOpacity
    };

    public struct UnderworldCache
    {
      public float Scale;
    }

    public struct OceanLineCache
    {
      public float YScreenPosition;
      public float OceanOpacity;
    }
  }
}
