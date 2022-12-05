// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.DontStarveSeed
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Enums;

namespace Terraria.GameContent
{
  public class DontStarveSeed
  {
    public static void ModifyNightColor(ref Color bgColorToSet, ref Color moonColor)
    {
      if (Main.GetMoonPhase() == MoonPhase.Full)
        return;
      double fromValue = Main.time / 32400.0;
      Color color1 = bgColorToSet;
      Color black = Color.Black;
      Color color2 = bgColorToSet;
      float amount1 = Utils.Remap((float) fromValue, 0.0f, 0.5f, 0.0f, 1f);
      float amount2 = Utils.Remap((float) fromValue, 0.5f, 1f, 0.0f, 1f);
      Color color3 = Color.Lerp(Color.Lerp(color1, black, amount1), color2, amount2);
      bgColorToSet = color3;
    }

    public static void ModifyMinimumLightColorAtNight(ref byte minimalLight)
    {
      switch (Main.GetMoonPhase())
      {
        case MoonPhase.Full:
          minimalLight = (byte) 45;
          break;
        case MoonPhase.ThreeQuartersAtLeft:
        case MoonPhase.ThreeQuartersAtRight:
          minimalLight = (byte) 1;
          break;
        case MoonPhase.HalfAtLeft:
        case MoonPhase.HalfAtRight:
          minimalLight = (byte) 1;
          break;
        case MoonPhase.QuarterAtLeft:
        case MoonPhase.QuarterAtRight:
          minimalLight = (byte) 1;
          break;
        case MoonPhase.Empty:
          minimalLight = (byte) 1;
          break;
      }
      if (!Main.bloodMoon)
        return;
      minimalLight = Utils.Max<byte>(minimalLight, (byte) 35);
    }

    public static void FixBiomeDarkness(ref Color bgColor, ref int R, ref int G, ref int B)
    {
      if (!Main.dontStarveWorld)
        return;
      R = (int) (byte) Math.Min((int) bgColor.R, R);
      G = (int) (byte) Math.Min((int) bgColor.G, G);
      B = (int) (byte) Math.Min((int) bgColor.B, B);
    }

    public static void Initialize() => Player.Hooks.OnEnterWorld += new Action<Player>(DontStarveSeed.Hook_OnEnterWorld);

    private static void Hook_OnEnterWorld(Player player) => player.UpdateStarvingState(false);
  }
}
