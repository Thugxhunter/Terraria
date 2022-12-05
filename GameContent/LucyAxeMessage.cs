// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.LucyAxeMessage
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using Terraria.Audio;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.UI;

namespace Terraria.GameContent
{
  public static class LucyAxeMessage
  {
    private static byte _variation;
    private static int[] _messageCooldownsByType = new int[7];

    private static string GetCategoryName(LucyAxeMessage.MessageSource source)
    {
      switch (source)
      {
        case LucyAxeMessage.MessageSource.Storage:
          return "LucyTheAxe_Storage";
        case LucyAxeMessage.MessageSource.ThrownAway:
          return "LucyTheAxe_ThrownAway";
        case LucyAxeMessage.MessageSource.PickedUp:
          return "LucyTheAxe_PickedUp";
        case LucyAxeMessage.MessageSource.ChoppedTree:
          return "LucyTheAxe_ChoppedTree";
        case LucyAxeMessage.MessageSource.ChoppedGemTree:
          return "LucyTheAxe_GemTree";
        case LucyAxeMessage.MessageSource.ChoppedCactus:
          return "LucyTheAxe_ChoppedCactus";
        default:
          return "LucyTheAxe_Idle";
      }
    }

    public static void Initialize()
    {
      ItemSlot.OnItemTransferred += new ItemSlot.ItemTransferEvent(LucyAxeMessage.ItemSlot_OnItemTransferred);
      Player.Hooks.OnEnterWorld += new Action<Player>(LucyAxeMessage.Hooks_OnEnterWorld);
    }

    private static void Hooks_OnEnterWorld(Player player)
    {
      if (player != Main.LocalPlayer)
        return;
      LucyAxeMessage.GiveIdleMessageCooldown();
    }

    public static void UpdateMessageCooldowns()
    {
      for (int index = 0; index < LucyAxeMessage._messageCooldownsByType.Length; ++index)
      {
        if (LucyAxeMessage._messageCooldownsByType[index] > 0)
          --LucyAxeMessage._messageCooldownsByType[index];
      }
    }

    public static void TryPlayingIdleMessage()
    {
      LucyAxeMessage.MessageSource source = LucyAxeMessage.MessageSource.Idle;
      if (LucyAxeMessage._messageCooldownsByType[(int) source] > 0)
        return;
      Player localPlayer = Main.LocalPlayer;
      LucyAxeMessage.Create(source, localPlayer.Top, new Vector2(Main.rand.NextFloatDirection() * 7f, (float) ((double) Main.rand.NextFloat() * -2.0 - 2.0)));
    }

    private static void ItemSlot_OnItemTransferred(ItemSlot.ItemTransferInfo info)
    {
      if (info.ItemType != 5095)
        return;
      bool flag1 = LucyAxeMessage.CountsAsStorage(info.FromContenxt);
      bool flag2 = LucyAxeMessage.CountsAsStorage(info.ToContext);
      if (flag1 == flag2)
        return;
      LucyAxeMessage.MessageSource source = flag1 ? LucyAxeMessage.MessageSource.PickedUp : LucyAxeMessage.MessageSource.Storage;
      if (LucyAxeMessage._messageCooldownsByType[(int) source] > 0)
        return;
      LucyAxeMessage.PutMessageTypeOnCooldown(source, 420);
      Player localPlayer = Main.LocalPlayer;
      LucyAxeMessage.Create(source, localPlayer.Top, new Vector2((float) (localPlayer.direction * 7), -2f));
    }

    private static void GiveIdleMessageCooldown() => LucyAxeMessage.PutMessageTypeOnCooldown(LucyAxeMessage.MessageSource.Idle, Main.rand.Next(7200, 14400));

    public static void PutMessageTypeOnCooldown(
      LucyAxeMessage.MessageSource source,
      int timeInFrames)
    {
      LucyAxeMessage._messageCooldownsByType[(int) source] = timeInFrames;
    }

    private static bool CountsAsStorage(int itemSlotContext) => itemSlotContext == 3 || itemSlotContext == 6 || itemSlotContext == 15;

    public static void TryCreatingMessageWithCooldown(
      LucyAxeMessage.MessageSource messageSource,
      Vector2 position,
      Vector2 velocity,
      int cooldownTimeInTicks)
    {
      if (Main.netMode == 2 || LucyAxeMessage._messageCooldownsByType[(int) messageSource] > 0)
        return;
      LucyAxeMessage.PutMessageTypeOnCooldown(messageSource, cooldownTimeInTicks);
      LucyAxeMessage.Create(messageSource, position, velocity);
    }

    public static void Create(
      LucyAxeMessage.MessageSource source,
      Vector2 position,
      Vector2 velocity)
    {
      if (Main.netMode == 2)
        return;
      LucyAxeMessage.GiveIdleMessageCooldown();
      LucyAxeMessage.SpawnPopupText(source, (int) LucyAxeMessage._variation, position, velocity);
      LucyAxeMessage.PlaySound(source, position);
      LucyAxeMessage.SpawnEmoteBubble();
      if (Main.netMode == 1)
        NetMessage.SendData(141, number: ((int) source), number2: ((float) LucyAxeMessage._variation), number3: velocity.X, number4: velocity.Y, number5: ((int) position.X), number6: ((int) position.Y));
      ++LucyAxeMessage._variation;
    }

    private static void SpawnEmoteBubble() => EmoteBubble.MakeLocalPlayerEmote(149);

    public static void CreateFromNet(
      LucyAxeMessage.MessageSource source,
      byte variation,
      Vector2 position,
      Vector2 velocity)
    {
      LucyAxeMessage.SpawnPopupText(source, (int) variation, position, velocity);
      LucyAxeMessage.PlaySound(source, position);
    }

    private static void PlaySound(LucyAxeMessage.MessageSource source, Vector2 position) => SoundEngine.PlaySound(SoundID.LucyTheAxeTalk, position);

    private static void SpawnPopupText(
      LucyAxeMessage.MessageSource source,
      int variationUnwrapped,
      Vector2 position,
      Vector2 velocity)
    {
      string textForVariation = LucyAxeMessage.GetTextForVariation(source, variationUnwrapped);
      PopupText.NewText(new AdvancedPopupRequest()
      {
        Text = textForVariation,
        DurationInFrames = 420,
        Velocity = velocity,
        Color = new Color(184, 96, 98) * 1.15f
      }, position);
    }

    private static string GetTextForVariation(
      LucyAxeMessage.MessageSource source,
      int variationUnwrapped)
    {
      string categoryName = LucyAxeMessage.GetCategoryName(source);
      return LanguageManager.Instance.IndexedFromCategory(categoryName, variationUnwrapped).Value;
    }

    public enum MessageSource
    {
      Idle,
      Storage,
      ThrownAway,
      PickedUp,
      ChoppedTree,
      ChoppedGemTree,
      ChoppedCactus,
      Count,
    }
  }
}
