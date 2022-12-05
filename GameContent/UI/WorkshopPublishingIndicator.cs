// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.WorkshopPublishingIndicator
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria.Audio;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.GameContent.UI
{
  public class WorkshopPublishingIndicator
  {
    private float _displayUpPercent;
    private int _frameCounter;
    private bool _shouldPlayEndingSound;
    private Asset<Texture2D> _indicatorTexture;
    private int _timesSoundWasPlayed;

    public void Hide()
    {
      this._displayUpPercent = 0.0f;
      this._frameCounter = 0;
      this._timesSoundWasPlayed = 0;
      this._shouldPlayEndingSound = false;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      WorkshopSocialModule workshop = SocialAPI.Workshop;
      if (workshop == null)
        return;
      AWorkshopProgressReporter progressReporter = workshop.ProgressReporter;
      bool hasOngoingTasks = progressReporter.HasOngoingTasks;
      int num1 = (double) this._displayUpPercent == 1.0 ? 1 : 0;
      this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float) hasOngoingTasks.ToDirectionInt() / 60f, 0.0f, 1f);
      bool flag = (double) this._displayUpPercent == 1.0;
      if (num1 != 0 && !flag)
        this._shouldPlayEndingSound = true;
      if ((double) this._displayUpPercent == 0.0)
        return;
      if (this._indicatorTexture == null)
        this._indicatorTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/InProgress", (AssetRequestMode) 1);
      Texture2D texture2D = this._indicatorTexture.Value;
      int num2 = 6;
      ++this._frameCounter;
      int num3 = 5;
      int frameY = this._frameCounter / num3 % num2;
      Vector2 vector2 = Main.ScreenSize.ToVector2() + new Vector2(-40f, 40f);
      Vector2 position = Vector2.Lerp(vector2, vector2 + new Vector2(0.0f, -80f), this._displayUpPercent);
      Rectangle r = texture2D.Frame(verticalFrames: 6, frameY: frameY);
      Vector2 origin1 = r.Size() / 2f;
      spriteBatch.Draw(texture2D, position, new Rectangle?(r), Color.White, 0.0f, origin1, 1f, SpriteEffects.None, 0.0f);
      float progress;
      if (progressReporter.TryGetProgress(out progress) && !float.IsNaN(progress))
      {
        string text = progress.ToString("P");
        DynamicSpriteFont font = FontAssets.ItemStack.Value;
        int scale = 1;
        Vector2 origin2 = font.MeasureString(text) * (float) scale * new Vector2(0.5f, 1f);
        Utils.DrawBorderStringFourWay(spriteBatch, font, text, position.X, position.Y - 10f, Color.White, Color.Black, origin2, (float) scale);
      }
      if ((frameY != 3 ? 0 : (this._frameCounter % num3 == 0 ? 1 : 0)) == 0)
        return;
      if (this._shouldPlayEndingSound)
      {
        this._shouldPlayEndingSound = false;
        this._timesSoundWasPlayed = 0;
        SoundEngine.PlaySound(64);
      }
      if (!hasOngoingTasks)
        return;
      SoundEngine.PlaySound(21, volumeScale: Utils.Remap((float) this._timesSoundWasPlayed, 0.0f, 10f, 1f, 0.0f));
      ++this._timesSoundWasPlayed;
    }
  }
}
