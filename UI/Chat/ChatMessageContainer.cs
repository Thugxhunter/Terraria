// Decompiled with JetBrains decompiler
// Type: Terraria.UI.Chat.ChatMessageContainer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent;

namespace Terraria.UI.Chat
{
  public class ChatMessageContainer
  {
    public string OriginalText;
    private bool _prepared;
    private List<TextSnippet[]> _parsedText;
    private Color _color;
    private int _widthLimitInPixels;
    private int _timeLeft;

    public void SetContents(string text, Color color, int widthLimitInPixels)
    {
      this.OriginalText = text;
      this._color = color;
      this._widthLimitInPixels = widthLimitInPixels;
      this.MarkToNeedRefresh();
      this._parsedText = new List<TextSnippet[]>();
      this._timeLeft = 600;
      this.Refresh();
    }

    public void MarkToNeedRefresh() => this._prepared = false;

    public void Update()
    {
      if (this._timeLeft > 0)
        --this._timeLeft;
      this.Refresh();
    }

    public TextSnippet[] GetSnippetWithInversedIndex(int snippetIndex) => this._parsedText[this._parsedText.Count - 1 - snippetIndex];

    public int LineCount => this._parsedText.Count;

    public bool CanBeShownWhenChatIsClosed => this._timeLeft > 0;

    public bool Prepared => this._prepared;

    public void Refresh()
    {
      if (this._prepared)
        return;
      this._prepared = true;
      int maxWidth = this._widthLimitInPixels;
      if (maxWidth == -1)
        maxWidth = Main.screenWidth - 320;
      List<List<TextSnippet>> textSnippetListList = Utils.WordwrapStringSmart(this.OriginalText, this._color, FontAssets.MouseText.Value, maxWidth, 10);
      this._parsedText.Clear();
      for (int index = 0; index < textSnippetListList.Count; ++index)
        this._parsedText.Add(textSnippetListList[index].ToArray());
    }
  }
}
