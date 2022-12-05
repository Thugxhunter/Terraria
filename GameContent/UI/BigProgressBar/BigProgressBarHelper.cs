// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.BigProgressBarHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class BigProgressBarHelper
  {
    private const string _bossBarTexturePath = "Images/UI/UI_BossBar";

    public static void DrawBareBonesBar(SpriteBatch spriteBatch, float lifePercent)
    {
      Rectangle destinationRectangle1 = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -50f), new Vector2(400f, 20f));
      Rectangle destinationRectangle2 = destinationRectangle1;
      destinationRectangle2.Inflate(2, 2);
      Texture2D texture = TextureAssets.MagicPixel.Value;
      Rectangle rectangle = new Rectangle(0, 0, 1, 1);
      Rectangle destinationRectangle3 = destinationRectangle1;
      destinationRectangle3.Width = (int) ((double) destinationRectangle3.Width * (double) lifePercent);
      spriteBatch.Draw(texture, destinationRectangle2, new Rectangle?(rectangle), Color.White * 0.6f);
      spriteBatch.Draw(texture, destinationRectangle1, new Rectangle?(rectangle), Color.Black * 0.6f);
      spriteBatch.Draw(texture, destinationRectangle3, new Rectangle?(rectangle), Color.LimeGreen * 0.5f);
    }

    public static void DrawFancyBar(
      SpriteBatch spriteBatch,
      float lifeAmount,
      float lifeMax,
      Texture2D barIconTexture,
      Rectangle barIconFrame)
    {
      Texture2D texture2D = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar", (AssetRequestMode) 1).Value;
      Point p1 = new Point(456, 22);
      Point p2 = new Point(32, 24);
      int verticalFrames = 6;
      Rectangle rectangle1 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 3);
      Color color = Color.White * 0.2f;
      float num1 = lifeAmount / lifeMax;
      int num2 = (int) ((double) p1.X * (double) num1);
      int num3 = num2 - num2 % 2;
      Rectangle rectangle2 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 2);
      rectangle2.X += p2.X;
      rectangle2.Y += p2.Y;
      rectangle2.Width = 2;
      rectangle2.Height = p1.Y;
      Rectangle rectangle3 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 1);
      rectangle3.X += p2.X;
      rectangle3.Y += p2.Y;
      rectangle3.Width = 2;
      rectangle3.Height = p1.Y;
      Rectangle rectangle4 = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -50f), p1.ToVector2());
      Vector2 position = rectangle4.TopLeft() - p2.ToVector2();
      spriteBatch.Draw(texture2D, position, new Rectangle?(rectangle1), color, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle4.TopLeft(), new Rectangle?(rectangle2), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (num3 / rectangle2.Width), 1f), SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle4.TopLeft() + new Vector2((float) (num3 - 2), 0.0f), new Rectangle?(rectangle3), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      Rectangle rectangle5 = texture2D.Frame(verticalFrames: verticalFrames);
      spriteBatch.Draw(texture2D, position, new Rectangle?(rectangle5), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      Vector2 vector2 = new Vector2(4f, 20f) + new Vector2(26f, 28f) / 2f;
      spriteBatch.Draw(barIconTexture, position + vector2, new Rectangle?(barIconFrame), Color.White, 0.0f, barIconFrame.Size() / 2f, 1f, SpriteEffects.None, 0.0f);
      if (!BigProgressBarSystem.ShowText)
        return;
      BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle4, lifeAmount, lifeMax);
    }

    private static void DrawHealthText(
      SpriteBatch spriteBatch,
      Rectangle area,
      float current,
      float max)
    {
      DynamicSpriteFont font = FontAssets.ItemStack.Value;
      Vector2 vector2_1 = area.Center.ToVector2();
      ++vector2_1.Y;
      string text1 = "/";
      Vector2 vector2_2 = font.MeasureString(text1);
      Utils.DrawBorderStringFourWay(spriteBatch, font, text1, vector2_1.X, vector2_1.Y, Color.White, Color.Black, vector2_2 * 0.5f);
      string text2 = ((int) current).ToString();
      Vector2 vector2_3 = font.MeasureString(text2);
      Utils.DrawBorderStringFourWay(spriteBatch, font, text2, vector2_1.X - 5f, vector2_1.Y, Color.White, Color.Black, vector2_3 * new Vector2(1f, 0.5f));
      string text3 = ((int) max).ToString();
      Vector2 vector2_4 = font.MeasureString(text3);
      Utils.DrawBorderStringFourWay(spriteBatch, font, text3, vector2_1.X + 5f, vector2_1.Y, Color.White, Color.Black, vector2_4 * new Vector2(0.0f, 0.5f));
    }

    public static void DrawFancyBar(
      SpriteBatch spriteBatch,
      float lifeAmount,
      float lifeMax,
      Texture2D barIconTexture,
      Rectangle barIconFrame,
      float shieldCurrent,
      float shieldMax)
    {
      Texture2D texture2D = Main.Assets.Request<Texture2D>("Images/UI/UI_BossBar", (AssetRequestMode) 1).Value;
      Point p1 = new Point(456, 22);
      Point p2 = new Point(32, 24);
      int verticalFrames = 6;
      Rectangle rectangle1 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 3);
      Color color = Color.White * 0.2f;
      float num1 = lifeAmount / lifeMax;
      int num2 = (int) ((double) p1.X * (double) num1);
      int num3 = num2 - num2 % 2;
      Rectangle rectangle2 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 2);
      rectangle2.X += p2.X;
      rectangle2.Y += p2.Y;
      rectangle2.Width = 2;
      rectangle2.Height = p1.Y;
      Rectangle rectangle3 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 1);
      rectangle3.X += p2.X;
      rectangle3.Y += p2.Y;
      rectangle3.Width = 2;
      rectangle3.Height = p1.Y;
      float num4 = shieldCurrent / shieldMax;
      int num5 = (int) ((double) p1.X * (double) num4);
      int num6 = num5 - num5 % 2;
      Rectangle rectangle4 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 5);
      rectangle4.X += p2.X;
      rectangle4.Y += p2.Y;
      rectangle4.Width = 2;
      rectangle4.Height = p1.Y;
      Rectangle rectangle5 = texture2D.Frame(verticalFrames: verticalFrames, frameY: 4);
      rectangle5.X += p2.X;
      rectangle5.Y += p2.Y;
      rectangle5.Width = 2;
      rectangle5.Height = p1.Y;
      Rectangle rectangle6 = Utils.CenteredRectangle(Main.ScreenSize.ToVector2() * new Vector2(0.5f, 1f) + new Vector2(0.0f, -50f), p1.ToVector2());
      Vector2 position = rectangle6.TopLeft() - p2.ToVector2();
      spriteBatch.Draw(texture2D, position, new Rectangle?(rectangle1), color, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle6.TopLeft(), new Rectangle?(rectangle2), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (num3 / rectangle2.Width), 1f), SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle6.TopLeft() + new Vector2((float) (num3 - 2), 0.0f), new Rectangle?(rectangle3), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle6.TopLeft(), new Rectangle?(rectangle4), Color.White, 0.0f, Vector2.Zero, new Vector2((float) (num6 / rectangle4.Width), 1f), SpriteEffects.None, 0.0f);
      spriteBatch.Draw(texture2D, rectangle6.TopLeft() + new Vector2((float) (num6 - 2), 0.0f), new Rectangle?(rectangle5), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      Rectangle rectangle7 = texture2D.Frame(verticalFrames: verticalFrames);
      spriteBatch.Draw(texture2D, position, new Rectangle?(rectangle7), Color.White, 0.0f, Vector2.Zero, 1f, SpriteEffects.None, 0.0f);
      Vector2 vector2 = new Vector2(4f, 20f) + barIconFrame.Size() / 2f;
      spriteBatch.Draw(barIconTexture, position + vector2, new Rectangle?(barIconFrame), Color.White, 0.0f, barIconFrame.Size() / 2f, 1f, SpriteEffects.None, 0.0f);
      if (!BigProgressBarSystem.ShowText)
        return;
      if ((double) shieldCurrent > 0.0)
        BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle6, shieldCurrent, shieldMax);
      else
        BigProgressBarHelper.DrawHealthText(spriteBatch, rectangle6, lifeAmount, lifeMax);
    }
  }
}
