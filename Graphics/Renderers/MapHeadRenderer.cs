// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Renderers.MapHeadRenderer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;

namespace Terraria.Graphics.Renderers
{
  public class MapHeadRenderer : INeedRenderTargetContent
  {
    private bool _anyDirty;
    private PlayerHeadDrawRenderTargetContent[] _playerRenders = new PlayerHeadDrawRenderTargetContent[(int) byte.MaxValue];
    private readonly List<DrawData> _drawData = new List<DrawData>();

    public MapHeadRenderer()
    {
      for (int index = 0; index < this._playerRenders.Length; ++index)
        this._playerRenders[index] = new PlayerHeadDrawRenderTargetContent();
    }

    public void Reset()
    {
      this._anyDirty = false;
      this._drawData.Clear();
      for (int index = 0; index < this._playerRenders.Length; ++index)
        this._playerRenders[index].Reset();
    }

    public void DrawPlayerHead(
      Camera camera,
      Player drawPlayer,
      Vector2 position,
      float alpha = 1f,
      float scale = 1f,
      Color borderColor = default (Color))
    {
      PlayerHeadDrawRenderTargetContent playerRender = this._playerRenders[drawPlayer.whoAmI];
      playerRender.UsePlayer(drawPlayer);
      playerRender.UseColor(borderColor);
      playerRender.Request();
      this._anyDirty = true;
      this._drawData.Clear();
      if (!playerRender.IsReady)
        return;
      RenderTarget2D target = playerRender.GetTarget();
      this._drawData.Add(new DrawData((Texture2D) target, position, new Rectangle?(), Color.White, 0.0f, target.Size() / 2f, scale, SpriteEffects.None));
      this.RenderDrawData(drawPlayer);
    }

    private void RenderDrawData(Player drawPlayer)
    {
      Effect pixelShader = Main.pixelShader;
      Projectile[] projectile = Main.projectile;
      SpriteBatch spriteBatch = Main.spriteBatch;
      for (int index = 0; index < this._drawData.Count; ++index)
      {
        DrawData cdd = this._drawData[index];
        if (!cdd.sourceRect.HasValue)
          cdd.sourceRect = new Rectangle?(cdd.texture.Frame());
        PlayerDrawHelper.SetShaderForData(drawPlayer, drawPlayer.cHead, ref cdd);
        if (cdd.texture != null)
          cdd.Draw(spriteBatch);
      }
      pixelShader.CurrentTechnique.Passes[0].Apply();
    }

    public bool IsReady => !this._anyDirty;

    public void PrepareRenderTarget(GraphicsDevice device, SpriteBatch spriteBatch)
    {
      if (!this._anyDirty)
        return;
      for (int index = 0; index < this._playerRenders.Length; ++index)
        this._playerRenders[index].PrepareRenderTarget(device, spriteBatch);
      this._anyDirty = false;
    }

    private void CreateOutlines(float alpha, float scale, Color borderColor)
    {
      if (!(borderColor != Color.Transparent))
        return;
      List<DrawData> collection = new List<DrawData>((IEnumerable<DrawData>) this._drawData);
      List<DrawData> drawDataList = new List<DrawData>((IEnumerable<DrawData>) this._drawData);
      this._drawData.Clear();
      float num1 = 2f * scale;
      Color color1 = borderColor * (alpha * alpha);
      Color color2 = Color.Black * (alpha * alpha);
      int colorOnlyShaderIndex = ContentSamples.CommonlyUsedContentSamples.ColorOnlyShaderIndex;
      for (int index = 0; index < drawDataList.Count; ++index)
      {
        DrawData drawData = drawDataList[index] with
        {
          shader = colorOnlyShaderIndex,
          color = color2
        };
        drawDataList[index] = drawData;
      }
      int num2 = 2;
      for (int index1 = -num2; index1 <= num2; ++index1)
      {
        for (int index2 = -num2; index2 <= num2; ++index2)
        {
          if (Math.Abs(index1) + Math.Abs(index2) == num2)
          {
            Vector2 vector2 = new Vector2((float) index1 * num1, (float) index2 * num1);
            for (int index3 = 0; index3 < drawDataList.Count; ++index3)
            {
              DrawData drawData = drawDataList[index3];
              drawData.position += vector2;
              this._drawData.Add(drawData);
            }
          }
        }
      }
      for (int index = 0; index < drawDataList.Count; ++index)
      {
        DrawData drawData = drawDataList[index] with
        {
          shader = colorOnlyShaderIndex,
          color = color1
        };
        drawDataList[index] = drawData;
      }
      Vector2 vector2_1 = Vector2.Zero;
      int num3 = 1;
      for (int index4 = -num3; index4 <= num3; ++index4)
      {
        for (int index5 = -num3; index5 <= num3; ++index5)
        {
          if (Math.Abs(index4) + Math.Abs(index5) == num3)
          {
            vector2_1 = new Vector2((float) index4 * num1, (float) index5 * num1);
            for (int index6 = 0; index6 < drawDataList.Count; ++index6)
            {
              DrawData drawData = drawDataList[index6];
              drawData.position += vector2_1;
              this._drawData.Add(drawData);
            }
          }
        }
      }
      this._drawData.AddRange((IEnumerable<DrawData>) collection);
    }
  }
}
