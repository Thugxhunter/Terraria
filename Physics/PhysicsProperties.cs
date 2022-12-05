// Decompiled with JetBrains decompiler
// Type: Terraria.Physics.PhysicsProperties
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Physics
{
  public class PhysicsProperties
  {
    public readonly float Gravity;
    public readonly float Drag;

    public PhysicsProperties(float gravity, float drag)
    {
      this.Gravity = gravity;
      this.Drag = drag;
    }
  }
}
