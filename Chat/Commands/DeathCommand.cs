// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.DeathCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Death")]
  public class DeathCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color((int) byte.MaxValue, 25, 25);

    public void ProcessIncomingMessage(string text, byte clientId)
    {
      NetworkText text1 = NetworkText.FromKey("LegacyMultiplayer.23", (object) Main.player[(int) clientId].name, (object) Main.player[(int) clientId].numberOfDeathsPVE);
      if (Main.player[(int) clientId].numberOfDeathsPVE == 1)
        text1 = NetworkText.FromKey("LegacyMultiplayer.25", (object) Main.player[(int) clientId].name, (object) Main.player[(int) clientId].numberOfDeathsPVE);
      ChatHelper.BroadcastChatMessage(text1, DeathCommand.RESPONSE_COLOR);
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
    }
  }
}
