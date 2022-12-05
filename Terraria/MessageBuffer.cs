// Decompiled with JetBrains decompiler
// Type: Terraria.MessageBuffer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Creative;
using Terraria.GameContent.Events;
using Terraria.GameContent.Golf;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Net;
using Terraria.Testing;
using Terraria.UI;

namespace Terraria
{
  public class MessageBuffer
  {
    public const int readBufferMax = 131070;
    public const int writeBufferMax = 131070;
    public bool broadcast;
    public byte[] readBuffer = new byte[131070];
    public byte[] writeBuffer = new byte[131070];
    public bool writeLocked;
    public int messageLength;
    public int totalData;
    public int whoAmI;
    public int spamCount;
    public int maxSpam;
    public bool checkBytes;
    public MemoryStream readerStream;
    public MemoryStream writerStream;
    public BinaryReader reader;
    public BinaryWriter writer;
    public PacketHistory History = new PacketHistory();
    private float[] _temporaryProjectileAI = new float[Projectile.maxAI];
    private float[] _temporaryNPCAI = new float[NPC.maxAI];

    public static event TileChangeReceivedEvent OnTileChangeReceived;

    public void Reset()
    {
      Array.Clear((Array) this.readBuffer, 0, this.readBuffer.Length);
      Array.Clear((Array) this.writeBuffer, 0, this.writeBuffer.Length);
      this.writeLocked = false;
      this.messageLength = 0;
      this.totalData = 0;
      this.spamCount = 0;
      this.broadcast = false;
      this.checkBytes = false;
      this.ResetReader();
      this.ResetWriter();
    }

    public void ResetReader()
    {
      if (this.readerStream != null)
        this.readerStream.Close();
      this.readerStream = new MemoryStream(this.readBuffer);
      this.reader = new BinaryReader((Stream) this.readerStream);
    }

    public void ResetWriter()
    {
      if (this.writerStream != null)
        this.writerStream.Close();
      this.writerStream = new MemoryStream(this.writeBuffer);
      this.writer = new BinaryWriter((Stream) this.writerStream);
    }

    private float[] ReUseTemporaryProjectileAI()
    {
      for (int index = 0; index < this._temporaryProjectileAI.Length; ++index)
        this._temporaryProjectileAI[index] = 0.0f;
      return this._temporaryProjectileAI;
    }

    private float[] ReUseTemporaryNPCAI()
    {
      for (int index = 0; index < this._temporaryNPCAI.Length; ++index)
        this._temporaryNPCAI[index] = 0.0f;
      return this._temporaryNPCAI;
    }

    public void GetData(int start, int length, out int messageType)
    {
      if (this.whoAmI < 256)
        Netplay.Clients[this.whoAmI].TimeOutTimer = 0;
      else
        Netplay.Connection.TimeOutTimer = 0;
      int num1 = start + 1;
      byte num2 = this.readBuffer[start];
      messageType = (int) num2;
      if ((int) num2 >= (int) MessageID.Count)
        return;
      Main.ActiveNetDiagnosticsUI.CountReadMessage((int) num2, length);
      if (Main.netMode == 1 && Netplay.Connection.StatusMax > 0)
        ++Netplay.Connection.StatusCount;
      if (Main.verboseNetplay)
      {
        int num3 = start;
        while (num3 < start + length)
          ++num3;
        for (int index = start; index < start + length; ++index)
        {
          int num4 = (int) this.readBuffer[index];
        }
      }
      if (Main.netMode == 2 && num2 != (byte) 38 && Netplay.Clients[this.whoAmI].State == -1)
      {
        NetMessage.TrySendData(2, this.whoAmI, text: Lang.mp[1].ToNetworkText());
      }
      else
      {
        if (Main.netMode == 2)
        {
          if (Netplay.Clients[this.whoAmI].State < 10 && num2 > (byte) 12 && num2 != (byte) 93 && num2 != (byte) 16 && num2 != (byte) 42 && num2 != (byte) 50 && num2 != (byte) 38 && num2 != (byte) 68 && num2 != (byte) 147)
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
          if (Netplay.Clients[this.whoAmI].State == 0 && num2 != (byte) 1)
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
        }
        if (this.reader == null)
          this.ResetReader();
        this.reader.BaseStream.Position = (long) num1;
        switch (num2)
        {
          case 1:
            if (Main.netMode != 2)
              break;
            if (Main.dedServ && Netplay.IsBanned(Netplay.Clients[this.whoAmI].Socket.GetRemoteAddress()))
            {
              NetMessage.TrySendData(2, this.whoAmI, text: Lang.mp[3].ToNetworkText());
              break;
            }
            if (Netplay.Clients[this.whoAmI].State != 0)
              break;
            if (this.reader.ReadString() == "Terraria" + (object) 279)
            {
              if (string.IsNullOrEmpty(Netplay.ServerPassword))
              {
                Netplay.Clients[this.whoAmI].State = 1;
                NetMessage.TrySendData(3, this.whoAmI);
                break;
              }
              Netplay.Clients[this.whoAmI].State = -1;
              NetMessage.TrySendData(37, this.whoAmI);
              break;
            }
            NetMessage.TrySendData(2, this.whoAmI, text: Lang.mp[4].ToNetworkText());
            break;
          case 2:
            if (Main.netMode != 1)
              break;
            Netplay.Disconnect = true;
            Main.statusText = NetworkText.Deserialize(this.reader).ToString();
            break;
          case 3:
            if (Main.netMode != 1)
              break;
            if (Netplay.Connection.State == 1)
              Netplay.Connection.State = 2;
            int index1 = (int) this.reader.ReadByte();
            bool flag1 = this.reader.ReadBoolean();
            Netplay.Connection.ServerSpecialFlags[2] = flag1;
            if (index1 != Main.myPlayer)
            {
              Main.player[index1] = Main.ActivePlayerFileData.Player;
              Main.player[Main.myPlayer] = new Player();
            }
            Main.player[index1].whoAmI = index1;
            Main.myPlayer = index1;
            Player player1 = Main.player[index1];
            NetMessage.TrySendData(4, number: index1);
            NetMessage.TrySendData(68, number: index1);
            NetMessage.TrySendData(16, number: index1);
            NetMessage.TrySendData(42, number: index1);
            NetMessage.TrySendData(50, number: index1);
            NetMessage.TrySendData(147, number: index1, number2: ((float) player1.CurrentLoadoutIndex));
            for (int index2 = 0; index2 < 59; ++index2)
              NetMessage.TrySendData(5, number: index1, number2: ((float) (PlayerItemSlotID.Inventory0 + index2)), number3: ((float) player1.inventory[index2].prefix));
            MessageBuffer.TrySendingItemArray(index1, player1.armor, PlayerItemSlotID.Armor0);
            MessageBuffer.TrySendingItemArray(index1, player1.dye, PlayerItemSlotID.Dye0);
            MessageBuffer.TrySendingItemArray(index1, player1.miscEquips, PlayerItemSlotID.Misc0);
            MessageBuffer.TrySendingItemArray(index1, player1.miscDyes, PlayerItemSlotID.MiscDye0);
            MessageBuffer.TrySendingItemArray(index1, player1.bank.item, PlayerItemSlotID.Bank1_0);
            MessageBuffer.TrySendingItemArray(index1, player1.bank2.item, PlayerItemSlotID.Bank2_0);
            NetMessage.TrySendData(5, number: index1, number2: ((float) PlayerItemSlotID.TrashItem), number3: ((float) player1.trashItem.prefix));
            MessageBuffer.TrySendingItemArray(index1, player1.bank3.item, PlayerItemSlotID.Bank3_0);
            MessageBuffer.TrySendingItemArray(index1, player1.bank4.item, PlayerItemSlotID.Bank4_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[0].Armor, PlayerItemSlotID.Loadout1_Armor_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[0].Dye, PlayerItemSlotID.Loadout1_Dye_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[1].Armor, PlayerItemSlotID.Loadout2_Armor_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[1].Dye, PlayerItemSlotID.Loadout2_Dye_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[2].Armor, PlayerItemSlotID.Loadout3_Armor_0);
            MessageBuffer.TrySendingItemArray(index1, player1.Loadouts[2].Dye, PlayerItemSlotID.Loadout3_Dye_0);
            NetMessage.TrySendData(6);
            if (Netplay.Connection.State != 2)
              break;
            Netplay.Connection.State = 3;
            break;
          case 4:
            int number1 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number1 = this.whoAmI;
            if (number1 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            Player player2 = Main.player[number1];
            player2.whoAmI = number1;
            player2.skinVariant = (int) this.reader.ReadByte();
            player2.skinVariant = (int) MathHelper.Clamp((float) player2.skinVariant, 0.0f, (float) (PlayerVariantID.Count - 1));
            player2.hair = (int) this.reader.ReadByte();
            if (player2.hair >= 165)
              player2.hair = 0;
            player2.name = this.reader.ReadString().Trim().Trim();
            player2.hairDye = this.reader.ReadByte();
            MessageBuffer.ReadAccessoryVisibility(this.reader, player2.hideVisibleAccessory);
            player2.hideMisc = (BitsByte) this.reader.ReadByte();
            player2.hairColor = this.reader.ReadRGB();
            player2.skinColor = this.reader.ReadRGB();
            player2.eyeColor = this.reader.ReadRGB();
            player2.shirtColor = this.reader.ReadRGB();
            player2.underShirtColor = this.reader.ReadRGB();
            player2.pantsColor = this.reader.ReadRGB();
            player2.shoeColor = this.reader.ReadRGB();
            BitsByte bitsByte1 = (BitsByte) this.reader.ReadByte();
            player2.difficulty = (byte) 0;
            if (bitsByte1[0])
              player2.difficulty = (byte) 1;
            if (bitsByte1[1])
              player2.difficulty = (byte) 2;
            if (bitsByte1[3])
              player2.difficulty = (byte) 3;
            if (player2.difficulty > (byte) 3)
              player2.difficulty = (byte) 3;
            player2.extraAccessory = bitsByte1[2];
            BitsByte bitsByte2 = (BitsByte) this.reader.ReadByte();
            player2.UsingBiomeTorches = bitsByte2[0];
            player2.happyFunTorchTime = bitsByte2[1];
            player2.unlockedBiomeTorches = bitsByte2[2];
            player2.unlockedSuperCart = bitsByte2[3];
            player2.enabledSuperCart = bitsByte2[4];
            BitsByte bitsByte3 = (BitsByte) this.reader.ReadByte();
            player2.usedAegisCrystal = bitsByte3[0];
            player2.usedAegisFruit = bitsByte3[1];
            player2.usedArcaneCrystal = bitsByte3[2];
            player2.usedGalaxyPearl = bitsByte3[3];
            player2.usedGummyWorm = bitsByte3[4];
            player2.usedAmbrosia = bitsByte3[5];
            player2.ateArtisanBread = bitsByte3[6];
            if (Main.netMode != 2)
              break;
            bool flag2 = false;
            if (Netplay.Clients[this.whoAmI].State < 10)
            {
              for (int index3 = 0; index3 < (int) byte.MaxValue; ++index3)
              {
                if (index3 != number1 && player2.name == Main.player[index3].name && Netplay.Clients[index3].IsActive)
                  flag2 = true;
              }
            }
            if (flag2)
            {
              NetMessage.TrySendData(2, this.whoAmI, text: NetworkText.FromKey(Lang.mp[5].Key, (object) player2.name));
              break;
            }
            if (player2.name.Length > Player.nameLen)
            {
              NetMessage.TrySendData(2, this.whoAmI, text: NetworkText.FromKey("Net.NameTooLong"));
              break;
            }
            if (player2.name == "")
            {
              NetMessage.TrySendData(2, this.whoAmI, text: NetworkText.FromKey("Net.EmptyName"));
              break;
            }
            if (player2.difficulty == (byte) 3 && !Main.GameModeInfo.IsJourneyMode)
            {
              NetMessage.TrySendData(2, this.whoAmI, text: NetworkText.FromKey("Net.PlayerIsCreativeAndWorldIsNotCreative"));
              break;
            }
            if (player2.difficulty != (byte) 3 && Main.GameModeInfo.IsJourneyMode)
            {
              NetMessage.TrySendData(2, this.whoAmI, text: NetworkText.FromKey("Net.PlayerIsNotCreativeAndWorldIsCreative"));
              break;
            }
            Netplay.Clients[this.whoAmI].Name = player2.name;
            Netplay.Clients[this.whoAmI].Name = player2.name;
            NetMessage.TrySendData(4, ignoreClient: this.whoAmI, number: number1);
            break;
          case 5:
            int number2 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number2 = this.whoAmI;
            if (number2 == Main.myPlayer && !Main.ServerSideCharacter && !Main.player[number2].HasLockedInventory())
              break;
            Player player3 = Main.player[number2];
            lock (player3)
            {
              int index4 = (int) this.reader.ReadInt16();
              int num5 = (int) this.reader.ReadInt16();
              int num6 = (int) this.reader.ReadByte();
              int type1 = (int) this.reader.ReadInt16();
              Item[] objArray1 = (Item[]) null;
              Item[] objArray2 = (Item[]) null;
              int index5 = 0;
              bool flag3 = false;
              Player clientPlayer = Main.clientPlayer;
              if (index4 >= PlayerItemSlotID.Loadout3_Dye_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout3_Dye_0;
                objArray1 = player3.Loadouts[2].Dye;
                objArray2 = clientPlayer.Loadouts[2].Dye;
              }
              else if (index4 >= PlayerItemSlotID.Loadout3_Armor_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout3_Armor_0;
                objArray1 = player3.Loadouts[2].Armor;
                objArray2 = clientPlayer.Loadouts[2].Armor;
              }
              else if (index4 >= PlayerItemSlotID.Loadout2_Dye_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout2_Dye_0;
                objArray1 = player3.Loadouts[1].Dye;
                objArray2 = clientPlayer.Loadouts[1].Dye;
              }
              else if (index4 >= PlayerItemSlotID.Loadout2_Armor_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout2_Armor_0;
                objArray1 = player3.Loadouts[1].Armor;
                objArray2 = clientPlayer.Loadouts[1].Armor;
              }
              else if (index4 >= PlayerItemSlotID.Loadout1_Dye_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout1_Dye_0;
                objArray1 = player3.Loadouts[0].Dye;
                objArray2 = clientPlayer.Loadouts[0].Dye;
              }
              else if (index4 >= PlayerItemSlotID.Loadout1_Armor_0)
              {
                index5 = index4 - PlayerItemSlotID.Loadout1_Armor_0;
                objArray1 = player3.Loadouts[0].Armor;
                objArray2 = clientPlayer.Loadouts[0].Armor;
              }
              else if (index4 >= PlayerItemSlotID.Bank4_0)
              {
                index5 = index4 - PlayerItemSlotID.Bank4_0;
                objArray1 = player3.bank4.item;
                objArray2 = clientPlayer.bank4.item;
                if (Main.netMode == 1 && player3.disableVoidBag == index5)
                {
                  player3.disableVoidBag = -1;
                  Recipe.FindRecipes(true);
                }
              }
              else if (index4 >= PlayerItemSlotID.Bank3_0)
              {
                index5 = index4 - PlayerItemSlotID.Bank3_0;
                objArray1 = player3.bank3.item;
                objArray2 = clientPlayer.bank3.item;
              }
              else if (index4 >= PlayerItemSlotID.TrashItem)
                flag3 = true;
              else if (index4 >= PlayerItemSlotID.Bank2_0)
              {
                index5 = index4 - PlayerItemSlotID.Bank2_0;
                objArray1 = player3.bank2.item;
                objArray2 = clientPlayer.bank2.item;
              }
              else if (index4 >= PlayerItemSlotID.Bank1_0)
              {
                index5 = index4 - PlayerItemSlotID.Bank1_0;
                objArray1 = player3.bank.item;
                objArray2 = clientPlayer.bank.item;
              }
              else if (index4 >= PlayerItemSlotID.MiscDye0)
              {
                index5 = index4 - PlayerItemSlotID.MiscDye0;
                objArray1 = player3.miscDyes;
                objArray2 = clientPlayer.miscDyes;
              }
              else if (index4 >= PlayerItemSlotID.Misc0)
              {
                index5 = index4 - PlayerItemSlotID.Misc0;
                objArray1 = player3.miscEquips;
                objArray2 = clientPlayer.miscEquips;
              }
              else if (index4 >= PlayerItemSlotID.Dye0)
              {
                index5 = index4 - PlayerItemSlotID.Dye0;
                objArray1 = player3.dye;
                objArray2 = clientPlayer.dye;
              }
              else if (index4 >= PlayerItemSlotID.Armor0)
              {
                index5 = index4 - PlayerItemSlotID.Armor0;
                objArray1 = player3.armor;
                objArray2 = clientPlayer.armor;
              }
              else
              {
                index5 = index4 - PlayerItemSlotID.Inventory0;
                objArray1 = player3.inventory;
                objArray2 = clientPlayer.inventory;
              }
              if (flag3)
              {
                player3.trashItem = new Item();
                player3.trashItem.netDefaults(type1);
                player3.trashItem.stack = num5;
                player3.trashItem.Prefix(num6);
                if (number2 == Main.myPlayer && !Main.ServerSideCharacter)
                  clientPlayer.trashItem = player3.trashItem.Clone();
              }
              else if (index4 <= 58)
              {
                int type2 = objArray1[index5].type;
                int stack = objArray1[index5].stack;
                objArray1[index5] = new Item();
                objArray1[index5].netDefaults(type1);
                objArray1[index5].stack = num5;
                objArray1[index5].Prefix(num6);
                if (number2 == Main.myPlayer && !Main.ServerSideCharacter)
                  objArray2[index5] = objArray1[index5].Clone();
                if (number2 == Main.myPlayer && index5 == 58)
                  Main.mouseItem = objArray1[index5].Clone();
                if (number2 == Main.myPlayer && Main.netMode == 1)
                {
                  Main.player[number2].inventoryChestStack[index4] = false;
                  if (objArray1[index5].stack != stack || objArray1[index5].type != type2)
                    Recipe.FindRecipes(true);
                }
              }
              else
              {
                objArray1[index5] = new Item();
                objArray1[index5].netDefaults(type1);
                objArray1[index5].stack = num5;
                objArray1[index5].Prefix(num6);
                if (number2 == Main.myPlayer && !Main.ServerSideCharacter)
                  objArray2[index5] = objArray1[index5].Clone();
              }
              bool[] canRelay = PlayerItemSlotID.CanRelay;
              if (Main.netMode != 2 || number2 != this.whoAmI || !canRelay.IndexInRange<bool>(index4) || !canRelay[index4])
                break;
              NetMessage.TrySendData(5, ignoreClient: this.whoAmI, number: number2, number2: ((float) index4), number3: ((float) num6));
              break;
            }
          case 6:
            if (Main.netMode != 2)
              break;
            if (Netplay.Clients[this.whoAmI].State == 1)
              Netplay.Clients[this.whoAmI].State = 2;
            NetMessage.TrySendData(7, this.whoAmI);
            Main.SyncAnInvasion(this.whoAmI);
            break;
          case 7:
            if (Main.netMode != 1)
              break;
            Main.time = (double) this.reader.ReadInt32();
            BitsByte bitsByte4 = (BitsByte) this.reader.ReadByte();
            Main.dayTime = bitsByte4[0];
            Main.bloodMoon = bitsByte4[1];
            Main.eclipse = bitsByte4[2];
            Main.moonPhase = (int) this.reader.ReadByte();
            Main.maxTilesX = (int) this.reader.ReadInt16();
            Main.maxTilesY = (int) this.reader.ReadInt16();
            Main.spawnTileX = (int) this.reader.ReadInt16();
            Main.spawnTileY = (int) this.reader.ReadInt16();
            Main.worldSurface = (double) this.reader.ReadInt16();
            Main.rockLayer = (double) this.reader.ReadInt16();
            Main.worldID = this.reader.ReadInt32();
            Main.worldName = this.reader.ReadString();
            Main.GameMode = (int) this.reader.ReadByte();
            Main.ActiveWorldFileData.UniqueId = new Guid(this.reader.ReadBytes(16));
            Main.ActiveWorldFileData.WorldGeneratorVersion = this.reader.ReadUInt64();
            Main.moonType = (int) this.reader.ReadByte();
            WorldGen.setBG(0, (int) this.reader.ReadByte());
            WorldGen.setBG(10, (int) this.reader.ReadByte());
            WorldGen.setBG(11, (int) this.reader.ReadByte());
            WorldGen.setBG(12, (int) this.reader.ReadByte());
            WorldGen.setBG(1, (int) this.reader.ReadByte());
            WorldGen.setBG(2, (int) this.reader.ReadByte());
            WorldGen.setBG(3, (int) this.reader.ReadByte());
            WorldGen.setBG(4, (int) this.reader.ReadByte());
            WorldGen.setBG(5, (int) this.reader.ReadByte());
            WorldGen.setBG(6, (int) this.reader.ReadByte());
            WorldGen.setBG(7, (int) this.reader.ReadByte());
            WorldGen.setBG(8, (int) this.reader.ReadByte());
            WorldGen.setBG(9, (int) this.reader.ReadByte());
            Main.iceBackStyle = (int) this.reader.ReadByte();
            Main.jungleBackStyle = (int) this.reader.ReadByte();
            Main.hellBackStyle = (int) this.reader.ReadByte();
            Main.windSpeedTarget = this.reader.ReadSingle();
            Main.numClouds = (int) this.reader.ReadByte();
            for (int index6 = 0; index6 < 3; ++index6)
              Main.treeX[index6] = this.reader.ReadInt32();
            for (int index7 = 0; index7 < 4; ++index7)
              Main.treeStyle[index7] = (int) this.reader.ReadByte();
            for (int index8 = 0; index8 < 3; ++index8)
              Main.caveBackX[index8] = this.reader.ReadInt32();
            for (int index9 = 0; index9 < 4; ++index9)
              Main.caveBackStyle[index9] = (int) this.reader.ReadByte();
            WorldGen.TreeTops.SyncReceive(this.reader);
            WorldGen.BackgroundsCache.UpdateCache();
            Main.maxRaining = this.reader.ReadSingle();
            Main.raining = (double) Main.maxRaining > 0.0;
            BitsByte bitsByte5 = (BitsByte) this.reader.ReadByte();
            WorldGen.shadowOrbSmashed = bitsByte5[0];
            NPC.downedBoss1 = bitsByte5[1];
            NPC.downedBoss2 = bitsByte5[2];
            NPC.downedBoss3 = bitsByte5[3];
            Main.hardMode = bitsByte5[4];
            NPC.downedClown = bitsByte5[5];
            Main.ServerSideCharacter = bitsByte5[6];
            NPC.downedPlantBoss = bitsByte5[7];
            if (Main.ServerSideCharacter)
              Main.ActivePlayerFileData.MarkAsServerSide();
            BitsByte bitsByte6 = (BitsByte) this.reader.ReadByte();
            NPC.downedMechBoss1 = bitsByte6[0];
            NPC.downedMechBoss2 = bitsByte6[1];
            NPC.downedMechBoss3 = bitsByte6[2];
            NPC.downedMechBossAny = bitsByte6[3];
            Main.cloudBGActive = bitsByte6[4] ? 1f : 0.0f;
            WorldGen.crimson = bitsByte6[5];
            Main.pumpkinMoon = bitsByte6[6];
            Main.snowMoon = bitsByte6[7];
            BitsByte bitsByte7 = (BitsByte) this.reader.ReadByte();
            Main.fastForwardTimeToDawn = bitsByte7[1];
            Main.UpdateTimeRate();
            int num7 = bitsByte7[2] ? 1 : 0;
            NPC.downedSlimeKing = bitsByte7[3];
            NPC.downedQueenBee = bitsByte7[4];
            NPC.downedFishron = bitsByte7[5];
            NPC.downedMartians = bitsByte7[6];
            NPC.downedAncientCultist = bitsByte7[7];
            BitsByte bitsByte8 = (BitsByte) this.reader.ReadByte();
            NPC.downedMoonlord = bitsByte8[0];
            NPC.downedHalloweenKing = bitsByte8[1];
            NPC.downedHalloweenTree = bitsByte8[2];
            NPC.downedChristmasIceQueen = bitsByte8[3];
            NPC.downedChristmasSantank = bitsByte8[4];
            NPC.downedChristmasTree = bitsByte8[5];
            NPC.downedGolemBoss = bitsByte8[6];
            BirthdayParty.ManualParty = bitsByte8[7];
            BitsByte bitsByte9 = (BitsByte) this.reader.ReadByte();
            NPC.downedPirates = bitsByte9[0];
            NPC.downedFrost = bitsByte9[1];
            NPC.downedGoblins = bitsByte9[2];
            Sandstorm.Happening = bitsByte9[3];
            DD2Event.Ongoing = bitsByte9[4];
            DD2Event.DownedInvasionT1 = bitsByte9[5];
            DD2Event.DownedInvasionT2 = bitsByte9[6];
            DD2Event.DownedInvasionT3 = bitsByte9[7];
            BitsByte bitsByte10 = (BitsByte) this.reader.ReadByte();
            NPC.combatBookWasUsed = bitsByte10[0];
            LanternNight.ManualLanterns = bitsByte10[1];
            NPC.downedTowerSolar = bitsByte10[2];
            NPC.downedTowerVortex = bitsByte10[3];
            NPC.downedTowerNebula = bitsByte10[4];
            NPC.downedTowerStardust = bitsByte10[5];
            Main.forceHalloweenForToday = bitsByte10[6];
            Main.forceXMasForToday = bitsByte10[7];
            BitsByte bitsByte11 = (BitsByte) this.reader.ReadByte();
            NPC.boughtCat = bitsByte11[0];
            NPC.boughtDog = bitsByte11[1];
            NPC.boughtBunny = bitsByte11[2];
            NPC.freeCake = bitsByte11[3];
            Main.drunkWorld = bitsByte11[4];
            NPC.downedEmpressOfLight = bitsByte11[5];
            NPC.downedQueenSlime = bitsByte11[6];
            Main.getGoodWorld = bitsByte11[7];
            BitsByte bitsByte12 = (BitsByte) this.reader.ReadByte();
            Main.tenthAnniversaryWorld = bitsByte12[0];
            Main.dontStarveWorld = bitsByte12[1];
            NPC.downedDeerclops = bitsByte12[2];
            Main.notTheBeesWorld = bitsByte12[3];
            Main.remixWorld = bitsByte12[4];
            NPC.unlockedSlimeBlueSpawn = bitsByte12[5];
            NPC.combatBookVolumeTwoWasUsed = bitsByte12[6];
            NPC.peddlersSatchelWasUsed = bitsByte12[7];
            BitsByte bitsByte13 = (BitsByte) this.reader.ReadByte();
            NPC.unlockedSlimeGreenSpawn = bitsByte13[0];
            NPC.unlockedSlimeOldSpawn = bitsByte13[1];
            NPC.unlockedSlimePurpleSpawn = bitsByte13[2];
            NPC.unlockedSlimeRainbowSpawn = bitsByte13[3];
            NPC.unlockedSlimeRedSpawn = bitsByte13[4];
            NPC.unlockedSlimeYellowSpawn = bitsByte13[5];
            NPC.unlockedSlimeCopperSpawn = bitsByte13[6];
            Main.fastForwardTimeToDusk = bitsByte13[7];
            BitsByte bitsByte14 = (BitsByte) this.reader.ReadByte();
            Main.noTrapsWorld = bitsByte14[0];
            Main.zenithWorld = bitsByte14[1];
            NPC.unlockedTruffleSpawn = bitsByte14[2];
            Main.sundialCooldown = (int) this.reader.ReadByte();
            Main.moondialCooldown = (int) this.reader.ReadByte();
            WorldGen.SavedOreTiers.Copper = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Iron = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Silver = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Gold = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Cobalt = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Mythril = (int) this.reader.ReadInt16();
            WorldGen.SavedOreTiers.Adamantite = (int) this.reader.ReadInt16();
            if (num7 != 0)
              Main.StartSlimeRain();
            else
              Main.StopSlimeRain();
            Main.invasionType = (int) this.reader.ReadSByte();
            Main.LobbyId = this.reader.ReadUInt64();
            Sandstorm.IntendedSeverity = this.reader.ReadSingle();
            if (Netplay.Connection.State == 3)
            {
              Main.windSpeedCurrent = Main.windSpeedTarget;
              Netplay.Connection.State = 4;
            }
            Main.checkHalloween();
            Main.checkXMas();
            break;
          case 8:
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(7, this.whoAmI);
            int x1 = this.reader.ReadInt32();
            int y1 = this.reader.ReadInt32();
            bool flag4 = true;
            if (x1 == -1 || y1 == -1)
              flag4 = false;
            else if (x1 < 10 || x1 > Main.maxTilesX - 10)
              flag4 = false;
            else if (y1 < 10 || y1 > Main.maxTilesY - 10)
              flag4 = false;
            int num8 = Netplay.GetSectionX(Main.spawnTileX) - 2;
            int num9 = Netplay.GetSectionY(Main.spawnTileY) - 1;
            int num10 = num8 + 5;
            int num11 = num9 + 3;
            if (num8 < 0)
              num8 = 0;
            if (num10 >= Main.maxSectionsX)
              num10 = Main.maxSectionsX;
            if (num9 < 0)
              num9 = 0;
            if (num11 >= Main.maxSectionsY)
              num11 = Main.maxSectionsY;
            int num12 = (num10 - num8) * (num11 - num9);
            List<Point> dontInclude = new List<Point>();
            for (int x2 = num8; x2 < num10; ++x2)
            {
              for (int y2 = num9; y2 < num11; ++y2)
                dontInclude.Add(new Point(x2, y2));
            }
            int num13 = -1;
            int num14 = -1;
            if (flag4)
            {
              x1 = Netplay.GetSectionX(x1) - 2;
              y1 = Netplay.GetSectionY(y1) - 1;
              num13 = x1 + 5;
              num14 = y1 + 3;
              if (x1 < 0)
                x1 = 0;
              if (num13 >= Main.maxSectionsX)
                num13 = Main.maxSectionsX - 1;
              if (y1 < 0)
                y1 = 0;
              if (num14 >= Main.maxSectionsY)
                num14 = Main.maxSectionsY - 1;
              for (int x3 = x1; x3 <= num13; ++x3)
              {
                for (int y3 = y1; y3 <= num14; ++y3)
                {
                  if (x3 < num8 || x3 >= num10 || y3 < num9 || y3 >= num11)
                  {
                    dontInclude.Add(new Point(x3, y3));
                    ++num12;
                  }
                }
              }
            }
            List<Point> portalSections;
            PortalHelper.SyncPortalsOnPlayerJoin(this.whoAmI, 1, dontInclude, out portalSections);
            int number3 = num12 + portalSections.Count;
            if (Netplay.Clients[this.whoAmI].State == 2)
              Netplay.Clients[this.whoAmI].State = 3;
            NetMessage.TrySendData(9, this.whoAmI, text: Lang.inter[44].ToNetworkText(), number: number3);
            Netplay.Clients[this.whoAmI].StatusText2 = Language.GetTextValue("Net.IsReceivingTileData");
            Netplay.Clients[this.whoAmI].StatusMax += number3;
            for (int sectionX = num8; sectionX < num10; ++sectionX)
            {
              for (int sectionY = num9; sectionY < num11; ++sectionY)
                NetMessage.SendSection(this.whoAmI, sectionX, sectionY);
            }
            if (flag4)
            {
              for (int sectionX = x1; sectionX <= num13; ++sectionX)
              {
                for (int sectionY = y1; sectionY <= num14; ++sectionY)
                  NetMessage.SendSection(this.whoAmI, sectionX, sectionY);
              }
            }
            for (int index10 = 0; index10 < portalSections.Count; ++index10)
              NetMessage.SendSection(this.whoAmI, portalSections[index10].X, portalSections[index10].Y);
            for (int number4 = 0; number4 < 400; ++number4)
            {
              if (Main.item[number4].active)
              {
                NetMessage.TrySendData(21, this.whoAmI, number: number4);
                NetMessage.TrySendData(22, this.whoAmI, number: number4);
              }
            }
            for (int number5 = 0; number5 < 200; ++number5)
            {
              if (Main.npc[number5].active)
                NetMessage.TrySendData(23, this.whoAmI, number: number5);
            }
            for (int number6 = 0; number6 < 1000; ++number6)
            {
              if (Main.projectile[number6].active && (Main.projPet[Main.projectile[number6].type] || Main.projectile[number6].netImportant))
                NetMessage.TrySendData(27, this.whoAmI, number: number6);
            }
            for (int number7 = 0; number7 < 290; ++number7)
              NetMessage.TrySendData(83, this.whoAmI, number: number7);
            NetMessage.TrySendData(57, this.whoAmI);
            NetMessage.TrySendData(103);
            NetMessage.TrySendData(101, this.whoAmI);
            NetMessage.TrySendData(136, this.whoAmI);
            NetMessage.TrySendData(49, this.whoAmI);
            Main.BestiaryTracker.OnPlayerJoining(this.whoAmI);
            CreativePowerManager.Instance.SyncThingsToJoiningPlayer(this.whoAmI);
            Main.PylonSystem.OnPlayerJoining(this.whoAmI);
            break;
          case 9:
            if (Main.netMode != 1)
              break;
            Netplay.Connection.StatusMax += this.reader.ReadInt32();
            Netplay.Connection.StatusText = NetworkText.Deserialize(this.reader).ToString();
            BitsByte bitsByte15 = (BitsByte) this.reader.ReadByte();
            BitsByte serverSpecialFlags = Netplay.Connection.ServerSpecialFlags;
            serverSpecialFlags[0] = bitsByte15[0];
            serverSpecialFlags[1] = bitsByte15[1];
            Netplay.Connection.ServerSpecialFlags = serverSpecialFlags;
            break;
          case 10:
            if (Main.netMode != 1)
              break;
            NetMessage.DecompressTileBlock(this.reader.BaseStream);
            break;
          case 11:
            if (Main.netMode != 1)
              break;
            WorldGen.SectionTileFrame((int) this.reader.ReadInt16(), (int) this.reader.ReadInt16(), (int) this.reader.ReadInt16(), (int) this.reader.ReadInt16());
            break;
          case 12:
            int index11 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index11 = this.whoAmI;
            Player player4 = Main.player[index11];
            player4.SpawnX = (int) this.reader.ReadInt16();
            player4.SpawnY = (int) this.reader.ReadInt16();
            player4.respawnTimer = this.reader.ReadInt32();
            player4.numberOfDeathsPVE = (int) this.reader.ReadInt16();
            player4.numberOfDeathsPVP = (int) this.reader.ReadInt16();
            if (player4.respawnTimer > 0)
              player4.dead = true;
            PlayerSpawnContext playerSpawnContext = (PlayerSpawnContext) this.reader.ReadByte();
            player4.Spawn(playerSpawnContext);
            if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State < 3)
              break;
            if (Netplay.Clients[this.whoAmI].State == 3)
            {
              Netplay.Clients[this.whoAmI].State = 10;
              NetMessage.buffer[this.whoAmI].broadcast = true;
              NetMessage.SyncConnectedPlayer(this.whoAmI);
              bool flag5 = NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI);
              Main.countsAsHostForGameplay[this.whoAmI] = flag5;
              if (NetMessage.DoesPlayerSlotCountAsAHost(this.whoAmI))
                NetMessage.TrySendData(139, this.whoAmI, number: this.whoAmI, number2: ((float) flag5.ToInt()));
              NetMessage.TrySendData(12, ignoreClient: this.whoAmI, number: this.whoAmI, number2: ((float) (byte) playerSpawnContext));
              NetMessage.TrySendData(74, this.whoAmI, text: NetworkText.FromLiteral(Main.player[this.whoAmI].name), number: Main.anglerQuest);
              NetMessage.TrySendData(129, this.whoAmI);
              NetMessage.greetPlayer(this.whoAmI);
              if (!Main.player[index11].unlockedBiomeTorches)
                break;
              NPC npc = new NPC();
              npc.SetDefaults(664);
              Main.BestiaryTracker.Kills.RegisterKill(npc);
              break;
            }
            NetMessage.TrySendData(12, ignoreClient: this.whoAmI, number: this.whoAmI, number2: ((float) (byte) playerSpawnContext));
            break;
          case 13:
            int number8 = (int) this.reader.ReadByte();
            if (number8 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number8 = this.whoAmI;
            Player player5 = Main.player[number8];
            BitsByte bitsByte16 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte17 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte18 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte19 = (BitsByte) this.reader.ReadByte();
            player5.controlUp = bitsByte16[0];
            player5.controlDown = bitsByte16[1];
            player5.controlLeft = bitsByte16[2];
            player5.controlRight = bitsByte16[3];
            player5.controlJump = bitsByte16[4];
            player5.controlUseItem = bitsByte16[5];
            player5.direction = bitsByte16[6] ? 1 : -1;
            if (bitsByte17[0])
            {
              player5.pulley = true;
              player5.pulleyDir = bitsByte17[1] ? (byte) 2 : (byte) 1;
            }
            else
              player5.pulley = false;
            player5.vortexStealthActive = bitsByte17[3];
            player5.gravDir = bitsByte17[4] ? 1f : -1f;
            player5.TryTogglingShield(bitsByte17[5]);
            player5.ghost = bitsByte17[6];
            player5.selectedItem = (int) this.reader.ReadByte();
            player5.position = this.reader.ReadVector2();
            if (bitsByte17[2])
              player5.velocity = this.reader.ReadVector2();
            else
              player5.velocity = Vector2.Zero;
            if (bitsByte18[6])
            {
              player5.PotionOfReturnOriginalUsePosition = new Vector2?(this.reader.ReadVector2());
              player5.PotionOfReturnHomePosition = new Vector2?(this.reader.ReadVector2());
            }
            else
            {
              player5.PotionOfReturnOriginalUsePosition = new Vector2?();
              player5.PotionOfReturnHomePosition = new Vector2?();
            }
            player5.tryKeepingHoveringUp = bitsByte18[0];
            player5.IsVoidVaultEnabled = bitsByte18[1];
            player5.sitting.isSitting = bitsByte18[2];
            player5.downedDD2EventAnyDifficulty = bitsByte18[3];
            player5.isPettingAnimal = bitsByte18[4];
            player5.isTheAnimalBeingPetSmall = bitsByte18[5];
            player5.tryKeepingHoveringDown = bitsByte18[7];
            player5.sleeping.SetIsSleepingAndAdjustPlayerRotation(player5, bitsByte19[0]);
            player5.autoReuseAllWeapons = bitsByte19[1];
            player5.controlDownHold = bitsByte19[2];
            player5.isOperatingAnotherEntity = bitsByte19[3];
            player5.controlUseTile = bitsByte19[4];
            if (Main.netMode != 2 || Netplay.Clients[this.whoAmI].State != 10)
              break;
            NetMessage.TrySendData(13, ignoreClient: this.whoAmI, number: number8);
            break;
          case 14:
            int playerIndex = (int) this.reader.ReadByte();
            int num15 = (int) this.reader.ReadByte();
            if (Main.netMode != 1)
              break;
            int num16 = Main.player[playerIndex].active ? 1 : 0;
            if (num15 == 1)
            {
              if (!Main.player[playerIndex].active)
                Main.player[playerIndex] = new Player();
              Main.player[playerIndex].active = true;
            }
            else
              Main.player[playerIndex].active = false;
            int num17 = Main.player[playerIndex].active ? 1 : 0;
            if (num16 == num17)
              break;
            if (Main.player[playerIndex].active)
            {
              Player.Hooks.PlayerConnect(playerIndex);
              break;
            }
            Player.Hooks.PlayerDisconnect(playerIndex);
            break;
          case 15:
            break;
          case 16:
            int number9 = (int) this.reader.ReadByte();
            if (number9 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number9 = this.whoAmI;
            Player player6 = Main.player[number9];
            player6.statLife = (int) this.reader.ReadInt16();
            player6.statLifeMax = (int) this.reader.ReadInt16();
            if (player6.statLifeMax < 100)
              player6.statLifeMax = 100;
            player6.dead = player6.statLife <= 0;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(16, ignoreClient: this.whoAmI, number: number9);
            break;
          case 17:
            byte number10 = this.reader.ReadByte();
            int index12 = (int) this.reader.ReadInt16();
            int index13 = (int) this.reader.ReadInt16();
            short index14 = this.reader.ReadInt16();
            int num18 = (int) this.reader.ReadByte();
            bool fail = index14 == (short) 1;
            if (!WorldGen.InWorld(index12, index13, 3))
              break;
            if (Main.tile[index12, index13] == null)
              Main.tile[index12, index13] = new Tile();
            if (Main.netMode == 2)
            {
              if (!fail)
              {
                if (number10 == (byte) 0 || number10 == (byte) 2 || number10 == (byte) 4)
                  ++Netplay.Clients[this.whoAmI].SpamDeleteBlock;
                if (number10 == (byte) 1 || number10 == (byte) 3)
                  ++Netplay.Clients[this.whoAmI].SpamAddBlock;
              }
              if (!Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(index12), Netplay.GetSectionY(index13)])
                fail = true;
            }
            if (number10 == (byte) 0)
            {
              WorldGen.KillTile(index12, index13, fail);
              if (Main.netMode == 1 && !fail)
                HitTile.ClearAllTilesAtThisLocation(index12, index13);
            }
            bool flag6 = false;
            if (number10 == (byte) 1)
            {
              bool forced = true;
              if (WorldGen.CheckTileBreakability2_ShouldTileSurvive(index12, index13))
              {
                flag6 = true;
                forced = false;
              }
              WorldGen.PlaceTile(index12, index13, (int) index14, forced: forced, style: num18);
            }
            if (number10 == (byte) 2)
              WorldGen.KillWall(index12, index13, fail);
            if (number10 == (byte) 3)
              WorldGen.PlaceWall(index12, index13, (int) index14);
            if (number10 == (byte) 4)
              WorldGen.KillTile(index12, index13, fail, noItem: true);
            if (number10 == (byte) 5)
              WorldGen.PlaceWire(index12, index13);
            if (number10 == (byte) 6)
              WorldGen.KillWire(index12, index13);
            if (number10 == (byte) 7)
              WorldGen.PoundTile(index12, index13);
            if (number10 == (byte) 8)
              WorldGen.PlaceActuator(index12, index13);
            if (number10 == (byte) 9)
              WorldGen.KillActuator(index12, index13);
            if (number10 == (byte) 10)
              WorldGen.PlaceWire2(index12, index13);
            if (number10 == (byte) 11)
              WorldGen.KillWire2(index12, index13);
            if (number10 == (byte) 12)
              WorldGen.PlaceWire3(index12, index13);
            if (number10 == (byte) 13)
              WorldGen.KillWire3(index12, index13);
            if (number10 == (byte) 14)
              WorldGen.SlopeTile(index12, index13, (int) index14);
            if (number10 == (byte) 15)
              Minecart.FrameTrack(index12, index13, true);
            if (number10 == (byte) 16)
              WorldGen.PlaceWire4(index12, index13);
            if (number10 == (byte) 17)
              WorldGen.KillWire4(index12, index13);
            if (number10 == (byte) 18)
            {
              Wiring.SetCurrentUser(this.whoAmI);
              Wiring.PokeLogicGate(index12, index13);
              Wiring.SetCurrentUser();
              break;
            }
            if (number10 == (byte) 19)
            {
              Wiring.SetCurrentUser(this.whoAmI);
              Wiring.Actuate(index12, index13);
              Wiring.SetCurrentUser();
              break;
            }
            if (number10 == (byte) 20)
            {
              if (!WorldGen.InWorld(index12, index13, 2))
                break;
              int type = (int) Main.tile[index12, index13].type;
              WorldGen.KillTile(index12, index13, fail);
              short number4 = !Main.tile[index12, index13].active() || (int) Main.tile[index12, index13].type != type ? (short) 0 : (short) 1;
              if (Main.netMode != 2)
                break;
              NetMessage.TrySendData(17, number: ((int) number10), number2: ((float) index12), number3: ((float) index13), number4: ((float) number4), number5: num18);
              break;
            }
            if (number10 == (byte) 21)
              WorldGen.ReplaceTile(index12, index13, (ushort) index14, num18);
            if (number10 == (byte) 22)
              WorldGen.ReplaceWall(index12, index13, (ushort) index14);
            if (number10 == (byte) 23)
            {
              WorldGen.SlopeTile(index12, index13, (int) index14);
              WorldGen.PoundTile(index12, index13);
            }
            if (Main.netMode != 2)
              break;
            if (flag6)
            {
              NetMessage.SendTileSquare(-1, index12, index13, 5);
              break;
            }
            if ((number10 == (byte) 1 || number10 == (byte) 21) && TileID.Sets.Falling[(int) index14] && !Main.tile[index12, index13].active())
              break;
            NetMessage.TrySendData(17, ignoreClient: this.whoAmI, number: ((int) number10), number2: ((float) index12), number3: ((float) index13), number4: ((float) index14), number5: num18);
            break;
          case 18:
            if (Main.netMode != 1)
              break;
            Main.dayTime = this.reader.ReadByte() == (byte) 1;
            Main.time = (double) this.reader.ReadInt32();
            Main.sunModY = this.reader.ReadInt16();
            Main.moonModY = this.reader.ReadInt16();
            break;
          case 19:
            byte number11 = this.reader.ReadByte();
            int num19 = (int) this.reader.ReadInt16();
            int num20 = (int) this.reader.ReadInt16();
            if (!WorldGen.InWorld(num19, num20, 3))
              break;
            int direction1 = this.reader.ReadByte() == (byte) 0 ? -1 : 1;
            switch (number11)
            {
              case 0:
                WorldGen.OpenDoor(num19, num20, direction1);
                break;
              case 1:
                WorldGen.CloseDoor(num19, num20, true);
                break;
              case 2:
                WorldGen.ShiftTrapdoor(num19, num20, direction1 == 1, 1);
                break;
              case 3:
                WorldGen.ShiftTrapdoor(num19, num20, direction1 == 1, 0);
                break;
              case 4:
                WorldGen.ShiftTallGate(num19, num20, false, true);
                break;
              case 5:
                WorldGen.ShiftTallGate(num19, num20, true, true);
                break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(19, ignoreClient: this.whoAmI, number: ((int) number11), number2: ((float) num19), number3: ((float) num20), number4: (direction1 == 1 ? 1f : 0.0f));
            break;
          case 20:
            int num21 = (int) this.reader.ReadInt16();
            int num22 = (int) this.reader.ReadInt16();
            ushort num23 = (ushort) this.reader.ReadByte();
            ushort num24 = (ushort) this.reader.ReadByte();
            byte number5_1 = this.reader.ReadByte();
            if (!WorldGen.InWorld(num21, num22, 3))
              break;
            TileChangeType type3 = TileChangeType.None;
            if (Enum.IsDefined(typeof (TileChangeType), (object) number5_1))
              type3 = (TileChangeType) number5_1;
            if (MessageBuffer.OnTileChangeReceived != null)
              MessageBuffer.OnTileChangeReceived(num21, num22, (int) Math.Max(num23, num24), type3);
            BitsByte bitsByte20 = (BitsByte) (byte) 0;
            BitsByte bitsByte21 = (BitsByte) (byte) 0;
            BitsByte bitsByte22 = (BitsByte) (byte) 0;
            for (int index15 = num21; index15 < num21 + (int) num23; ++index15)
            {
              for (int index16 = num22; index16 < num22 + (int) num24; ++index16)
              {
                if (Main.tile[index15, index16] == null)
                  Main.tile[index15, index16] = new Tile();
                Tile tile = Main.tile[index15, index16];
                bool flag7 = tile.active();
                BitsByte bitsByte23 = (BitsByte) this.reader.ReadByte();
                BitsByte bitsByte24 = (BitsByte) this.reader.ReadByte();
                BitsByte bitsByte25 = (BitsByte) this.reader.ReadByte();
                tile.active(bitsByte23[0]);
                tile.wall = bitsByte23[2] ? (ushort) 1 : (ushort) 0;
                bool flag8 = bitsByte23[3];
                if (Main.netMode != 2)
                  tile.liquid = flag8 ? (byte) 1 : (byte) 0;
                tile.wire(bitsByte23[4]);
                tile.halfBrick(bitsByte23[5]);
                tile.actuator(bitsByte23[6]);
                tile.inActive(bitsByte23[7]);
                tile.wire2(bitsByte24[0]);
                tile.wire3(bitsByte24[1]);
                if (bitsByte24[2])
                  tile.color(this.reader.ReadByte());
                if (bitsByte24[3])
                  tile.wallColor(this.reader.ReadByte());
                if (tile.active())
                {
                  int type4 = (int) tile.type;
                  tile.type = this.reader.ReadUInt16();
                  if (Main.tileFrameImportant[(int) tile.type])
                  {
                    tile.frameX = this.reader.ReadInt16();
                    tile.frameY = this.reader.ReadInt16();
                  }
                  else if (!flag7 || (int) tile.type != type4)
                  {
                    tile.frameX = (short) -1;
                    tile.frameY = (short) -1;
                  }
                  byte slope = 0;
                  if (bitsByte24[4])
                    ++slope;
                  if (bitsByte24[5])
                    slope += (byte) 2;
                  if (bitsByte24[6])
                    slope += (byte) 4;
                  tile.slope(slope);
                }
                tile.wire4(bitsByte24[7]);
                tile.fullbrightBlock(bitsByte25[0]);
                tile.fullbrightWall(bitsByte25[1]);
                tile.invisibleBlock(bitsByte25[2]);
                tile.invisibleWall(bitsByte25[3]);
                if (tile.wall > (ushort) 0)
                  tile.wall = this.reader.ReadUInt16();
                if (flag8)
                {
                  tile.liquid = this.reader.ReadByte();
                  tile.liquidType((int) this.reader.ReadByte());
                }
              }
            }
            WorldGen.RangeFrame(num21, num22, num21 + (int) num23, num22 + (int) num24);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num2, ignoreClient: this.whoAmI, number: num21, number2: ((float) num22), number3: ((float) num23), number4: ((float) num24), number5: ((int) number5_1));
            break;
          case 21:
          case 90:
          case 145:
          case 148:
            int index17 = (int) this.reader.ReadInt16();
            Vector2 vector2_1 = this.reader.ReadVector2();
            Vector2 vector2_2 = this.reader.ReadVector2();
            int Stack = (int) this.reader.ReadInt16();
            int prefixWeWant1 = (int) this.reader.ReadByte();
            int num25 = (int) this.reader.ReadByte();
            int type5 = (int) this.reader.ReadInt16();
            bool flag9 = false;
            float num26 = 0.0f;
            int num27 = 0;
            if (num2 == (byte) 145)
            {
              flag9 = this.reader.ReadBoolean();
              num26 = this.reader.ReadSingle();
            }
            if (num2 == (byte) 148)
              num27 = (int) this.reader.ReadByte();
            if (Main.netMode == 1)
            {
              if (type5 == 0)
              {
                Main.item[index17].active = false;
                break;
              }
              int index18 = index17;
              Item obj = Main.item[index18];
              ItemSyncPersistentStats syncPersistentStats = new ItemSyncPersistentStats();
              syncPersistentStats.CopyFrom(obj);
              bool flag10 = (obj.newAndShiny || obj.netID != type5) && ItemSlot.Options.HighlightNewItems && (type5 < 0 || type5 >= (int) ItemID.Count || !ItemID.Sets.NeverAppearsAsNewInInventory[type5]);
              obj.netDefaults(type5);
              obj.newAndShiny = flag10;
              obj.Prefix(prefixWeWant1);
              obj.stack = Stack;
              obj.position = vector2_1;
              obj.velocity = vector2_2;
              obj.active = true;
              obj.shimmered = flag9;
              obj.shimmerTime = num26;
              if (num2 == (byte) 90)
              {
                obj.instanced = true;
                obj.playerIndexTheItemIsReservedFor = Main.myPlayer;
                obj.keepTime = 600;
              }
              obj.timeLeftInWhichTheItemCannotBeTakenByEnemies = num27;
              obj.wet = Collision.WetCollision(obj.position, obj.width, obj.height);
              syncPersistentStats.PasteInto(obj);
              break;
            }
            if (Main.timeItemSlotCannotBeReusedFor[index17] > 0)
              break;
            if (type5 == 0)
            {
              if (index17 >= 400)
                break;
              Main.item[index17].active = false;
              NetMessage.TrySendData(21, number: index17);
              break;
            }
            bool flag11 = false;
            if (index17 == 400)
              flag11 = true;
            if (flag11)
            {
              Item obj = new Item();
              obj.netDefaults(type5);
              index17 = Item.NewItem((IEntitySource) new EntitySource_Sync(), (int) vector2_1.X, (int) vector2_1.Y, obj.width, obj.height, obj.type, Stack, true);
            }
            Item obj1 = Main.item[index17];
            obj1.netDefaults(type5);
            obj1.Prefix(prefixWeWant1);
            obj1.stack = Stack;
            obj1.position = vector2_1;
            obj1.velocity = vector2_2;
            obj1.active = true;
            obj1.playerIndexTheItemIsReservedFor = Main.myPlayer;
            obj1.timeLeftInWhichTheItemCannotBeTakenByEnemies = num27;
            if (num2 == (byte) 145)
            {
              obj1.shimmered = flag9;
              obj1.shimmerTime = num26;
            }
            if (flag11)
            {
              NetMessage.TrySendData((int) num2, number: index17);
              if (num25 == 0)
              {
                Main.item[index17].ownIgnore = this.whoAmI;
                Main.item[index17].ownTime = 100;
              }
              Main.item[index17].FindOwner(index17);
              break;
            }
            NetMessage.TrySendData((int) num2, ignoreClient: this.whoAmI, number: index17);
            break;
          case 22:
            int number12 = (int) this.reader.ReadInt16();
            int num28 = (int) this.reader.ReadByte();
            if (Main.netMode == 2 && Main.item[number12].playerIndexTheItemIsReservedFor != this.whoAmI)
              break;
            Main.item[number12].playerIndexTheItemIsReservedFor = num28;
            Main.item[number12].keepTime = num28 != Main.myPlayer ? 0 : 15;
            if (Main.netMode != 2)
              break;
            Main.item[number12].playerIndexTheItemIsReservedFor = (int) byte.MaxValue;
            Main.item[number12].keepTime = 15;
            NetMessage.TrySendData(22, number: number12);
            break;
          case 23:
            if (Main.netMode != 1)
              break;
            int index19 = (int) this.reader.ReadInt16();
            Vector2 vector2_3 = this.reader.ReadVector2();
            Vector2 vector2_4 = this.reader.ReadVector2();
            int num29 = (int) this.reader.ReadUInt16();
            if (num29 == (int) ushort.MaxValue)
              num29 = 0;
            BitsByte bitsByte26 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte27 = (BitsByte) this.reader.ReadByte();
            float[] numArray1 = this.ReUseTemporaryNPCAI();
            for (int index20 = 0; index20 < NPC.maxAI; ++index20)
              numArray1[index20] = !bitsByte26[index20 + 2] ? 0.0f : this.reader.ReadSingle();
            int Type1 = (int) this.reader.ReadInt16();
            int? nullable = new int?(1);
            if (bitsByte27[0])
              nullable = new int?((int) this.reader.ReadByte());
            float num30 = 1f;
            if (bitsByte27[2])
              num30 = this.reader.ReadSingle();
            int num31 = 0;
            if (!bitsByte26[7])
            {
              switch (this.reader.ReadByte())
              {
                case 2:
                  num31 = (int) this.reader.ReadInt16();
                  break;
                case 4:
                  num31 = this.reader.ReadInt32();
                  break;
                default:
                  num31 = (int) this.reader.ReadSByte();
                  break;
              }
            }
            int oldType = -1;
            NPC npc1 = Main.npc[index19];
            if (npc1.active && Main.multiplayerNPCSmoothingRange > 0 && (double) Vector2.DistanceSquared(npc1.position, vector2_3) < 640000.0)
              npc1.netOffset += npc1.position - vector2_3;
            if (!npc1.active || npc1.netID != Type1)
            {
              npc1.netOffset *= 0.0f;
              if (npc1.active)
                oldType = npc1.type;
              npc1.active = true;
              npc1.SetDefaults(Type1);
            }
            npc1.position = vector2_3;
            npc1.velocity = vector2_4;
            npc1.target = num29;
            npc1.direction = bitsByte26[0] ? 1 : -1;
            npc1.directionY = bitsByte26[1] ? 1 : -1;
            npc1.spriteDirection = bitsByte26[6] ? 1 : -1;
            if (bitsByte26[7])
              num31 = npc1.life = npc1.lifeMax;
            else
              npc1.life = num31;
            if (num31 <= 0)
              npc1.active = false;
            npc1.SpawnedFromStatue = bitsByte27[1];
            if (npc1.SpawnedFromStatue)
              npc1.value = 0.0f;
            for (int index21 = 0; index21 < NPC.maxAI; ++index21)
              npc1.ai[index21] = numArray1[index21];
            if (oldType > -1 && oldType != npc1.type)
              npc1.TransformVisuals(oldType, npc1.type);
            if (Type1 == 262)
              NPC.plantBoss = index19;
            if (Type1 == 245)
              NPC.golemBoss = index19;
            if (Type1 == 668)
              NPC.deerclopsBoss = index19;
            if (npc1.type < 0 || npc1.type >= (int) NPCID.Count || !Main.npcCatchable[npc1.type])
              break;
            npc1.releaseOwner = (short) this.reader.ReadByte();
            break;
          case 24:
            int number13 = (int) this.reader.ReadInt16();
            int number2_1 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number2_1 = this.whoAmI;
            Player player7 = Main.player[number2_1];
            Main.npc[number13].StrikeNPC(player7.inventory[player7.selectedItem].damage, player7.inventory[player7.selectedItem].knockBack, player7.direction);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(24, ignoreClient: this.whoAmI, number: number13, number2: ((float) number2_1));
            NetMessage.TrySendData(23, number: number13);
            break;
          case 25:
            break;
          case 26:
            break;
          case 27:
            int num32 = (int) this.reader.ReadInt16();
            Vector2 vector2_5 = this.reader.ReadVector2();
            Vector2 vector2_6 = this.reader.ReadVector2();
            int index22 = (int) this.reader.ReadByte();
            int Type2 = (int) this.reader.ReadInt16();
            BitsByte bitsByte28 = (BitsByte) this.reader.ReadByte();
            BitsByte bitsByte29 = (BitsByte) (bitsByte28[2] ? this.reader.ReadByte() : (byte) 0);
            float[] numArray2 = this.ReUseTemporaryProjectileAI();
            numArray2[0] = bitsByte28[0] ? this.reader.ReadSingle() : 0.0f;
            numArray2[1] = bitsByte28[1] ? this.reader.ReadSingle() : 0.0f;
            int num33 = bitsByte28[3] ? (int) this.reader.ReadUInt16() : 0;
            int num34 = bitsByte28[4] ? (int) this.reader.ReadInt16() : 0;
            float num35 = bitsByte28[5] ? this.reader.ReadSingle() : 0.0f;
            int num36 = bitsByte28[6] ? (int) this.reader.ReadInt16() : 0;
            int index23 = bitsByte28[7] ? (int) this.reader.ReadInt16() : -1;
            if (index23 >= 1000)
              index23 = -1;
            numArray2[2] = bitsByte29[0] ? this.reader.ReadSingle() : 0.0f;
            if (Main.netMode == 2)
            {
              if (Type2 == 949)
              {
                index22 = (int) byte.MaxValue;
              }
              else
              {
                index22 = this.whoAmI;
                if (Main.projHostile[Type2])
                  break;
              }
            }
            int number14 = 1000;
            for (int index24 = 0; index24 < 1000; ++index24)
            {
              if (Main.projectile[index24].owner == index22 && Main.projectile[index24].identity == num32 && Main.projectile[index24].active)
              {
                number14 = index24;
                break;
              }
            }
            if (number14 == 1000)
            {
              for (int index25 = 0; index25 < 1000; ++index25)
              {
                if (!Main.projectile[index25].active)
                {
                  number14 = index25;
                  break;
                }
              }
            }
            if (number14 == 1000)
              number14 = Projectile.FindOldestProjectile();
            Projectile projectile = Main.projectile[number14];
            if (!projectile.active || projectile.type != Type2)
            {
              projectile.SetDefaults(Type2);
              if (Main.netMode == 2)
                ++Netplay.Clients[this.whoAmI].SpamProjectile;
            }
            projectile.identity = num32;
            projectile.position = vector2_5;
            projectile.velocity = vector2_6;
            projectile.type = Type2;
            projectile.damage = num34;
            projectile.bannerIdToRespondTo = num33;
            projectile.originalDamage = num36;
            projectile.knockBack = num35;
            projectile.owner = index22;
            for (int index26 = 0; index26 < Projectile.maxAI; ++index26)
              projectile.ai[index26] = numArray2[index26];
            if (index23 >= 0)
            {
              projectile.projUUID = index23;
              Main.projectileIdentity[index22, index23] = number14;
            }
            projectile.ProjectileFixDesperation();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(27, ignoreClient: this.whoAmI, number: number14);
            break;
          case 28:
            int number15 = (int) this.reader.ReadInt16();
            int num37 = (int) this.reader.ReadInt16();
            float num38 = this.reader.ReadSingle();
            int num39 = (int) this.reader.ReadByte() - 1;
            byte number5_2 = this.reader.ReadByte();
            if (Main.netMode == 2)
            {
              if (num37 < 0)
                num37 = 0;
              Main.npc[number15].PlayerInteraction(this.whoAmI);
            }
            if (num37 >= 0)
            {
              Main.npc[number15].StrikeNPC(num37, num38, num39, number5_2 == (byte) 1, fromNet: true);
            }
            else
            {
              Main.npc[number15].life = 0;
              Main.npc[number15].HitEffect();
              Main.npc[number15].active = false;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(28, ignoreClient: this.whoAmI, number: number15, number2: ((float) num37), number3: num38, number4: ((float) num39), number5: ((int) number5_2));
            if (Main.npc[number15].life <= 0)
              NetMessage.TrySendData(23, number: number15);
            else
              Main.npc[number15].netUpdate = true;
            if (Main.npc[number15].realLife < 0)
              break;
            if (Main.npc[Main.npc[number15].realLife].life <= 0)
            {
              NetMessage.TrySendData(23, number: Main.npc[number15].realLife);
              break;
            }
            Main.npc[Main.npc[number15].realLife].netUpdate = true;
            break;
          case 29:
            int number16 = (int) this.reader.ReadInt16();
            int number2_2 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number2_2 = this.whoAmI;
            for (int index27 = 0; index27 < 1000; ++index27)
            {
              if (Main.projectile[index27].owner == number2_2 && Main.projectile[index27].identity == number16 && Main.projectile[index27].active)
              {
                Main.projectile[index27].Kill();
                break;
              }
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(29, ignoreClient: this.whoAmI, number: number16, number2: ((float) number2_2));
            break;
          case 30:
            int number17 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number17 = this.whoAmI;
            bool flag12 = this.reader.ReadBoolean();
            Main.player[number17].hostile = flag12;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(30, ignoreClient: this.whoAmI, number: number17);
            LocalizedText localizedText1 = flag12 ? Lang.mp[11] : Lang.mp[12];
            Color color1 = Main.teamColor[Main.player[number17].team];
            ChatHelper.BroadcastChatMessage(NetworkText.FromKey(localizedText1.Key, (object) Main.player[number17].name), color1);
            break;
          case 31:
            if (Main.netMode != 2)
              break;
            int num40 = (int) this.reader.ReadInt16();
            int num41 = (int) this.reader.ReadInt16();
            int chest1 = Chest.FindChest(num40, num41);
            if (chest1 <= -1 || Chest.UsingChest(chest1) != -1)
              break;
            for (int number2_3 = 0; number2_3 < 40; ++number2_3)
              NetMessage.TrySendData(32, this.whoAmI, number: chest1, number2: ((float) number2_3));
            NetMessage.TrySendData(33, this.whoAmI, number: chest1);
            Main.player[this.whoAmI].chest = chest1;
            if (Main.myPlayer == this.whoAmI)
              Main.recBigList = false;
            NetMessage.TrySendData(80, ignoreClient: this.whoAmI, number: this.whoAmI, number2: ((float) chest1));
            if (Main.netMode != 2 || !WorldGen.IsChestRigged(num40, num41))
              break;
            Wiring.SetCurrentUser(this.whoAmI);
            Wiring.HitSwitch(num40, num41);
            Wiring.SetCurrentUser();
            NetMessage.TrySendData(59, ignoreClient: this.whoAmI, number: num40, number2: ((float) num41));
            break;
          case 32:
            int index28 = (int) this.reader.ReadInt16();
            int index29 = (int) this.reader.ReadByte();
            int num42 = (int) this.reader.ReadInt16();
            int prefixWeWant2 = (int) this.reader.ReadByte();
            int type6 = (int) this.reader.ReadInt16();
            if (index28 < 0 || index28 >= 8000)
              break;
            if (Main.chest[index28] == null)
              Main.chest[index28] = new Chest();
            if (Main.chest[index28].item[index29] == null)
              Main.chest[index28].item[index29] = new Item();
            Main.chest[index28].item[index29].netDefaults(type6);
            Main.chest[index28].item[index29].Prefix(prefixWeWant2);
            Main.chest[index28].item[index29].stack = num42;
            Recipe.FindRecipes(true);
            break;
          case 33:
            int number2_4 = (int) this.reader.ReadInt16();
            int index30 = (int) this.reader.ReadInt16();
            int index31 = (int) this.reader.ReadInt16();
            int num43 = (int) this.reader.ReadByte();
            string str1 = string.Empty;
            if (num43 != 0)
            {
              if (num43 <= 20)
                str1 = this.reader.ReadString();
              else if (num43 != (int) byte.MaxValue)
                num43 = 0;
            }
            if (Main.netMode == 1)
            {
              Player player8 = Main.player[Main.myPlayer];
              if (player8.chest == -1)
              {
                Main.playerInventory = true;
                SoundEngine.PlaySound(10);
              }
              else if (player8.chest != number2_4 && number2_4 != -1)
              {
                Main.playerInventory = true;
                SoundEngine.PlaySound(12);
                Main.recBigList = false;
              }
              else if (player8.chest != -1 && number2_4 == -1)
              {
                SoundEngine.PlaySound(11);
                Main.recBigList = false;
              }
              player8.chest = number2_4;
              player8.chestX = index30;
              player8.chestY = index31;
              Recipe.FindRecipes(true);
              if (Main.tile[index30, index31].frameX < (short) 36 || Main.tile[index30, index31].frameX >= (short) 72)
                break;
              AchievementsHelper.HandleSpecialEvent(Main.player[Main.myPlayer], 16);
              break;
            }
            if (num43 != 0)
            {
              int chest2 = Main.player[this.whoAmI].chest;
              Chest chest3 = Main.chest[chest2];
              chest3.name = str1;
              NetMessage.TrySendData(69, ignoreClient: this.whoAmI, number: chest2, number2: ((float) chest3.x), number3: ((float) chest3.y));
            }
            Main.player[this.whoAmI].chest = number2_4;
            Recipe.FindRecipes(true);
            NetMessage.TrySendData(80, ignoreClient: this.whoAmI, number: this.whoAmI, number2: ((float) number2_4));
            break;
          case 34:
            byte number18 = this.reader.ReadByte();
            int index32 = (int) this.reader.ReadInt16();
            int index33 = (int) this.reader.ReadInt16();
            int index34 = (int) this.reader.ReadInt16();
            int id = (int) this.reader.ReadInt16();
            if (Main.netMode == 2)
              id = 0;
            if (Main.netMode == 2)
            {
              if (number18 == (byte) 0)
              {
                int number5_3 = WorldGen.PlaceChest(index32, index33, style: index34);
                if (number5_3 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_3);
                  Item.NewItem((IEntitySource) new EntitySource_TileBreak(index32, index33), index32 * 16, index33 * 16, 32, 32, Chest.chestItemSpawn[index34], noBroadcast: true);
                  break;
                }
                NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_3);
                break;
              }
              if (number18 == (byte) 1 && Main.tile[index32, index33].type == (ushort) 21)
              {
                Tile tile = Main.tile[index32, index33];
                if ((int) tile.frameX % 36 != 0)
                  --index32;
                if ((int) tile.frameY % 36 != 0)
                  --index33;
                int chest4 = Chest.FindChest(index32, index33);
                WorldGen.KillTile(index32, index33);
                if (tile.active())
                  break;
                NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number5: chest4);
                break;
              }
              if (number18 == (byte) 2)
              {
                int number5_4 = WorldGen.PlaceChest(index32, index33, (ushort) 88, style: index34);
                if (number5_4 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_4);
                  Item.NewItem((IEntitySource) new EntitySource_TileBreak(index32, index33), index32 * 16, index33 * 16, 32, 32, Chest.dresserItemSpawn[index34], noBroadcast: true);
                  break;
                }
                NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_4);
                break;
              }
              if (number18 == (byte) 3 && Main.tile[index32, index33].type == (ushort) 88)
              {
                Tile tile = Main.tile[index32, index33];
                int num44 = index32 - (int) tile.frameX % 54 / 18;
                if ((int) tile.frameY % 36 != 0)
                  --index33;
                int chest5 = Chest.FindChest(num44, index33);
                WorldGen.KillTile(num44, index33);
                if (tile.active())
                  break;
                NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) num44), number3: ((float) index33), number5: chest5);
                break;
              }
              if (number18 == (byte) 4)
              {
                int number5_5 = WorldGen.PlaceChest(index32, index33, (ushort) 467, style: index34);
                if (number5_5 == -1)
                {
                  NetMessage.TrySendData(34, this.whoAmI, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_5);
                  Item.NewItem((IEntitySource) new EntitySource_TileBreak(index32, index33), index32 * 16, index33 * 16, 32, 32, Chest.chestItemSpawn2[index34], noBroadcast: true);
                  break;
                }
                NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number4: ((float) index34), number5: number5_5);
                break;
              }
              if (number18 != (byte) 5 || Main.tile[index32, index33].type != (ushort) 467)
                break;
              Tile tile1 = Main.tile[index32, index33];
              if ((int) tile1.frameX % 36 != 0)
                --index32;
              if ((int) tile1.frameY % 36 != 0)
                --index33;
              int chest6 = Chest.FindChest(index32, index33);
              WorldGen.KillTile(index32, index33);
              if (tile1.active())
                break;
              NetMessage.TrySendData(34, number: ((int) number18), number2: ((float) index32), number3: ((float) index33), number5: chest6);
              break;
            }
            switch (number18)
            {
              case 0:
                if (id == -1)
                {
                  WorldGen.KillTile(index32, index33);
                  return;
                }
                SoundEngine.PlaySound(0, index32 * 16, index33 * 16);
                WorldGen.PlaceChestDirect(index32, index33, (ushort) 21, index34, id);
                return;
              case 2:
                if (id == -1)
                {
                  WorldGen.KillTile(index32, index33);
                  return;
                }
                SoundEngine.PlaySound(0, index32 * 16, index33 * 16);
                WorldGen.PlaceDresserDirect(index32, index33, (ushort) 88, index34, id);
                return;
              case 4:
                if (id == -1)
                {
                  WorldGen.KillTile(index32, index33);
                  return;
                }
                SoundEngine.PlaySound(0, index32 * 16, index33 * 16);
                WorldGen.PlaceChestDirect(index32, index33, (ushort) 467, index34, id);
                return;
              default:
                Chest.DestroyChestDirect(index32, index33, id);
                WorldGen.KillTile(index32, index33);
                return;
            }
          case 35:
            int number19 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number19 = this.whoAmI;
            int num45 = (int) this.reader.ReadInt16();
            if (number19 != Main.myPlayer || Main.ServerSideCharacter)
              Main.player[number19].HealEffect(num45);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(35, ignoreClient: this.whoAmI, number: number19, number2: ((float) num45));
            break;
          case 36:
            int index35 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index35 = this.whoAmI;
            Player player9 = Main.player[index35];
            bool flag13 = player9.zone5[0];
            player9.zone1 = (BitsByte) this.reader.ReadByte();
            player9.zone2 = (BitsByte) this.reader.ReadByte();
            player9.zone3 = (BitsByte) this.reader.ReadByte();
            player9.zone4 = (BitsByte) this.reader.ReadByte();
            player9.zone5 = (BitsByte) this.reader.ReadByte();
            if (Main.netMode != 2)
              break;
            if (!flag13 && player9.zone5[0])
              NPC.SpawnFaelings(index35);
            NetMessage.TrySendData(36, ignoreClient: this.whoAmI, number: index35);
            break;
          case 37:
            if (Main.netMode != 1)
              break;
            if (Main.autoPass)
            {
              NetMessage.TrySendData(38);
              Main.autoPass = false;
              break;
            }
            Netplay.ServerPassword = "";
            Main.menuMode = 31;
            break;
          case 38:
            if (Main.netMode != 2)
              break;
            if (this.reader.ReadString() == Netplay.ServerPassword)
            {
              Netplay.Clients[this.whoAmI].State = 1;
              NetMessage.TrySendData(3, this.whoAmI);
              break;
            }
            NetMessage.TrySendData(2, this.whoAmI, text: Lang.mp[1].ToNetworkText());
            break;
          case 39:
            if (Main.netMode != 1)
              break;
            int number20 = (int) this.reader.ReadInt16();
            Main.item[number20].playerIndexTheItemIsReservedFor = (int) byte.MaxValue;
            NetMessage.TrySendData(22, number: number20);
            break;
          case 40:
            int number21 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number21 = this.whoAmI;
            int npcIndex = (int) this.reader.ReadInt16();
            Main.player[number21].SetTalkNPC(npcIndex, true);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(40, ignoreClient: this.whoAmI, number: number21);
            break;
          case 41:
            int number22 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number22 = this.whoAmI;
            Player player10 = Main.player[number22];
            float num46 = this.reader.ReadSingle();
            int num47 = (int) this.reader.ReadInt16();
            player10.itemRotation = num46;
            player10.itemAnimation = num47;
            player10.channel = player10.inventory[player10.selectedItem].channel;
            if (Main.netMode == 2)
              NetMessage.TrySendData(41, ignoreClient: this.whoAmI, number: number22);
            if (Main.netMode != 1)
              break;
            Item obj2 = player10.inventory[player10.selectedItem];
            if (obj2.UseSound == null)
              break;
            SoundEngine.PlaySound(obj2.UseSound, player10.Center);
            break;
          case 42:
            int index36 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index36 = this.whoAmI;
            else if (Main.myPlayer == index36 && !Main.ServerSideCharacter)
              break;
            int num48 = (int) this.reader.ReadInt16();
            int num49 = (int) this.reader.ReadInt16();
            Main.player[index36].statMana = num48;
            Main.player[index36].statManaMax = num49;
            break;
          case 43:
            int number23 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number23 = this.whoAmI;
            int num50 = (int) this.reader.ReadInt16();
            if (number23 != Main.myPlayer)
              Main.player[number23].ManaEffect(num50);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(43, ignoreClient: this.whoAmI, number: number23, number2: ((float) num50));
            break;
          case 44:
            break;
          case 45:
            int number24 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number24 = this.whoAmI;
            int index37 = (int) this.reader.ReadByte();
            Player player11 = Main.player[number24];
            int team = player11.team;
            player11.team = index37;
            Color color2 = Main.teamColor[index37];
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(45, ignoreClient: this.whoAmI, number: number24);
            LocalizedText localizedText2 = Lang.mp[13 + index37];
            if (index37 == 5)
              localizedText2 = Lang.mp[22];
            for (int playerId = 0; playerId < (int) byte.MaxValue; ++playerId)
            {
              if (playerId == this.whoAmI || team > 0 && Main.player[playerId].team == team || index37 > 0 && Main.player[playerId].team == index37)
                ChatHelper.SendChatMessageToClient(NetworkText.FromKey(localizedText2.Key, (object) player11.name), color2, playerId);
            }
            break;
          case 46:
            if (Main.netMode != 2)
              break;
            int number25 = Sign.ReadSign((int) this.reader.ReadInt16(), (int) this.reader.ReadInt16());
            if (number25 < 0)
              break;
            NetMessage.TrySendData(47, this.whoAmI, number: number25, number2: ((float) this.whoAmI));
            break;
          case 47:
            int index38 = (int) this.reader.ReadInt16();
            int num51 = (int) this.reader.ReadInt16();
            int num52 = (int) this.reader.ReadInt16();
            string text1 = this.reader.ReadString();
            int number2_5 = (int) this.reader.ReadByte();
            BitsByte bitsByte30 = (BitsByte) this.reader.ReadByte();
            if (index38 < 0 || index38 >= 1000)
              break;
            string str2 = (string) null;
            if (Main.sign[index38] != null)
              str2 = Main.sign[index38].text;
            Main.sign[index38] = new Sign();
            Main.sign[index38].x = num51;
            Main.sign[index38].y = num52;
            Sign.TextSign(index38, text1);
            if (Main.netMode == 2 && str2 != text1)
            {
              number2_5 = this.whoAmI;
              NetMessage.TrySendData(47, ignoreClient: this.whoAmI, number: index38, number2: ((float) number2_5));
            }
            if (Main.netMode != 1 || number2_5 != Main.myPlayer || Main.sign[index38] == null || bitsByte30[0])
              break;
            Main.playerInventory = false;
            Main.player[Main.myPlayer].SetTalkNPC(-1, true);
            Main.npcChatCornerItem = 0;
            Main.editSign = false;
            SoundEngine.PlaySound(10);
            Main.player[Main.myPlayer].sign = index38;
            Main.npcChatText = Main.sign[index38].text;
            break;
          case 48:
            int index39 = (int) this.reader.ReadInt16();
            int index40 = (int) this.reader.ReadInt16();
            byte num53 = this.reader.ReadByte();
            byte liquidType = this.reader.ReadByte();
            if (Main.netMode == 2 && Netplay.SpamCheck)
            {
              int whoAmI = this.whoAmI;
              int num54 = (int) ((double) Main.player[whoAmI].position.X + (double) (Main.player[whoAmI].width / 2));
              int num55 = (int) ((double) Main.player[whoAmI].position.Y + (double) (Main.player[whoAmI].height / 2));
              int num56 = 10;
              int num57 = num54 - num56;
              int num58 = num54 + num56;
              int num59 = num55 - num56;
              int num60 = num55 + num56;
              if (index39 < num57 || index39 > num58 || index40 < num59 || index40 > num60)
                ++Netplay.Clients[this.whoAmI].SpamWater;
            }
            if (Main.tile[index39, index40] == null)
              Main.tile[index39, index40] = new Tile();
            lock (Main.tile[index39, index40])
            {
              Main.tile[index39, index40].liquid = num53;
              Main.tile[index39, index40].liquidType((int) liquidType);
              if (Main.netMode != 2)
                break;
              WorldGen.SquareTileFrame(index39, index40);
              if (num53 != (byte) 0)
                break;
              NetMessage.SendData(48, ignoreClient: this.whoAmI, number: index39, number2: ((float) index40));
              break;
            }
          case 49:
            if (Netplay.Connection.State != 6)
              break;
            Netplay.Connection.State = 10;
            Main.player[Main.myPlayer].Spawn(PlayerSpawnContext.SpawningIntoWorld);
            break;
          case 50:
            int number26 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number26 = this.whoAmI;
            else if (number26 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            Player player12 = Main.player[number26];
            for (int index41 = 0; index41 < Player.maxBuffs; ++index41)
            {
              player12.buffType[index41] = (int) this.reader.ReadUInt16();
              player12.buffTime[index41] = player12.buffType[index41] <= 0 ? 0 : 60;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(50, ignoreClient: this.whoAmI, number: number26);
            break;
          case 51:
            byte index42 = this.reader.ReadByte();
            byte number2_6 = this.reader.ReadByte();
            switch (number2_6)
            {
              case 1:
                NPC.SpawnSkeletron((int) index42);
                return;
              case 2:
                if (Main.netMode == 2)
                {
                  NetMessage.TrySendData(51, ignoreClient: this.whoAmI, number: ((int) index42), number2: ((float) number2_6));
                  return;
                }
                SoundEngine.PlaySound(SoundID.Item1, (int) Main.player[(int) index42].position.X, (int) Main.player[(int) index42].position.Y);
                return;
              case 3:
                if (Main.netMode != 2)
                  return;
                Main.Sundialing();
                return;
              case 4:
                Main.npc[(int) index42].BigMimicSpawnSmoke();
                return;
              case 5:
                if (Main.netMode != 2)
                  return;
                NPC npc2 = new NPC();
                npc2.SetDefaults(664);
                Main.BestiaryTracker.Kills.RegisterKill(npc2);
                return;
              case 6:
                if (Main.netMode != 2)
                  return;
                Main.Moondialing();
                return;
              default:
                return;
            }
          case 52:
            int number2_7 = (int) this.reader.ReadByte();
            int num61 = (int) this.reader.ReadInt16();
            int num62 = (int) this.reader.ReadInt16();
            if (number2_7 == 1)
            {
              Chest.Unlock(num61, num62);
              if (Main.netMode == 2)
              {
                NetMessage.TrySendData(52, ignoreClient: this.whoAmI, number2: ((float) number2_7), number3: ((float) num61), number4: ((float) num62));
                NetMessage.SendTileSquare(-1, num61, num62, 2);
              }
            }
            if (number2_7 == 2)
            {
              WorldGen.UnlockDoor(num61, num62);
              if (Main.netMode == 2)
              {
                NetMessage.TrySendData(52, ignoreClient: this.whoAmI, number2: ((float) number2_7), number3: ((float) num61), number4: ((float) num62));
                NetMessage.SendTileSquare(-1, num61, num62, 2);
              }
            }
            if (number2_7 != 3)
              break;
            Chest.Lock(num61, num62);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(52, ignoreClient: this.whoAmI, number2: ((float) number2_7), number3: ((float) num61), number4: ((float) num62));
            NetMessage.SendTileSquare(-1, num61, num62, 2);
            break;
          case 53:
            int number27 = (int) this.reader.ReadInt16();
            int type7 = (int) this.reader.ReadUInt16();
            int time1 = (int) this.reader.ReadInt16();
            Main.npc[number27].AddBuff(type7, time1, true);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(54, number: number27);
            break;
          case 54:
            if (Main.netMode != 1)
              break;
            int index43 = (int) this.reader.ReadInt16();
            NPC npc3 = Main.npc[index43];
            for (int index44 = 0; index44 < NPC.maxBuffs; ++index44)
            {
              npc3.buffType[index44] = (int) this.reader.ReadUInt16();
              npc3.buffTime[index44] = (int) this.reader.ReadInt16();
            }
            break;
          case 55:
            int number28 = (int) this.reader.ReadByte();
            int index45 = (int) this.reader.ReadUInt16();
            int num63 = this.reader.ReadInt32();
            if (Main.netMode == 2 && number28 != this.whoAmI && !Main.pvpBuff[index45])
              break;
            if (Main.netMode == 1 && number28 == Main.myPlayer)
            {
              Main.player[number28].AddBuff(index45, num63);
              break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(55, number: number28, number2: ((float) index45), number3: ((float) num63));
            break;
          case 56:
            int number29 = (int) this.reader.ReadInt16();
            if (number29 < 0 || number29 >= 200)
              break;
            if (Main.netMode == 1)
            {
              string str3 = this.reader.ReadString();
              Main.npc[number29].GivenName = str3;
              int num64 = this.reader.ReadInt32();
              Main.npc[number29].townNpcVariationIndex = num64;
              break;
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(56, this.whoAmI, number: number29);
            break;
          case 57:
            if (Main.netMode != 1)
              break;
            WorldGen.tGood = this.reader.ReadByte();
            WorldGen.tEvil = this.reader.ReadByte();
            WorldGen.tBlood = this.reader.ReadByte();
            break;
          case 58:
            int index46 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index46 = this.whoAmI;
            float num65 = this.reader.ReadSingle();
            if (Main.netMode == 2)
            {
              NetMessage.TrySendData(58, ignoreClient: this.whoAmI, number: this.whoAmI, number2: num65);
              break;
            }
            Player player13 = Main.player[index46];
            int type8 = player13.inventory[player13.selectedItem].type;
            switch (type8)
            {
              case 4057:
              case 4372:
              case 4715:
                player13.PlayGuitarChord(num65);
                return;
              case 4673:
                player13.PlayDrums(num65);
                return;
              default:
                Main.musicPitch = num65;
                LegacySoundStyle type9 = SoundID.Item26;
                if (type8 == 507)
                  type9 = SoundID.Item35;
                if (type8 == 1305)
                  type9 = SoundID.Item47;
                SoundEngine.PlaySound(type9, player13.position);
                return;
            }
          case 59:
            int num66 = (int) this.reader.ReadInt16();
            int num67 = (int) this.reader.ReadInt16();
            Wiring.SetCurrentUser(this.whoAmI);
            Wiring.HitSwitch(num66, num67);
            Wiring.SetCurrentUser();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(59, ignoreClient: this.whoAmI, number: num66, number2: ((float) num67));
            break;
          case 60:
            int n = (int) this.reader.ReadInt16();
            int x4 = (int) this.reader.ReadInt16();
            int y4 = (int) this.reader.ReadInt16();
            byte num68 = this.reader.ReadByte();
            if (n >= 200)
            {
              NetMessage.BootPlayer(this.whoAmI, NetworkText.FromKey("Net.CheatingInvalid"));
              break;
            }
            NPC npc4 = Main.npc[n];
            int num69 = npc4.isLikeATownNPC ? 1 : 0;
            if (Main.netMode == 1)
            {
              npc4.homeless = num68 == (byte) 1;
              npc4.homeTileX = x4;
              npc4.homeTileY = y4;
            }
            if (num69 == 0)
              break;
            if (Main.netMode == 1)
            {
              if (num68 == (byte) 1)
              {
                WorldGen.TownManager.KickOut(npc4.type);
                break;
              }
              if (num68 != (byte) 2)
                break;
              WorldGen.TownManager.SetRoom(npc4.type, x4, y4);
              break;
            }
            if (num68 == (byte) 1)
            {
              WorldGen.kickOut(n);
              break;
            }
            WorldGen.moveRoom(x4, y4, n);
            break;
          case 61:
            int num70 = (int) this.reader.ReadInt16();
            int index47 = (int) this.reader.ReadInt16();
            if (Main.netMode != 2)
              break;
            if ((index47 < 0 || index47 >= (int) NPCID.Count ? 0 : (NPCID.Sets.MPAllowedEnemies[index47] ? 1 : 0)) != 0)
            {
              if (NPC.AnyNPCs(index47))
                break;
              NPC.SpawnOnPlayer(num70, index47);
              break;
            }
            switch (index47)
            {
              case -18:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.PeddlersSatchelUsed"), new Color(50, (int) byte.MaxValue, 130));
                NPC.peddlersSatchelWasUsed = true;
                NetMessage.TrySendData(7);
                return;
              case -17:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookVolumeTwoUsed"), new Color(50, (int) byte.MaxValue, 130));
                NPC.combatBookVolumeTwoWasUsed = true;
                NetMessage.TrySendData(7);
                return;
              case -16:
                NPC.SpawnMechQueen(num70);
                return;
              case -15:
                NPC.UnlockOrExchangePet(ref NPC.unlockedSlimeBlueSpawn, 670, "Misc.LicenseSlimeUsed", index47);
                return;
              case -14:
                NPC.UnlockOrExchangePet(ref NPC.boughtBunny, 656, "Misc.LicenseBunnyUsed", index47);
                return;
              case -13:
                NPC.UnlockOrExchangePet(ref NPC.boughtDog, 638, "Misc.LicenseDogUsed", index47);
                return;
              case -12:
                NPC.UnlockOrExchangePet(ref NPC.boughtCat, 637, "Misc.LicenseCatUsed", index47);
                return;
              case -11:
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Misc.CombatBookUsed"), new Color(50, (int) byte.MaxValue, 130));
                NPC.combatBookWasUsed = true;
                NetMessage.TrySendData(7);
                return;
              case -10:
                if (Main.dayTime || Main.bloodMoon)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[8].Key), new Color(50, (int) byte.MaxValue, 130));
                Main.bloodMoon = true;
                if (Main.GetMoonPhase() == MoonPhase.Empty)
                  Main.moonPhase = 5;
                AchievementsHelper.NotifyProgressionEvent(4);
                NetMessage.TrySendData(7);
                return;
              case -8:
                if (!NPC.downedGolemBoss || !Main.hardMode || NPC.AnyDanger() || NPC.AnyoneNearCultists())
                  return;
                WorldGen.StartImpendingDoom(720);
                NetMessage.TrySendData(7);
                return;
              case -7:
                Main.invasionDelay = 0;
                Main.StartInvasion(4);
                NetMessage.TrySendData(7);
                NetMessage.TrySendData(78, number2: 1f, number3: ((float) (Main.invasionType + 3)));
                return;
              case -6:
                if (!Main.dayTime || Main.eclipse)
                  return;
                if (Main.remixWorld)
                  ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[106].Key), new Color(50, (int) byte.MaxValue, 130));
                else
                  ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[20].Key), new Color(50, (int) byte.MaxValue, 130));
                Main.eclipse = true;
                NetMessage.TrySendData(7);
                return;
              case -5:
                if (Main.dayTime || DD2Event.Ongoing)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[34].Key), new Color(50, (int) byte.MaxValue, 130));
                Main.startSnowMoon();
                NetMessage.TrySendData(7);
                NetMessage.TrySendData(78, number2: 1f, number3: 1f, number4: 1f);
                return;
              case -4:
                if (Main.dayTime || DD2Event.Ongoing)
                  return;
                ChatHelper.BroadcastChatMessage(NetworkText.FromKey(Lang.misc[31].Key), new Color(50, (int) byte.MaxValue, 130));
                Main.startPumpkinMoon();
                NetMessage.TrySendData(7);
                NetMessage.TrySendData(78, number2: 1f, number3: 2f, number4: 1f);
                return;
              default:
                if (index47 >= 0)
                  return;
                int type10 = 1;
                if (index47 > (int) -InvasionID.Count)
                  type10 = -index47;
                if (type10 > 0 && Main.invasionType == 0)
                {
                  Main.invasionDelay = 0;
                  Main.StartInvasion(type10);
                }
                NetMessage.TrySendData(78, number2: 1f, number3: ((float) (Main.invasionType + 3)));
                return;
            }
          case 62:
            int number30 = (int) this.reader.ReadByte();
            int number2_8 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number30 = this.whoAmI;
            if (number2_8 == 1)
              Main.player[number30].NinjaDodge();
            if (number2_8 == 2)
              Main.player[number30].ShadowDodge();
            if (number2_8 == 4)
              Main.player[number30].BrainOfConfusionDodge();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(62, ignoreClient: this.whoAmI, number: number30, number2: ((float) number2_8));
            break;
          case 63:
            int num71 = (int) this.reader.ReadInt16();
            int num72 = (int) this.reader.ReadInt16();
            byte num73 = this.reader.ReadByte();
            byte number4_1 = this.reader.ReadByte();
            if (number4_1 == (byte) 0)
              WorldGen.paintTile(num71, num72, num73);
            else
              WorldGen.paintCoatTile(num71, num72, num73);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(63, ignoreClient: this.whoAmI, number: num71, number2: ((float) num72), number3: ((float) num73), number4: ((float) number4_1));
            break;
          case 64:
            int num74 = (int) this.reader.ReadInt16();
            int num75 = (int) this.reader.ReadInt16();
            byte num76 = this.reader.ReadByte();
            byte number4_2 = this.reader.ReadByte();
            if (number4_2 == (byte) 0)
              WorldGen.paintWall(num74, num75, num76);
            else
              WorldGen.paintCoatWall(num74, num75, num76);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(64, ignoreClient: this.whoAmI, number: num74, number2: ((float) num75), number3: ((float) num76), number4: ((float) number4_2));
            break;
          case 65:
            BitsByte bitsByte31 = (BitsByte) this.reader.ReadByte();
            int number2_9 = (int) this.reader.ReadInt16();
            if (Main.netMode == 2)
              number2_9 = this.whoAmI;
            Vector2 vector2_7 = this.reader.ReadVector2();
            int num77 = (int) this.reader.ReadByte();
            int number31 = 0;
            if (bitsByte31[0])
              ++number31;
            if (bitsByte31[1])
              number31 += 2;
            bool flag14 = false;
            if (bitsByte31[2])
              flag14 = true;
            int num78 = 0;
            if (bitsByte31[3])
              num78 = this.reader.ReadInt32();
            if (flag14)
              vector2_7 = Main.player[number2_9].position;
            switch (number31)
            {
              case 0:
                Main.player[number2_9].Teleport(vector2_7, num77, num78);
                break;
              case 1:
                Main.npc[number2_9].Teleport(vector2_7, num77, num78);
                break;
              case 2:
                Main.player[number2_9].Teleport(vector2_7, num77, num78);
                if (Main.netMode == 2)
                {
                  RemoteClient.CheckSection(this.whoAmI, vector2_7);
                  NetMessage.TrySendData(65, number2: ((float) number2_9), number3: vector2_7.X, number4: vector2_7.Y, number5: num77, number6: flag14.ToInt(), number7: num78);
                  int index48 = -1;
                  float num79 = 9999f;
                  for (int index49 = 0; index49 < (int) byte.MaxValue; ++index49)
                  {
                    if (Main.player[index49].active && index49 != this.whoAmI)
                    {
                      Vector2 vector2_8 = Main.player[index49].position - Main.player[this.whoAmI].position;
                      if ((double) vector2_8.Length() < (double) num79)
                      {
                        num79 = vector2_8.Length();
                        index48 = index49;
                      }
                    }
                  }
                  if (index48 >= 0)
                  {
                    ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Game.HasTeleportedTo", (object) Main.player[this.whoAmI].name, (object) Main.player[index48].name), new Color(250, 250, 0));
                    break;
                  }
                  break;
                }
                break;
            }
            if (Main.netMode != 2 || number31 != 0)
              break;
            NetMessage.TrySendData(65, ignoreClient: this.whoAmI, number: number31, number2: ((float) number2_9), number3: vector2_7.X, number4: vector2_7.Y, number5: num77, number6: flag14.ToInt(), number7: num78);
            break;
          case 66:
            int number32 = (int) this.reader.ReadByte();
            int num80 = (int) this.reader.ReadInt16();
            if (num80 <= 0)
              break;
            Player player14 = Main.player[number32];
            player14.statLife += num80;
            if (player14.statLife > player14.statLifeMax2)
              player14.statLife = player14.statLifeMax2;
            player14.HealEffect(num80, false);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(66, ignoreClient: this.whoAmI, number: number32, number2: ((float) num80));
            break;
          case 67:
            break;
          case 68:
            this.reader.ReadString();
            break;
          case 69:
            int number33 = (int) this.reader.ReadInt16();
            int num81 = (int) this.reader.ReadInt16();
            int num82 = (int) this.reader.ReadInt16();
            if (Main.netMode == 1)
            {
              if (number33 < 0 || number33 >= 8000)
                break;
              Chest chest7 = Main.chest[number33];
              if (chest7 == null)
              {
                chest7 = new Chest();
                chest7.x = num81;
                chest7.y = num82;
                Main.chest[number33] = chest7;
              }
              else if (chest7.x != num81 || chest7.y != num82)
                break;
              chest7.name = this.reader.ReadString();
              break;
            }
            if (number33 < -1 || number33 >= 8000)
              break;
            if (number33 == -1)
            {
              number33 = Chest.FindChest(num81, num82);
              if (number33 == -1)
                break;
            }
            Chest chest8 = Main.chest[number33];
            if (chest8.x != num81 || chest8.y != num82)
              break;
            NetMessage.TrySendData(69, this.whoAmI, number: number33, number2: ((float) num81), number3: ((float) num82));
            break;
          case 70:
            if (Main.netMode != 2)
              break;
            int i1 = (int) this.reader.ReadInt16();
            int who = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              who = this.whoAmI;
            if (i1 >= 200 || i1 < 0)
              break;
            NPC.CatchNPC(i1, who);
            break;
          case 71:
            if (Main.netMode != 2)
              break;
            int x5 = this.reader.ReadInt32();
            int num83 = this.reader.ReadInt32();
            int num84 = (int) this.reader.ReadInt16();
            byte num85 = this.reader.ReadByte();
            int y5 = num83;
            int Type3 = num84;
            int Style1 = (int) num85;
            int whoAmI1 = this.whoAmI;
            NPC.ReleaseNPC(x5, y5, Type3, Style1, whoAmI1);
            break;
          case 72:
            if (Main.netMode != 1)
              break;
            for (int index50 = 0; index50 < 40; ++index50)
              Main.travelShop[index50] = (int) this.reader.ReadInt16();
            break;
          case 73:
            switch (this.reader.ReadByte())
            {
              case 0:
                Main.player[this.whoAmI].TeleportationPotion();
                return;
              case 1:
                Main.player[this.whoAmI].MagicConch();
                return;
              case 2:
                Main.player[this.whoAmI].DemonConch();
                return;
              case 3:
                Main.player[this.whoAmI].Shellphone_Spawn();
                return;
              default:
                return;
            }
          case 74:
            if (Main.netMode != 1)
              break;
            Main.anglerQuest = (int) this.reader.ReadByte();
            Main.anglerQuestFinished = this.reader.ReadBoolean();
            break;
          case 75:
            if (Main.netMode != 2)
              break;
            string name = Main.player[this.whoAmI].name;
            if (Main.anglerWhoFinishedToday.Contains(name))
              break;
            Main.anglerWhoFinishedToday.Add(name);
            break;
          case 76:
            int number34 = (int) this.reader.ReadByte();
            if (number34 == Main.myPlayer && !Main.ServerSideCharacter)
              break;
            if (Main.netMode == 2)
              number34 = this.whoAmI;
            Player player15 = Main.player[number34];
            player15.anglerQuestsFinished = this.reader.ReadInt32();
            player15.golferScoreAccumulated = this.reader.ReadInt32();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(76, ignoreClient: this.whoAmI, number: number34);
            break;
          case 77:
            int type11 = (int) this.reader.ReadInt16();
            ushort num86 = this.reader.ReadUInt16();
            short num87 = this.reader.ReadInt16();
            short num88 = this.reader.ReadInt16();
            int tileType = (int) num86;
            int x6 = (int) num87;
            int y6 = (int) num88;
            Animation.NewTemporaryAnimation(type11, (ushort) tileType, x6, y6);
            break;
          case 78:
            if (Main.netMode != 1)
              break;
            Main.ReportInvasionProgress(this.reader.ReadInt32(), this.reader.ReadInt32(), (int) this.reader.ReadSByte(), (int) this.reader.ReadSByte());
            break;
          case 79:
            int x7 = (int) this.reader.ReadInt16();
            int y7 = (int) this.reader.ReadInt16();
            short type12 = this.reader.ReadInt16();
            int style = (int) this.reader.ReadInt16();
            int num89 = (int) this.reader.ReadByte();
            int random = (int) this.reader.ReadSByte();
            int direction2 = !this.reader.ReadBoolean() ? -1 : 1;
            if (Main.netMode == 2)
            {
              ++Netplay.Clients[this.whoAmI].SpamAddBlock;
              if (!WorldGen.InWorld(x7, y7, 10) || !Netplay.Clients[this.whoAmI].TileSections[Netplay.GetSectionX(x7), Netplay.GetSectionY(y7)])
                break;
            }
            WorldGen.PlaceObject(x7, y7, (int) type12, style: style, alternate: num89, random: random, direction: direction2);
            if (Main.netMode != 2)
              break;
            NetMessage.SendObjectPlacement(this.whoAmI, x7, y7, (int) type12, style, num89, random, direction2);
            break;
          case 80:
            if (Main.netMode != 1)
              break;
            int index51 = (int) this.reader.ReadByte();
            int num90 = (int) this.reader.ReadInt16();
            if (num90 < -3 || num90 >= 8000)
              break;
            Main.player[index51].chest = num90;
            Recipe.FindRecipes(true);
            break;
          case 81:
            if (Main.netMode != 1)
              break;
            int x8 = (int) this.reader.ReadSingle();
            int num91 = (int) this.reader.ReadSingle();
            Color color3 = this.reader.ReadRGB();
            int amount = this.reader.ReadInt32();
            int y8 = num91;
            CombatText.NewText(new Rectangle(x8, y8, 0, 0), color3, amount);
            break;
          case 82:
            NetManager.Instance.Read(this.reader, this.whoAmI, length);
            break;
          case 83:
            if (Main.netMode != 1)
              break;
            int index52 = (int) this.reader.ReadInt16();
            int num92 = this.reader.ReadInt32();
            if (index52 < 0 || index52 >= 290)
              break;
            NPC.killCount[index52] = num92;
            break;
          case 84:
            int number35 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number35 = this.whoAmI;
            float num93 = this.reader.ReadSingle();
            Main.player[number35].stealth = num93;
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(84, ignoreClient: this.whoAmI, number: number35);
            break;
          case 85:
            int whoAmI2 = this.whoAmI;
            int slot = (int) this.reader.ReadInt16();
            if (Main.netMode != 2 || whoAmI2 >= (int) byte.MaxValue)
              break;
            Chest.ServerPlaceItem(this.whoAmI, slot);
            break;
          case 86:
            if (Main.netMode != 1)
              break;
            int key1 = this.reader.ReadInt32();
            if (!this.reader.ReadBoolean())
            {
              TileEntity tileEntity;
              if (!TileEntity.ByID.TryGetValue(key1, out tileEntity))
                break;
              TileEntity.ByID.Remove(key1);
              TileEntity.ByPosition.Remove(tileEntity.Position);
              break;
            }
            TileEntity tileEntity1 = TileEntity.Read(this.reader, true);
            tileEntity1.ID = key1;
            TileEntity.ByID[tileEntity1.ID] = tileEntity1;
            TileEntity.ByPosition[tileEntity1.Position] = tileEntity1;
            break;
          case 87:
            if (Main.netMode != 2)
              break;
            int num94 = (int) this.reader.ReadInt16();
            int num95 = (int) this.reader.ReadInt16();
            int type13 = (int) this.reader.ReadByte();
            if (!WorldGen.InWorld(num94, num95) || TileEntity.ByPosition.ContainsKey(new Point16(num94, num95)))
              break;
            TileEntity.PlaceEntityNet(num94, num95, type13);
            break;
          case 88:
            if (Main.netMode != 1)
              break;
            int index53 = (int) this.reader.ReadInt16();
            if (index53 < 0 || index53 > 400)
              break;
            Item obj3 = Main.item[index53];
            BitsByte bitsByte32 = (BitsByte) this.reader.ReadByte();
            if (bitsByte32[0])
              obj3.color.PackedValue = this.reader.ReadUInt32();
            if (bitsByte32[1])
              obj3.damage = (int) this.reader.ReadUInt16();
            if (bitsByte32[2])
              obj3.knockBack = this.reader.ReadSingle();
            if (bitsByte32[3])
              obj3.useAnimation = (int) this.reader.ReadUInt16();
            if (bitsByte32[4])
              obj3.useTime = (int) this.reader.ReadUInt16();
            if (bitsByte32[5])
              obj3.shoot = (int) this.reader.ReadInt16();
            if (bitsByte32[6])
              obj3.shootSpeed = this.reader.ReadSingle();
            if (!bitsByte32[7])
              break;
            bitsByte32 = (BitsByte) this.reader.ReadByte();
            if (bitsByte32[0])
              obj3.width = (int) this.reader.ReadInt16();
            if (bitsByte32[1])
              obj3.height = (int) this.reader.ReadInt16();
            if (bitsByte32[2])
              obj3.scale = this.reader.ReadSingle();
            if (bitsByte32[3])
              obj3.ammo = (int) this.reader.ReadInt16();
            if (bitsByte32[4])
              obj3.useAmmo = (int) this.reader.ReadInt16();
            if (!bitsByte32[5])
              break;
            obj3.notAmmo = this.reader.ReadBoolean();
            break;
          case 89:
            if (Main.netMode != 2)
              break;
            int x9 = (int) this.reader.ReadInt16();
            int num96 = (int) this.reader.ReadInt16();
            int num97 = (int) this.reader.ReadInt16();
            int num98 = (int) this.reader.ReadByte();
            int num99 = (int) this.reader.ReadInt16();
            int y9 = num96;
            int netid1 = num97;
            int prefix1 = num98;
            int stack1 = num99;
            TEItemFrame.TryPlacing(x9, y9, netid1, prefix1, stack1);
            break;
          case 91:
            if (Main.netMode != 1)
              break;
            int num100 = this.reader.ReadInt32();
            int type14 = (int) this.reader.ReadByte();
            if (type14 == (int) byte.MaxValue)
            {
              if (!EmoteBubble.byID.ContainsKey(num100))
                break;
              EmoteBubble.byID.Remove(num100);
              break;
            }
            int meta = (int) this.reader.ReadUInt16();
            int time2 = (int) this.reader.ReadUInt16();
            int emotion = (int) this.reader.ReadByte();
            int num101 = 0;
            if (emotion < 0)
              num101 = (int) this.reader.ReadInt16();
            WorldUIAnchor bubbleAnchor = EmoteBubble.DeserializeNetAnchor(type14, meta);
            if (type14 == 1)
              Main.player[meta].emoteTime = 360;
            lock (EmoteBubble.byID)
            {
              if (!EmoteBubble.byID.ContainsKey(num100))
              {
                EmoteBubble.byID[num100] = new EmoteBubble(emotion, bubbleAnchor, time2);
              }
              else
              {
                EmoteBubble.byID[num100].lifeTime = time2;
                EmoteBubble.byID[num100].lifeTimeStart = time2;
                EmoteBubble.byID[num100].emote = emotion;
                EmoteBubble.byID[num100].anchor = bubbleAnchor;
              }
              EmoteBubble.byID[num100].ID = num100;
              EmoteBubble.byID[num100].metadata = num101;
              EmoteBubble.OnBubbleChange(num100);
              break;
            }
          case 92:
            int number36 = (int) this.reader.ReadInt16();
            int num102 = this.reader.ReadInt32();
            float num103 = this.reader.ReadSingle();
            float num104 = this.reader.ReadSingle();
            if (number36 < 0 || number36 > 200)
              break;
            if (Main.netMode == 1)
            {
              Main.npc[number36].moneyPing(new Vector2(num103, num104));
              Main.npc[number36].extraValue = num102;
              break;
            }
            Main.npc[number36].extraValue += num102;
            NetMessage.TrySendData(92, number: number36, number2: ((float) Main.npc[number36].extraValue), number3: num103, number4: num104);
            break;
          case 93:
            break;
          case 95:
            ushort number2_10 = this.reader.ReadUInt16();
            int num105 = (int) this.reader.ReadByte();
            if (Main.netMode != 2)
              break;
            for (int index54 = 0; index54 < 1000; ++index54)
            {
              if (Main.projectile[index54].owner == (int) number2_10 && Main.projectile[index54].active && Main.projectile[index54].type == 602 && (double) Main.projectile[index54].ai[1] == (double) num105)
              {
                Main.projectile[index54].Kill();
                NetMessage.TrySendData(29, number: Main.projectile[index54].identity, number2: ((float) number2_10));
                break;
              }
            }
            break;
          case 96:
            int number37 = (int) this.reader.ReadByte();
            Player player16 = Main.player[number37];
            int num106 = (int) this.reader.ReadInt16();
            Vector2 newPos1 = this.reader.ReadVector2();
            Vector2 vector2_9 = this.reader.ReadVector2();
            player16.lastPortalColorIndex = num106 + (num106 % 2 == 0 ? 1 : -1);
            player16.Teleport(newPos1, 4, num106);
            player16.velocity = vector2_9;
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(96, number: number37, number2: newPos1.X, number3: newPos1.Y, number4: ((float) num106));
            break;
          case 97:
            if (Main.netMode != 1)
              break;
            AchievementsHelper.NotifyNPCKilledDirect(Main.player[Main.myPlayer], (int) this.reader.ReadInt16());
            break;
          case 98:
            if (Main.netMode != 1)
              break;
            AchievementsHelper.NotifyProgressionEvent((int) this.reader.ReadInt16());
            break;
          case 99:
            int number38 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number38 = this.whoAmI;
            Main.player[number38].MinionRestTargetPoint = this.reader.ReadVector2();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(99, ignoreClient: this.whoAmI, number: number38);
            break;
          case 100:
            int index55 = (int) this.reader.ReadUInt16();
            NPC npc5 = Main.npc[index55];
            int extraInfo = (int) this.reader.ReadInt16();
            Vector2 newPos2 = this.reader.ReadVector2();
            Vector2 vector2_10 = this.reader.ReadVector2();
            npc5.lastPortalColorIndex = extraInfo + (extraInfo % 2 == 0 ? 1 : -1);
            npc5.Teleport(newPos2, 4, extraInfo);
            npc5.velocity = vector2_10;
            npc5.netOffset *= 0.0f;
            break;
          case 101:
            if (Main.netMode == 2)
              break;
            NPC.ShieldStrengthTowerSolar = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerVortex = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerNebula = (int) this.reader.ReadUInt16();
            NPC.ShieldStrengthTowerStardust = (int) this.reader.ReadUInt16();
            if (NPC.ShieldStrengthTowerSolar < 0)
              NPC.ShieldStrengthTowerSolar = 0;
            if (NPC.ShieldStrengthTowerVortex < 0)
              NPC.ShieldStrengthTowerVortex = 0;
            if (NPC.ShieldStrengthTowerNebula < 0)
              NPC.ShieldStrengthTowerNebula = 0;
            if (NPC.ShieldStrengthTowerStardust < 0)
              NPC.ShieldStrengthTowerStardust = 0;
            if (NPC.ShieldStrengthTowerSolar > NPC.LunarShieldPowerMax)
              NPC.ShieldStrengthTowerSolar = NPC.LunarShieldPowerMax;
            if (NPC.ShieldStrengthTowerVortex > NPC.LunarShieldPowerMax)
              NPC.ShieldStrengthTowerVortex = NPC.LunarShieldPowerMax;
            if (NPC.ShieldStrengthTowerNebula > NPC.LunarShieldPowerMax)
              NPC.ShieldStrengthTowerNebula = NPC.LunarShieldPowerMax;
            if (NPC.ShieldStrengthTowerStardust <= NPC.LunarShieldPowerMax)
              break;
            NPC.ShieldStrengthTowerStardust = NPC.LunarShieldPowerMax;
            break;
          case 102:
            int index56 = (int) this.reader.ReadByte();
            ushort num107 = this.reader.ReadUInt16();
            Vector2 Other = this.reader.ReadVector2();
            if (Main.netMode == 2)
            {
              NetMessage.TrySendData(102, number: this.whoAmI, number2: ((float) num107), number3: Other.X, number4: Other.Y);
              break;
            }
            Player player17 = Main.player[index56];
            for (int index57 = 0; index57 < (int) byte.MaxValue; ++index57)
            {
              Player player18 = Main.player[index57];
              if (player18.active && !player18.dead && (player17.team == 0 || player17.team == player18.team) && (double) player18.Distance(Other) < 700.0)
              {
                Vector2 vector2_11 = player17.Center - player18.Center;
                Vector2 vec = Vector2.Normalize(vector2_11);
                if (!vec.HasNaNs())
                {
                  int num108 = 90;
                  float radians = 0.0f;
                  float num109 = 0.209439516f;
                  Vector2 spinningpoint = new Vector2(0.0f, -8f);
                  Vector2 vector2_12 = new Vector2(-3f);
                  float num110 = 0.0f;
                  float num111 = 0.005f;
                  switch (num107)
                  {
                    case 173:
                      num108 = 90;
                      break;
                    case 176:
                      num108 = 88;
                      break;
                    case 179:
                      num108 = 86;
                      break;
                  }
                  for (int index58 = 0; (double) index58 < (double) vector2_11.Length() / 6.0; ++index58)
                  {
                    Vector2 Position = player18.Center + 6f * (float) index58 * vec + spinningpoint.RotatedBy((double) radians) + vector2_12;
                    radians += num109;
                    int Type4 = num108;
                    Color newColor = new Color();
                    int index59 = Dust.NewDust(Position, 6, 6, Type4, Alpha: 100, newColor: newColor, Scale: 1.5f);
                    Main.dust[index59].noGravity = true;
                    Main.dust[index59].velocity = Vector2.Zero;
                    Main.dust[index59].fadeIn = (num110 += num111);
                    Main.dust[index59].velocity += vec * 1.5f;
                  }
                }
                player18.NebulaLevelup((int) num107);
              }
            }
            break;
          case 103:
            if (Main.netMode != 1)
              break;
            NPC.MaxMoonLordCountdown = this.reader.ReadInt32();
            NPC.MoonLordCountdown = this.reader.ReadInt32();
            break;
          case 104:
            if (Main.netMode != 1 || Main.npcShop <= 0)
              break;
            Item[] objArray = Main.instance.shop[Main.npcShop].item;
            int index60 = (int) this.reader.ReadByte();
            int type15 = (int) this.reader.ReadInt16();
            int num112 = (int) this.reader.ReadInt16();
            int prefixWeWant3 = (int) this.reader.ReadByte();
            int num113 = this.reader.ReadInt32();
            BitsByte bitsByte33 = (BitsByte) this.reader.ReadByte();
            if (index60 >= objArray.Length)
              break;
            objArray[index60] = new Item();
            objArray[index60].netDefaults(type15);
            objArray[index60].stack = num112;
            objArray[index60].Prefix(prefixWeWant3);
            objArray[index60].value = num113;
            objArray[index60].buyOnce = bitsByte33[0];
            break;
          case 105:
            if (Main.netMode == 1)
              break;
            int i2 = (int) this.reader.ReadInt16();
            int num114 = (int) this.reader.ReadInt16();
            bool flag15 = this.reader.ReadBoolean();
            int j = num114;
            int num115 = flag15 ? 1 : 0;
            WorldGen.ToggleGemLock(i2, j, num115 != 0);
            break;
          case 106:
            if (Main.netMode != 1)
              break;
            Utils.PoofOfSmoke(new HalfVector2()
            {
              PackedValue = this.reader.ReadUInt32()
            }.ToVector2());
            break;
          case 107:
            if (Main.netMode != 1)
              break;
            Color color4 = this.reader.ReadRGB();
            string text2 = NetworkText.Deserialize(this.reader).ToString();
            int num116 = (int) this.reader.ReadInt16();
            Color c = color4;
            int WidthLimit = num116;
            Main.NewTextMultiline(text2, c: c, WidthLimit: WidthLimit);
            break;
          case 108:
            if (Main.netMode != 1)
              break;
            int Damage = (int) this.reader.ReadInt16();
            float KnockBack = this.reader.ReadSingle();
            int x10 = (int) this.reader.ReadInt16();
            int y10 = (int) this.reader.ReadInt16();
            int angle = (int) this.reader.ReadInt16();
            int ammo = (int) this.reader.ReadInt16();
            int owner = (int) this.reader.ReadByte();
            if (owner != Main.myPlayer)
              break;
            WorldGen.ShootFromCannon(x10, y10, angle, ammo, Damage, KnockBack, owner, true);
            break;
          case 109:
            if (Main.netMode != 2)
              break;
            int x11 = (int) this.reader.ReadInt16();
            int num117 = (int) this.reader.ReadInt16();
            int x12 = (int) this.reader.ReadInt16();
            int y11 = (int) this.reader.ReadInt16();
            int num118 = (int) this.reader.ReadByte();
            int whoAmI3 = this.whoAmI;
            WiresUI.Settings.MultiToolMode toolMode = WiresUI.Settings.ToolMode;
            WiresUI.Settings.ToolMode = (WiresUI.Settings.MultiToolMode) num118;
            int y12 = num117;
            Wiring.MassWireOperation(new Point(x11, y12), new Point(x12, y11), Main.player[whoAmI3]);
            WiresUI.Settings.ToolMode = toolMode;
            break;
          case 110:
            if (Main.netMode != 1)
              break;
            int type16 = (int) this.reader.ReadInt16();
            int num119 = (int) this.reader.ReadInt16();
            int index61 = (int) this.reader.ReadByte();
            if (index61 != Main.myPlayer)
              break;
            Player player19 = Main.player[index61];
            for (int index62 = 0; index62 < num119; ++index62)
              player19.ConsumeItem(type16);
            player19.wireOperationsCooldown = 0;
            break;
          case 111:
            if (Main.netMode != 2)
              break;
            BirthdayParty.ToggleManualParty();
            break;
          case 112:
            int number39 = (int) this.reader.ReadByte();
            int num120 = this.reader.ReadInt32();
            int num121 = this.reader.ReadInt32();
            int num122 = (int) this.reader.ReadByte();
            int num123 = (int) this.reader.ReadInt16();
            switch (number39)
            {
              case 1:
                if (Main.netMode == 1)
                  WorldGen.TreeGrowFX(num120, num121, num122, num123);
                if (Main.netMode != 2)
                  return;
                NetMessage.TrySendData((int) num2, number: number39, number2: ((float) num120), number3: ((float) num121), number4: ((float) num122), number5: num123);
                return;
              case 2:
                NPC.FairyEffects(new Vector2((float) num120, (float) num121), num123);
                return;
              default:
                return;
            }
          case 113:
            int x13 = (int) this.reader.ReadInt16();
            int y13 = (int) this.reader.ReadInt16();
            if (Main.netMode != 2 || Main.snowMoon || Main.pumpkinMoon)
              break;
            if (DD2Event.WouldFailSpawningHere(x13, y13))
              DD2Event.FailureMessage(this.whoAmI);
            DD2Event.SummonCrystal(x13, y13, this.whoAmI);
            break;
          case 114:
            if (Main.netMode != 1)
              break;
            DD2Event.WipeEntities();
            break;
          case 115:
            int number40 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number40 = this.whoAmI;
            Main.player[number40].MinionAttackTargetNPC = (int) this.reader.ReadInt16();
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(115, ignoreClient: this.whoAmI, number: number40);
            break;
          case 116:
            if (Main.netMode != 1)
              break;
            DD2Event.TimeLeftBetweenWaves = this.reader.ReadInt32();
            break;
          case 117:
            int playerTargetIndex1 = (int) this.reader.ReadByte();
            if (Main.netMode == 2 && this.whoAmI != playerTargetIndex1 && (!Main.player[playerTargetIndex1].hostile || !Main.player[this.whoAmI].hostile))
              break;
            PlayerDeathReason playerDeathReason1 = PlayerDeathReason.FromReader(this.reader);
            int num124 = (int) this.reader.ReadInt16();
            int num125 = (int) this.reader.ReadByte() - 1;
            BitsByte bitsByte34 = (BitsByte) this.reader.ReadByte();
            bool flag16 = bitsByte34[0];
            bool pvp1 = bitsByte34[1];
            int num126 = (int) this.reader.ReadSByte();
            Main.player[playerTargetIndex1].Hurt(playerDeathReason1, num124, num125, pvp1, true, flag16, num126);
            if (Main.netMode != 2)
              break;
            NetMessage.SendPlayerHurt(playerTargetIndex1, playerDeathReason1, num124, num125, flag16, pvp1, num126, ignoreClient: this.whoAmI);
            break;
          case 118:
            int playerTargetIndex2 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              playerTargetIndex2 = this.whoAmI;
            PlayerDeathReason playerDeathReason2 = PlayerDeathReason.FromReader(this.reader);
            int num127 = (int) this.reader.ReadInt16();
            int num128 = (int) this.reader.ReadByte() - 1;
            bool pvp2 = ((BitsByte) this.reader.ReadByte())[0];
            Main.player[playerTargetIndex2].KillMe(playerDeathReason2, (double) num127, num128, pvp2);
            if (Main.netMode != 2)
              break;
            NetMessage.SendPlayerDeath(playerTargetIndex2, playerDeathReason2, num127, num128, pvp2, ignoreClient: this.whoAmI);
            break;
          case 119:
            if (Main.netMode != 1)
              break;
            int x14 = (int) this.reader.ReadSingle();
            int num129 = (int) this.reader.ReadSingle();
            Color color5 = this.reader.ReadRGB();
            NetworkText networkText = NetworkText.Deserialize(this.reader);
            int y14 = num129;
            CombatText.NewText(new Rectangle(x14, y14, 0, 0), color5, networkText.ToString());
            break;
          case 120:
            int index63 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index63 = this.whoAmI;
            int num130 = (int) this.reader.ReadByte();
            if (num130 < 0 || num130 >= EmoteID.Count || Main.netMode != 2)
              break;
            EmoteBubble.NewBubble(num130, new WorldUIAnchor((Entity) Main.player[index63]), 360);
            EmoteBubble.CheckForNPCsToReactToEmoteBubble(num130, Main.player[index63]);
            break;
          case 121:
            int num131 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num131 = this.whoAmI;
            int num132 = this.reader.ReadInt32();
            int num133 = (int) this.reader.ReadByte();
            bool dye1 = false;
            if (num133 >= 8)
            {
              dye1 = true;
              num133 -= 8;
            }
            TileEntity tileEntity2;
            if (!TileEntity.ByID.TryGetValue(num132, out tileEntity2))
            {
              this.reader.ReadInt32();
              int num134 = (int) this.reader.ReadByte();
              break;
            }
            if (num133 >= 8)
              tileEntity2 = (TileEntity) null;
            if (tileEntity2 is TEDisplayDoll teDisplayDoll)
            {
              teDisplayDoll.ReadItem(num133, this.reader, dye1);
            }
            else
            {
              this.reader.ReadInt32();
              int num135 = (int) this.reader.ReadByte();
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num2, ignoreClient: num131, number: num131, number2: ((float) num132), number3: ((float) num133), number4: ((float) dye1.ToInt()));
            break;
          case 122:
            int num136 = this.reader.ReadInt32();
            int number2_11 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number2_11 = this.whoAmI;
            if (Main.netMode == 2)
            {
              if (num136 == -1)
              {
                Main.player[number2_11].tileEntityAnchor.Clear();
                NetMessage.TrySendData((int) num2, number: num136, number2: ((float) number2_11));
                break;
              }
              TileEntity tileEntity3;
              if (!TileEntity.IsOccupied(num136, out int _) && TileEntity.ByID.TryGetValue(num136, out tileEntity3))
              {
                Main.player[number2_11].tileEntityAnchor.Set(num136, (int) tileEntity3.Position.X, (int) tileEntity3.Position.Y);
                NetMessage.TrySendData((int) num2, number: num136, number2: ((float) number2_11));
              }
            }
            if (Main.netMode != 1)
              break;
            if (num136 == -1)
            {
              Main.player[number2_11].tileEntityAnchor.Clear();
              break;
            }
            TileEntity tileEntity4;
            if (!TileEntity.ByID.TryGetValue(num136, out tileEntity4))
              break;
            TileEntity.SetInteractionAnchor(Main.player[number2_11], (int) tileEntity4.Position.X, (int) tileEntity4.Position.Y, num136);
            break;
          case 123:
            if (Main.netMode != 2)
              break;
            int x15 = (int) this.reader.ReadInt16();
            int num137 = (int) this.reader.ReadInt16();
            int num138 = (int) this.reader.ReadInt16();
            int num139 = (int) this.reader.ReadByte();
            int num140 = (int) this.reader.ReadInt16();
            int y15 = num137;
            int netid2 = num138;
            int prefix2 = num139;
            int stack2 = num140;
            TEWeaponsRack.TryPlacing(x15, y15, netid2, prefix2, stack2);
            break;
          case 124:
            int num141 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num141 = this.whoAmI;
            int num142 = this.reader.ReadInt32();
            int num143 = (int) this.reader.ReadByte();
            bool dye2 = false;
            if (num143 >= 2)
            {
              dye2 = true;
              num143 -= 2;
            }
            TileEntity tileEntity5;
            if (!TileEntity.ByID.TryGetValue(num142, out tileEntity5))
            {
              this.reader.ReadInt32();
              int num144 = (int) this.reader.ReadByte();
              break;
            }
            if (num143 >= 2)
              tileEntity5 = (TileEntity) null;
            if (tileEntity5 is TEHatRack teHatRack)
            {
              teHatRack.ReadItem(num143, this.reader, dye2);
            }
            else
            {
              this.reader.ReadInt32();
              int num145 = (int) this.reader.ReadByte();
            }
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num2, ignoreClient: num141, number: num141, number2: ((float) num142), number3: ((float) num143), number4: ((float) dye2.ToInt()));
            break;
          case 125:
            int num146 = (int) this.reader.ReadByte();
            int num147 = (int) this.reader.ReadInt16();
            int num148 = (int) this.reader.ReadInt16();
            int num149 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              num146 = this.whoAmI;
            if (Main.netMode == 1)
              Main.player[Main.myPlayer].GetOtherPlayersPickTile(num147, num148, num149);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(125, ignoreClient: num146, number: num146, number2: ((float) num147), number3: ((float) num148), number4: ((float) num149));
            break;
          case 126:
            if (Main.netMode != 1)
              break;
            NPC.RevengeManager.AddMarkerFromReader(this.reader);
            break;
          case 127:
            int markerUniqueID = this.reader.ReadInt32();
            if (Main.netMode != 1)
              break;
            NPC.RevengeManager.DestroyMarker(markerUniqueID);
            break;
          case 128:
            int num150 = (int) this.reader.ReadByte();
            int num151 = (int) this.reader.ReadUInt16();
            int num152 = (int) this.reader.ReadUInt16();
            int num153 = (int) this.reader.ReadUInt16();
            int num154 = (int) this.reader.ReadUInt16();
            if (Main.netMode == 2)
            {
              NetMessage.SendData(128, ignoreClient: num150, number: num150, number2: ((float) num153), number3: ((float) num154), number5: num151, number6: num152);
              break;
            }
            GolfHelper.ContactListener.PutBallInCup_TextAndEffects(new Point(num151, num152), num150, num153, num154);
            break;
          case 129:
            if (Main.netMode != 1)
              break;
            Main.FixUIScale();
            Main.TrySetPreparationState(Main.WorldPreparationState.ProcessingData);
            break;
          case 130:
            if (Main.netMode != 2)
              break;
            int num155 = (int) this.reader.ReadUInt16();
            int num156 = (int) this.reader.ReadUInt16();
            int Type5 = (int) this.reader.ReadInt16();
            if (Type5 == 682)
            {
              if (NPC.unlockedSlimeRedSpawn)
                break;
              NPC.unlockedSlimeRedSpawn = true;
              NetMessage.TrySendData(7);
            }
            int X = num155 * 16;
            int Y = num156 * 16;
            NPC npc6 = new NPC();
            npc6.SetDefaults(Type5);
            int type17 = npc6.type;
            int netId = npc6.netID;
            int number41 = NPC.NewNPC((IEntitySource) new EntitySource_FishedOut((Entity) Main.player[this.whoAmI]), X, Y, Type5);
            if (netId != type17)
            {
              Main.npc[number41].SetDefaults(netId);
              NetMessage.TrySendData(23, number: number41);
            }
            if (Type5 != 682)
              break;
            WorldGen.CheckAchievement_RealEstateAndTownSlimes();
            break;
          case 131:
            if (Main.netMode != 1)
              break;
            int index64 = (int) this.reader.ReadUInt16();
            NPC npc7 = index64 >= 200 ? new NPC() : Main.npc[index64];
            if (this.reader.ReadByte() != (byte) 1)
              break;
            int time3 = this.reader.ReadInt32();
            int fromWho = (int) this.reader.ReadInt16();
            npc7.GetImmuneTime(fromWho, time3);
            break;
          case 132:
            if (Main.netMode != 1)
              break;
            Point point = this.reader.ReadVector2().ToPoint();
            ushort key2 = this.reader.ReadUInt16();
            LegacySoundStyle legacySoundStyle = SoundID.SoundByIndex[key2];
            BitsByte bitsByte35 = (BitsByte) this.reader.ReadByte();
            int Style2 = !bitsByte35[0] ? legacySoundStyle.Style : this.reader.ReadInt32();
            float volumeScale = !bitsByte35[1] ? legacySoundStyle.Volume : MathHelper.Clamp(this.reader.ReadSingle(), 0.0f, 1f);
            float pitchOffset = !bitsByte35[2] ? legacySoundStyle.GetRandomPitch() : MathHelper.Clamp(this.reader.ReadSingle(), -1f, 1f);
            SoundEngine.PlaySound(legacySoundStyle.SoundId, point.X, point.Y, Style2, volumeScale, pitchOffset);
            break;
          case 133:
            if (Main.netMode != 2)
              break;
            int x16 = (int) this.reader.ReadInt16();
            int num157 = (int) this.reader.ReadInt16();
            int num158 = (int) this.reader.ReadInt16();
            int num159 = (int) this.reader.ReadByte();
            int num160 = (int) this.reader.ReadInt16();
            int y16 = num157;
            int netid3 = num158;
            int prefix3 = num159;
            int stack3 = num160;
            TEFoodPlatter.TryPlacing(x16, y16, netid3, prefix3, stack3);
            break;
          case 134:
            int index65 = (int) this.reader.ReadByte();
            int num161 = this.reader.ReadInt32();
            float num162 = this.reader.ReadSingle();
            byte num163 = this.reader.ReadByte();
            bool flag17 = this.reader.ReadBoolean();
            float num164 = this.reader.ReadSingle();
            float num165 = this.reader.ReadSingle();
            if (Main.netMode == 2)
              index65 = this.whoAmI;
            Player player20 = Main.player[index65];
            player20.ladyBugLuckTimeLeft = num161;
            player20.torchLuck = num162;
            player20.luckPotion = num163;
            player20.HasGardenGnomeNearby = flag17;
            player20.equipmentBasedLuckBonus = num164;
            player20.coinLuck = num165;
            player20.RecalculateLuck();
            if (Main.netMode != 2)
              break;
            NetMessage.SendData(134, ignoreClient: index65, number: index65);
            break;
          case 135:
            int index66 = (int) this.reader.ReadByte();
            if (Main.netMode != 1)
              break;
            Main.player[index66].immuneAlpha = (int) byte.MaxValue;
            break;
          case 136:
            for (int index67 = 0; index67 < 2; ++index67)
            {
              for (int index68 = 0; index68 < 3; ++index68)
                NPC.cavernMonsterType[index67, index68] = (int) this.reader.ReadUInt16();
            }
            break;
          case 137:
            if (Main.netMode != 2)
              break;
            int index69 = (int) this.reader.ReadInt16();
            int buffTypeToRemove = (int) this.reader.ReadUInt16();
            if (index69 < 0 || index69 >= 200)
              break;
            Main.npc[index69].RequestBuffRemoval(buffTypeToRemove);
            break;
          case 139:
            if (Main.netMode == 2)
              break;
            int index70 = (int) this.reader.ReadByte();
            bool flag18 = this.reader.ReadBoolean();
            Main.countsAsHostForGameplay[index70] = flag18;
            break;
          case 140:
            int num166 = (int) this.reader.ReadByte();
            int num167 = this.reader.ReadInt32();
            switch (num166)
            {
              case 0:
                if (Main.netMode != 1)
                  return;
                CreditsRollEvent.SetRemainingTimeDirect(num167);
                return;
              case 1:
                if (Main.netMode != 2)
                  return;
                NPC.TransformCopperSlime(num167);
                return;
              case 2:
                if (Main.netMode != 2)
                  return;
                NPC.TransformElderSlime(num167);
                return;
              default:
                return;
            }
          case 141:
            LucyAxeMessage.MessageSource messageSource = (LucyAxeMessage.MessageSource) this.reader.ReadByte();
            byte num168 = this.reader.ReadByte();
            Vector2 velocity = this.reader.ReadVector2();
            int num169 = this.reader.ReadInt32();
            int num170 = this.reader.ReadInt32();
            if (Main.netMode == 2)
            {
              NetMessage.SendData(141, ignoreClient: this.whoAmI, number: ((int) messageSource), number2: ((float) num168), number3: velocity.X, number4: velocity.Y, number5: num169, number6: num170);
              break;
            }
            LucyAxeMessage.CreateFromNet(messageSource, num168, new Vector2((float) num169, (float) num170), velocity);
            break;
          case 142:
            int number42 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              number42 = this.whoAmI;
            Player player21 = Main.player[number42];
            player21.piggyBankProjTracker.TryReading(this.reader);
            player21.voidLensChest.TryReading(this.reader);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData(142, ignoreClient: this.whoAmI, number: number42);
            break;
          case 143:
            if (Main.netMode != 2)
              break;
            DD2Event.AttemptToSkipWaitTime();
            break;
          case 144:
            if (Main.netMode != 2)
              break;
            NPC.HaveDryadDoStardewAnimation();
            break;
          case 146:
            switch (this.reader.ReadByte())
            {
              case 0:
                Item.ShimmerEffect(this.reader.ReadVector2());
                return;
              case 1:
                Vector2 coinPosition = this.reader.ReadVector2();
                int coinAmount = this.reader.ReadInt32();
                Main.player[Main.myPlayer].AddCoinLuck(coinPosition, coinAmount);
                return;
              case 2:
                int index71 = this.reader.ReadInt32();
                Main.npc[index71].SetNetShimmerEffect();
                return;
              default:
                return;
            }
          case 147:
            int index72 = (int) this.reader.ReadByte();
            if (Main.netMode == 2)
              index72 = this.whoAmI;
            int num171 = (int) this.reader.ReadByte();
            Main.player[index72].TrySwitchingLoadout(num171);
            MessageBuffer.ReadAccessoryVisibility(this.reader, Main.player[index72].hideVisibleAccessory);
            if (Main.netMode != 2)
              break;
            NetMessage.TrySendData((int) num2, ignoreClient: index72, number: index72, number2: ((float) num171));
            break;
          default:
            if (Netplay.Clients[this.whoAmI].State != 0)
              break;
            NetMessage.BootPlayer(this.whoAmI, Lang.mp[2].ToNetworkText());
            break;
        }
      }
    }

    private static void ReadAccessoryVisibility(BinaryReader reader, bool[] hideVisibleAccessory)
    {
      ushort num = reader.ReadUInt16();
      for (int index = 0; index < hideVisibleAccessory.Length; ++index)
        hideVisibleAccessory[index] = ((uint) num & (uint) (1 << index)) > 0U;
    }

    private static void TrySendingItemArray(int plr, Item[] array, int slotStartIndex)
    {
      for (int index = 0; index < array.Length; ++index)
        NetMessage.TrySendData(5, number: plr, number2: ((float) (slotStartIndex + index)), number3: ((float) array[index].prefix));
    }
  }
}
