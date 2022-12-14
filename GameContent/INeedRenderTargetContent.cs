// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.INeedRenderTargetContent
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent
{
  public interface INeedRenderTargetContent
  {
    bool IsReady { get; }

    void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch);

    void Reset();
  }
}
