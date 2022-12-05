// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.States.UIWorldLoad
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.GameContent.UI.Elements;
using Terraria.GameInput;
using Terraria.Localization;
using Terraria.UI;
using Terraria.UI.Gamepad;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.UI.States
{
  public class UIWorldLoad : UIState
  {
    private UIGenProgressBar _progressBar = new UIGenProgressBar();
    private UIHeader _progressMessage = new UIHeader();
    private GenerationProgress _progress;

    public UIWorldLoad()
    {
      this._progressBar.Top.Pixels = 270f;
      this._progressBar.HAlign = 0.5f;
      this._progressBar.VAlign = 0.0f;
      this._progressBar.Recalculate();
      this._progressMessage.CopyStyle((UIElement) this._progressBar);
      this._progressMessage.Top.Pixels -= 70f;
      this._progressMessage.Recalculate();
      this.Append((UIElement) this._progressBar);
      this.Append((UIElement) this._progressMessage);
    }

    public override void OnActivate()
    {
      if (!PlayerInput.UsingGamepadUI)
        return;
      UILinkPointNavigator.Points[3000].Unlink();
      UILinkPointNavigator.ChangePoint(3000);
    }

    public override void Update(GameTime gameTime)
    {
      this._progressBar.Top.Pixels = MathHelper.Lerp(270f, 370f, Utils.GetLerpValue(600f, 700f, (float) Main.screenHeight, true));
      this._progressMessage.Top.Pixels = this._progressBar.Top.Pixels - 70f;
      this._progressBar.Recalculate();
      this._progressMessage.Recalculate();
      base.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
      this._progress = WorldGenerator.CurrentGenerationProgress;
      if (this._progress == null)
        return;
      base.Draw(spriteBatch);
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      float overallProgress = 0.0f;
      float currentProgress = 0.0f;
      string str1 = string.Empty;
      if (this._progress != null)
      {
        overallProgress = (float) this._progress.TotalProgress;
        currentProgress = (float) this._progress.Value;
        str1 = this._progress.Message;
      }
      this._progressBar.SetProgress(overallProgress, currentProgress);
      this._progressMessage.Text = str1;
      if (WorldGen.drunkWorldGenText && !WorldGen.placingTraps && !WorldGen.getGoodWorldGen)
      {
        this._progressMessage.Text = string.Concat((object) Main.rand.Next(999999999));
        for (int index = 0; index < 3; ++index)
        {
          if (Main.rand.Next(2) == 0)
            this._progressMessage.Text += (string) (object) Main.rand.Next(999999999);
        }
      }
      if (WorldGen.getGoodWorldGen)
      {
        if (!WorldGen.noTrapsWorldGen || !WorldGen.placingTraps)
        {
          string str2 = "";
          for (int startIndex = this._progressMessage.Text.Length - 1; startIndex >= 0; --startIndex)
            str2 += this._progressMessage.Text.Substring(startIndex, 1);
          this._progressMessage.Text = str2;
        }
      }
      else if (WorldGen.notTheBees)
        this._progressMessage.Text = Language.GetTextValue("UI.WorldGenEasterEgg_GeneratingBees");
      Main.gameTips.Update();
      Main.gameTips.Draw();
      this.UpdateGamepadSquiggle();
    }

    private void UpdateGamepadSquiggle()
    {
      Vector2 vector2 = new Vector2((float) Math.Cos((double) Main.GlobalTimeWrappedHourly * 6.2831854820251465), (float) Math.Sin((double) Main.GlobalTimeWrappedHourly * 6.2831854820251465 * 2.0)) * new Vector2(30f, 15f) + Vector2.UnitY * 20f;
      UILinkPointNavigator.Points[3000].Unlink();
      UILinkPointNavigator.SetPosition(3000, new Vector2((float) Main.screenWidth, (float) Main.screenHeight) / 2f + vector2);
    }

    public string GetStatusText() => this._progress == null ? string.Format("{0:0.0%} - ... - {1:0.0%}", (object) 0, (object) 0) : string.Format("{0:0.0%} - " + this._progress.Message + " - {1:0.0%}", (object) this._progress.TotalProgress, (object) this._progress.Value);
  }
}
