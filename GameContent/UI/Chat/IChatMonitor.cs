// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Chat.IChatMonitor
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.GameContent.UI.Chat
{
  public interface IChatMonitor
  {
    void NewText(string newText, byte R = 255, byte G = 255, byte B = 255);

    void NewTextMultiline(string text, bool force = false, Color c = default (Color), int WidthLimit = -1);

    void DrawChat(bool drawingPlayerChat);

    void Clear();

    void Update();

    void Offset(int linesOffset);

    void ResetOffset();

    void OnResolutionChange();
  }
}
