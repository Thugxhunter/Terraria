// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.EmoteCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Emote")]
  public class EmoteCommand : IChatCommand
  {
    private static readonly Color RESPONSE_COLOR = new Color(200, 100, 0);

    public void ProcessIncomingMessage(string text, byte clientId)
    {
      if (!(text != ""))
        return;
      text = string.Format("*{0} {1}", (object) Main.player[(int) clientId].name, (object) text);
      ChatHelper.BroadcastChatMessage(NetworkText.FromLiteral(text), EmoteCommand.RESPONSE_COLOR);
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
    }
  }
}
