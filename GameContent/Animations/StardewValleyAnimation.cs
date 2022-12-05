// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Animations.StardewValleyAnimation
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria.DataStructures;

namespace Terraria.GameContent.Animations
{
  public class StardewValleyAnimation
  {
    private List<IAnimationSegment> _segments = new List<IAnimationSegment>();

    public StardewValleyAnimation() => this.ComposeAnimation();

    private void ComposeAnimation()
    {
      Asset<Texture2D> asset1 = TextureAssets.Extra[247];
      Rectangle r1 = asset1.Frame();
      DrawData data1 = new DrawData(asset1.Value, Vector2.Zero, new Rectangle?(r1), Color.White, 0.0f, r1.Size() * new Vector2(0.5f, 0.5f), 1f, SpriteEffects.None);
      int targetTime = 128;
      int duration1 = 60;
      int durationInFrames = 360;
      int duration2 = 60;
      int num = 4;
      this._segments.Add((IAnimationSegment) new Segments.SpriteSegment(asset1, targetTime, data1, Vector2.Zero).UseShaderEffect((Segments.SpriteSegment.IShaderEffect) new Segments.SpriteSegment.MaskedFadeEffect(new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.GetMatrixForAnimation), "StardewValleyFade", 8, num).WithPanX(new Segments.Panning()
      {
        Delay = 128f,
        Duration = (float) (durationInFrames - 120 + duration1 - 60),
        AmountOverTime = 0.55f,
        StartAmount = -0.4f
      }).WithPanY(new Segments.Panning()
      {
        StartAmount = 0.0f
      })).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.Zero)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.One, duration1)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.Zero, duration2)));
      Asset<Texture2D> asset2 = TextureAssets.Extra[249];
      Rectangle r2 = asset2.Frame(verticalFrames: 8);
      DrawData data2 = new DrawData(asset2.Value, Vector2.Zero, new Rectangle?(r2), Color.White, 0.0f, r2.Size() * new Vector2(0.5f, 0.5f), 1f, SpriteEffects.None);
      this._segments.Add((IAnimationSegment) new Segments.SpriteSegment(asset2, targetTime, data2, Vector2.Zero).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.Zero)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.One, duration1)).With((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.SetFrameSequence(100, new Point[8]
      {
        new Point(0, 0),
        new Point(0, 1),
        new Point(0, 2),
        new Point(0, 3),
        new Point(0, 4),
        new Point(0, 5),
        new Point(0, 6),
        new Point(0, 7)
      }, num, 0, 0)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.Wait(durationInFrames)).Then((IAnimationSegmentAction<Segments.LooseSprite>) new Actions.Sprites.OutCircleScale(Vector2.Zero, duration2)));
    }

    private Matrix GetMatrixForAnimation() => Main.Transform;

    public void Draw(SpriteBatch spriteBatch, int timeInAnimation, Vector2 positionInScreen)
    {
      GameAnimationSegment info = new GameAnimationSegment()
      {
        SpriteBatch = spriteBatch,
        AnchorPositionOnScreen = positionInScreen,
        TimeInAnimation = timeInAnimation,
        DisplayOpacity = 1f
      };
      for (int index = 0; index < this._segments.Count; ++index)
        this._segments[index].Draw(ref info);
    }
  }
}
