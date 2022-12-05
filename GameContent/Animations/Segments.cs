// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Animations.Segments
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Shaders;
using Terraria.Localization;
using Terraria.UI.Chat;

namespace Terraria.GameContent.Animations
{
  public class Segments
  {
    private const float PixelsToRollUpPerFrame = 0.5f;

    public class LocalizedTextSegment : IAnimationSegment
    {
      private const int PixelsForALine = 120;
      private LocalizedText _text;
      private float _timeToShowPeak;
      private Vector2 _anchorOffset;

      public float DedicatedTimeNeeded => 240f;

      public LocalizedTextSegment(float timeInAnimation, string textKey)
      {
        this._text = Language.GetText(textKey);
        this._timeToShowPeak = timeInAnimation;
      }

      public LocalizedTextSegment(
        float timeInAnimation,
        LocalizedText textObject,
        Vector2 anchorOffset)
      {
        this._text = textObject;
        this._timeToShowPeak = timeInAnimation;
        this._anchorOffset = anchorOffset;
      }

      public void Draw(ref GameAnimationSegment info)
      {
        float num1 = 250f;
        float num2 = 250f;
        int timeInAnimation = info.TimeInAnimation;
        float num3 = Utils.GetLerpValue(this._timeToShowPeak - num1, this._timeToShowPeak, (float) timeInAnimation, true) * Utils.GetLerpValue(this._timeToShowPeak + num2, this._timeToShowPeak, (float) timeInAnimation, true);
        if ((double) num3 <= 0.0)
          return;
        float num4 = this._timeToShowPeak - (float) timeInAnimation;
        Vector2 position = info.AnchorPositionOnScreen + new Vector2(0.0f, num4 * 0.5f) + this._anchorOffset;
        Vector2 baseScale = new Vector2(0.7f);
        float Hue = (float) ((double) Main.GlobalTimeWrappedHourly * 0.019999999552965164 % 1.0);
        if ((double) Hue < 0.0)
          ++Hue;
        Color rgb = Main.hslToRgb(Hue, 1f, 0.5f);
        string text = this._text.Value;
        Vector2 origin = FontAssets.DeathText.Value.MeasureString(text) * 0.5f;
        float num5 = (float) (1.0 - (1.0 - (double) num3) * (1.0 - (double) num3));
        ChatManager.DrawColorCodedStringShadow(info.SpriteBatch, FontAssets.DeathText.Value, text, position, rgb * num5 * num5 * 0.25f * info.DisplayOpacity, 0.0f, origin, baseScale);
        ChatManager.DrawColorCodedString(info.SpriteBatch, FontAssets.DeathText.Value, text, position, Color.White * num5 * info.DisplayOpacity, 0.0f, origin, baseScale);
      }
    }

    public abstract class AnimationSegmentWithActions<T> : IAnimationSegment
    {
      private int _dedicatedTimeNeeded;
      private int _lastDedicatedTimeNeeded;
      protected int _targetTime;
      private List<IAnimationSegmentAction<T>> _actions = new List<IAnimationSegmentAction<T>>();

      public float DedicatedTimeNeeded => (float) this._dedicatedTimeNeeded;

      public AnimationSegmentWithActions(int targetTime)
      {
        this._targetTime = targetTime;
        this._dedicatedTimeNeeded = 0;
      }

      protected void ProcessActions(T obj, float localTimeForObject)
      {
        for (int index = 0; index < this._actions.Count; ++index)
          this._actions[index].ApplyTo(obj, localTimeForObject);
      }

      public Segments.AnimationSegmentWithActions<T> Then(IAnimationSegmentAction<T> act)
      {
        this.Bind(act);
        act.SetDelay((float) this._dedicatedTimeNeeded);
        this._actions.Add(act);
        this._lastDedicatedTimeNeeded = this._dedicatedTimeNeeded;
        this._dedicatedTimeNeeded += act.ExpectedLengthOfActionInFrames;
        return this;
      }

      public Segments.AnimationSegmentWithActions<T> With(IAnimationSegmentAction<T> act)
      {
        this.Bind(act);
        act.SetDelay((float) this._lastDedicatedTimeNeeded);
        this._actions.Add(act);
        return this;
      }

      protected abstract void Bind(IAnimationSegmentAction<T> act);

      public abstract void Draw(ref GameAnimationSegment info);
    }

    public class PlayerSegment : Segments.AnimationSegmentWithActions<Player>
    {
      private Player _player;
      private Vector2 _anchorOffset;
      private Vector2 _normalizedOriginForHitbox;
      private Segments.PlayerSegment.IShaderEffect _shaderEffect;
      private static Item _blankItem = new Item();

      public PlayerSegment(int targetTime, Vector2 anchorOffset, Vector2 normalizedHitboxOrigin)
        : base(targetTime)
      {
        this._player = new Player();
        this._anchorOffset = anchorOffset;
        this._normalizedOriginForHitbox = normalizedHitboxOrigin;
      }

      public Segments.PlayerSegment UseShaderEffect(
        Segments.PlayerSegment.IShaderEffect shaderEffect)
      {
        this._shaderEffect = shaderEffect;
        return this;
      }

      protected override void Bind(IAnimationSegmentAction<Player> act) => act.BindTo(this._player);

      public override void Draw(ref GameAnimationSegment info)
      {
        if ((double) info.TimeInAnimation > (double) this._targetTime + (double) this.DedicatedTimeNeeded || info.TimeInAnimation < this._targetTime)
          return;
        this.ResetPlayerAnimation(ref info);
        this.ProcessActions(this._player, (float) (info.TimeInAnimation - this._targetTime));
        if ((double) info.DisplayOpacity == 0.0)
          return;
        this._player.ResetEffects();
        this._player.ResetVisibleAccessories();
        this._player.UpdateMiscCounter();
        this._player.UpdateDyes();
        this._player.PlayerFrame();
        this._player.socialIgnoreLight = true;
        Player player1 = this._player;
        player1.position = player1.position + Main.screenPosition;
        Player player2 = this._player;
        player2.position = player2.position - new Vector2((float) (this._player.width / 2), (float) this._player.height);
        this._player.opacityForAnimation *= info.DisplayOpacity;
        Item obj = this._player.inventory[this._player.selectedItem];
        this._player.inventory[this._player.selectedItem] = Segments.PlayerSegment._blankItem;
        float num = 1f - this._player.opacityForAnimation;
        float shadow = 0.0f;
        if (this._shaderEffect != null)
          this._shaderEffect.BeforeDrawing(ref info);
        Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, this._player.position, 0.0f, this._player.fullRotationOrigin, shadow);
        if (this._shaderEffect != null)
          this._shaderEffect.AfterDrawing(ref info);
        this._player.inventory[this._player.selectedItem] = obj;
      }

      private void ResetPlayerAnimation(ref GameAnimationSegment info)
      {
        this._player.CopyVisuals(Main.LocalPlayer);
        this._player.position = info.AnchorPositionOnScreen + this._anchorOffset;
        this._player.opacityForAnimation = 1f;
      }

      public interface IShaderEffect
      {
        void BeforeDrawing(ref GameAnimationSegment info);

        void AfterDrawing(ref GameAnimationSegment info);
      }

      public class ImmediateSpritebatchForPlayerDyesEffect : Segments.PlayerSegment.IShaderEffect
      {
        public void BeforeDrawing(ref GameAnimationSegment info)
        {
          info.SpriteBatch.End();
          info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
        }

        public void AfterDrawing(ref GameAnimationSegment info)
        {
          info.SpriteBatch.End();
          info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect) null, Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll);
        }
      }
    }

    public class NPCSegment : Segments.AnimationSegmentWithActions<NPC>
    {
      private NPC _npc;
      private Vector2 _anchorOffset;
      private Vector2 _normalizedOriginForHitbox;

      public NPCSegment(
        int targetTime,
        int npcId,
        Vector2 anchorOffset,
        Vector2 normalizedNPCHitboxOrigin)
        : base(targetTime)
      {
        this._npc = new NPC();
        this._npc.SetDefaults(npcId);
        this._npc.IsABestiaryIconDummy = true;
        this._anchorOffset = anchorOffset;
        this._normalizedOriginForHitbox = normalizedNPCHitboxOrigin;
      }

      protected override void Bind(IAnimationSegmentAction<NPC> act) => act.BindTo(this._npc);

      public override void Draw(ref GameAnimationSegment info)
      {
        if ((double) info.TimeInAnimation > (double) this._targetTime + (double) this.DedicatedTimeNeeded || info.TimeInAnimation < this._targetTime)
          return;
        this.ResetNPCAnimation(ref info);
        this.ProcessActions(this._npc, (float) (info.TimeInAnimation - this._targetTime));
        if (this._npc.alpha >= (int) byte.MaxValue)
          return;
        this._npc.FindFrame();
        ITownNPCProfile profile;
        if (TownNPCProfiles.Instance.GetProfile(this._npc.type, out profile))
          TextureAssets.Npc[this._npc.type] = profile.GetTextureNPCShouldUse(this._npc);
        this._npc.Opacity *= info.DisplayOpacity;
        Main.instance.DrawNPCDirect(info.SpriteBatch, this._npc, this._npc.behindTiles, Vector2.Zero);
      }

      private void ResetNPCAnimation(ref GameAnimationSegment info)
      {
        this._npc.position = info.AnchorPositionOnScreen + this._anchorOffset - this._npc.Size * this._normalizedOriginForHitbox;
        this._npc.alpha = 0;
        this._npc.velocity = Vector2.Zero;
      }
    }

    public class LooseSprite
    {
      private DrawData _originalDrawData;
      private Asset<Texture2D> _asset;
      public DrawData CurrentDrawData;
      public float CurrentOpacity;

      public LooseSprite(DrawData data, Asset<Texture2D> asset)
      {
        this._originalDrawData = data;
        this._asset = asset;
        this.Reset();
      }

      public void Reset()
      {
        this._originalDrawData.texture = this._asset.Value;
        this.CurrentDrawData = this._originalDrawData;
        this.CurrentOpacity = 1f;
      }
    }

    public class SpriteSegment : Segments.AnimationSegmentWithActions<Segments.LooseSprite>
    {
      private Segments.LooseSprite _sprite;
      private Vector2 _anchorOffset;
      private Segments.SpriteSegment.IShaderEffect _shaderEffect;

      public SpriteSegment(
        Asset<Texture2D> asset,
        int targetTime,
        DrawData data,
        Vector2 anchorOffset)
        : base(targetTime)
      {
        this._sprite = new Segments.LooseSprite(data, asset);
        this._anchorOffset = anchorOffset;
      }

      protected override void Bind(IAnimationSegmentAction<Segments.LooseSprite> act) => act.BindTo(this._sprite);

      public Segments.SpriteSegment UseShaderEffect(
        Segments.SpriteSegment.IShaderEffect shaderEffect)
      {
        this._shaderEffect = shaderEffect;
        return this;
      }

      public override void Draw(ref GameAnimationSegment info)
      {
        if ((double) info.TimeInAnimation > (double) this._targetTime + (double) this.DedicatedTimeNeeded || info.TimeInAnimation < this._targetTime)
          return;
        this.ResetSpriteAnimation(ref info);
        this.ProcessActions(this._sprite, (float) (info.TimeInAnimation - this._targetTime));
        DrawData currentDrawData = this._sprite.CurrentDrawData;
        currentDrawData.position += info.AnchorPositionOnScreen + this._anchorOffset;
        currentDrawData.color *= this._sprite.CurrentOpacity * info.DisplayOpacity;
        if (this._shaderEffect != null)
          this._shaderEffect.BeforeDrawing(ref info, ref currentDrawData);
        currentDrawData.Draw(info.SpriteBatch);
        if (this._shaderEffect == null)
          return;
        this._shaderEffect.AfterDrawing(ref info, ref currentDrawData);
      }

      private void ResetSpriteAnimation(ref GameAnimationSegment info) => this._sprite.Reset();

      public interface IShaderEffect
      {
        void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData);

        void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData);
      }

      public class MaskedFadeEffect : Segments.SpriteSegment.IShaderEffect
      {
        private readonly string _shaderKey;
        private readonly int _verticalFrameCount;
        private readonly int _verticalFrameWait;
        private Segments.Panning _panX;
        private Segments.Panning _panY;
        private Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction _fetchMatrix;

        public MaskedFadeEffect(
          Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction fetchMatrixMethod = null,
          string shaderKey = "MaskedFade",
          int verticalFrameCount = 1,
          int verticalFrameWait = 1)
        {
          this._fetchMatrix = fetchMatrixMethod;
          this._shaderKey = shaderKey;
          this._verticalFrameCount = verticalFrameCount;
          if (verticalFrameWait < 1)
            verticalFrameWait = 1;
          this._verticalFrameWait = verticalFrameWait;
          if (this._fetchMatrix != null)
            return;
          this._fetchMatrix = new Segments.SpriteSegment.MaskedFadeEffect.GetMatrixAction(this.DefaultFetchMatrix);
        }

        private Matrix DefaultFetchMatrix() => Main.CurrentFrameFlags.Hacks.CurrentBackgroundMatrixForCreditsRoll;

        public void BeforeDrawing(ref GameAnimationSegment info, ref DrawData drawData)
        {
          info.SpriteBatch.End();
          info.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect) null, this._fetchMatrix());
          MiscShaderData miscShaderData = GameShaders.Misc[this._shaderKey];
          miscShaderData.UseShaderSpecificData(new Vector4(1f / (float) this._verticalFrameCount, (float) (info.TimeInAnimation / this._verticalFrameWait % this._verticalFrameCount) / (float) this._verticalFrameCount, this._panX.GetPanAmount((float) info.TimeInAnimation), this._panY.GetPanAmount((float) info.TimeInAnimation)));
          miscShaderData.Apply(new DrawData?(drawData));
        }

        public Segments.SpriteSegment.MaskedFadeEffect WithPanX(Segments.Panning panning)
        {
          this._panX = panning;
          return this;
        }

        public Segments.SpriteSegment.MaskedFadeEffect WithPanY(Segments.Panning panning)
        {
          this._panY = panning;
          return this;
        }

        public void AfterDrawing(ref GameAnimationSegment info, ref DrawData drawData)
        {
          Main.pixelShader.CurrentTechnique.Passes[0].Apply();
          info.SpriteBatch.End();
          info.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect) null, this._fetchMatrix());
        }

        public delegate Matrix GetMatrixAction();
      }
    }

    public struct Panning
    {
      public float AmountOverTime;
      public float StartAmount;
      public float Delay;
      public float Duration;

      public float GetPanAmount(float time) => this.StartAmount + MathHelper.Clamp((time - this.Delay) / this.Duration, 0.0f, 1f) * this.AmountOverTime;
    }

    public class EmoteSegment : IAnimationSegment
    {
      private int _targetTime;
      private Vector2 _offset;
      private SpriteEffects _effect;
      private int _emoteId;
      private Vector2 _velocity;

      public float DedicatedTimeNeeded { get; private set; }

      public EmoteSegment(
        int emoteId,
        int targetTime,
        int timeToPlay,
        Vector2 position,
        SpriteEffects drawEffect,
        Vector2 velocity = default (Vector2))
      {
        this._emoteId = emoteId;
        this._targetTime = targetTime;
        this._effect = drawEffect;
        this._offset = position;
        this._velocity = velocity;
        this.DedicatedTimeNeeded = (float) timeToPlay;
      }

      public void Draw(ref GameAnimationSegment info)
      {
        int num = info.TimeInAnimation - this._targetTime;
        if (num < 0 || (double) num >= (double) this.DedicatedTimeNeeded)
          return;
        Vector2 position = (info.AnchorPositionOnScreen + this._offset + this._velocity * (float) num).Floor();
        bool flag = num < 6 || (double) num >= (double) this.DedicatedTimeNeeded - 6.0;
        Texture2D texture2D = TextureAssets.Extra[48].Value;
        Rectangle rectangle = texture2D.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, flag ? 0 : 1);
        Vector2 origin = new Vector2((float) (rectangle.Width / 2), (float) rectangle.Height);
        SpriteEffects effect = this._effect;
        info.SpriteBatch.Draw(texture2D, position, new Rectangle?(rectangle), Color.White * info.DisplayOpacity, 0.0f, origin, 1f, effect, 0.0f);
        if (flag)
          return;
        switch (this._emoteId)
        {
          case 87:
          case 89:
            if (effect.HasFlag((Enum) SpriteEffects.FlipHorizontally))
            {
              effect &= ~SpriteEffects.FlipHorizontally;
              position.X += 4f;
              break;
            }
            break;
        }
        info.SpriteBatch.Draw(texture2D, position, new Rectangle?(this.GetFrame(num % 20)), Color.White, 0.0f, origin, 1f, effect, 0.0f);
      }

      private Rectangle GetFrame(int wrappedTime)
      {
        int num = wrappedTime >= 10 ? 1 : 0;
        return TextureAssets.Extra[48].Value.Frame(8, EmoteBubble.EMOTE_SHEET_VERTICAL_FRAMES, this._emoteId % 4 * 2 + num, this._emoteId / 4 + 1);
      }
    }
  }
}
