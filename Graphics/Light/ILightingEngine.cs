// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Light.ILightingEngine
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.Graphics.Light
{
  public interface ILightingEngine
  {
    void Rebuild();

    void AddLight(int x, int y, Vector3 color);

    void ProcessArea(Rectangle area);

    Vector3 GetColor(int x, int y);

    void Clear();
  }
}
