// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.BigProgressBar.NeverValidProgressBar
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework.Graphics;

namespace Terraria.GameContent.UI.BigProgressBar
{
  public class NeverValidProgressBar : IBigProgressBar
  {
    public bool ValidateAndCollectNecessaryInfo(ref BigProgressBarInfo info) => false;

    public void Draw(ref BigProgressBarInfo info, SpriteBatch spriteBatch)
    {
    }
  }
}
