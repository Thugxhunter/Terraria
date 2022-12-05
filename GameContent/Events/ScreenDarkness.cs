// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Events.ScreenDarkness
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.Events
{
  public class ScreenDarkness
  {
    public static float screenObstruction;
    public static Color frontColor = new Color(0, 0, 120);

    public static void Update()
    {
      float num1 = 0.0f;
      float amount = 0.0166666675f;
      Vector2 mountedCenter = Main.player[Main.myPlayer].MountedCenter;
      for (int index = 0; index < 200; ++index)
      {
        if (Main.npc[index].active && Main.npc[index].type == 370 && (double) Main.npc[index].Distance(mountedCenter) < 3000.0 && ((double) Main.npc[index].ai[0] >= 10.0 || (double) Main.npc[index].ai[0] == 9.0 && (double) Main.npc[index].ai[2] > 120.0))
        {
          num1 = 0.95f;
          ScreenDarkness.frontColor = new Color(0, 0, 120) * 0.3f;
          amount = 0.03f;
        }
        if (Main.npc[index].active && Main.npc[index].type == 113 && (double) Main.npc[index].Distance(mountedCenter) < 3000.0)
        {
          float num2 = Utils.Remap(Main.npc[index].Distance(mountedCenter), 2000f, 3000f, 1f, 0.0f);
          num1 = Main.npc[index].localAI[1] * num2;
          amount = 1f;
          ScreenDarkness.frontColor = Color.Black;
        }
      }
      ScreenDarkness.screenObstruction = MathHelper.Lerp(ScreenDarkness.screenObstruction, num1, amount);
    }

    public static void DrawBack(SpriteBatch spriteBatch)
    {
      if ((double) ScreenDarkness.screenObstruction == 0.0)
        return;
      Color color = Color.Black * ScreenDarkness.screenObstruction;
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
    }

    public static void DrawFront(SpriteBatch spriteBatch)
    {
      if ((double) ScreenDarkness.screenObstruction == 0.0)
        return;
      Color color = ScreenDarkness.frontColor * ScreenDarkness.screenObstruction;
      spriteBatch.Draw(TextureAssets.MagicPixel.Value, new Rectangle(-2, -2, Main.screenWidth + 4, Main.screenHeight + 4), new Rectangle?(new Rectangle(0, 0, 1, 1)), color);
    }
  }
}
