﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Animations.IAnimationSegmentAction`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.Animations
{
  public interface IAnimationSegmentAction<T>
  {
    void BindTo(T obj);

    int ExpectedLengthOfActionInFrames { get; }

    void ApplyTo(T obj, float localTimeForObj);

    void SetDelay(float delay);
  }
}
