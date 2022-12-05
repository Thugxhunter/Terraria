// Decompiled with JetBrains decompiler
// Type: Terraria.Map.TeleportPylonsMapLayer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameInput;
using Terraria.UI;

namespace Terraria.Map
{
  public class TeleportPylonsMapLayer : IMapLayer
  {
    public void Draw(ref MapOverlayDrawContext context, ref string text)
    {
      List<TeleportPylonInfo> pylons = Main.PylonSystem.Pylons;
      float scaleIfNotSelected = 1f;
      float scaleIfSelected = scaleIfNotSelected * 2f;
      Texture2D texture = TextureAssets.Extra[182].Value;
      int num = !TeleportPylonsSystem.IsPlayerNearAPylon(Main.LocalPlayer) ? 0 : (Main.DroneCameraTracker == null ? 1 : (!Main.DroneCameraTracker.IsInUse() ? 1 : 0));
      Color color = Color.White;
      if (num == 0)
        color = Color.Gray * 0.5f;
      for (int index = 0; index < pylons.Count; ++index)
      {
        TeleportPylonInfo info = pylons[index];
        if (context.Draw(texture, info.PositionInTiles.ToVector2() + new Vector2(1.5f, 2f), color, new SpriteFrame((byte) 9, (byte) 1, (byte) info.TypeOfPylon, (byte) 0)
        {
          PaddingY = 0
        }, scaleIfNotSelected, scaleIfSelected, Alignment.Center).IsMouseOver)
        {
          Main.cancelWormHole = true;
          string itemNameValue = Lang.GetItemNameValue(TETeleportationPylon.GetPylonItemTypeFromTileStyle((int) info.TypeOfPylon));
          text = itemNameValue;
          if (Main.mouseLeft && Main.mouseLeftRelease)
          {
            Main.mouseLeftRelease = false;
            Main.mapFullscreen = false;
            PlayerInput.LockGamepadButtons("MouseLeft");
            Main.PylonSystem.RequestTeleportation(info, Main.LocalPlayer);
            SoundEngine.PlaySound(11);
          }
        }
      }
    }
  }
}
