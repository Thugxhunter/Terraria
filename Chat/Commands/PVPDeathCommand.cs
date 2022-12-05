// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.PVPDeathCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("PVPDeath")]
  public class PVPDeathCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 25, 25);

    public void ProcessIncomingMessage(string text, byte clientId)
    {
      NetworkText text1 = NetworkText.FromKey("LegacyMultiplayer.24", (object) Main.player[(int) clientId].name, (object) Main.player[(int) clientId].numberOfDeathsPVP);
      if (Main.player[(int) clientId].numberOfDeathsPVP == 1)
        text1 = NetworkText.FromKey("LegacyMultiplayer.26", (object) Main.player[(int) clientId].name, (object) Main.player[(int) clientId].numberOfDeathsPVP);
      ChatHelper.BroadcastChatMessage(text1, PVPDeathCommand.RESPONSE_COLOR);
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
    }
  }
}
