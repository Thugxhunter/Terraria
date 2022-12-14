// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.IBestiaryBackgroundImagePathAndColorProvider
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace Terraria.GameContent.Bestiary
{
  public interface IBestiaryBackgroundImagePathAndColorProvider
  {
    Asset<Texture2D> GetBackgroundImage();

    Color? GetBackgroundColor();
  }
}
