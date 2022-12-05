// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.ChatHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.GameContent.NetModules;
using Terraria.GameContent.UI.Chat;
using Terraria.Localization;
using Terraria.Net;

namespace Terraria.Chat
{
  public static class ChatHelper
  {
    private static List<Tuple<string, Color>> _cachedMessages = new List<Tuple<string, Color>>();

    public static void DisplayMessageOnClient(NetworkText text, Color color, int playerId)
    {
      if (Main.dedServ)
      {
        NetPacket packet = NetTextModule.SerializeServerMessage(text, color, byte.MaxValue);
        NetManager.Instance.SendToClient(packet, playerId);
      }
      else
        ChatHelper.DisplayMessage(text, color, byte.MaxValue);
    }

    public static void SendChatMessageToClient(NetworkText text, Color color, int playerId) => ChatHelper.SendChatMessageToClientAs(byte.MaxValue, text, color, playerId);

    public static void SendChatMessageToClientAs(
      byte messageAuthor,
      NetworkText text,
      Color color,
      int playerId)
    {
      if (Main.dedServ)
      {
        NetPacket packet = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
        NetManager.Instance.SendToClient(packet, playerId);
      }
      if (playerId != Main.myPlayer)
        return;
      ChatHelper.DisplayMessage(text, color, messageAuthor);
    }

    public static void BroadcastChatMessage(NetworkText text, Color color, int excludedPlayer = -1) => ChatHelper.BroadcastChatMessageAs(byte.MaxValue, text, color, excludedPlayer);

    public static void BroadcastChatMessageAs(
      byte messageAuthor,
      NetworkText text,
      Color color,
      int excludedPlayer = -1)
    {
      if (Main.dedServ)
      {
        NetPacket packet = NetTextModule.SerializeServerMessage(text, color, messageAuthor);
        NetManager.Instance.Broadcast(packet, new NetManager.BroadcastCondition(ChatHelper.OnlySendToPlayersWhoAreLoggedIn), excludedPlayer);
      }
      else
      {
        if (excludedPlayer == Main.myPlayer)
          return;
        ChatHelper.DisplayMessage(text, color, messageAuthor);
      }
    }

    public static bool OnlySendToPlayersWhoAreLoggedIn(int clientIndex) => Netplay.Clients[clientIndex].State == 10;

    public static void SendChatMessageFromClient(ChatMessage message)
    {
      if (message.IsConsumed)
        return;
      NetPacket packet = NetTextModule.SerializeClientMessage(message);
      NetManager.Instance.SendToServer(packet);
    }

    public static void DisplayMessage(NetworkText text, Color color, byte messageAuthor)
    {
      string str = text.ToString();
      if (messageAuthor < byte.MaxValue)
      {
        Main.player[(int) messageAuthor].chatOverhead.NewMessage(str, Main.PlayerOverheadChatMessageDisplayTime);
        Main.player[(int) messageAuthor].chatOverhead.color = color;
        str = NameTagHandler.GenerateTag(Main.player[(int) messageAuthor].name) + " " + str;
      }
      if (ChatHelper.ShouldCacheMessage())
        ChatHelper.CacheMessage(str, color);
      else
        Main.NewTextMultiline(str, c: color);
    }

    private static void CacheMessage(string message, Color color) => ChatHelper._cachedMessages.Add(new Tuple<string, Color>(message, color));

    public static void ShowCachedMessages()
    {
      lock (ChatHelper._cachedMessages)
      {
        foreach (Tuple<string, Color> cachedMessage in ChatHelper._cachedMessages)
          Main.NewTextMultiline(cachedMessage.Item1, c: cachedMessage.Item2);
      }
    }

    public static void ClearDelayedMessagesCache() => ChatHelper._cachedMessages.Clear();

    private static bool ShouldCacheMessage() => Main.netMode == 1 && Main.gameMenu;
  }
}
