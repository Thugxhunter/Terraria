// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.NPCHeadRenderer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent;

namespace Terraria.Graphics.Renderers
{
  public class NPCHeadRenderer : INeedRenderTargetContent
  {
    private NPCHeadDrawRenderTargetContent[] _contents;
    private Asset<Texture2D>[] _matchingArray;

    public NPCHeadRenderer(Asset<Texture2D>[] matchingArray)
    {
      this._matchingArray = matchingArray;
      this.Reset();
    }

    public void Reset() => this._contents = new NPCHeadDrawRenderTargetContent[this._matchingArray.Length];

    public void DrawWithOutlines(
      Entity entity,
      int headId,
      Vector2 position,
      Color color,
      float rotation,
      float scale,
      SpriteEffects effects)
    {
      if (this._contents[headId] == null)
      {
        this._contents[headId] = new NPCHeadDrawRenderTargetContent();
        this._contents[headId].SetTexture(this._matchingArray[headId].Value);
      }
      NPCHeadDrawRenderTargetContent content = this._contents[headId];
      if (content.IsReady)
      {
        RenderTarget2D target = content.GetTarget();
        Main.spriteBatch.Draw((Texture2D) target, position, new Rectangle?(), color, rotation, target.Size() / 2f, scale, effects, 0.0f);
      }
      else
        content.Request();
    }

    public bool IsReady => false;

    public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
    {
      for (int index = 0; index < this._contents.Length; ++index)
      {
        if (this._contents[index] != null && !this._contents[index].IsReady)
          this._contents[index].PrepareRenderTarget(device, spriteBatch);
      }
    }
  }
}
