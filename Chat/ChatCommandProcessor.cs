// Decompiled with JetBrains decompiler
// Type: Terraria.Chat.ChatCommandProcessor
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Chat.Commands;
using Terraria.Localization;

namespace Terraria.Chat
{
  public class ChatCommandProcessor : IChatProcessor
  {
    private readonly Dictionary<LocalizedText, ChatCommandId> _localizedCommands = new Dictionary<LocalizedText, ChatCommandId>();
    private readonly Dictionary<ChatCommandId, IChatCommand> _commands = new Dictionary<ChatCommandId, IChatCommand>();
    private readonly Dictionary<LocalizedText, NetworkText> _aliases = new Dictionary<LocalizedText, NetworkText>();
    private IChatCommand _defaultCommand;

    public ChatCommandProcessor AddCommand<T>() where T : IChatCommand, new()
    {
      string commandKey = "ChatCommand." + AttributeUtilities.GetCacheableAttribute<T, ChatCommandAttribute>().Name;
      ChatCommandId key1 = ChatCommandId.FromType<T>();
      this._commands[key1] = (IChatCommand) new T();
      if (Language.Exists(commandKey))
      {
        this._localizedCommands.Add(Language.GetText(commandKey), key1);
      }
      else
      {
        commandKey += "_";
        foreach (LocalizedText key2 in Language.FindAll((LanguageSearchFilter) ((key, text) => key.StartsWith(commandKey))))
          this._localizedCommands.Add(key2, key1);
      }
      return this;
    }

    public void AddAlias(LocalizedText text, NetworkText result) => this._aliases[text] = result;

    public void ClearAliases() => this._aliases.Clear();

    public ChatCommandProcessor AddDefaultCommand<T>() where T : IChatCommand, new()
    {
      this.AddCommand<T>();
      this._defaultCommand = this._commands[ChatCommandId.FromType<T>()];
      return this;
    }

    private static bool HasLocalizedCommand(ChatMessage message, LocalizedText command)
    {
      string lower = message.Text.ToLower();
      string str = command.Value;
      if (!lower.StartsWith(str))
        return false;
      return lower.Length == str.Length || lower[str.Length] == ' ';
    }

    private static string RemoveCommandPrefix(string messageText, LocalizedText command)
    {
      string str = command.Value;
      return !messageText.StartsWith(str) || messageText.Length == str.Length || messageText[str.Length] != ' ' ? "" : messageText.Substring(str.Length + 1);
    }

    public ChatMessage CreateOutgoingMessage(string text)
    {
      ChatMessage message = new ChatMessage(text);
      KeyValuePair<LocalizedText, ChatCommandId> keyValuePair1 = this._localizedCommands.FirstOrDefault<KeyValuePair<LocalizedText, ChatCommandId>>((Func<KeyValuePair<LocalizedText, ChatCommandId>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key)));
      ChatCommandId chatCommandId = keyValuePair1.Value;
      if (keyValuePair1.Key != null)
      {
        message.SetCommand(chatCommandId);
        message.Text = ChatCommandProcessor.RemoveCommandPrefix(message.Text, keyValuePair1.Key);
        this._commands[chatCommandId].ProcessOutgoingMessage(message);
      }
      else
      {
        bool flag = false;
        for (KeyValuePair<LocalizedText, NetworkText> keyValuePair2 = this._aliases.FirstOrDefault<KeyValuePair<LocalizedText, NetworkText>>((Func<KeyValuePair<LocalizedText, NetworkText>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key))); keyValuePair2.Key != null; keyValuePair2 = this._aliases.FirstOrDefault<KeyValuePair<LocalizedText, NetworkText>>((Func<KeyValuePair<LocalizedText, NetworkText>, bool>) (pair => ChatCommandProcessor.HasLocalizedCommand(message, pair.Key))))
        {
          flag = true;
          message = new ChatMessage(keyValuePair2.Value.ToString());
        }
        if (flag)
          return this.CreateOutgoingMessage(message.Text);
      }
      return message;
    }

    public void ProcessIncomingMessage(ChatMessage message, int clientId)
    {
      IChatCommand chatCommand;
      if (this._commands.TryGetValue(message.CommandId, out chatCommand))
      {
        chatCommand.ProcessIncomingMessage(message.Text, (byte) clientId);
        message.Consume();
      }
      else
      {
        if (this._defaultCommand == null)
          return;
        this._defaultCommand.ProcessIncomingMessage(message.Text, (byte) clientId);
        message.Consume();
      }
    }
  }
}
