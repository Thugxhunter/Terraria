// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Shaders.SepiaScreenShaderData
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Enums;
using Terraria.Graphics.Shaders;

namespace Terraria.GameContent.Shaders
{
  public class SepiaScreenShaderData : ScreenShaderData
  {
    public SepiaScreenShaderData(string passName)
      : base(passName)
    {
    }

    public override void Update(GameTime gameTime)
    {
      float x = (float) (((double) Main.screenPosition.Y + (double) (Main.screenHeight / 2)) / 16.0);
      float num1 = 1f - Utils.SmoothStep((float) Main.worldSurface, (float) Main.worldSurface + 30f, x);
      Vector3 vector3_1;
      Vector3 vector3_2 = vector3_1 = new Vector3(0.191f, -0.054f, -0.221f);
      Vector3 vector3_3 = vector3_1 * 0.5f;
      Vector3 vector3_4 = new Vector3(0.0f, -0.03f, 0.15f);
      Vector3 vector3_5 = new Vector3(-0.11f, 0.01f, 0.16f);
      float cloudAlpha = Main.cloudAlpha;
      float nightlightPower;
      float daylightPower;
      float moonPower;
      float dawnPower;
      SepiaScreenShaderData.GetDaylightPowers(out nightlightPower, out daylightPower, out moonPower, out dawnPower);
      float num2 = nightlightPower * 0.13f;
      if (Main.starGame)
      {
        float num3 = (float) Main.starGameMath() - 1f;
        nightlightPower = num3;
        daylightPower = 1f - num3;
        moonPower = num3;
        dawnPower = 1f - num3;
        num2 = nightlightPower * 0.13f;
      }
      else if (!Main.dayTime)
      {
        if (Main.GetMoonPhase() == MoonPhase.Full)
        {
          vector3_2 = new Vector3(-0.19f, 0.01f, 0.22f);
          num2 += 0.07f * moonPower;
        }
        if (Main.bloodMoon)
        {
          vector3_2 = new Vector3(0.2f, -0.1f, -0.221f);
          num2 = 0.2f;
        }
      }
      float num4 = nightlightPower * num1;
      float num5 = daylightPower * num1;
      float amount1 = moonPower * num1;
      float amount2 = dawnPower * num1;
      this.UseOpacity(1f);
      this.UseIntensity((float) (1.3999999761581421 - (double) num5 * 0.20000000298023224));
      this.UseProgress(MathHelper.Lerp(MathHelper.Lerp((float) (0.30000001192092896 - (double) num2 * (double) num4), 0.1f, cloudAlpha), 0.2f, 1f - num1));
      this.UseColor(Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(Vector3.Lerp(vector3_1, vector3_2, amount1), vector3_4, amount2), vector3_5, cloudAlpha), vector3_3, 1f - num1));
    }

    private static void GetDaylightPowers(
      out float nightlightPower,
      out float daylightPower,
      out float moonPower,
      out float dawnPower)
    {
      nightlightPower = 0.0f;
      daylightPower = 0.0f;
      moonPower = 0.0f;
      Vector2 directionIn24Hclock1 = Utils.GetDayTimeAsDirectionIn24HClock();
      Vector2 directionIn24Hclock2 = Utils.GetDayTimeAsDirectionIn24HClock(4.5f);
      float fromValue1 = Vector2.Dot(directionIn24Hclock1, Utils.GetDayTimeAsDirectionIn24HClock(0.0f));
      float fromValue2 = Vector2.Dot(directionIn24Hclock1, directionIn24Hclock2);
      nightlightPower = Utils.Remap(fromValue1, -0.2f, 0.1f, 0.0f, 1f);
      daylightPower = Utils.Remap(fromValue1, 0.1f, -1f, 0.0f, 1f);
      dawnPower = Utils.Remap(fromValue2, 0.66f, 1f, 0.0f, 1f);
      if (Main.dayTime)
        return;
      float fromValue3 = (float) (Main.time / 32400.0) * 2f;
      if ((double) fromValue3 > 1.0)
        fromValue3 = 2f - fromValue3;
      moonPower = Utils.Remap(fromValue3, 0.0f, 0.25f, 0.0f, 1f);
    }
  }
}
