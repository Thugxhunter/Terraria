// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.DontStarveDarknessDamageDealer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class DontStarveDarknessDamageDealer
  {
    public const int DARKNESS_HIT_TIMER_MAX_BEFORE_HIT = 60;
    public static int darknessTimer = -1;
    public static int darknessHitTimer = 0;
    public static bool saidMessage = false;
    public static bool lastFrameWasTooBright = true;

    public static void Reset()
    {
      DontStarveDarknessDamageDealer.ResetTimer();
      DontStarveDarknessDamageDealer.saidMessage = false;
      DontStarveDarknessDamageDealer.lastFrameWasTooBright = true;
    }

    private static void ResetTimer()
    {
      DontStarveDarknessDamageDealer.darknessTimer = -1;
      DontStarveDarknessDamageDealer.darknessHitTimer = 0;
    }

    private static int GetDarknessDamagePerHit() => 250;

    private static int GetDarknessTimeBeforeStartingHits() => 120;

    private static int GetDarknessTimeForMessage() => 60;

    public static void Update(Player player)
    {
      if (player.DeadOrGhost || Main.gameInactive || player.shimmering)
      {
        DontStarveDarknessDamageDealer.ResetTimer();
      }
      else
      {
        DontStarveDarknessDamageDealer.UpdateDarknessState(player);
        int beforeStartingHits = DontStarveDarknessDamageDealer.GetDarknessTimeBeforeStartingHits();
        if (DontStarveDarknessDamageDealer.darknessTimer < beforeStartingHits)
          return;
        DontStarveDarknessDamageDealer.darknessTimer = beforeStartingHits;
        ++DontStarveDarknessDamageDealer.darknessHitTimer;
        if (DontStarveDarknessDamageDealer.darknessHitTimer <= 60 || player.immune)
          return;
        int darknessDamagePerHit = DontStarveDarknessDamageDealer.GetDarknessDamagePerHit();
        SoundEngine.PlaySound(SoundID.Item1, player.Center);
        player.Hurt(PlayerDeathReason.ByOther(17), darknessDamagePerHit, 0);
        DontStarveDarknessDamageDealer.darknessHitTimer = 0;
      }
    }

    private static void UpdateDarknessState(Player player)
    {
      if (DontStarveDarknessDamageDealer.lastFrameWasTooBright = DontStarveDarknessDamageDealer.IsPlayerSafe(player))
      {
        if (DontStarveDarknessDamageDealer.saidMessage)
        {
          if (!Main.getGoodWorld)
            Main.NewText(Language.GetTextValue("Game.DarknessSafe"), (byte) 50, (byte) 200, (byte) 50);
          DontStarveDarknessDamageDealer.saidMessage = false;
        }
        DontStarveDarknessDamageDealer.ResetTimer();
      }
      else
      {
        int darknessTimeForMessage = DontStarveDarknessDamageDealer.GetDarknessTimeForMessage();
        if (DontStarveDarknessDamageDealer.darknessTimer >= darknessTimeForMessage && !DontStarveDarknessDamageDealer.saidMessage)
        {
          if (!Main.getGoodWorld)
            Main.NewText(Language.GetTextValue("Game.DarknessDanger"), (byte) 200, (byte) 50, (byte) 50);
          DontStarveDarknessDamageDealer.saidMessage = true;
        }
        ++DontStarveDarknessDamageDealer.darknessTimer;
      }
    }

    private static bool IsPlayerSafe(Player player)
    {
      Vector3 vector3 = Lighting.GetColor((int) player.Center.X / 16, (int) player.Center.Y / 16).ToVector3();
      return Main.LocalGolfState == null || !Main.LocalGolfState.ShouldCameraTrackBallLastKnownLocation && !Main.LocalGolfState.IsTrackingBall ? (Main.DroneCameraTracker == null || !Main.DroneCameraTracker.IsInUse() ? (double) vector3.Length() >= 0.10000000149011612 : DontStarveDarknessDamageDealer.lastFrameWasTooBright) : DontStarveDarknessDamageDealer.lastFrameWasTooBright;
    }
  }
}
