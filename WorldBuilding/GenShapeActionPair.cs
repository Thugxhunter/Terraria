// Decompiled with JetBrains decompiler
// Type: Terraria.WorldBuilding.GenShapeActionPair
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.WorldBuilding
{
  public struct GenShapeActionPair
  {
    public readonly GenShape Shape;
    public readonly GenAction Action;

    public GenShapeActionPair(GenShape shape, GenAction action)
    {
      this.Shape = shape;
      this.Action = action;
    }
  }
}
