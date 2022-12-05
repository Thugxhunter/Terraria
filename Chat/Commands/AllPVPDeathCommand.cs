// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.AllPVPDeathCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("AllPVPDeath")]
  public class AllPVPDeathCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 25, 25);

    public void ProcessIncomingMessage(string text, byte clientId)
    {
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (player != null && player.active)
        {
          NetworkText text1 = NetworkText.FromKey("LegacyMultiplayer.24", (object) player.name, (object) player.numberOfDeathsPVP);
          if (player.numberOfDeathsPVP == 1)
            text1 = NetworkText.FromKey("LegacyMultiplayer.26", (object) player.name, (object) player.numberOfDeathsPVP);
          ChatHelper.BroadcastChatMessage(text1, AllPVPDeathCommand.RESPONSE_COLOR);
        }
      }
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
    }
  }
}
