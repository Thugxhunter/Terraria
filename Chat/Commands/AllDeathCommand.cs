// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.AllDeathCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("AllDeath")]
  public class AllDeathCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 25, 25);

    public void ProcessIncomingMessage(string text, byte clientId)
    {
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (player != null && player.active)
        {
          NetworkText text1 = NetworkText.FromKey("LegacyMultiplayer.23", (object) player.name, (object) player.numberOfDeathsPVE);
          if (player.numberOfDeathsPVE == 1)
            text1 = NetworkText.FromKey("LegacyMultiplayer.25", (object) player.name, (object) player.numberOfDeathsPVE);
          ChatHelper.BroadcastChatMessage(text1, AllDeathCommand.RESPONSE_COLOR);
        }
      }
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
    }
  }
}
