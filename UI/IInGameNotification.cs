// Decompiled with JetBrains decompiler
// Type: Terraria.UI.IInGameNotification
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.UI
{
  public interface IInGameNotification
  {
    object CreationObject { get; }

    bool ShouldBeRemoved { get; }

    void Update();

    void DrawInGame(SpriteBatch spriteBatch, Vector2 bottomAnchorPosition);

    void PushAnchor(ref Vector2 positionAnchorBottom);

    void DrawInNotificationsArea(
      SpriteBatch spriteBatch,
      Rectangle area,
      ref int gamepadPointLocalIndexTouse);
  }
}
