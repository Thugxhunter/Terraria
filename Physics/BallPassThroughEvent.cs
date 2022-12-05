// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.BallPassThroughEvent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Physics
{
  public struct BallPassThroughEvent
  {
    public readonly Tile Tile;
    public readonly Entity Entity;
    public readonly BallPassThroughType Type;
    public readonly float TimeScale;

    public BallPassThroughEvent(
      float timeScale,
      Tile tile,
      Entity entity,
      BallPassThroughType type)
    {
      this.Tile = tile;
      this.Entity = entity;
      this.Type = type;
      this.TimeScale = timeScale;
    }
  }
}
