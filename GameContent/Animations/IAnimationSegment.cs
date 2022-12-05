// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Animations.IAnimationSegment
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Animations
{
  public interface IAnimationSegment
  {
    float DedicatedTimeNeeded { get; }

    void Draw(ref GameAnimationSegment info);
  }
}
