// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.IssueReportsIndicator
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent.UI
{
  public class IssueReportsIndicator
  {
    private float _displayUpPercent;
    private bool _shouldBeShowing;
    private Asset<Texture2D> _buttonTexture;
    private Asset<Texture2D> _buttonOutlineTexture;

    public void AttemptLettingPlayerKnow()
    {
      this.Setup();
      this._shouldBeShowing = true;
      SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode);
    }

    public void Hide()
    {
      this._shouldBeShowing = false;
      this._displayUpPercent = 0.0f;
    }

    private void OpenUI()
    {
      this.Setup();
      Main.OpenReportsMenu();
    }

    private void Setup()
    {
      this._buttonTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton", (AssetRequestMode) 1);
      this._buttonOutlineTexture = Main.Assets.Request<Texture2D>("Images/UI/Workshop/IssueButton_Outline", (AssetRequestMode) 1);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
      this._displayUpPercent = MathHelper.Clamp(this._displayUpPercent + (float) this._shouldBeShowing.ToDirectionInt(), 0.0f, 1f);
      if ((double) this._displayUpPercent == 0.0)
        return;
      Texture2D texture2D1 = this._buttonTexture.Value;
      Vector2 vector2_1 = Main.ScreenSize.ToVector2() + new Vector2(40f, -80f);
      Vector2 vector2_2 = Vector2.Lerp(vector2_1, vector2_1 + new Vector2(-80f, 0.0f), this._displayUpPercent);
      Rectangle r1 = texture2D1.Frame();
      Vector2 origin = r1.Size() / 2f;
      bool flag = false;
      if (Utils.CenteredRectangle(vector2_2, r1.Size()).Contains(Main.MouseScreen.ToPoint()))
      {
        flag = true;
        string textValue = Language.GetTextValue("UI.IssueReporterHasThingsToShow");
        Main.instance.MouseText(textValue);
        if (Main.mouseLeft)
        {
          this.OpenUI();
          this.Hide();
          return;
        }
      }
      float scale = 1f;
      spriteBatch.Draw(texture2D1, vector2_2, new Rectangle?(r1), Color.White, 0.0f, origin, scale, SpriteEffects.None, 0.0f);
      if (!flag)
        return;
      Texture2D texture2D2 = this._buttonOutlineTexture.Value;
      Rectangle r2 = texture2D2.Frame();
      spriteBatch.Draw(texture2D2, vector2_2, new Rectangle?(r2), Color.White, 0.0f, r2.Size() / 2f, scale, SpriteEffects.None, 0.0f);
    }
  }
}
