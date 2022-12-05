// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.Commands.EmojiCommand
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using Terraria.GameContent.UI;
using Terraria.Localization;

namespace Terraria.Chat.Commands
{
  [ChatCommand("Emoji")]
  public class EmojiCommand : IChatCommand
  {
    public const int PlayerEmojiDuration = 360;
    private readonly Dictionary<LocalizedText, int> _byName = new Dictionary<LocalizedText, int>();

    public EmojiCommand() => this.Initialize();

    public void Initialize()
    {
      this._byName.Clear();
      for (int id = 0; id < EmoteID.Count; ++id)
      {
        LocalizedText emojiName = Lang.GetEmojiName(id);
        if (emojiName != LocalizedText.Empty)
          this._byName[emojiName] = id;
      }
    }

    public void ProcessIncomingMessage(string text, byte clientId)
    {
    }

    public void ProcessOutgoingMessage(ChatMessage message)
    {
      int result = -1;
      if (int.TryParse(message.Text, out result))
      {
        if (result < 0 || result >= EmoteID.Count)
          return;
      }
      else
        result = -1;
      if (result == -1)
      {
        foreach (LocalizedText key in this._byName.Keys)
        {
          if (message.Text == key.Value)
          {
            result = this._byName[key];
            break;
          }
        }
      }
      if (result != -1)
      {
        if (Main.netMode == 0)
        {
          EmoteBubble.NewBubble(result, new WorldUIAnchor((Entity) Main.LocalPlayer), 360);
          EmoteBubble.CheckForNPCsToReactToEmoteBubble(result, Main.LocalPlayer);
        }
        else
          NetMessage.SendData(120, number: Main.myPlayer, number2: ((float) result));
      }
      message.Consume();
    }

    public void PrintWarning(string text) => throw new Exception("This needs localized text!");
  }
}
