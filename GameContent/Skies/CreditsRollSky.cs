// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Skies.CreditsRollSky
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.GameContent.Animations;
using Terraria.GameContent.Skies.CreditsRoll;
using Terraria.Graphics.Effects;

namespace Terraria.GameContent.Skies
{
  public class CreditsRollSky : CustomSky
  {
    private int _endTime;
    private int _currentTime;
    private CreditsRollComposer _composer = new CreditsRollComposer();
    private List<IAnimationSegment> _segmentsInGame = new List<IAnimationSegment>();
    private List<IAnimationSegment> _segmentsInMainMenu = new List<IAnimationSegment>();
    private bool _isActive;
    private bool _wantsToBeSeen;
    private float _opacity;

    public int AmountOfTimeNeededForFullPlay => this._endTime;

    public CreditsRollSky() => this.EnsureSegmentsAreMade();

    public override void Update(GameTime gameTime)
    {
      if (Main.gamePaused || !Main.hasFocus)
        return;
      ++this._currentTime;
      float num = 0.008333334f;
      if (Main.gameMenu)
        num = 0.06666667f;
      this._opacity = MathHelper.Clamp(this._opacity + num * (float) this._wantsToBeSeen.ToDirectionInt(), 0.0f, 1f);
      if ((double) this._opacity == 0.0 && !this._wantsToBeSeen)
      {
        this._isActive = false;
      }
      else
      {
        bool flag = true;
        if (!Main.CanPlayCreditsRoll())
          flag = false;
        if (this._currentTime >= this._endTime)
          flag = false;
        if (flag)
          return;
        SkyManager.Instance.Deactivate("CreditsRoll");
      }
    }

    public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
    {
      float num = 1f;
      if ((double) num < (double) minDepth || (double) num > (double) maxDepth)
        return;
      Vector2 vector2 = Main.ScreenSize.ToVector2() / 2f;
      if (Main.gameMenu)
        vector2.Y = 300f;
      GameAnimationSegment info = new GameAnimationSegment()
      {
        SpriteBatch = spriteBatch,
        AnchorPositionOnScreen = vector2,
        TimeInAnimation = this._currentTime,
        DisplayOpacity = this._opacity
      };
      List<IAnimationSegment> animationSegmentList = this._segmentsInGame;
      if (Main.gameMenu)
        animationSegmentList = this._segmentsInMainMenu;
      for (int index = 0; index < animationSegmentList.Count; ++index)
        animationSegmentList[index].Draw(ref info);
    }

    public override bool IsActive() => this._isActive;

    public override void Reset()
    {
      this._currentTime = 0;
      this.EnsureSegmentsAreMade();
      this._isActive = false;
      this._wantsToBeSeen = false;
    }

    public override void Activate(Vector2 position, params object[] args)
    {
      this._isActive = true;
      this._wantsToBeSeen = true;
      if ((double) this._opacity != 0.0)
        return;
      this.EnsureSegmentsAreMade();
      this._currentTime = 0;
    }

    private void EnsureSegmentsAreMade()
    {
      if (this._segmentsInMainMenu.Count > 0 && this._segmentsInGame.Count > 0)
        return;
      this._segmentsInGame.Clear();
      this._composer.FillSegments(this._segmentsInGame, out this._endTime, true);
      this._segmentsInMainMenu.Clear();
      this._composer.FillSegments(this._segmentsInMainMenu, out this._endTime, false);
    }

    public override void Deactivate(params object[] args) => this._wantsToBeSeen = false;
  }
}
