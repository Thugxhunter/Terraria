// Decompiled with JetBrains decompiler
// Type: Terraria.Chest
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ObjectData;

namespace Terraria
{
  public class Chest : IFixLoadedData
  {
    public const float chestStackRange = 600f;
    public const int maxChestTypes = 52;
    public static int[] chestTypeToIcon = new int[52];
    public static int[] chestItemSpawn = new int[52];
    public const int maxChestTypes2 = 17;
    public static int[] chestTypeToIcon2 = new int[17];
    public static int[] chestItemSpawn2 = new int[17];
    public const int maxDresserTypes = 43;
    public static int[] dresserTypeToIcon = new int[43];
    public static int[] dresserItemSpawn = new int[43];
    public const int maxItems = 40;
    public const int MaxNameLength = 20;
    public Item[] item;
    public int x;
    public int y;
    public bool bankChest;
    public string name;
    public int frameCounter;
    public int frame;
    public int eatingAnimationTime;
    private static HashSet<int> _chestInUse = new HashSet<int>();

    public Chest(bool bank = false)
    {
      this.item = new Item[40];
      this.bankChest = bank;
      this.name = string.Empty;
    }

    public override string ToString()
    {
      int num = 0;
      for (int index = 0; index < this.item.Length; ++index)
      {
        if (this.item[index].stack > 0)
          ++num;
      }
      return string.Format("{{X: {0}, Y: {1}, Count: {2}}}", (object) this.x, (object) this.y, (object) num);
    }

    public static void Initialize()
    {
      int[] chestItemSpawn = Chest.chestItemSpawn;
      int[] chestTypeToIcon = Chest.chestTypeToIcon;
      chestTypeToIcon[0] = chestItemSpawn[0] = 48;
      chestTypeToIcon[1] = chestItemSpawn[1] = 306;
      chestTypeToIcon[2] = 327;
      chestItemSpawn[2] = 306;
      chestTypeToIcon[3] = chestItemSpawn[3] = 328;
      chestTypeToIcon[4] = 329;
      chestItemSpawn[4] = 328;
      chestTypeToIcon[5] = chestItemSpawn[5] = 343;
      chestTypeToIcon[6] = chestItemSpawn[6] = 348;
      chestTypeToIcon[7] = chestItemSpawn[7] = 625;
      chestTypeToIcon[8] = chestItemSpawn[8] = 626;
      chestTypeToIcon[9] = chestItemSpawn[9] = 627;
      chestTypeToIcon[10] = chestItemSpawn[10] = 680;
      chestTypeToIcon[11] = chestItemSpawn[11] = 681;
      chestTypeToIcon[12] = chestItemSpawn[12] = 831;
      chestTypeToIcon[13] = chestItemSpawn[13] = 838;
      chestTypeToIcon[14] = chestItemSpawn[14] = 914;
      chestTypeToIcon[15] = chestItemSpawn[15] = 952;
      chestTypeToIcon[16] = chestItemSpawn[16] = 1142;
      chestTypeToIcon[17] = chestItemSpawn[17] = 1298;
      chestTypeToIcon[18] = chestItemSpawn[18] = 1528;
      chestTypeToIcon[19] = chestItemSpawn[19] = 1529;
      chestTypeToIcon[20] = chestItemSpawn[20] = 1530;
      chestTypeToIcon[21] = chestItemSpawn[21] = 1531;
      chestTypeToIcon[22] = chestItemSpawn[22] = 1532;
      chestTypeToIcon[23] = 1533;
      chestItemSpawn[23] = 1528;
      chestTypeToIcon[24] = 1534;
      chestItemSpawn[24] = 1529;
      chestTypeToIcon[25] = 1535;
      chestItemSpawn[25] = 1530;
      chestTypeToIcon[26] = 1536;
      chestItemSpawn[26] = 1531;
      chestTypeToIcon[27] = 1537;
      chestItemSpawn[27] = 1532;
      chestTypeToIcon[28] = chestItemSpawn[28] = 2230;
      chestTypeToIcon[29] = chestItemSpawn[29] = 2249;
      chestTypeToIcon[30] = chestItemSpawn[30] = 2250;
      chestTypeToIcon[31] = chestItemSpawn[31] = 2526;
      chestTypeToIcon[32] = chestItemSpawn[32] = 2544;
      chestTypeToIcon[33] = chestItemSpawn[33] = 2559;
      chestTypeToIcon[34] = chestItemSpawn[34] = 2574;
      chestTypeToIcon[35] = chestItemSpawn[35] = 2612;
      chestTypeToIcon[36] = 327;
      chestItemSpawn[36] = 2612;
      chestTypeToIcon[37] = chestItemSpawn[37] = 2613;
      chestTypeToIcon[38] = 327;
      chestItemSpawn[38] = 2613;
      chestTypeToIcon[39] = chestItemSpawn[39] = 2614;
      chestTypeToIcon[40] = 327;
      chestItemSpawn[40] = 2614;
      chestTypeToIcon[41] = chestItemSpawn[41] = 2615;
      chestTypeToIcon[42] = chestItemSpawn[42] = 2616;
      chestTypeToIcon[43] = chestItemSpawn[43] = 2617;
      chestTypeToIcon[44] = chestItemSpawn[44] = 2618;
      chestTypeToIcon[45] = chestItemSpawn[45] = 2619;
      chestTypeToIcon[46] = chestItemSpawn[46] = 2620;
      chestTypeToIcon[47] = chestItemSpawn[47] = 2748;
      chestTypeToIcon[48] = chestItemSpawn[48] = 2814;
      chestTypeToIcon[49] = chestItemSpawn[49] = 3180;
      chestTypeToIcon[50] = chestItemSpawn[50] = 3125;
      chestTypeToIcon[51] = chestItemSpawn[51] = 3181;
      int[] chestItemSpawn2 = Chest.chestItemSpawn2;
      int[] chestTypeToIcon2 = Chest.chestTypeToIcon2;
      chestTypeToIcon2[0] = chestItemSpawn2[0] = 3884;
      chestTypeToIcon2[1] = chestItemSpawn2[1] = 3885;
      chestTypeToIcon2[2] = chestItemSpawn2[2] = 3939;
      chestTypeToIcon2[3] = chestItemSpawn2[3] = 3965;
      chestTypeToIcon2[4] = chestItemSpawn2[4] = 3988;
      chestTypeToIcon2[5] = chestItemSpawn2[5] = 4153;
      chestTypeToIcon2[6] = chestItemSpawn2[6] = 4174;
      chestTypeToIcon2[7] = chestItemSpawn2[7] = 4195;
      chestTypeToIcon2[8] = chestItemSpawn2[8] = 4216;
      chestTypeToIcon2[9] = chestItemSpawn2[9] = 4265;
      chestTypeToIcon2[10] = chestItemSpawn2[10] = 4267;
      chestTypeToIcon2[11] = chestItemSpawn2[11] = 4574;
      chestTypeToIcon2[12] = chestItemSpawn2[12] = 4712;
      chestTypeToIcon2[13] = 4714;
      chestItemSpawn2[13] = 4712;
      chestTypeToIcon2[14] = chestItemSpawn2[14] = 5156;
      chestTypeToIcon2[15] = chestItemSpawn2[15] = 5177;
      chestTypeToIcon2[16] = chestItemSpawn2[16] = 5198;
      Chest.dresserTypeToIcon[0] = Chest.dresserItemSpawn[0] = 334;
      Chest.dresserTypeToIcon[1] = Chest.dresserItemSpawn[1] = 647;
      Chest.dresserTypeToIcon[2] = Chest.dresserItemSpawn[2] = 648;
      Chest.dresserTypeToIcon[3] = Chest.dresserItemSpawn[3] = 649;
      Chest.dresserTypeToIcon[4] = Chest.dresserItemSpawn[4] = 918;
      Chest.dresserTypeToIcon[5] = Chest.dresserItemSpawn[5] = 2386;
      Chest.dresserTypeToIcon[6] = Chest.dresserItemSpawn[6] = 2387;
      Chest.dresserTypeToIcon[7] = Chest.dresserItemSpawn[7] = 2388;
      Chest.dresserTypeToIcon[8] = Chest.dresserItemSpawn[8] = 2389;
      Chest.dresserTypeToIcon[9] = Chest.dresserItemSpawn[9] = 2390;
      Chest.dresserTypeToIcon[10] = Chest.dresserItemSpawn[10] = 2391;
      Chest.dresserTypeToIcon[11] = Chest.dresserItemSpawn[11] = 2392;
      Chest.dresserTypeToIcon[12] = Chest.dresserItemSpawn[12] = 2393;
      Chest.dresserTypeToIcon[13] = Chest.dresserItemSpawn[13] = 2394;
      Chest.dresserTypeToIcon[14] = Chest.dresserItemSpawn[14] = 2395;
      Chest.dresserTypeToIcon[15] = Chest.dresserItemSpawn[15] = 2396;
      Chest.dresserTypeToIcon[16] = Chest.dresserItemSpawn[16] = 2529;
      Chest.dresserTypeToIcon[17] = Chest.dresserItemSpawn[17] = 2545;
      Chest.dresserTypeToIcon[18] = Chest.dresserItemSpawn[18] = 2562;
      Chest.dresserTypeToIcon[19] = Chest.dresserItemSpawn[19] = 2577;
      Chest.dresserTypeToIcon[20] = Chest.dresserItemSpawn[20] = 2637;
      Chest.dresserTypeToIcon[21] = Chest.dresserItemSpawn[21] = 2638;
      Chest.dresserTypeToIcon[22] = Chest.dresserItemSpawn[22] = 2639;
      Chest.dresserTypeToIcon[23] = Chest.dresserItemSpawn[23] = 2640;
      Chest.dresserTypeToIcon[24] = Chest.dresserItemSpawn[24] = 2816;
      Chest.dresserTypeToIcon[25] = Chest.dresserItemSpawn[25] = 3132;
      Chest.dresserTypeToIcon[26] = Chest.dresserItemSpawn[26] = 3134;
      Chest.dresserTypeToIcon[27] = Chest.dresserItemSpawn[27] = 3133;
      Chest.dresserTypeToIcon[28] = Chest.dresserItemSpawn[28] = 3911;
      Chest.dresserTypeToIcon[29] = Chest.dresserItemSpawn[29] = 3912;
      Chest.dresserTypeToIcon[30] = Chest.dresserItemSpawn[30] = 3913;
      Chest.dresserTypeToIcon[31] = Chest.dresserItemSpawn[31] = 3914;
      Chest.dresserTypeToIcon[32] = Chest.dresserItemSpawn[32] = 3934;
      Chest.dresserTypeToIcon[33] = Chest.dresserItemSpawn[33] = 3968;
      Chest.dresserTypeToIcon[34] = Chest.dresserItemSpawn[34] = 4148;
      Chest.dresserTypeToIcon[35] = Chest.dresserItemSpawn[35] = 4169;
      Chest.dresserTypeToIcon[36] = Chest.dresserItemSpawn[36] = 4190;
      Chest.dresserTypeToIcon[37] = Chest.dresserItemSpawn[37] = 4211;
      Chest.dresserTypeToIcon[38] = Chest.dresserItemSpawn[38] = 4301;
      Chest.dresserTypeToIcon[39] = Chest.dresserItemSpawn[39] = 4569;
      Chest.dresserTypeToIcon[40] = Chest.dresserItemSpawn[40] = 5151;
      Chest.dresserTypeToIcon[41] = Chest.dresserItemSpawn[41] = 5172;
      Chest.dresserTypeToIcon[42] = Chest.dresserItemSpawn[42] = 5193;
    }

    private static bool IsPlayerInChest(int i)
    {
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].chest == i)
          return true;
      }
      return false;
    }

    public static List<int> GetCurrentlyOpenChests()
    {
      List<int> currentlyOpenChests = new List<int>();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].chest > -1)
          currentlyOpenChests.Add(Main.player[index].chest);
      }
      return currentlyOpenChests;
    }

    public static bool IsLocked(int x, int y) => Chest.IsLocked(Main.tile[x, y]);

    public static bool IsLocked(Tile t)
    {
      if (t == null || t.type == (ushort) 21 && (t.frameX >= (short) 72 && t.frameX <= (short) 106 || t.frameX >= (short) 144 && t.frameX <= (short) 178 || t.frameX >= (short) 828 && t.frameX <= (short) 1006 || t.frameX >= (short) 1296 && t.frameX <= (short) 1330 || t.frameX >= (short) 1368 && t.frameX <= (short) 1402 || t.frameX >= (short) 1440 && t.frameX <= (short) 1474))
        return true;
      return t.type == (ushort) 467 && (int) t.frameX / 36 == 13;
    }

    public static void ServerPlaceItem(int plr, int slot)
    {
      if (slot >= PlayerItemSlotID.Bank4_0 && slot < PlayerItemSlotID.Bank4_0 + 40)
      {
        int index = slot - PlayerItemSlotID.Bank4_0;
        Main.player[plr].bank4.item[index] = Chest.PutItemInNearbyChest(Main.player[plr].bank4.item[index], Main.player[plr].Center);
        NetMessage.SendData(5, number: plr, number2: ((float) slot), number3: ((float) Main.player[plr].bank4.item[index].prefix));
      }
      else
      {
        if (slot >= 58)
          return;
        Main.player[plr].inventory[slot] = Chest.PutItemInNearbyChest(Main.player[plr].inventory[slot], Main.player[plr].Center);
        NetMessage.SendData(5, number: plr, number2: ((float) slot), number3: ((float) Main.player[plr].inventory[slot].prefix));
      }
    }

    public static Item PutItemInNearbyChest(Item item, Vector2 position)
    {
      if (Main.netMode == 1)
        return item;
      bool flag1 = true;
      for (int i = 0; i < 8000; ++i)
      {
        bool flag2 = false;
        bool flag3 = false;
        if (Main.chest[i] != null && !Chest.IsPlayerInChest(i) && !Chest.IsLocked(Main.chest[i].x, Main.chest[i].y))
        {
          Vector2 chestPosition = new Vector2((float) (Main.chest[i].x * 16 + 16), (float) (Main.chest[i].y * 16 + 16));
          if ((double) (chestPosition - position).Length() < 600.0)
          {
            for (int index = 0; index < Main.chest[i].item.Length; ++index)
            {
              if (Main.chest[i].item[index].IsAir)
                flag3 = true;
              else if (item.IsTheSameAs(Main.chest[i].item[index]))
              {
                flag2 = true;
                int amountMoved = Main.chest[i].item[index].maxStack - Main.chest[i].item[index].stack;
                if (amountMoved > 0)
                {
                  if (amountMoved > item.stack)
                    amountMoved = item.stack;
                  Chest.VisualizeChestTransfer(position, chestPosition, item, amountMoved);
                  if (flag1)
                  {
                    item.stack -= amountMoved;
                    Main.chest[i].item[index].stack += amountMoved;
                  }
                  if (item.stack <= 0)
                  {
                    item.SetDefaults();
                    return item;
                  }
                }
              }
              else
                flag3 = true;
            }
            if (flag2 & flag3 && item.stack > 0)
            {
              for (int index = 0; index < Main.chest[i].item.Length; ++index)
              {
                if (Main.chest[i].item[index].type == 0 || Main.chest[i].item[index].stack == 0)
                {
                  Chest.VisualizeChestTransfer(position, chestPosition, item, item.stack);
                  if (flag1)
                  {
                    Main.chest[i].item[index] = item.Clone();
                    item.SetDefaults();
                  }
                  return item;
                }
              }
            }
          }
        }
      }
      return item;
    }

    public static void VisualizeChestTransfer(
      Vector2 position,
      Vector2 chestPosition,
      Item item,
      int amountMoved)
    {
      ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.ItemTransfer, new ParticleOrchestraSettings()
      {
        PositionInWorld = position,
        MovementVector = chestPosition - position,
        UniqueInfoPiece = item.type
      });
    }

    public static void VisualizeChestTransfer_CoinsBatch(
      Vector2 position,
      Vector2 chestPosition,
      long coinsMoved)
    {
      int[] numArray = Utils.CoinsSplit(coinsMoved);
      for (int index = 0; index < numArray.Length; ++index)
      {
        if (numArray[index] >= 1)
          ParticleOrchestrator.BroadcastOrRequestParticleSpawn(ParticleOrchestraType.ItemTransfer, new ParticleOrchestraSettings()
          {
            PositionInWorld = position,
            MovementVector = chestPosition - position,
            UniqueInfoPiece = 71 + index
          });
      }
    }

    public object Clone() => this.MemberwiseClone();

    public static bool Unlock(int X, int Y)
    {
      if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
        return false;
      short num1 = 0;
      int Type = 0;
      Tile tileSafely1 = Framing.GetTileSafely(X, Y);
      int type = (int) tileSafely1.type;
      int num2 = (int) tileSafely1.frameX / 36;
      switch (type)
      {
        case 21:
          switch (num2)
          {
            case 2:
              num1 = (short) 36;
              Type = 11;
              AchievementsHelper.NotifyProgressionEvent(19);
              break;
            case 4:
              num1 = (short) 36;
              Type = 11;
              break;
            case 23:
            case 24:
            case 25:
            case 26:
            case 27:
              if (!NPC.downedPlantBoss)
                return false;
              num1 = (short) 180;
              Type = 11;
              AchievementsHelper.NotifyProgressionEvent(20);
              break;
            case 36:
            case 38:
            case 40:
              num1 = (short) 36;
              Type = 11;
              break;
            default:
              return false;
          }
          break;
        case 467:
          if (num2 != 13 || !NPC.downedPlantBoss)
            return false;
          num1 = (short) 36;
          Type = 11;
          AchievementsHelper.NotifyProgressionEvent(20);
          break;
      }
      SoundEngine.PlaySound(22, X * 16, Y * 16);
      for (int i = X; i <= X + 1; ++i)
      {
        for (int j = Y; j <= Y + 1; ++j)
        {
          Tile tileSafely2 = Framing.GetTileSafely(i, j);
          if ((int) tileSafely2.type == type)
          {
            tileSafely2.frameX -= num1;
            Main.tile[i, j] = tileSafely2;
            for (int index = 0; index < 4; ++index)
              Dust.NewDust(new Vector2((float) (i * 16), (float) (j * 16)), 16, 16, Type);
          }
        }
      }
      return true;
    }

    public static bool Lock(int X, int Y)
    {
      if (Main.tile[X, Y] == null || Main.tile[X + 1, Y] == null || Main.tile[X, Y + 1] == null || Main.tile[X + 1, Y + 1] == null)
        return false;
      short num1 = 0;
      Tile tileSafely1 = Framing.GetTileSafely(X, Y);
      int type = (int) tileSafely1.type;
      int num2 = (int) tileSafely1.frameX / 36;
      switch (type)
      {
        case 21:
          switch (num2)
          {
            case 1:
              num1 = (short) 36;
              break;
            case 3:
              num1 = (short) 36;
              break;
            case 18:
            case 19:
            case 20:
            case 21:
            case 22:
              if (!NPC.downedPlantBoss)
                return false;
              num1 = (short) 180;
              break;
            case 35:
            case 37:
            case 39:
              num1 = (short) 36;
              break;
            default:
              return false;
          }
          break;
        case 467:
          if (num2 != 12 || !NPC.downedPlantBoss)
            return false;
          num1 = (short) 36;
          AchievementsHelper.NotifyProgressionEvent(20);
          break;
      }
      SoundEngine.PlaySound(22, X * 16, Y * 16);
      for (int i = X; i <= X + 1; ++i)
      {
        for (int j = Y; j <= Y + 1; ++j)
        {
          Tile tileSafely2 = Framing.GetTileSafely(i, j);
          if ((int) tileSafely2.type == type)
          {
            tileSafely2.frameX += num1;
            Main.tile[i, j] = tileSafely2;
          }
        }
      }
      return true;
    }

    public static int UsingChest(int i)
    {
      if (Main.chest[i] != null)
      {
        for (int index = 0; index < (int) byte.MaxValue; ++index)
        {
          if (Main.player[index].active && Main.player[index].chest == i)
            return index;
        }
      }
      return -1;
    }

    public static int FindChest(int X, int Y)
    {
      for (int chest = 0; chest < 8000; ++chest)
      {
        if (Main.chest[chest] != null && Main.chest[chest].x == X && Main.chest[chest].y == Y)
          return chest;
      }
      return -1;
    }

    public static int FindChestByGuessing(int X, int Y)
    {
      for (int chestByGuessing = 0; chestByGuessing < 8000; ++chestByGuessing)
      {
        if (Main.chest[chestByGuessing] != null && Main.chest[chestByGuessing].x >= X && Main.chest[chestByGuessing].x < X + 2 && Main.chest[chestByGuessing].y >= Y && Main.chest[chestByGuessing].y < Y + 2)
          return chestByGuessing;
      }
      return -1;
    }

    public static int FindEmptyChest(
      int x,
      int y,
      int type = 21,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      int emptyChest = -1;
      for (int index = 0; index < 8000; ++index)
      {
        Chest chest = Main.chest[index];
        if (chest != null)
        {
          if (chest.x == x && chest.y == y)
            return -1;
        }
        else if (emptyChest == -1)
          emptyChest = index;
      }
      return emptyChest;
    }

    public static bool NearOtherChests(int x, int y)
    {
      for (int i = x - 25; i < x + 25; ++i)
      {
        for (int j = y - 8; j < y + 8; ++j)
        {
          Tile tileSafely = Framing.GetTileSafely(i, j);
          if (tileSafely.active() && TileID.Sets.BasicChest[(int) tileSafely.type])
            return true;
        }
      }
      return false;
    }

    public static int AfterPlacement_Hook(
      int x,
      int y,
      int type = 21,
      int style = 0,
      int direction = 1,
      int alternate = 0)
    {
      Point16 baseCoords = new Point16(x, y);
      TileObjectData.OriginToTopLeft(type, style, ref baseCoords);
      int emptyChest = Chest.FindEmptyChest((int) baseCoords.X, (int) baseCoords.Y);
      if (emptyChest == -1)
        return -1;
      if (Main.netMode != 1)
      {
        Chest chest = new Chest();
        chest.x = (int) baseCoords.X;
        chest.y = (int) baseCoords.Y;
        for (int index = 0; index < 40; ++index)
          chest.item[index] = new Item();
        Main.chest[emptyChest] = chest;
      }
      else
      {
        switch (type)
        {
          case 21:
            NetMessage.SendData(34, number2: ((float) x), number3: ((float) y), number4: ((float) style));
            break;
          case 467:
            NetMessage.SendData(34, number: 4, number2: ((float) x), number3: ((float) y), number4: ((float) style));
            break;
          default:
            NetMessage.SendData(34, number: 2, number2: ((float) x), number3: ((float) y), number4: ((float) style));
            break;
        }
      }
      return emptyChest;
    }

    public static int CreateChest(int X, int Y, int id = -1)
    {
      int chest = id;
      if (chest == -1)
      {
        chest = Chest.FindEmptyChest(X, Y);
        if (chest == -1)
          return -1;
        if (Main.netMode == 1)
          return chest;
      }
      Main.chest[chest] = new Chest();
      Main.chest[chest].x = X;
      Main.chest[chest].y = Y;
      for (int index = 0; index < 40; ++index)
        Main.chest[chest].item[index] = new Item();
      return chest;
    }

    public static bool CanDestroyChest(int X, int Y)
    {
      for (int index1 = 0; index1 < 8000; ++index1)
      {
        Chest chest = Main.chest[index1];
        if (chest != null && chest.x == X && chest.y == Y)
        {
          for (int index2 = 0; index2 < 40; ++index2)
          {
            if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
              return false;
          }
          return true;
        }
      }
      return true;
    }

    public static bool DestroyChest(int X, int Y)
    {
      for (int index1 = 0; index1 < 8000; ++index1)
      {
        Chest chest = Main.chest[index1];
        if (chest != null && chest.x == X && chest.y == Y)
        {
          for (int index2 = 0; index2 < 40; ++index2)
          {
            if (chest.item[index2] != null && chest.item[index2].type > 0 && chest.item[index2].stack > 0)
              return false;
          }
          Main.chest[index1] = (Chest) null;
          if (Main.player[Main.myPlayer].chest == index1)
            Main.player[Main.myPlayer].chest = -1;
          Recipe.FindRecipes();
          return true;
        }
      }
      return true;
    }

    public static void DestroyChestDirect(int X, int Y, int id)
    {
      if (id < 0)
        return;
      if (id >= Main.chest.Length)
        return;
      try
      {
        Chest chest = Main.chest[id];
        if (chest == null || chest.x != X || chest.y != Y)
          return;
        Main.chest[id] = (Chest) null;
        if (Main.player[Main.myPlayer].chest == id)
          Main.player[Main.myPlayer].chest = -1;
        Recipe.FindRecipes();
      }
      catch
      {
      }
    }

    public void AddItemToShop(Item newItem)
    {
      int num1 = Main.shopSellbackHelper.Remove(newItem);
      if (num1 >= newItem.stack)
        return;
      for (int index = 0; index < 39; ++index)
      {
        if (this.item[index] == null || this.item[index].type == 0)
        {
          this.item[index] = newItem.Clone();
          this.item[index].favorited = false;
          this.item[index].buyOnce = true;
          this.item[index].stack -= num1;
          int num2 = this.item[index].value;
          break;
        }
      }
    }

    public static void SetupTravelShop_AddToShop(int it, ref int added, ref int count)
    {
      if (it == 0)
        return;
      ++added;
      Main.travelShop[count] = it;
      ++count;
      if (it == 2260)
      {
        Main.travelShop[count] = 2261;
        ++count;
        Main.travelShop[count] = 2262;
        ++count;
      }
      if (it == 4555)
      {
        Main.travelShop[count] = 4556;
        ++count;
        Main.travelShop[count] = 4557;
        ++count;
      }
      if (it == 4321)
      {
        Main.travelShop[count] = 4322;
        ++count;
      }
      if (it == 4323)
      {
        Main.travelShop[count] = 4324;
        ++count;
        Main.travelShop[count] = 4365;
        ++count;
      }
      if (it == 5390)
      {
        Main.travelShop[count] = 5386;
        ++count;
        Main.travelShop[count] = 5387;
        ++count;
      }
      if (it == 4666)
      {
        Main.travelShop[count] = 4664;
        ++count;
        Main.travelShop[count] = 4665;
        ++count;
      }
      if (it != 3637)
        return;
      --count;
      switch (Main.rand.Next(6))
      {
        case 0:
          Main.travelShop[count++] = 3637;
          Main.travelShop[count++] = 3642;
          break;
        case 1:
          Main.travelShop[count++] = 3621;
          Main.travelShop[count++] = 3622;
          break;
        case 2:
          Main.travelShop[count++] = 3634;
          Main.travelShop[count++] = 3639;
          break;
        case 3:
          Main.travelShop[count++] = 3633;
          Main.travelShop[count++] = 3638;
          break;
        case 4:
          Main.travelShop[count++] = 3635;
          Main.travelShop[count++] = 3640;
          break;
        case 5:
          Main.travelShop[count++] = 3636;
          Main.travelShop[count++] = 3641;
          break;
      }
    }

    public static bool SetupTravelShop_CanAddItemToShop(int it)
    {
      if (it == 0)
        return false;
      for (int index = 0; index < 40; ++index)
      {
        if (Main.travelShop[index] == it)
          return false;
        if (it == 3637)
        {
          switch (Main.travelShop[index])
          {
            case 3621:
            case 3622:
            case 3633:
            case 3634:
            case 3635:
            case 3636:
            case 3637:
            case 3638:
            case 3639:
            case 3640:
            case 3641:
            case 3642:
              return false;
            default:
              continue;
          }
        }
      }
      return true;
    }

    public static void SetupTravelShop_GetPainting(
      Player playerWithHighestLuck,
      int[] rarity,
      ref int it,
      int minimumRarity = 0)
    {
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && !Main.dontStarveWorld)
        it = 5121;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && !Main.dontStarveWorld)
        it = 5122;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && !Main.dontStarveWorld)
        it = 5124;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && !Main.dontStarveWorld)
        it = 5123;
      if (minimumRarity > 2)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && Main.hardMode && NPC.downedMoonlord)
        it = 3596;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && Main.hardMode && NPC.downedMartians)
        it = 2865;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && Main.hardMode && NPC.downedMartians)
        it = 2866;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && Main.hardMode && NPC.downedMartians)
        it = 2867;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedFrost)
        it = 3055;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedFrost)
        it = 3056;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedFrost)
        it = 3057;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedFrost)
        it = 3058;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedFrost)
        it = 3059;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && Main.hardMode && NPC.downedMoonlord)
        it = 5243;
      if (minimumRarity > 1)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0 && Main.dontStarveWorld)
        it = 5121;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0 && Main.dontStarveWorld)
        it = 5122;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0 && Main.dontStarveWorld)
        it = 5124;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0 && Main.dontStarveWorld)
        it = 5123;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5225;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5229;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5232;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5389;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5233;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5241;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 5244;
      if (playerWithHighestLuck.RollLuck(rarity[1]) != 0)
        return;
      it = 5242;
    }

    public static void SetupTravelShop_AdjustSlotRarities(int slotItemAttempts, ref int[] rarity)
    {
      if (rarity[5] > 1 && slotItemAttempts > 4700)
        rarity[5] = 1;
      if (rarity[4] > 1 && slotItemAttempts > 4600)
        rarity[4] = 1;
      if (rarity[3] > 1 && slotItemAttempts > 4500)
        rarity[3] = 1;
      if (rarity[2] > 1 && slotItemAttempts > 4400)
        rarity[2] = 1;
      if (rarity[1] > 1 && slotItemAttempts > 4300)
        rarity[1] = 1;
      if (rarity[0] <= 1 || slotItemAttempts <= 4200)
        return;
      rarity[0] = 1;
    }

    public static void SetupTravelShop_GetItem(
      Player playerWithHighestLuck,
      int[] rarity,
      ref int it,
      int minimumRarity = 0)
    {
      if (minimumRarity <= 4 && playerWithHighestLuck.RollLuck(rarity[4]) == 0)
        it = 3309;
      if (minimumRarity <= 3 && playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 3314;
      if (playerWithHighestLuck.RollLuck(rarity[5]) == 0)
        it = 1987;
      if (minimumRarity > 4)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[4]) == 0 && Main.hardMode)
        it = 2270;
      if (playerWithHighestLuck.RollLuck(rarity[4]) == 0 && Main.hardMode)
        it = 4760;
      if (playerWithHighestLuck.RollLuck(rarity[4]) == 0)
        it = 2278;
      if (playerWithHighestLuck.RollLuck(rarity[4]) == 0)
        it = 2271;
      if (minimumRarity > 3)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
        it = 2223;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2272;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2276;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2284;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2285;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2286;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 2287;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 4744;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && NPC.downedBoss3)
        it = 2296;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 3628;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0 && Main.hardMode)
        it = 4091;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 4603;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 4604;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 5297;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 4605;
      if (playerWithHighestLuck.RollLuck(rarity[3]) == 0)
        it = 4550;
      if (minimumRarity > 2)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 2268;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && WorldGen.shadowOrbSmashed)
        it = 2269;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 1988;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 2275;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 2279;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 2277;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4555;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4321;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4323;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 5390;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4549;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4561;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4774;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 5136;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 5305;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4562;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4558;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4559;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4563;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0)
        it = 4666;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && (NPC.downedBoss1 || NPC.downedBoss2 || NPC.downedBoss3 || NPC.downedQueenBee || Main.hardMode))
      {
        it = 4347;
        if (Main.hardMode)
          it = 4348;
      }
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedBoss1)
        it = 3262;
      if (playerWithHighestLuck.RollLuck(rarity[2]) == 0 && NPC.downedMechBossAny)
        it = 3284;
      if (minimumRarity > 1)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2267;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2214;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2215;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2216;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2217;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 3624;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = !Main.remixWorld ? 2273 : 671;
      if (playerWithHighestLuck.RollLuck(rarity[1]) == 0)
        it = 2274;
      if (minimumRarity > 0)
        return;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 2266;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 2281 + Main.rand.Next(3);
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 2258;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 2242;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 2260;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 3637;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 4420;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 3119;
      if (playerWithHighestLuck.RollLuck(rarity[0]) == 0)
        it = 3118;
      if (playerWithHighestLuck.RollLuck(rarity[0]) != 0)
        return;
      it = 3099;
    }

    public static void SetupTravelShop()
    {
      for (int index = 0; index < 40; ++index)
        Main.travelShop[index] = 0;
      Player playerWithHighestLuck = (Player) null;
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        Player player = Main.player[index];
        if (player.active && (playerWithHighestLuck == null || (double) playerWithHighestLuck.luck < (double) player.luck))
          playerWithHighestLuck = player;
      }
      if (playerWithHighestLuck == null)
        playerWithHighestLuck = new Player();
      int num = Main.rand.Next(4, 7);
      if (playerWithHighestLuck.RollLuck(4) == 0)
        ++num;
      if (playerWithHighestLuck.RollLuck(8) == 0)
        ++num;
      if (playerWithHighestLuck.RollLuck(16) == 0)
        ++num;
      if (playerWithHighestLuck.RollLuck(32) == 0)
        ++num;
      if (Main.expertMode && playerWithHighestLuck.RollLuck(2) == 0)
        ++num;
      if (NPC.peddlersSatchelWasUsed)
        ++num;
      if (Main.tenthAnniversaryWorld)
      {
        if (!Main.getGoodWorld)
          ++num;
        ++num;
      }
      int count = 0;
      int added = 0;
      int[] rarity1 = new int[6]
      {
        100,
        200,
        300,
        400,
        500,
        600
      };
      int[] rarity2 = rarity1;
      int slotItemAttempts1 = 0;
      if (Main.hardMode)
      {
        int it = 0;
        while (slotItemAttempts1 < 5000)
        {
          ++slotItemAttempts1;
          Chest.SetupTravelShop_AdjustSlotRarities(slotItemAttempts1, ref rarity2);
          Chest.SetupTravelShop_GetItem(playerWithHighestLuck, rarity2, ref it, 2);
          if (Chest.SetupTravelShop_CanAddItemToShop(it))
          {
            Chest.SetupTravelShop_AddToShop(it, ref added, ref count);
            break;
          }
        }
      }
      while (added < num)
      {
        int it = 0;
        Chest.SetupTravelShop_GetItem(playerWithHighestLuck, rarity1, ref it);
        if (Chest.SetupTravelShop_CanAddItemToShop(it))
          Chest.SetupTravelShop_AddToShop(it, ref added, ref count);
      }
      int[] rarity3 = rarity1;
      int slotItemAttempts2 = 0;
      int it1 = 0;
      while (slotItemAttempts2 < 5000)
      {
        ++slotItemAttempts2;
        Chest.SetupTravelShop_AdjustSlotRarities(slotItemAttempts2, ref rarity3);
        Chest.SetupTravelShop_GetPainting(playerWithHighestLuck, rarity3, ref it1);
        if (Chest.SetupTravelShop_CanAddItemToShop(it1))
        {
          Chest.SetupTravelShop_AddToShop(it1, ref added, ref count);
          break;
        }
      }
    }

    public void SetupShop(int type)
    {
      bool flag1 = Main.LocalPlayer.currentShoppingSettings.PriceAdjustment <= 0.89999997615814209;
      Item[] objArray1 = this.item;
      for (int index = 0; index < 40; ++index)
        objArray1[index] = new Item();
      int index1 = 0;
      switch (type)
      {
        case 1:
          objArray1[index1].SetDefaults(88);
          int index2 = index1 + 1;
          objArray1[index2].SetDefaults(87);
          int index3 = index2 + 1;
          objArray1[index3].SetDefaults(35);
          int index4 = index3 + 1;
          objArray1[index4].SetDefaults(1991);
          int index5 = index4 + 1;
          objArray1[index5].SetDefaults(3509);
          int index6 = index5 + 1;
          objArray1[index6].SetDefaults(3506);
          int index7 = index6 + 1;
          objArray1[index7].SetDefaults(8);
          int index8 = index7 + 1;
          objArray1[index8].SetDefaults(28);
          int index9 = index8 + 1;
          if (Main.hardMode)
          {
            objArray1[index9].SetDefaults(188);
            ++index9;
          }
          objArray1[index9].SetDefaults(110);
          int index10 = index9 + 1;
          if (Main.hardMode)
          {
            objArray1[index10].SetDefaults(189);
            ++index10;
          }
          objArray1[index10].SetDefaults(40);
          int index11 = index10 + 1;
          objArray1[index11].SetDefaults(42);
          int index12 = index11 + 1;
          objArray1[index12].SetDefaults(965);
          int index13 = index12 + 1;
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index13].SetDefaults(967);
            ++index13;
          }
          if (Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[index13].SetDefaults(33);
            ++index13;
          }
          if (Main.dayTime && Main.IsItAHappyWindyDay)
            objArray1[index13++].SetDefaults(4074);
          if (Main.bloodMoon)
          {
            objArray1[index13].SetDefaults(279);
            ++index13;
          }
          if (!Main.dayTime)
          {
            objArray1[index13].SetDefaults(282);
            ++index13;
          }
          if (NPC.downedBoss3)
          {
            objArray1[index13].SetDefaults(346);
            ++index13;
          }
          if (Main.hardMode)
          {
            objArray1[index13].SetDefaults(488);
            ++index13;
          }
          for (int index14 = 0; index14 < 58; ++index14)
          {
            if (Main.player[Main.myPlayer].inventory[index14].type == 930)
            {
              objArray1[index13].SetDefaults(931);
              int index15 = index13 + 1;
              objArray1[index15].SetDefaults(1614);
              index13 = index15 + 1;
              break;
            }
          }
          objArray1[index13].SetDefaults(1786);
          index1 = index13 + 1;
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1348);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(3198);
            ++index1;
          }
          if (NPC.downedBoss2 || NPC.downedBoss3 || Main.hardMode)
          {
            Item[] objArray2 = objArray1;
            int index16 = index1;
            int num = index16 + 1;
            objArray2[index16].SetDefaults(4063);
            Item[] objArray3 = objArray1;
            int index17 = num;
            index1 = index17 + 1;
            objArray3[index17].SetDefaults(4673);
          }
          if (Main.player[Main.myPlayer].HasItem(3107))
          {
            objArray1[index1].SetDefaults(3108);
            ++index1;
            break;
          }
          break;
        case 2:
          objArray1[index1].SetDefaults(97);
          int index18 = index1 + 1;
          if (Main.bloodMoon || Main.hardMode)
          {
            if (WorldGen.SavedOreTiers.Silver == 168)
            {
              objArray1[index18].SetDefaults(4915);
              ++index18;
            }
            else
            {
              objArray1[index18].SetDefaults(278);
              ++index18;
            }
          }
          if (NPC.downedBoss2 && !Main.dayTime || Main.hardMode)
          {
            objArray1[index18].SetDefaults(47);
            ++index18;
          }
          objArray1[index18].SetDefaults(95);
          int index19 = index18 + 1;
          objArray1[index19].SetDefaults(98);
          index1 = index19 + 1;
          if (Main.player[Main.myPlayer].ZoneGraveyard && NPC.downedBoss3)
            objArray1[index1++].SetDefaults(4703);
          if (!Main.dayTime)
          {
            objArray1[index1].SetDefaults(324);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(534);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1432);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(2177);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1258))
          {
            objArray1[index1].SetDefaults(1261);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1835))
          {
            objArray1[index1].SetDefaults(1836);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(3107))
          {
            objArray1[index1].SetDefaults(3108);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1782))
          {
            objArray1[index1].SetDefaults(1783);
            ++index1;
          }
          if (Main.player[Main.myPlayer].HasItem(1784))
          {
            objArray1[index1].SetDefaults(1785);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1736);
            int index20 = index1 + 1;
            objArray1[index20].SetDefaults(1737);
            int index21 = index20 + 1;
            objArray1[index21].SetDefaults(1738);
            index1 = index21 + 1;
            break;
          }
          break;
        case 3:
          int index22;
          if (Main.bloodMoon)
          {
            if (WorldGen.crimson)
            {
              if (!Main.remixWorld)
              {
                objArray1[index1].SetDefaults(2886);
                ++index1;
              }
              objArray1[index1].SetDefaults(2171);
              int index23 = index1 + 1;
              objArray1[index23].SetDefaults(4508);
              index22 = index23 + 1;
            }
            else
            {
              if (!Main.remixWorld)
              {
                objArray1[index1].SetDefaults(67);
                ++index1;
              }
              objArray1[index1].SetDefaults(59);
              int index24 = index1 + 1;
              objArray1[index24].SetDefaults(4504);
              index22 = index24 + 1;
            }
          }
          else
          {
            if (!Main.remixWorld)
            {
              objArray1[index1].SetDefaults(66);
              ++index1;
            }
            objArray1[index1].SetDefaults(62);
            int index25 = index1 + 1;
            objArray1[index25].SetDefaults(63);
            int index26 = index25 + 1;
            objArray1[index26].SetDefaults(745);
            index22 = index26 + 1;
          }
          if (Main.hardMode && Main.player[Main.myPlayer].ZoneGraveyard)
          {
            if (WorldGen.crimson)
              objArray1[index22].SetDefaults(59);
            else
              objArray1[index22].SetDefaults(2171);
            ++index22;
          }
          objArray1[index22].SetDefaults(27);
          int index27 = index22 + 1;
          objArray1[index27].SetDefaults(5309);
          int index28 = index27 + 1;
          objArray1[index28].SetDefaults(114);
          int index29 = index28 + 1;
          objArray1[index29].SetDefaults(1828);
          int index30 = index29 + 1;
          objArray1[index30].SetDefaults(747);
          int index31 = index30 + 1;
          if (Main.hardMode)
          {
            objArray1[index31].SetDefaults(746);
            ++index31;
          }
          if (Main.hardMode)
          {
            objArray1[index31].SetDefaults(369);
            ++index31;
          }
          if (Main.hardMode)
          {
            objArray1[index31].SetDefaults(4505);
            ++index31;
          }
          if (Main.player[Main.myPlayer].ZoneUnderworldHeight)
            objArray1[index31++].SetDefaults(5214);
          else if (Main.player[Main.myPlayer].ZoneGlowshroom)
            objArray1[index31++].SetDefaults(194);
          if (Main.halloween)
          {
            objArray1[index31].SetDefaults(1853);
            int index32 = index31 + 1;
            objArray1[index32].SetDefaults(1854);
            index31 = index32 + 1;
          }
          if (NPC.downedSlimeKing)
          {
            objArray1[index31].SetDefaults(3215);
            ++index31;
          }
          if (NPC.downedQueenBee)
          {
            objArray1[index31].SetDefaults(3216);
            ++index31;
          }
          if (NPC.downedBoss1)
          {
            objArray1[index31].SetDefaults(3219);
            ++index31;
          }
          if (NPC.downedBoss2)
          {
            if (WorldGen.crimson)
            {
              objArray1[index31].SetDefaults(3218);
              ++index31;
            }
            else
            {
              objArray1[index31].SetDefaults(3217);
              ++index31;
            }
          }
          if (NPC.downedBoss3)
          {
            objArray1[index31].SetDefaults(3220);
            int index33 = index31 + 1;
            objArray1[index33].SetDefaults(3221);
            index31 = index33 + 1;
          }
          if (Main.hardMode)
          {
            objArray1[index31].SetDefaults(3222);
            ++index31;
          }
          Item[] objArray4 = objArray1;
          int index34 = index31;
          int num1 = index34 + 1;
          objArray4[index34].SetDefaults(4047);
          Item[] objArray5 = objArray1;
          int index35 = num1;
          int num2 = index35 + 1;
          objArray5[index35].SetDefaults(4045);
          Item[] objArray6 = objArray1;
          int index36 = num2;
          int num3 = index36 + 1;
          objArray6[index36].SetDefaults(4044);
          Item[] objArray7 = objArray1;
          int index37 = num3;
          int num4 = index37 + 1;
          objArray7[index37].SetDefaults(4043);
          Item[] objArray8 = objArray1;
          int index38 = num4;
          int num5 = index38 + 1;
          objArray8[index38].SetDefaults(4042);
          Item[] objArray9 = objArray1;
          int index39 = num5;
          int num6 = index39 + 1;
          objArray9[index39].SetDefaults(4046);
          Item[] objArray10 = objArray1;
          int index40 = num6;
          int num7 = index40 + 1;
          objArray10[index40].SetDefaults(4041);
          Item[] objArray11 = objArray1;
          int index41 = num7;
          int num8 = index41 + 1;
          objArray11[index41].SetDefaults(4241);
          Item[] objArray12 = objArray1;
          int index42 = num8;
          index1 = index42 + 1;
          objArray12[index42].SetDefaults(4048);
          if (Main.hardMode)
          {
            switch (Main.moonPhase / 2)
            {
              case 0:
                Item[] objArray13 = objArray1;
                int index43 = index1;
                int num9 = index43 + 1;
                objArray13[index43].SetDefaults(4430);
                Item[] objArray14 = objArray1;
                int index44 = num9;
                int num10 = index44 + 1;
                objArray14[index44].SetDefaults(4431);
                Item[] objArray15 = objArray1;
                int index45 = num10;
                index1 = index45 + 1;
                objArray15[index45].SetDefaults(4432);
                break;
              case 1:
                Item[] objArray16 = objArray1;
                int index46 = index1;
                int num11 = index46 + 1;
                objArray16[index46].SetDefaults(4433);
                Item[] objArray17 = objArray1;
                int index47 = num11;
                int num12 = index47 + 1;
                objArray17[index47].SetDefaults(4434);
                Item[] objArray18 = objArray1;
                int index48 = num12;
                index1 = index48 + 1;
                objArray18[index48].SetDefaults(4435);
                break;
              case 2:
                Item[] objArray19 = objArray1;
                int index49 = index1;
                int num13 = index49 + 1;
                objArray19[index49].SetDefaults(4436);
                Item[] objArray20 = objArray1;
                int index50 = num13;
                int num14 = index50 + 1;
                objArray20[index50].SetDefaults(4437);
                Item[] objArray21 = objArray1;
                int index51 = num14;
                index1 = index51 + 1;
                objArray21[index51].SetDefaults(4438);
                break;
              default:
                Item[] objArray22 = objArray1;
                int index52 = index1;
                int num15 = index52 + 1;
                objArray22[index52].SetDefaults(4439);
                Item[] objArray23 = objArray1;
                int index53 = num15;
                int num16 = index53 + 1;
                objArray23[index53].SetDefaults(4440);
                Item[] objArray24 = objArray1;
                int index54 = num16;
                index1 = index54 + 1;
                objArray24[index54].SetDefaults(4441);
                break;
            }
          }
          else
            break;
          break;
        case 4:
          objArray1[index1].SetDefaults(168);
          int index55 = index1 + 1;
          objArray1[index55].SetDefaults(166);
          int index56 = index55 + 1;
          objArray1[index56].SetDefaults(167);
          index1 = index56 + 1;
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(265);
            ++index1;
          }
          if (Main.hardMode && NPC.downedPlantBoss && NPC.downedPirates)
          {
            objArray1[index1].SetDefaults(937);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1347);
            ++index1;
          }
          for (int index57 = 0; index57 < 58; ++index57)
          {
            if (Main.player[Main.myPlayer].inventory[index57].type == 4827)
            {
              objArray1[index1].SetDefaults(4827);
              ++index1;
              break;
            }
          }
          for (int index58 = 0; index58 < 58; ++index58)
          {
            if (Main.player[Main.myPlayer].inventory[index58].type == 4824)
            {
              objArray1[index1].SetDefaults(4824);
              ++index1;
              break;
            }
          }
          for (int index59 = 0; index59 < 58; ++index59)
          {
            if (Main.player[Main.myPlayer].inventory[index59].type == 4825)
            {
              objArray1[index1].SetDefaults(4825);
              ++index1;
              break;
            }
          }
          for (int index60 = 0; index60 < 58; ++index60)
          {
            if (Main.player[Main.myPlayer].inventory[index60].type == 4826)
            {
              objArray1[index1].SetDefaults(4826);
              ++index1;
              break;
            }
          }
          break;
        case 5:
          objArray1[index1].SetDefaults(254);
          int index61 = index1 + 1;
          objArray1[index61].SetDefaults(981);
          int index62 = index61 + 1;
          if (Main.dayTime)
          {
            objArray1[index62].SetDefaults(242);
            ++index62;
          }
          switch (Main.moonPhase)
          {
            case 0:
              objArray1[index62].SetDefaults(245);
              int index63 = index62 + 1;
              objArray1[index63].SetDefaults(246);
              index62 = index63 + 1;
              if (!Main.dayTime)
              {
                Item[] objArray25 = objArray1;
                int index64 = index62;
                int num17 = index64 + 1;
                objArray25[index64].SetDefaults(1288);
                Item[] objArray26 = objArray1;
                int index65 = num17;
                index62 = index65 + 1;
                objArray26[index65].SetDefaults(1289);
                break;
              }
              break;
            case 1:
              objArray1[index62].SetDefaults(325);
              int index66 = index62 + 1;
              objArray1[index66].SetDefaults(326);
              index62 = index66 + 1;
              break;
          }
          objArray1[index62].SetDefaults(269);
          int index67 = index62 + 1;
          objArray1[index67].SetDefaults(270);
          int index68 = index67 + 1;
          objArray1[index68].SetDefaults(271);
          int index69 = index68 + 1;
          if (NPC.downedClown)
          {
            objArray1[index69].SetDefaults(503);
            int index70 = index69 + 1;
            objArray1[index70].SetDefaults(504);
            int index71 = index70 + 1;
            objArray1[index71].SetDefaults(505);
            index69 = index71 + 1;
          }
          if (Main.bloodMoon)
          {
            objArray1[index69].SetDefaults(322);
            ++index69;
            if (!Main.dayTime)
            {
              Item[] objArray27 = objArray1;
              int index72 = index69;
              int num18 = index72 + 1;
              objArray27[index72].SetDefaults(3362);
              Item[] objArray28 = objArray1;
              int index73 = num18;
              index69 = index73 + 1;
              objArray28[index73].SetDefaults(3363);
            }
          }
          if (NPC.downedAncientCultist)
          {
            if (Main.dayTime)
            {
              Item[] objArray29 = objArray1;
              int index74 = index69;
              int num19 = index74 + 1;
              objArray29[index74].SetDefaults(2856);
              Item[] objArray30 = objArray1;
              int index75 = num19;
              index69 = index75 + 1;
              objArray30[index75].SetDefaults(2858);
            }
            else
            {
              Item[] objArray31 = objArray1;
              int index76 = index69;
              int num20 = index76 + 1;
              objArray31[index76].SetDefaults(2857);
              Item[] objArray32 = objArray1;
              int index77 = num20;
              index69 = index77 + 1;
              objArray32[index77].SetDefaults(2859);
            }
          }
          if (NPC.AnyNPCs(441))
          {
            Item[] objArray33 = objArray1;
            int index78 = index69;
            int num21 = index78 + 1;
            objArray33[index78].SetDefaults(3242);
            Item[] objArray34 = objArray1;
            int index79 = num21;
            int num22 = index79 + 1;
            objArray34[index79].SetDefaults(3243);
            Item[] objArray35 = objArray1;
            int index80 = num22;
            index69 = index80 + 1;
            objArray35[index80].SetDefaults(3244);
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            Item[] objArray36 = objArray1;
            int index81 = index69;
            int num23 = index81 + 1;
            objArray36[index81].SetDefaults(4685);
            Item[] objArray37 = objArray1;
            int index82 = num23;
            int num24 = index82 + 1;
            objArray37[index82].SetDefaults(4686);
            Item[] objArray38 = objArray1;
            int index83 = num24;
            int num25 = index83 + 1;
            objArray38[index83].SetDefaults(4704);
            Item[] objArray39 = objArray1;
            int index84 = num25;
            int num26 = index84 + 1;
            objArray39[index84].SetDefaults(4705);
            Item[] objArray40 = objArray1;
            int index85 = num26;
            int num27 = index85 + 1;
            objArray40[index85].SetDefaults(4706);
            Item[] objArray41 = objArray1;
            int index86 = num27;
            int num28 = index86 + 1;
            objArray41[index86].SetDefaults(4707);
            Item[] objArray42 = objArray1;
            int index87 = num28;
            int num29 = index87 + 1;
            objArray42[index87].SetDefaults(4708);
            Item[] objArray43 = objArray1;
            int index88 = num29;
            index69 = index88 + 1;
            objArray43[index88].SetDefaults(4709);
          }
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index69].SetDefaults(1429);
            ++index69;
          }
          if (Main.halloween)
          {
            objArray1[index69].SetDefaults(1740);
            ++index69;
          }
          if (Main.hardMode)
          {
            if (Main.moonPhase == 2)
            {
              objArray1[index69].SetDefaults(869);
              ++index69;
            }
            if (Main.moonPhase == 3)
            {
              objArray1[index69].SetDefaults(4994);
              int index89 = index69 + 1;
              objArray1[index89].SetDefaults(4997);
              index69 = index89 + 1;
            }
            if (Main.moonPhase == 4)
            {
              objArray1[index69].SetDefaults(864);
              int index90 = index69 + 1;
              objArray1[index90].SetDefaults(865);
              index69 = index90 + 1;
            }
            if (Main.moonPhase == 5)
            {
              objArray1[index69].SetDefaults(4995);
              int index91 = index69 + 1;
              objArray1[index91].SetDefaults(4998);
              index69 = index91 + 1;
            }
            if (Main.moonPhase == 6)
            {
              objArray1[index69].SetDefaults(873);
              int index92 = index69 + 1;
              objArray1[index92].SetDefaults(874);
              int index93 = index92 + 1;
              objArray1[index93].SetDefaults(875);
              index69 = index93 + 1;
            }
            if (Main.moonPhase == 7)
            {
              objArray1[index69].SetDefaults(4996);
              int index94 = index69 + 1;
              objArray1[index94].SetDefaults(4999);
              index69 = index94 + 1;
            }
          }
          if (NPC.downedFrost)
          {
            if (Main.dayTime)
            {
              objArray1[index69].SetDefaults(1275);
              ++index69;
            }
            else
            {
              objArray1[index69].SetDefaults(1276);
              ++index69;
            }
          }
          if (Main.halloween)
          {
            Item[] objArray44 = objArray1;
            int index95 = index69;
            int num30 = index95 + 1;
            objArray44[index95].SetDefaults(3246);
            Item[] objArray45 = objArray1;
            int index96 = num30;
            index69 = index96 + 1;
            objArray45[index96].SetDefaults(3247);
          }
          if (BirthdayParty.PartyIsUp)
          {
            Item[] objArray46 = objArray1;
            int index97 = index69;
            int num31 = index97 + 1;
            objArray46[index97].SetDefaults(3730);
            Item[] objArray47 = objArray1;
            int index98 = num31;
            int num32 = index98 + 1;
            objArray47[index98].SetDefaults(3731);
            Item[] objArray48 = objArray1;
            int index99 = num32;
            int num33 = index99 + 1;
            objArray48[index99].SetDefaults(3733);
            Item[] objArray49 = objArray1;
            int index100 = num33;
            int num34 = index100 + 1;
            objArray49[index100].SetDefaults(3734);
            Item[] objArray50 = objArray1;
            int index101 = num34;
            index69 = index101 + 1;
            objArray50[index101].SetDefaults(3735);
          }
          int scoreAccumulated1 = Main.LocalPlayer.golferScoreAccumulated;
          if (index69 < 38 && scoreAccumulated1 >= 2000)
          {
            objArray1[index69].SetDefaults(4744);
            ++index69;
          }
          objArray1[index69].SetDefaults(5308);
          index1 = index69 + 1;
          break;
        case 6:
          objArray1[index1].SetDefaults(128);
          int index102 = index1 + 1;
          objArray1[index102].SetDefaults(486);
          int index103 = index102 + 1;
          objArray1[index103].SetDefaults(398);
          int index104 = index103 + 1;
          objArray1[index104].SetDefaults(84);
          int index105 = index104 + 1;
          objArray1[index105].SetDefaults(407);
          int index106 = index105 + 1;
          objArray1[index106].SetDefaults(161);
          index1 = index106 + 1;
          if (Main.hardMode)
          {
            objArray1[index1++].SetDefaults(5324);
            break;
          }
          break;
        case 7:
          objArray1[index1].SetDefaults(487);
          int index107 = index1 + 1;
          objArray1[index107].SetDefaults(496);
          int index108 = index107 + 1;
          objArray1[index108].SetDefaults(500);
          int index109 = index108 + 1;
          objArray1[index109].SetDefaults(507);
          int index110 = index109 + 1;
          objArray1[index110].SetDefaults(508);
          int index111 = index110 + 1;
          objArray1[index111].SetDefaults(531);
          int index112 = index111 + 1;
          objArray1[index112].SetDefaults(149);
          int index113 = index112 + 1;
          objArray1[index113].SetDefaults(576);
          int index114 = index113 + 1;
          objArray1[index114].SetDefaults(3186);
          index1 = index114 + 1;
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1739);
            ++index1;
            break;
          }
          break;
        case 8:
          objArray1[index1].SetDefaults(509);
          int index115 = index1 + 1;
          objArray1[index115].SetDefaults(850);
          int index116 = index115 + 1;
          objArray1[index116].SetDefaults(851);
          int index117 = index116 + 1;
          objArray1[index117].SetDefaults(3612);
          int index118 = index117 + 1;
          objArray1[index118].SetDefaults(510);
          int index119 = index118 + 1;
          objArray1[index119].SetDefaults(530);
          int index120 = index119 + 1;
          objArray1[index120].SetDefaults(513);
          int index121 = index120 + 1;
          objArray1[index121].SetDefaults(538);
          int index122 = index121 + 1;
          objArray1[index122].SetDefaults(529);
          int index123 = index122 + 1;
          objArray1[index123].SetDefaults(541);
          int index124 = index123 + 1;
          objArray1[index124].SetDefaults(542);
          int index125 = index124 + 1;
          objArray1[index125].SetDefaults(543);
          int index126 = index125 + 1;
          objArray1[index126].SetDefaults(852);
          int index127 = index126 + 1;
          objArray1[index127].SetDefaults(853);
          int num35 = index127 + 1;
          Item[] objArray51 = objArray1;
          int index128 = num35;
          int num36 = index128 + 1;
          objArray51[index128].SetDefaults(4261);
          Item[] objArray52 = objArray1;
          int index129 = num36;
          int index130 = index129 + 1;
          objArray52[index129].SetDefaults(3707);
          objArray1[index130].SetDefaults(2739);
          int index131 = index130 + 1;
          objArray1[index131].SetDefaults(849);
          int num37 = index131 + 1;
          Item[] objArray53 = objArray1;
          int index132 = num37;
          int num38 = index132 + 1;
          objArray53[index132].SetDefaults(1263);
          Item[] objArray54 = objArray1;
          int index133 = num38;
          int num39 = index133 + 1;
          objArray54[index133].SetDefaults(3616);
          Item[] objArray55 = objArray1;
          int index134 = num39;
          int num40 = index134 + 1;
          objArray55[index134].SetDefaults(3725);
          Item[] objArray56 = objArray1;
          int index135 = num40;
          int num41 = index135 + 1;
          objArray56[index135].SetDefaults(2799);
          Item[] objArray57 = objArray1;
          int index136 = num41;
          int num42 = index136 + 1;
          objArray57[index136].SetDefaults(3619);
          Item[] objArray58 = objArray1;
          int index137 = num42;
          int num43 = index137 + 1;
          objArray58[index137].SetDefaults(3627);
          Item[] objArray59 = objArray1;
          int index138 = num43;
          int num44 = index138 + 1;
          objArray59[index138].SetDefaults(3629);
          Item[] objArray60 = objArray1;
          int index139 = num44;
          int num45 = index139 + 1;
          objArray60[index139].SetDefaults(585);
          Item[] objArray61 = objArray1;
          int index140 = num45;
          int num46 = index140 + 1;
          objArray61[index140].SetDefaults(584);
          Item[] objArray62 = objArray1;
          int index141 = num46;
          int num47 = index141 + 1;
          objArray62[index141].SetDefaults(583);
          Item[] objArray63 = objArray1;
          int index142 = num47;
          int num48 = index142 + 1;
          objArray63[index142].SetDefaults(4484);
          Item[] objArray64 = objArray1;
          int index143 = num48;
          index1 = index143 + 1;
          objArray64[index143].SetDefaults(4485);
          if (NPC.AnyNPCs(369) && (Main.moonPhase == 1 || Main.moonPhase == 3 || Main.moonPhase == 5 || Main.moonPhase == 7))
          {
            objArray1[index1].SetDefaults(2295);
            ++index1;
            break;
          }
          break;
        case 9:
          objArray1[index1].SetDefaults(588);
          int index144 = index1 + 1;
          objArray1[index144].SetDefaults(589);
          int index145 = index144 + 1;
          objArray1[index145].SetDefaults(590);
          int index146 = index145 + 1;
          objArray1[index146].SetDefaults(597);
          int index147 = index146 + 1;
          objArray1[index147].SetDefaults(598);
          int index148 = index147 + 1;
          objArray1[index148].SetDefaults(596);
          index1 = index148 + 1;
          for (int Type = 1873; Type < 1906; ++Type)
          {
            objArray1[index1].SetDefaults(Type);
            ++index1;
          }
          break;
        case 10:
          if (NPC.downedMechBossAny)
          {
            objArray1[index1].SetDefaults(756);
            int index149 = index1 + 1;
            objArray1[index149].SetDefaults(787);
            index1 = index149 + 1;
          }
          objArray1[index1].SetDefaults(868);
          int index150 = index1 + 1;
          if (NPC.downedPlantBoss)
          {
            objArray1[index150].SetDefaults(1551);
            ++index150;
          }
          objArray1[index150].SetDefaults(1181);
          int num49 = index150 + 1;
          Item[] objArray65 = objArray1;
          int index151 = num49;
          index1 = index151 + 1;
          objArray65[index151].SetDefaults(5231);
          if (!Main.remixWorld)
          {
            objArray1[index1++].SetDefaults(783);
            break;
          }
          break;
        case 11:
          if (!Main.remixWorld)
            objArray1[index1++].SetDefaults(779);
          int num50;
          if (Main.moonPhase >= 4 && Main.hardMode)
          {
            Item[] objArray66 = objArray1;
            int index152 = index1;
            num50 = index152 + 1;
            objArray66[index152].SetDefaults(748);
          }
          else
          {
            Item[] objArray67 = objArray1;
            int index153 = index1;
            int num51 = index153 + 1;
            objArray67[index153].SetDefaults(839);
            Item[] objArray68 = objArray1;
            int index154 = num51;
            int num52 = index154 + 1;
            objArray68[index154].SetDefaults(840);
            Item[] objArray69 = objArray1;
            int index155 = num52;
            num50 = index155 + 1;
            objArray69[index155].SetDefaults(841);
          }
          if (NPC.downedGolemBoss)
            objArray1[num50++].SetDefaults(948);
          if (Main.hardMode)
            objArray1[num50++].SetDefaults(3623);
          Item[] objArray70 = objArray1;
          int index156 = num50;
          int num53 = index156 + 1;
          objArray70[index156].SetDefaults(3603);
          Item[] objArray71 = objArray1;
          int index157 = num53;
          int num54 = index157 + 1;
          objArray71[index157].SetDefaults(3604);
          Item[] objArray72 = objArray1;
          int index158 = num54;
          int num55 = index158 + 1;
          objArray72[index158].SetDefaults(3607);
          Item[] objArray73 = objArray1;
          int index159 = num55;
          int num56 = index159 + 1;
          objArray73[index159].SetDefaults(3605);
          Item[] objArray74 = objArray1;
          int index160 = num56;
          int num57 = index160 + 1;
          objArray74[index160].SetDefaults(3606);
          Item[] objArray75 = objArray1;
          int index161 = num57;
          int num58 = index161 + 1;
          objArray75[index161].SetDefaults(3608);
          Item[] objArray76 = objArray1;
          int index162 = num58;
          int num59 = index162 + 1;
          objArray76[index162].SetDefaults(3618);
          Item[] objArray77 = objArray1;
          int index163 = num59;
          int num60 = index163 + 1;
          objArray77[index163].SetDefaults(3602);
          Item[] objArray78 = objArray1;
          int index164 = num60;
          int num61 = index164 + 1;
          objArray78[index164].SetDefaults(3663);
          Item[] objArray79 = objArray1;
          int index165 = num61;
          int num62 = index165 + 1;
          objArray79[index165].SetDefaults(3609);
          Item[] objArray80 = objArray1;
          int index166 = num62;
          int num63 = index166 + 1;
          objArray80[index166].SetDefaults(3610);
          if (Main.hardMode || !Main.getGoodWorld)
            objArray1[num63++].SetDefaults(995);
          if (NPC.downedBoss1 && NPC.downedBoss2 && NPC.downedBoss3)
            objArray1[num63++].SetDefaults(2203);
          if (WorldGen.crimson)
          {
            Item[] objArray81 = objArray1;
            int index167 = num63;
            index1 = index167 + 1;
            objArray81[index167].SetDefaults(2193);
          }
          else
          {
            Item[] objArray82 = objArray1;
            int index168 = num63;
            index1 = index168 + 1;
            objArray82[index168].SetDefaults(4142);
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
            objArray1[index1++].SetDefaults(2192);
          if (Main.player[Main.myPlayer].ZoneJungle)
            objArray1[index1++].SetDefaults(2204);
          if (Main.player[Main.myPlayer].ZoneSnow)
            objArray1[index1++].SetDefaults(2198);
          if ((double) Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.34999999403953552)
            objArray1[index1++].SetDefaults(2197);
          if (Main.player[Main.myPlayer].HasItem(832))
            objArray1[index1++].SetDefaults(2196);
          if (!Main.remixWorld)
          {
            if (Main.eclipse || Main.bloodMoon)
            {
              if (WorldGen.crimson)
                objArray1[index1++].SetDefaults(784);
              else
                objArray1[index1++].SetDefaults(782);
            }
            else if (Main.player[Main.myPlayer].ZoneHallow)
              objArray1[index1++].SetDefaults(781);
            else
              objArray1[index1++].SetDefaults(780);
            if (NPC.downedMoonlord)
            {
              Item[] objArray83 = objArray1;
              int index169 = index1;
              int num64 = index169 + 1;
              objArray83[index169].SetDefaults(5392);
              Item[] objArray84 = objArray1;
              int index170 = num64;
              int num65 = index170 + 1;
              objArray84[index170].SetDefaults(5393);
              Item[] objArray85 = objArray1;
              int index171 = num65;
              index1 = index171 + 1;
              objArray85[index171].SetDefaults(5394);
            }
          }
          if (Main.hardMode)
          {
            Item[] objArray86 = objArray1;
            int index172 = index1;
            int num66 = index172 + 1;
            objArray86[index172].SetDefaults(1344);
            Item[] objArray87 = objArray1;
            int index173 = num66;
            index1 = index173 + 1;
            objArray87[index173].SetDefaults(4472);
          }
          if (Main.halloween)
          {
            objArray1[index1++].SetDefaults(1742);
            break;
          }
          break;
        case 12:
          objArray1[index1].SetDefaults(1037);
          int index174 = index1 + 1;
          objArray1[index174].SetDefaults(2874);
          int index175 = index174 + 1;
          objArray1[index175].SetDefaults(1120);
          index1 = index175 + 1;
          if (Main.netMode == 1)
          {
            objArray1[index1].SetDefaults(1969);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(3248);
            int index176 = index1 + 1;
            objArray1[index176].SetDefaults(1741);
            index1 = index176 + 1;
          }
          if (Main.moonPhase == 0)
          {
            objArray1[index1].SetDefaults(2871);
            int index177 = index1 + 1;
            objArray1[index177].SetDefaults(2872);
            index1 = index177 + 1;
          }
          if (!Main.dayTime && Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(4663);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4662);
            ++index1;
            break;
          }
          break;
        case 13:
          objArray1[index1].SetDefaults(859);
          int index178 = index1 + 1;
          if (Main.LocalPlayer.golferScoreAccumulated > 500)
            objArray1[index178++].SetDefaults(4743);
          objArray1[index178].SetDefaults(1000);
          int index179 = index178 + 1;
          objArray1[index179].SetDefaults(1168);
          int index180 = index179 + 1;
          int index181;
          if (Main.dayTime)
          {
            objArray1[index180].SetDefaults(1449);
            index181 = index180 + 1;
          }
          else
          {
            objArray1[index180].SetDefaults(4552);
            index181 = index180 + 1;
          }
          objArray1[index181].SetDefaults(1345);
          int index182 = index181 + 1;
          objArray1[index182].SetDefaults(1450);
          int num67 = index182 + 1;
          Item[] objArray88 = objArray1;
          int index183 = num67;
          int num68 = index183 + 1;
          objArray88[index183].SetDefaults(3253);
          Item[] objArray89 = objArray1;
          int index184 = num68;
          int num69 = index184 + 1;
          objArray89[index184].SetDefaults(4553);
          Item[] objArray90 = objArray1;
          int index185 = num69;
          int num70 = index185 + 1;
          objArray90[index185].SetDefaults(2700);
          Item[] objArray91 = objArray1;
          int index186 = num70;
          int num71 = index186 + 1;
          objArray91[index186].SetDefaults(2738);
          Item[] objArray92 = objArray1;
          int index187 = num71;
          int num72 = index187 + 1;
          objArray92[index187].SetDefaults(4470);
          Item[] objArray93 = objArray1;
          int index188 = num72;
          int index189 = index188 + 1;
          objArray93[index188].SetDefaults(4681);
          if (Main.player[Main.myPlayer].ZoneGraveyard)
            objArray1[index189++].SetDefaults(4682);
          if (LanternNight.LanternsUp)
            objArray1[index189++].SetDefaults(4702);
          if (Main.player[Main.myPlayer].HasItem(3548))
          {
            objArray1[index189].SetDefaults(3548);
            ++index189;
          }
          if (NPC.AnyNPCs(229))
            objArray1[index189++].SetDefaults(3369);
          if (NPC.downedGolemBoss)
            objArray1[index189++].SetDefaults(3546);
          if (Main.hardMode)
          {
            objArray1[index189].SetDefaults(3214);
            int index190 = index189 + 1;
            objArray1[index190].SetDefaults(2868);
            int index191 = index190 + 1;
            objArray1[index191].SetDefaults(970);
            int index192 = index191 + 1;
            objArray1[index192].SetDefaults(971);
            int index193 = index192 + 1;
            objArray1[index193].SetDefaults(972);
            int index194 = index193 + 1;
            objArray1[index194].SetDefaults(973);
            index189 = index194 + 1;
          }
          Item[] objArray94 = objArray1;
          int index195 = index189;
          int num73 = index195 + 1;
          objArray94[index195].SetDefaults(4791);
          Item[] objArray95 = objArray1;
          int index196 = num73;
          int num74 = index196 + 1;
          objArray95[index196].SetDefaults(3747);
          Item[] objArray96 = objArray1;
          int index197 = num74;
          int num75 = index197 + 1;
          objArray96[index197].SetDefaults(3732);
          Item[] objArray97 = objArray1;
          int index198 = num75;
          index1 = index198 + 1;
          objArray97[index198].SetDefaults(3742);
          if (BirthdayParty.PartyIsUp)
          {
            Item[] objArray98 = objArray1;
            int index199 = index1;
            int num76 = index199 + 1;
            objArray98[index199].SetDefaults(3749);
            Item[] objArray99 = objArray1;
            int index200 = num76;
            int num77 = index200 + 1;
            objArray99[index200].SetDefaults(3746);
            Item[] objArray100 = objArray1;
            int index201 = num77;
            int num78 = index201 + 1;
            objArray100[index201].SetDefaults(3739);
            Item[] objArray101 = objArray1;
            int index202 = num78;
            int num79 = index202 + 1;
            objArray101[index202].SetDefaults(3740);
            Item[] objArray102 = objArray1;
            int index203 = num79;
            int num80 = index203 + 1;
            objArray102[index203].SetDefaults(3741);
            Item[] objArray103 = objArray1;
            int index204 = num80;
            int num81 = index204 + 1;
            objArray103[index204].SetDefaults(3737);
            Item[] objArray104 = objArray1;
            int index205 = num81;
            int num82 = index205 + 1;
            objArray104[index205].SetDefaults(3738);
            Item[] objArray105 = objArray1;
            int index206 = num82;
            int num83 = index206 + 1;
            objArray105[index206].SetDefaults(3736);
            Item[] objArray106 = objArray1;
            int index207 = num83;
            int num84 = index207 + 1;
            objArray106[index207].SetDefaults(3745);
            Item[] objArray107 = objArray1;
            int index208 = num84;
            int num85 = index208 + 1;
            objArray107[index208].SetDefaults(3744);
            Item[] objArray108 = objArray1;
            int index209 = num85;
            index1 = index209 + 1;
            objArray108[index209].SetDefaults(3743);
            break;
          }
          break;
        case 14:
          objArray1[index1].SetDefaults(771);
          ++index1;
          if (Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(772);
            ++index1;
          }
          if (!Main.dayTime || Main.eclipse)
          {
            objArray1[index1].SetDefaults(773);
            ++index1;
          }
          if (Main.eclipse)
          {
            objArray1[index1].SetDefaults(774);
            ++index1;
          }
          if (NPC.downedMartians)
          {
            objArray1[index1++].SetDefaults(4445);
            if (Main.bloodMoon || Main.eclipse)
              objArray1[index1++].SetDefaults(4446);
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(4459);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(760);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1346);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(5451);
            ++index1;
          }
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(5452);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4409);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4392);
            ++index1;
          }
          if (Main.halloween)
          {
            objArray1[index1].SetDefaults(1743);
            int index210 = index1 + 1;
            objArray1[index210].SetDefaults(1744);
            int index211 = index210 + 1;
            objArray1[index211].SetDefaults(1745);
            index1 = index211 + 1;
          }
          if (NPC.downedMartians)
          {
            Item[] objArray109 = objArray1;
            int index212 = index1;
            int num86 = index212 + 1;
            objArray109[index212].SetDefaults(2862);
            Item[] objArray110 = objArray1;
            int index213 = num86;
            index1 = index213 + 1;
            objArray110[index213].SetDefaults(3109);
          }
          if (Main.player[Main.myPlayer].HasItem(3384) || Main.player[Main.myPlayer].HasItem(3664))
          {
            objArray1[index1].SetDefaults(3664);
            ++index1;
            break;
          }
          break;
        case 15:
          objArray1[index1].SetDefaults(1071);
          int index214 = index1 + 1;
          objArray1[index214].SetDefaults(1072);
          int index215 = index214 + 1;
          objArray1[index215].SetDefaults(1100);
          int index216 = index215 + 1;
          for (int Type = 1073; Type <= 1084; ++Type)
          {
            objArray1[index216].SetDefaults(Type);
            ++index216;
          }
          objArray1[index216].SetDefaults(1097);
          int index217 = index216 + 1;
          objArray1[index217].SetDefaults(1099);
          int index218 = index217 + 1;
          objArray1[index218].SetDefaults(1098);
          int index219 = index218 + 1;
          objArray1[index219].SetDefaults(1966);
          index1 = index219 + 1;
          if (Main.hardMode)
          {
            objArray1[index1].SetDefaults(1967);
            int index220 = index1 + 1;
            objArray1[index220].SetDefaults(1968);
            index1 = index220 + 1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4668);
            ++index1;
            if (NPC.downedPlantBoss)
            {
              objArray1[index1].SetDefaults(5344);
              ++index1;
              break;
            }
            break;
          }
          break;
        case 16:
          Item[] objArray111 = objArray1;
          int index221 = index1;
          int num87 = index221 + 1;
          objArray111[index221].SetDefaults(1430);
          Item[] objArray112 = objArray1;
          int index222 = num87;
          int num88 = index222 + 1;
          objArray112[index222].SetDefaults(986);
          if (NPC.AnyNPCs(108))
            objArray1[num88++].SetDefaults(2999);
          if (!Main.dayTime)
            objArray1[num88++].SetDefaults(1158);
          if (Main.hardMode && NPC.downedPlantBoss)
          {
            Item[] objArray113 = objArray1;
            int index223 = num88;
            int num89 = index223 + 1;
            objArray113[index223].SetDefaults(1159);
            Item[] objArray114 = objArray1;
            int index224 = num89;
            int num90 = index224 + 1;
            objArray114[index224].SetDefaults(1160);
            Item[] objArray115 = objArray1;
            int index225 = num90;
            int num91 = index225 + 1;
            objArray115[index225].SetDefaults(1161);
            if (Main.player[Main.myPlayer].ZoneJungle)
              objArray1[num91++].SetDefaults(1167);
            Item[] objArray116 = objArray1;
            int index226 = num91;
            num88 = index226 + 1;
            objArray116[index226].SetDefaults(1339);
          }
          if (Main.hardMode && Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[num88++].SetDefaults(1171);
            if (!Main.dayTime && NPC.downedPlantBoss)
              objArray1[num88++].SetDefaults(1162);
          }
          Item[] objArray117 = objArray1;
          int index227 = num88;
          int num92 = index227 + 1;
          objArray117[index227].SetDefaults(909);
          Item[] objArray118 = objArray1;
          int index228 = num92;
          int num93 = index228 + 1;
          objArray118[index228].SetDefaults(910);
          Item[] objArray119 = objArray1;
          int index229 = num93;
          int num94 = index229 + 1;
          objArray119[index229].SetDefaults(940);
          Item[] objArray120 = objArray1;
          int index230 = num94;
          int num95 = index230 + 1;
          objArray120[index230].SetDefaults(941);
          Item[] objArray121 = objArray1;
          int index231 = num95;
          int num96 = index231 + 1;
          objArray121[index231].SetDefaults(942);
          Item[] objArray122 = objArray1;
          int index232 = num96;
          int num97 = index232 + 1;
          objArray122[index232].SetDefaults(943);
          Item[] objArray123 = objArray1;
          int index233 = num97;
          int num98 = index233 + 1;
          objArray123[index233].SetDefaults(944);
          Item[] objArray124 = objArray1;
          int index234 = num98;
          int num99 = index234 + 1;
          objArray124[index234].SetDefaults(945);
          Item[] objArray125 = objArray1;
          int index235 = num99;
          int num100 = index235 + 1;
          objArray125[index235].SetDefaults(4922);
          Item[] objArray126 = objArray1;
          int index236 = num100;
          index1 = index236 + 1;
          objArray126[index236].SetDefaults(4417);
          if (Main.player[Main.myPlayer].HasItem(1835))
            objArray1[index1++].SetDefaults(1836);
          if (Main.player[Main.myPlayer].HasItem(1258))
            objArray1[index1++].SetDefaults(1261);
          if (Main.halloween)
          {
            objArray1[index1++].SetDefaults(1791);
            break;
          }
          break;
        case 17:
          objArray1[index1].SetDefaults(928);
          int index237 = index1 + 1;
          objArray1[index237].SetDefaults(929);
          int index238 = index237 + 1;
          objArray1[index238].SetDefaults(876);
          int index239 = index238 + 1;
          objArray1[index239].SetDefaults(877);
          int index240 = index239 + 1;
          objArray1[index240].SetDefaults(878);
          int index241 = index240 + 1;
          objArray1[index241].SetDefaults(2434);
          index1 = index241 + 1;
          int num101 = (int) (((double) Main.screenPosition.X + (double) (Main.screenWidth / 2)) / 16.0);
          if ((double) Main.screenPosition.Y / 16.0 < Main.worldSurface + 10.0 && (num101 < 380 || num101 > Main.maxTilesX - 380))
          {
            objArray1[index1].SetDefaults(1180);
            ++index1;
          }
          if (Main.hardMode && NPC.downedMechBossAny && NPC.AnyNPCs(208))
          {
            objArray1[index1].SetDefaults(1337);
            ++index1;
            break;
          }
          break;
        case 18:
          objArray1[index1].SetDefaults(1990);
          int index242 = index1 + 1;
          objArray1[index242].SetDefaults(1979);
          int index243 = index242 + 1;
          if (Main.player[Main.myPlayer].statLifeMax >= 400)
          {
            objArray1[index243].SetDefaults(1977);
            ++index243;
          }
          if (Main.player[Main.myPlayer].statManaMax >= 200)
          {
            objArray1[index243].SetDefaults(1978);
            ++index243;
          }
          long num102 = 0;
          for (int index244 = 0; index244 < 54; ++index244)
          {
            if (Main.player[Main.myPlayer].inventory[index244].type == 71)
              num102 += (long) Main.player[Main.myPlayer].inventory[index244].stack;
            if (Main.player[Main.myPlayer].inventory[index244].type == 72)
              num102 += (long) (Main.player[Main.myPlayer].inventory[index244].stack * 100);
            if (Main.player[Main.myPlayer].inventory[index244].type == 73)
              num102 += (long) (Main.player[Main.myPlayer].inventory[index244].stack * 10000);
            if (Main.player[Main.myPlayer].inventory[index244].type == 74)
              num102 += (long) (Main.player[Main.myPlayer].inventory[index244].stack * 1000000);
          }
          if (num102 >= 1000000L)
          {
            objArray1[index243].SetDefaults(1980);
            ++index243;
          }
          if (Main.moonPhase % 2 == 0 && Main.dayTime || Main.moonPhase % 2 == 1 && !Main.dayTime)
          {
            objArray1[index243].SetDefaults(1981);
            ++index243;
          }
          if (Main.player[Main.myPlayer].team != 0)
          {
            objArray1[index243].SetDefaults(1982);
            ++index243;
          }
          if (Main.hardMode)
          {
            objArray1[index243].SetDefaults(1983);
            ++index243;
          }
          if (NPC.AnyNPCs(208))
          {
            objArray1[index243].SetDefaults(1984);
            ++index243;
          }
          if (Main.hardMode && NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
          {
            objArray1[index243].SetDefaults(1985);
            ++index243;
          }
          if (Main.hardMode && NPC.downedMechBossAny)
          {
            objArray1[index243].SetDefaults(1986);
            ++index243;
          }
          if (Main.hardMode && NPC.downedMartians)
          {
            objArray1[index243].SetDefaults(2863);
            int index245 = index243 + 1;
            objArray1[index245].SetDefaults(3259);
            index243 = index245 + 1;
          }
          Item[] objArray127 = objArray1;
          int index246 = index243;
          index1 = index246 + 1;
          objArray127[index246].SetDefaults(5104);
          break;
        case 19:
          for (int index247 = 0; index247 < 40; ++index247)
          {
            if (Main.travelShop[index247] != 0)
            {
              objArray1[index1].netDefaults(Main.travelShop[index247]);
              ++index1;
            }
          }
          break;
        case 20:
          if (Main.moonPhase == 0)
          {
            objArray1[index1].SetDefaults(284);
            ++index1;
          }
          if (Main.moonPhase == 1)
          {
            objArray1[index1].SetDefaults(946);
            ++index1;
          }
          if (Main.moonPhase == 2 && !Main.remixWorld)
          {
            objArray1[index1].SetDefaults(3069);
            ++index1;
          }
          if (Main.moonPhase == 2 && Main.remixWorld)
          {
            objArray1[index1].SetDefaults(517);
            ++index1;
          }
          if (Main.moonPhase == 3)
          {
            objArray1[index1].SetDefaults(4341);
            ++index1;
          }
          if (Main.moonPhase == 4)
          {
            objArray1[index1].SetDefaults(285);
            ++index1;
          }
          if (Main.moonPhase == 5)
          {
            objArray1[index1].SetDefaults(953);
            ++index1;
          }
          if (Main.moonPhase == 6)
          {
            objArray1[index1].SetDefaults(3068);
            ++index1;
          }
          if (Main.moonPhase == 7)
          {
            objArray1[index1].SetDefaults(3084);
            ++index1;
          }
          if (Main.moonPhase % 2 == 0)
          {
            objArray1[index1].SetDefaults(3001);
            ++index1;
          }
          if (Main.moonPhase % 2 != 0)
          {
            objArray1[index1].SetDefaults(28);
            ++index1;
          }
          if (Main.moonPhase % 2 != 0 && Main.hardMode)
          {
            objArray1[index1].SetDefaults(188);
            ++index1;
          }
          if (!Main.dayTime || Main.moonPhase == 0)
          {
            objArray1[index1].SetDefaults(3002);
            ++index1;
            if (Main.player[Main.myPlayer].HasItem(930))
            {
              objArray1[index1].SetDefaults(5377);
              ++index1;
            }
          }
          else if (Main.dayTime && Main.moonPhase != 0)
          {
            objArray1[index1].SetDefaults(282);
            ++index1;
          }
          if (Main.time % 60.0 * 60.0 * 6.0 <= 10800.0)
            objArray1[index1].SetDefaults(3004);
          else
            objArray1[index1].SetDefaults(8);
          int index248 = index1 + 1;
          if (Main.moonPhase == 0 || Main.moonPhase == 1 || Main.moonPhase == 4 || Main.moonPhase == 5)
            objArray1[index248].SetDefaults(3003);
          else
            objArray1[index248].SetDefaults(40);
          int index249 = index248 + 1;
          if (Main.moonPhase % 4 == 0)
            objArray1[index249].SetDefaults(3310);
          else if (Main.moonPhase % 4 == 1)
            objArray1[index249].SetDefaults(3313);
          else if (Main.moonPhase % 4 == 2)
            objArray1[index249].SetDefaults(3312);
          else
            objArray1[index249].SetDefaults(3311);
          int index250 = index249 + 1;
          objArray1[index250].SetDefaults(166);
          int index251 = index250 + 1;
          objArray1[index251].SetDefaults(965);
          index1 = index251 + 1;
          if (Main.hardMode)
          {
            if (Main.moonPhase < 4)
              objArray1[index1].SetDefaults(3316);
            else
              objArray1[index1].SetDefaults(3315);
            int index252 = index1 + 1;
            objArray1[index252].SetDefaults(3334);
            index1 = index252 + 1;
            if (Main.bloodMoon)
            {
              objArray1[index1].SetDefaults(3258);
              ++index1;
            }
          }
          if (Main.moonPhase == 0 && !Main.dayTime)
          {
            objArray1[index1].SetDefaults(3043);
            ++index1;
          }
          if (!Main.player[Main.myPlayer].ateArtisanBread && Main.moonPhase >= 3 && Main.moonPhase <= 5)
          {
            objArray1[index1].SetDefaults(5326);
            ++index1;
            break;
          }
          break;
        case 21:
          bool flag2 = Main.hardMode && NPC.downedMechBossAny;
          int num103 = !Main.hardMode ? 0 : (NPC.downedGolemBoss ? 1 : 0);
          objArray1[index1].SetDefaults(353);
          int index253 = index1 + 1;
          objArray1[index253].SetDefaults(3828);
          objArray1[index253].shopCustomPrice = num103 == 0 ? (!flag2 ? new int?(Item.buyPrice(silver: 25)) : new int?(Item.buyPrice(gold: 1))) : new int?(Item.buyPrice(gold: 4));
          int index254 = index253 + 1;
          objArray1[index254].SetDefaults(3816);
          int index255 = index254 + 1;
          objArray1[index255].SetDefaults(3813);
          objArray1[index255].shopCustomPrice = new int?(50);
          objArray1[index255].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int num104 = index255 + 1;
          int index256 = 10;
          objArray1[index256].SetDefaults(3818);
          objArray1[index256].shopCustomPrice = new int?(5);
          objArray1[index256].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index257 = index256 + 1;
          objArray1[index257].SetDefaults(3824);
          objArray1[index257].shopCustomPrice = new int?(5);
          objArray1[index257].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index258 = index257 + 1;
          objArray1[index258].SetDefaults(3832);
          objArray1[index258].shopCustomPrice = new int?(5);
          objArray1[index258].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          int index259 = index258 + 1;
          objArray1[index259].SetDefaults(3829);
          objArray1[index259].shopCustomPrice = new int?(5);
          objArray1[index259].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          if (flag2)
          {
            int index260 = 20;
            objArray1[index260].SetDefaults(3819);
            objArray1[index260].shopCustomPrice = new int?(15);
            objArray1[index260].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index261 = index260 + 1;
            objArray1[index261].SetDefaults(3825);
            objArray1[index261].shopCustomPrice = new int?(15);
            objArray1[index261].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index262 = index261 + 1;
            objArray1[index262].SetDefaults(3833);
            objArray1[index262].shopCustomPrice = new int?(15);
            objArray1[index262].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index263 = index262 + 1;
            objArray1[index263].SetDefaults(3830);
            objArray1[index263].shopCustomPrice = new int?(15);
            objArray1[index263].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          }
          if (num103 != 0)
          {
            int index264 = 30;
            objArray1[index264].SetDefaults(3820);
            objArray1[index264].shopCustomPrice = new int?(60);
            objArray1[index264].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index265 = index264 + 1;
            objArray1[index265].SetDefaults(3826);
            objArray1[index265].shopCustomPrice = new int?(60);
            objArray1[index265].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index266 = index265 + 1;
            objArray1[index266].SetDefaults(3834);
            objArray1[index266].shopCustomPrice = new int?(60);
            objArray1[index266].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index267 = index266 + 1;
            objArray1[index267].SetDefaults(3831);
            objArray1[index267].shopCustomPrice = new int?(60);
            objArray1[index267].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
          }
          if (flag2)
          {
            int index268 = 4;
            objArray1[index268].SetDefaults(3800);
            objArray1[index268].shopCustomPrice = new int?(15);
            objArray1[index268].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index269 = index268 + 1;
            objArray1[index269].SetDefaults(3801);
            objArray1[index269].shopCustomPrice = new int?(15);
            objArray1[index269].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index270 = index269 + 1;
            objArray1[index270].SetDefaults(3802);
            objArray1[index270].shopCustomPrice = new int?(15);
            objArray1[index270].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index270 + 1;
            int index271 = 14;
            objArray1[index271].SetDefaults(3797);
            objArray1[index271].shopCustomPrice = new int?(15);
            objArray1[index271].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index272 = index271 + 1;
            objArray1[index272].SetDefaults(3798);
            objArray1[index272].shopCustomPrice = new int?(15);
            objArray1[index272].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index273 = index272 + 1;
            objArray1[index273].SetDefaults(3799);
            objArray1[index273].shopCustomPrice = new int?(15);
            objArray1[index273].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index273 + 1;
            int index274 = 24;
            objArray1[index274].SetDefaults(3803);
            objArray1[index274].shopCustomPrice = new int?(15);
            objArray1[index274].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index275 = index274 + 1;
            objArray1[index275].SetDefaults(3804);
            objArray1[index275].shopCustomPrice = new int?(15);
            objArray1[index275].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index276 = index275 + 1;
            objArray1[index276].SetDefaults(3805);
            objArray1[index276].shopCustomPrice = new int?(15);
            objArray1[index276].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index276 + 1;
            int index277 = 34;
            objArray1[index277].SetDefaults(3806);
            objArray1[index277].shopCustomPrice = new int?(15);
            objArray1[index277].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index278 = index277 + 1;
            objArray1[index278].SetDefaults(3807);
            objArray1[index278].shopCustomPrice = new int?(15);
            objArray1[index278].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index279 = index278 + 1;
            objArray1[index279].SetDefaults(3808);
            objArray1[index279].shopCustomPrice = new int?(15);
            objArray1[index279].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index279 + 1;
          }
          if (num103 != 0)
          {
            int index280 = 7;
            objArray1[index280].SetDefaults(3871);
            objArray1[index280].shopCustomPrice = new int?(50);
            objArray1[index280].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index281 = index280 + 1;
            objArray1[index281].SetDefaults(3872);
            objArray1[index281].shopCustomPrice = new int?(50);
            objArray1[index281].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index282 = index281 + 1;
            objArray1[index282].SetDefaults(3873);
            objArray1[index282].shopCustomPrice = new int?(50);
            objArray1[index282].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index282 + 1;
            int index283 = 17;
            objArray1[index283].SetDefaults(3874);
            objArray1[index283].shopCustomPrice = new int?(50);
            objArray1[index283].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index284 = index283 + 1;
            objArray1[index284].SetDefaults(3875);
            objArray1[index284].shopCustomPrice = new int?(50);
            objArray1[index284].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index285 = index284 + 1;
            objArray1[index285].SetDefaults(3876);
            objArray1[index285].shopCustomPrice = new int?(50);
            objArray1[index285].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index285 + 1;
            int index286 = 27;
            objArray1[index286].SetDefaults(3877);
            objArray1[index286].shopCustomPrice = new int?(50);
            objArray1[index286].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index287 = index286 + 1;
            objArray1[index287].SetDefaults(3878);
            objArray1[index287].shopCustomPrice = new int?(50);
            objArray1[index287].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index288 = index287 + 1;
            objArray1[index288].SetDefaults(3879);
            objArray1[index288].shopCustomPrice = new int?(50);
            objArray1[index288].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index288 + 1;
            int index289 = 37;
            objArray1[index289].SetDefaults(3880);
            objArray1[index289].shopCustomPrice = new int?(50);
            objArray1[index289].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index290 = index289 + 1;
            objArray1[index290].SetDefaults(3881);
            objArray1[index290].shopCustomPrice = new int?(50);
            objArray1[index290].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            int index291 = index290 + 1;
            objArray1[index291].SetDefaults(3882);
            objArray1[index291].shopCustomPrice = new int?(50);
            objArray1[index291].shopSpecialCurrency = CustomCurrencyID.DefenderMedals;
            num104 = index291 + 1;
          }
          index1 = num103 == 0 ? (!flag2 ? 4 : 30) : 39;
          break;
        case 22:
          Item[] objArray128 = objArray1;
          int index292 = index1;
          int num105 = index292 + 1;
          objArray128[index292].SetDefaults(4587);
          Item[] objArray129 = objArray1;
          int index293 = num105;
          int num106 = index293 + 1;
          objArray129[index293].SetDefaults(4590);
          Item[] objArray130 = objArray1;
          int index294 = num106;
          int num107 = index294 + 1;
          objArray130[index294].SetDefaults(4589);
          Item[] objArray131 = objArray1;
          int index295 = num107;
          int num108 = index295 + 1;
          objArray131[index295].SetDefaults(4588);
          Item[] objArray132 = objArray1;
          int index296 = num108;
          int num109 = index296 + 1;
          objArray132[index296].SetDefaults(4083);
          Item[] objArray133 = objArray1;
          int index297 = num109;
          int num110 = index297 + 1;
          objArray133[index297].SetDefaults(4084);
          Item[] objArray134 = objArray1;
          int index298 = num110;
          int num111 = index298 + 1;
          objArray134[index298].SetDefaults(4085);
          Item[] objArray135 = objArray1;
          int index299 = num111;
          int num112 = index299 + 1;
          objArray135[index299].SetDefaults(4086);
          Item[] objArray136 = objArray1;
          int index300 = num112;
          int num113 = index300 + 1;
          objArray136[index300].SetDefaults(4087);
          Item[] objArray137 = objArray1;
          int index301 = num113;
          int index302 = index301 + 1;
          objArray137[index301].SetDefaults(4088);
          int scoreAccumulated2 = Main.LocalPlayer.golferScoreAccumulated;
          if (scoreAccumulated2 > 500)
          {
            objArray1[index302].SetDefaults(4039);
            int index303 = index302 + 1;
            objArray1[index303].SetDefaults(4094);
            int index304 = index303 + 1;
            objArray1[index304].SetDefaults(4093);
            int index305 = index304 + 1;
            objArray1[index305].SetDefaults(4092);
            index302 = index305 + 1;
          }
          Item[] objArray138 = objArray1;
          int index306 = index302;
          int num114 = index306 + 1;
          objArray138[index306].SetDefaults(4089);
          Item[] objArray139 = objArray1;
          int index307 = num114;
          int num115 = index307 + 1;
          objArray139[index307].SetDefaults(3989);
          Item[] objArray140 = objArray1;
          int index308 = num115;
          int num116 = index308 + 1;
          objArray140[index308].SetDefaults(4095);
          Item[] objArray141 = objArray1;
          int index309 = num116;
          int num117 = index309 + 1;
          objArray141[index309].SetDefaults(4040);
          Item[] objArray142 = objArray1;
          int index310 = num117;
          int num118 = index310 + 1;
          objArray142[index310].SetDefaults(4319);
          Item[] objArray143 = objArray1;
          int index311 = num118;
          int index312 = index311 + 1;
          objArray143[index311].SetDefaults(4320);
          if (scoreAccumulated2 > 1000)
          {
            objArray1[index312].SetDefaults(4591);
            int index313 = index312 + 1;
            objArray1[index313].SetDefaults(4594);
            int index314 = index313 + 1;
            objArray1[index314].SetDefaults(4593);
            int index315 = index314 + 1;
            objArray1[index315].SetDefaults(4592);
            index312 = index315 + 1;
          }
          Item[] objArray144 = objArray1;
          int index316 = index312;
          int num119 = index316 + 1;
          objArray144[index316].SetDefaults(4135);
          Item[] objArray145 = objArray1;
          int index317 = num119;
          int num120 = index317 + 1;
          objArray145[index317].SetDefaults(4138);
          Item[] objArray146 = objArray1;
          int index318 = num120;
          int num121 = index318 + 1;
          objArray146[index318].SetDefaults(4136);
          Item[] objArray147 = objArray1;
          int index319 = num121;
          int num122 = index319 + 1;
          objArray147[index319].SetDefaults(4137);
          Item[] objArray148 = objArray1;
          int index320 = num122;
          index1 = index320 + 1;
          objArray148[index320].SetDefaults(4049);
          if (scoreAccumulated2 > 500)
          {
            objArray1[index1].SetDefaults(4265);
            ++index1;
          }
          if (scoreAccumulated2 > 2000)
          {
            objArray1[index1].SetDefaults(4595);
            int index321 = index1 + 1;
            objArray1[index321].SetDefaults(4598);
            int index322 = index321 + 1;
            objArray1[index322].SetDefaults(4597);
            int index323 = index322 + 1;
            objArray1[index323].SetDefaults(4596);
            index1 = index323 + 1;
            if (NPC.downedBoss3)
            {
              objArray1[index1].SetDefaults(4264);
              ++index1;
            }
          }
          if (scoreAccumulated2 > 500)
          {
            objArray1[index1].SetDefaults(4599);
            ++index1;
          }
          if (scoreAccumulated2 >= 1000)
          {
            objArray1[index1].SetDefaults(4600);
            ++index1;
          }
          if (scoreAccumulated2 >= 2000)
          {
            objArray1[index1].SetDefaults(4601);
            ++index1;
          }
          if (scoreAccumulated2 >= 2000)
          {
            switch (Main.moonPhase)
            {
              case 0:
              case 1:
                objArray1[index1].SetDefaults(4658);
                ++index1;
                break;
              case 2:
              case 3:
                objArray1[index1].SetDefaults(4659);
                ++index1;
                break;
              case 4:
              case 5:
                objArray1[index1].SetDefaults(4660);
                ++index1;
                break;
              case 6:
              case 7:
                objArray1[index1].SetDefaults(4661);
                ++index1;
                break;
            }
          }
          else
            break;
          break;
        case 23:
          BestiaryUnlockProgressReport bestiaryProgressReport = Main.GetBestiaryProgressReport();
          if (Chest.BestiaryGirl_IsFairyTorchAvailable())
            objArray1[index1++].SetDefaults(4776);
          Item[] objArray149 = objArray1;
          int index324 = index1;
          int num123 = index324 + 1;
          objArray149[index324].SetDefaults(4767);
          Item[] objArray150 = objArray1;
          int index325 = num123;
          int num124 = index325 + 1;
          objArray150[index325].SetDefaults(4759);
          if (Main.moonPhase == 0 && !Main.dayTime)
            objArray1[num124++].SetDefaults(5253);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.10000000149011612)
            objArray1[num124++].SetDefaults(4672);
          Item[] objArray151 = objArray1;
          int index326 = num124;
          index1 = index326 + 1;
          objArray151[index326].SetDefaults(4829);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.25)
            objArray1[index1++].SetDefaults(4830);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.44999998807907104)
            objArray1[index1++].SetDefaults(4910);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4871);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4907);
          if (NPC.downedTowerSolar)
            objArray1[index1++].SetDefaults(4677);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.10000000149011612)
            objArray1[index1++].SetDefaults(4676);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4762);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.25)
            objArray1[index1++].SetDefaults(4716);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4785);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4786);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896)
            objArray1[index1++].SetDefaults(4787);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.30000001192092896 && Main.hardMode)
            objArray1[index1++].SetDefaults(4788);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.34999999403953552)
            objArray1[index1++].SetDefaults(4763);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.40000000596046448)
            objArray1[index1++].SetDefaults(4955);
          if (Main.hardMode && Main.bloodMoon)
            objArray1[index1++].SetDefaults(4736);
          if (NPC.downedPlantBoss)
            objArray1[index1++].SetDefaults(4701);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4765);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4766);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(5285);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.5)
            objArray1[index1++].SetDefaults(4777);
          if ((double) bestiaryProgressReport.CompletionPercent >= 0.699999988079071)
            objArray1[index1++].SetDefaults(4735);
          if ((double) bestiaryProgressReport.CompletionPercent >= 1.0)
            objArray1[index1++].SetDefaults(4951);
          switch (Main.moonPhase)
          {
            case 0:
            case 1:
              Item[] objArray152 = objArray1;
              int index327 = index1;
              int num125 = index327 + 1;
              objArray152[index327].SetDefaults(4768);
              Item[] objArray153 = objArray1;
              int index328 = num125;
              index1 = index328 + 1;
              objArray153[index328].SetDefaults(4769);
              break;
            case 2:
            case 3:
              Item[] objArray154 = objArray1;
              int index329 = index1;
              int num126 = index329 + 1;
              objArray154[index329].SetDefaults(4770);
              Item[] objArray155 = objArray1;
              int index330 = num126;
              index1 = index330 + 1;
              objArray155[index330].SetDefaults(4771);
              break;
            case 4:
            case 5:
              Item[] objArray156 = objArray1;
              int index331 = index1;
              int num127 = index331 + 1;
              objArray156[index331].SetDefaults(4772);
              Item[] objArray157 = objArray1;
              int index332 = num127;
              index1 = index332 + 1;
              objArray157[index332].SetDefaults(4773);
              break;
            case 6:
            case 7:
              Item[] objArray158 = objArray1;
              int index333 = index1;
              int num128 = index333 + 1;
              objArray158[index333].SetDefaults(4560);
              Item[] objArray159 = objArray1;
              int index334 = num128;
              index1 = index334 + 1;
              objArray159[index334].SetDefaults(4775);
              break;
          }
          break;
        case 24:
          Item[] objArray160 = objArray1;
          int index335 = index1;
          int num129 = index335 + 1;
          objArray160[index335].SetDefaults(5071);
          Item[] objArray161 = objArray1;
          int index336 = num129;
          int num130 = index336 + 1;
          objArray161[index336].SetDefaults(5072);
          Item[] objArray162 = objArray1;
          int index337 = num130;
          int num131 = index337 + 1;
          objArray162[index337].SetDefaults(5073);
          Item[] objArray163 = objArray1;
          int index338 = num131;
          int num132 = index338 + 1;
          objArray163[index338].SetDefaults(5076);
          Item[] objArray164 = objArray1;
          int index339 = num132;
          int num133 = index339 + 1;
          objArray164[index339].SetDefaults(5077);
          Item[] objArray165 = objArray1;
          int index340 = num133;
          int num134 = index340 + 1;
          objArray165[index340].SetDefaults(5078);
          Item[] objArray166 = objArray1;
          int index341 = num134;
          int num135 = index341 + 1;
          objArray166[index341].SetDefaults(5079);
          Item[] objArray167 = objArray1;
          int index342 = num135;
          int num136 = index342 + 1;
          objArray167[index342].SetDefaults(5080);
          Item[] objArray168 = objArray1;
          int index343 = num136;
          int num137 = index343 + 1;
          objArray168[index343].SetDefaults(5081);
          Item[] objArray169 = objArray1;
          int index344 = num137;
          int num138 = index344 + 1;
          objArray169[index344].SetDefaults(5082);
          Item[] objArray170 = objArray1;
          int index345 = num138;
          int num139 = index345 + 1;
          objArray170[index345].SetDefaults(5083);
          Item[] objArray171 = objArray1;
          int index346 = num139;
          int num140 = index346 + 1;
          objArray171[index346].SetDefaults(5084);
          Item[] objArray172 = objArray1;
          int index347 = num140;
          int num141 = index347 + 1;
          objArray172[index347].SetDefaults(5085);
          Item[] objArray173 = objArray1;
          int index348 = num141;
          int num142 = index348 + 1;
          objArray173[index348].SetDefaults(5086);
          Item[] objArray174 = objArray1;
          int index349 = num142;
          int num143 = index349 + 1;
          objArray174[index349].SetDefaults(5087);
          Item[] objArray175 = objArray1;
          int index350 = num143;
          int num144 = index350 + 1;
          objArray175[index350].SetDefaults(5310);
          Item[] objArray176 = objArray1;
          int index351 = num144;
          int num145 = index351 + 1;
          objArray176[index351].SetDefaults(5222);
          Item[] objArray177 = objArray1;
          int index352 = num145;
          int num146 = index352 + 1;
          objArray177[index352].SetDefaults(5228);
          if (NPC.downedSlimeKing && NPC.downedQueenSlime)
            objArray1[num146++].SetDefaults(5266);
          if (Main.hardMode && NPC.downedMoonlord)
            objArray1[num146++].SetDefaults(5044);
          if (Main.tenthAnniversaryWorld)
          {
            Item[] objArray178 = objArray1;
            int index353 = num146;
            int num147 = index353 + 1;
            objArray178[index353].SetDefaults(1309);
            Item[] objArray179 = objArray1;
            int index354 = num147;
            int num148 = index354 + 1;
            objArray179[index354].SetDefaults(1859);
            Item[] objArray180 = objArray1;
            int index355 = num148;
            num146 = index355 + 1;
            objArray180[index355].SetDefaults(1358);
            if (Main.player[Main.myPlayer].ZoneDesert)
              objArray1[num146++].SetDefaults(857);
            if (Main.bloodMoon)
              objArray1[num146++].SetDefaults(4144);
            if (Main.hardMode && NPC.downedPirates)
            {
              if (Main.moonPhase == 0 || Main.moonPhase == 1)
                objArray1[num146++].SetDefaults(2584);
              if (Main.moonPhase == 2 || Main.moonPhase == 3)
                objArray1[num146++].SetDefaults(854);
              if (Main.moonPhase == 4 || Main.moonPhase == 5)
                objArray1[num146++].SetDefaults(855);
              if (Main.moonPhase == 6 || Main.moonPhase == 7)
                objArray1[num146++].SetDefaults(905);
            }
          }
          Item[] objArray181 = objArray1;
          int index356 = num146;
          index1 = index356 + 1;
          objArray181[index356].SetDefaults(5088);
          break;
        case 25:
          if (Main.xMas)
          {
            for (int Type = 1948; Type <= 1957 && index1 < 39; ++index1)
            {
              objArray1[index1].SetDefaults(Type);
              ++Type;
            }
          }
          for (int Type = 2158; Type <= 2160 && index1 < 39; ++index1)
          {
            objArray1[index1].SetDefaults(Type);
            ++Type;
          }
          for (int Type = 2008; Type <= 2014 && index1 < 39; ++index1)
          {
            objArray1[index1].SetDefaults(Type);
            ++Type;
          }
          if (!Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(1490);
            int index357 = index1 + 1;
            if (Main.moonPhase <= 1)
            {
              objArray1[index357].SetDefaults(1481);
              index1 = index357 + 1;
            }
            else if (Main.moonPhase <= 3)
            {
              objArray1[index357].SetDefaults(1482);
              index1 = index357 + 1;
            }
            else if (Main.moonPhase <= 5)
            {
              objArray1[index357].SetDefaults(1483);
              index1 = index357 + 1;
            }
            else
            {
              objArray1[index357].SetDefaults(1484);
              index1 = index357 + 1;
            }
          }
          if (Main.player[Main.myPlayer].ShoppingZone_Forest)
          {
            objArray1[index1].SetDefaults(5245);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneCrimson)
          {
            objArray1[index1].SetDefaults(1492);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneCorrupt)
          {
            objArray1[index1].SetDefaults(1488);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneHallow)
          {
            objArray1[index1].SetDefaults(1489);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneJungle)
          {
            objArray1[index1].SetDefaults(1486);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneSnow)
          {
            objArray1[index1].SetDefaults(1487);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneDesert)
          {
            objArray1[index1].SetDefaults(1491);
            ++index1;
          }
          if (Main.bloodMoon)
          {
            objArray1[index1].SetDefaults(1493);
            ++index1;
          }
          if (!Main.player[Main.myPlayer].ZoneGraveyard)
          {
            if ((double) Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.34999999403953552)
            {
              objArray1[index1].SetDefaults(1485);
              ++index1;
            }
            if ((double) Main.player[Main.myPlayer].position.Y / 16.0 < Main.worldSurface * 0.34999999403953552 && Main.hardMode)
            {
              objArray1[index1].SetDefaults(1494);
              ++index1;
            }
          }
          if (Main.IsItStorming)
          {
            objArray1[index1].SetDefaults(5251);
            ++index1;
          }
          if (Main.player[Main.myPlayer].ZoneGraveyard)
          {
            objArray1[index1].SetDefaults(4723);
            int index358 = index1 + 1;
            objArray1[index358].SetDefaults(4724);
            int index359 = index358 + 1;
            objArray1[index359].SetDefaults(4725);
            int index360 = index359 + 1;
            objArray1[index360].SetDefaults(4726);
            int index361 = index360 + 1;
            objArray1[index361].SetDefaults(4727);
            int index362 = index361 + 1;
            objArray1[index362].SetDefaults(5257);
            int index363 = index362 + 1;
            objArray1[index363].SetDefaults(4728);
            int index364 = index363 + 1;
            objArray1[index364].SetDefaults(4729);
            index1 = index364 + 1;
            break;
          }
          break;
      }
      int num149 = type == 19 ? 0 : (type != 20 ? 1 : 0);
      bool flag3 = TeleportPylonsSystem.DoesPositionHaveEnoughNPCs(2, Main.LocalPlayer.Center.ToTileCoordinates16());
      if (((num149 == 0 ? 0 : (flag1 ? 1 : (Main.remixWorld ? 1 : 0))) & (flag3 ? 1 : 0)) != 0 && !Main.player[Main.myPlayer].ZoneCorrupt && !Main.player[Main.myPlayer].ZoneCrimson)
      {
        if (!Main.player[Main.myPlayer].ZoneSnow && !Main.player[Main.myPlayer].ZoneDesert && !Main.player[Main.myPlayer].ZoneBeach && !Main.player[Main.myPlayer].ZoneJungle && !Main.player[Main.myPlayer].ZoneHallow && !Main.player[Main.myPlayer].ZoneGlowshroom)
        {
          if (Main.remixWorld)
          {
            if ((double) Main.player[Main.myPlayer].Center.Y / 16.0 > Main.rockLayer && (double) Main.player[Main.myPlayer].Center.Y / 16.0 < (double) (Main.maxTilesY - 350) && index1 < 39)
              objArray1[index1++].SetDefaults(4876);
          }
          else if ((double) Main.player[Main.myPlayer].Center.Y / 16.0 < Main.worldSurface && index1 < 39)
            objArray1[index1++].SetDefaults(4876);
        }
        if (Main.player[Main.myPlayer].ZoneSnow && index1 < 39)
          objArray1[index1++].SetDefaults(4920);
        if (Main.player[Main.myPlayer].ZoneDesert && index1 < 39)
          objArray1[index1++].SetDefaults(4919);
        if (Main.remixWorld)
        {
          if (!Main.player[Main.myPlayer].ZoneSnow && !Main.player[Main.myPlayer].ZoneDesert && !Main.player[Main.myPlayer].ZoneBeach && !Main.player[Main.myPlayer].ZoneJungle && !Main.player[Main.myPlayer].ZoneHallow && (double) Main.player[Main.myPlayer].Center.Y / 16.0 >= Main.worldSurface && index1 < 39)
            objArray1[index1++].SetDefaults(4917);
        }
        else if (!Main.player[Main.myPlayer].ZoneSnow && !Main.player[Main.myPlayer].ZoneDesert && !Main.player[Main.myPlayer].ZoneBeach && !Main.player[Main.myPlayer].ZoneJungle && !Main.player[Main.myPlayer].ZoneHallow && !Main.player[Main.myPlayer].ZoneGlowshroom && (double) Main.player[Main.myPlayer].Center.Y / 16.0 >= Main.worldSurface && index1 < 39)
          objArray1[index1++].SetDefaults(4917);
        bool flag4 = Main.player[Main.myPlayer].ZoneBeach && (double) Main.player[Main.myPlayer].position.Y < Main.worldSurface * 16.0;
        if (Main.remixWorld)
        {
          float num150 = Main.player[Main.myPlayer].position.X / 16f;
          float num151 = Main.player[Main.myPlayer].position.Y / 16f;
          flag4 = ((flag4 ? 1 : 0) | ((double) num150 < (double) Main.maxTilesX * 0.43 || (double) num150 > (double) Main.maxTilesX * 0.57 ? ((double) num151 <= Main.rockLayer ? 0 : ((double) num151 < (double) (Main.maxTilesY - 350) ? 1 : 0)) : 0)) != 0;
        }
        if (flag4 && index1 < 39)
          objArray1[index1++].SetDefaults(4918);
        if (Main.player[Main.myPlayer].ZoneJungle && index1 < 39)
          objArray1[index1++].SetDefaults(4875);
        if (Main.player[Main.myPlayer].ZoneHallow && index1 < 39)
          objArray1[index1++].SetDefaults(4916);
        if (Main.player[Main.myPlayer].ZoneGlowshroom && (!Main.remixWorld || (double) Main.player[Main.myPlayer].Center.Y / 16.0 < (double) (Main.maxTilesY - 200)) && index1 < 39)
          objArray1[index1++].SetDefaults(4921);
      }
      for (int index365 = 0; index365 < index1; ++index365)
        objArray1[index365].isAShopItem = true;
    }

    private static bool BestiaryGirl_IsFairyTorchAvailable() => Chest.DidDiscoverBestiaryEntry(585) && Chest.DidDiscoverBestiaryEntry(584) && Chest.DidDiscoverBestiaryEntry(583);

    private static bool DidDiscoverBestiaryEntry(int npcId) => Main.BestiaryDB.FindEntryByNPCID(npcId).UIInfoProvider.GetEntryUICollectionInfo().UnlockState > BestiaryEntryUnlockState.NotKnownAtAll_0;

    public static void AskForChestToEatItem(Vector2 worldPosition, int duration)
    {
      Point tileCoordinates = worldPosition.ToTileCoordinates();
      int chest = Chest.FindChest(tileCoordinates.X, tileCoordinates.Y);
      if (chest == -1)
        return;
      Main.chest[chest].eatingAnimationTime = duration;
    }

    public static void UpdateChestFrames()
    {
      int num = 8000;
      Chest._chestInUse.Clear();
      for (int index = 0; index < (int) byte.MaxValue; ++index)
      {
        if (Main.player[index].active && Main.player[index].chest >= 0 && Main.player[index].chest < num)
          Chest._chestInUse.Add(Main.player[index].chest);
      }
      for (int index = 0; index < num; ++index)
      {
        Chest chest = Main.chest[index];
        if (chest != null)
        {
          if (Chest._chestInUse.Contains(index))
            ++chest.frameCounter;
          else
            --chest.frameCounter;
          if (chest.eatingAnimationTime == 9 && chest.frame == 1)
            SoundEngine.PlaySound(7, new Vector2((float) (chest.x * 16 + 16), (float) (chest.y * 16 + 16)));
          if (chest.eatingAnimationTime > 0)
            --chest.eatingAnimationTime;
          if (chest.frameCounter < chest.eatingAnimationTime)
            chest.frameCounter = chest.eatingAnimationTime;
          if (chest.frameCounter < 0)
            chest.frameCounter = 0;
          if (chest.frameCounter > 10)
            chest.frameCounter = 10;
          chest.frame = chest.frameCounter != 0 ? (chest.frameCounter != 10 ? 1 : 2) : 0;
        }
      }
    }

    public void FixLoadedData()
    {
      foreach (Item obj in this.item)
        obj.FixAgainstExploit();
    }
  }
}
