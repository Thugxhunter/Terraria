// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.PlayerSittingHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Terraria.ID;

namespace Terraria.GameContent
{
  public struct PlayerSittingHelper
  {
    public const int ChairSittingMaxDistance = 40;
    public bool isSitting;
    public ExtraSeatInfo details;
    public Vector2 offsetForSeat;
    public int sittingIndex;

    public void GetSittingOffsetInfo(
      Player player,
      out Vector2 posOffset,
      out float seatAdjustment)
    {
      if (this.isSitting)
      {
        posOffset = new Vector2((float) (this.sittingIndex * player.direction * 8), (float) ((double) this.sittingIndex * (double) player.gravDir * -4.0));
        seatAdjustment = -4f;
        seatAdjustment += (float) (int) this.offsetForSeat.Y;
        posOffset += this.offsetForSeat * player.Directions;
      }
      else
      {
        posOffset = Vector2.Zero;
        seatAdjustment = 0.0f;
      }
    }

    public bool TryGetSittingBlock(Player player, out Tile tile)
    {
      tile = (Tile) null;
      if (!this.isSitting)
        return false;
      Point tileCoordinates = (player.Bottom + new Vector2(0.0f, -2f)).ToTileCoordinates();
      if (!PlayerSittingHelper.GetSittingTargetInfo(player, tileCoordinates.X, tileCoordinates.Y, out int _, out Vector2 _, out Vector2 _, out ExtraSeatInfo _))
        return false;
      tile = Framing.GetTileSafely(tileCoordinates);
      return true;
    }

    public void UpdateSitting(Player player)
    {
      if (!this.isSitting)
        return;
      Point tileCoordinates = (player.Bottom + new Vector2(0.0f, -2f)).ToTileCoordinates();
      int targetDirection;
      Vector2 seatDownOffset;
      ExtraSeatInfo extraInfo;
      if (!PlayerSittingHelper.GetSittingTargetInfo(player, tileCoordinates.X, tileCoordinates.Y, out targetDirection, out Vector2 _, out seatDownOffset, out extraInfo))
      {
        this.SitUp(player);
      }
      else
      {
        if (player.controlLeft || player.controlRight || player.controlUp || player.controlDown || player.controlJump || player.pulley || player.mount.Active || targetDirection != player.direction)
          this.SitUp(player);
        if (Main.sittingManager.GetNextPlayerStackIndexInCoords(tileCoordinates) >= 2)
          this.SitUp(player);
        if (!this.isSitting)
          return;
        this.offsetForSeat = seatDownOffset;
        this.details = extraInfo;
        Main.sittingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, tileCoordinates, out this.sittingIndex);
      }
    }

    public void SitUp(Player player, bool multiplayerBroadcast = true)
    {
      if (!this.isSitting)
        return;
      this.isSitting = false;
      this.offsetForSeat = Vector2.Zero;
      this.sittingIndex = -1;
      this.details = new ExtraSeatInfo();
      if (!multiplayerBroadcast || Main.myPlayer != player.whoAmI)
        return;
      NetMessage.SendData(13, number: player.whoAmI);
    }

    public void SitDown(Player player, int x, int y)
    {
      int targetDirection;
      Vector2 playerSittingPosition;
      Vector2 seatDownOffset;
      ExtraSeatInfo extraInfo;
      if (!PlayerSittingHelper.GetSittingTargetInfo(player, x, y, out targetDirection, out playerSittingPosition, out seatDownOffset, out extraInfo))
        return;
      Vector2 offset = playerSittingPosition - player.Bottom;
      bool position = player.CanSnapToPosition(offset);
      if (position)
        position &= Main.sittingManager.GetNextPlayerStackIndexInCoords((playerSittingPosition + new Vector2(0.0f, -2f)).ToTileCoordinates()) < 2;
      if (!position)
        return;
      if (this.isSitting && player.Bottom == playerSittingPosition)
      {
        this.SitUp(player);
      }
      else
      {
        player.StopVanityActions();
        player.RemoveAllGrapplingHooks();
        if (player.mount.Active)
          player.mount.Dismount(player);
        player.Bottom = playerSittingPosition;
        player.ChangeDir(targetDirection);
        this.isSitting = true;
        this.details = extraInfo;
        this.offsetForSeat = seatDownOffset;
        Main.sittingManager.AddPlayerAndGetItsStackedIndexInCoords(player.whoAmI, new Point(x, y), out this.sittingIndex);
        player.velocity = Vector2.Zero;
        player.gravDir = 1f;
        if (Main.myPlayer != player.whoAmI)
          return;
        NetMessage.SendData(13, number: player.whoAmI);
      }
    }

    public static bool GetSittingTargetInfo(
      Player player,
      int x,
      int y,
      out int targetDirection,
      out Vector2 playerSittingPosition,
      out Vector2 seatDownOffset,
      out ExtraSeatInfo extraInfo)
    {
      extraInfo = new ExtraSeatInfo();
      Tile tileSafely = Framing.GetTileSafely(x, y);
      if (!TileID.Sets.CanBeSatOnForPlayers[(int) tileSafely.type] || !tileSafely.active())
      {
        targetDirection = 1;
        seatDownOffset = Vector2.Zero;
        playerSittingPosition = new Vector2();
        return false;
      }
      int x1 = x;
      int num1 = y;
      targetDirection = 1;
      seatDownOffset = Vector2.Zero;
      int num2 = 6;
      Vector2 zero1 = Vector2.Zero;
      switch (tileSafely.type)
      {
        case 15:
        case 497:
          int num3 = tileSafely.type != (ushort) 15 ? 0 : ((int) tileSafely.frameY / 40 == 1 ? 1 : ((int) tileSafely.frameY / 40 == 20 ? 1 : 0));
          bool flag = tileSafely.type == (ushort) 15 && (int) tileSafely.frameY / 40 == 27;
          seatDownOffset.Y = (float) (flag.ToInt() * 4);
          if ((int) tileSafely.frameY % 40 != 0)
            --num1;
          targetDirection = -1;
          if (tileSafely.frameX != (short) 0)
            targetDirection = 1;
          if (num3 != 0 || tileSafely.type == (ushort) 497)
          {
            extraInfo.IsAToilet = true;
            break;
          }
          break;
        case 89:
          targetDirection = player.direction;
          num2 = 0;
          Vector2 vector2_1 = new Vector2(-4f, 2f);
          Vector2 vector2_2 = new Vector2(4f, 2f);
          Vector2 vector2_3 = new Vector2(0.0f, 2f);
          Vector2 zero2 = Vector2.Zero with
          {
            X = 1f
          };
          zero1.X = -1f;
          switch ((int) tileSafely.frameX / 54)
          {
            case 0:
              vector2_3.Y = vector2_1.Y = vector2_2.Y = 1f;
              break;
            case 1:
              vector2_3.Y = 1f;
              break;
            case 2:
            case 14:
            case 15:
            case 17:
            case 20:
            case 21:
            case 22:
            case 23:
            case 25:
            case 26:
            case 27:
            case 28:
            case 35:
            case 37:
            case 38:
            case 39:
            case 40:
            case 41:
            case 42:
              vector2_3.Y = vector2_1.Y = vector2_2.Y = 1f;
              break;
            case 3:
            case 4:
            case 5:
            case 7:
            case 8:
            case 9:
            case 10:
            case 11:
            case 12:
            case 13:
            case 16:
            case 18:
            case 19:
            case 36:
              vector2_3.Y = vector2_1.Y = vector2_2.Y = 0.0f;
              break;
            case 6:
              vector2_3.Y = vector2_1.Y = vector2_2.Y = -1f;
              break;
            case 24:
              vector2_3.Y = 0.0f;
              vector2_1.Y = -4f;
              vector2_1.X = 0.0f;
              vector2_2.X = 0.0f;
              vector2_2.Y = -4f;
              break;
          }
          if ((int) tileSafely.frameY % 40 != 0)
            --num1;
          seatDownOffset = (int) tileSafely.frameX % 54 == 0 && targetDirection == -1 || (int) tileSafely.frameX % 54 == 36 && targetDirection == 1 ? vector2_1 : ((int) tileSafely.frameX % 54 == 0 && targetDirection == 1 || (int) tileSafely.frameX % 54 == 36 && targetDirection == -1 ? vector2_2 : vector2_3);
          seatDownOffset += zero2;
          break;
        case 102:
          int num4 = (int) tileSafely.frameX / 18;
          if (num4 == 0)
            ++x1;
          if (num4 == 2)
            --x1;
          int num5 = (int) tileSafely.frameY / 18;
          if (num5 == 0)
            num1 += 2;
          if (num5 == 1)
            ++num1;
          if (num5 == 3)
            --num1;
          targetDirection = player.direction;
          num2 = 0;
          break;
        case 487:
          int num6 = (int) tileSafely.frameX % 72 / 18;
          if (num6 == 1)
            --x1;
          if (num6 == 2)
            ++x1;
          if ((int) tileSafely.frameY / 18 != 0)
            --num1;
          targetDirection = (num6 <= 1).ToDirectionInt();
          num2 = 0;
          --seatDownOffset.Y;
          break;
      }
      playerSittingPosition = new Point(x1, num1 + 1).ToWorldCoordinates(autoAddY: 16f);
      playerSittingPosition.X += (float) (targetDirection * num2);
      playerSittingPosition += zero1;
      return true;
    }
  }
}
