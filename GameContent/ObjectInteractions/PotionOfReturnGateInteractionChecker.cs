// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ObjectInteractions.PotionOfReturnGateInteractionChecker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.GameContent.ObjectInteractions
{
  public class PotionOfReturnGateInteractionChecker : AHoverInteractionChecker
  {
    internal override bool? AttemptOverridingHoverStatus(Player player, Rectangle rectangle) => Main.SmartInteractPotionOfReturn ? new bool?(true) : new bool?();

    internal override void DoHoverEffect(Player player, Rectangle hitbox)
    {
      player.noThrow = 2;
      player.cursorItemIconEnabled = true;
      player.cursorItemIconID = 4870;
    }

    internal override bool ShouldBlockInteraction(Player player, Rectangle hitbox) => Player.BlockInteractionWithProjectiles != 0;

    internal override void PerformInteraction(Player player, Rectangle hitbox) => player.DoPotionOfReturnReturnToOriginalUsePosition();
  }
}
