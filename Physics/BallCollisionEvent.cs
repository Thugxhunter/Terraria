// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.BallCollisionEvent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.Physics
{
  public struct BallCollisionEvent
  {
    public readonly Vector2 Normal;
    public readonly Vector2 ImpactPoint;
    public readonly Tile Tile;
    public readonly Entity Entity;
    public readonly float TimeScale;

    public BallCollisionEvent(
      float timeScale,
      Vector2 normal,
      Vector2 impactPoint,
      Tile tile,
      Entity entity)
    {
      this.Normal = normal;
      this.ImpactPoint = impactPoint;
      this.Tile = tile;
      this.Entity = entity;
      this.TimeScale = timeScale;
    }
  }
}
