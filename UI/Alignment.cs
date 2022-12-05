// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Alignment
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.UI
{
  public struct Alignment
  {
    public static readonly Alignment TopLeft = new Alignment(0.0f, 0.0f);
    public static readonly Alignment Top = new Alignment(0.5f, 0.0f);
    public static readonly Alignment TopRight = new Alignment(1f, 0.0f);
    public static readonly Alignment Left = new Alignment(0.0f, 0.5f);
    public static readonly Alignment Center = new Alignment(0.5f, 0.5f);
    public static readonly Alignment Right = new Alignment(1f, 0.5f);
    public static readonly Alignment BottomLeft = new Alignment(0.0f, 1f);
    public static readonly Alignment Bottom = new Alignment(0.5f, 1f);
    public static readonly Alignment BottomRight = new Alignment(1f, 1f);
    public readonly float VerticalOffsetMultiplier;
    public readonly float HorizontalOffsetMultiplier;

    public Vector2 OffsetMultiplier => new Vector2(this.HorizontalOffsetMultiplier, this.VerticalOffsetMultiplier);

    private Alignment(float horizontal, float vertical)
    {
      this.HorizontalOffsetMultiplier = horizontal;
      this.VerticalOffsetMultiplier = vertical;
    }
  }
}
