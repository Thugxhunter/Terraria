// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.PlayerDeathReason
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.IO;
using Terraria.Localization;

namespace Terraria.DataStructures
{
  public class PlayerDeathReason
  {
    private int _sourcePlayerIndex = -1;
    private int _sourceNPCIndex = -1;
    private int _sourceProjectileLocalIndex = -1;
    private int _sourceOtherIndex = -1;
    private int _sourceProjectileType;
    private int _sourceItemType;
    private int _sourceItemPrefix;
    private string _sourceCustomReason;

    public int? SourceProjectileType => this._sourceProjectileLocalIndex == -1 ? new int?() : new int?(this._sourceProjectileType);

    public bool TryGetCausingEntity(out Entity entity)
    {
      entity = (Entity) null;
      if (Main.npc.IndexInRange<NPC>(this._sourceNPCIndex))
      {
        entity = (Entity) Main.npc[this._sourceNPCIndex];
        return true;
      }
      if (Main.projectile.IndexInRange<Projectile>(this._sourceProjectileLocalIndex))
      {
        entity = (Entity) Main.projectile[this._sourceProjectileLocalIndex];
        return true;
      }
      if (!Main.player.IndexInRange<Player>(this._sourcePlayerIndex))
        return false;
      entity = (Entity) Main.player[this._sourcePlayerIndex];
      return true;
    }

    public static PlayerDeathReason LegacyEmpty() => new PlayerDeathReason()
    {
      _sourceOtherIndex = 254
    };

    public static PlayerDeathReason LegacyDefault() => new PlayerDeathReason()
    {
      _sourceOtherIndex = (int) byte.MaxValue
    };

    public static PlayerDeathReason ByNPC(int index) => new PlayerDeathReason()
    {
      _sourceNPCIndex = index
    };

    public static PlayerDeathReason ByCustomReason(string reasonInEnglish) => new PlayerDeathReason()
    {
      _sourceCustomReason = reasonInEnglish
    };

    public static PlayerDeathReason ByPlayer(int index) => new PlayerDeathReason()
    {
      _sourcePlayerIndex = index,
      _sourceItemType = Main.player[index].inventory[Main.player[index].selectedItem].type,
      _sourceItemPrefix = (int) Main.player[index].inventory[Main.player[index].selectedItem].prefix
    };

    public static PlayerDeathReason ByOther(int type) => new PlayerDeathReason()
    {
      _sourceOtherIndex = type
    };

    public static PlayerDeathReason ByProjectile(
      int playerIndex,
      int projectileIndex)
    {
      PlayerDeathReason playerDeathReason = new PlayerDeathReason()
      {
        _sourcePlayerIndex = playerIndex,
        _sourceProjectileLocalIndex = projectileIndex,
        _sourceProjectileType = Main.projectile[projectileIndex].type
      };
      if (playerIndex >= 0 && playerIndex <= (int) byte.MaxValue)
      {
        playerDeathReason._sourceItemType = Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].type;
        playerDeathReason._sourceItemPrefix = (int) Main.player[playerIndex].inventory[Main.player[playerIndex].selectedItem].prefix;
      }
      return playerDeathReason;
    }

    public NetworkText GetDeathText(string deadPlayerName) => this._sourceCustomReason != null ? NetworkText.FromLiteral(this._sourceCustomReason) : Lang.CreateDeathMessage(deadPlayerName, this._sourcePlayerIndex, this._sourceNPCIndex, this._sourceProjectileLocalIndex, this._sourceOtherIndex, this._sourceProjectileType, this._sourceItemType);

    public void WriteSelfTo(BinaryWriter writer)
    {
      BitsByte bitsByte = (BitsByte) (byte) 0;
      bitsByte[0] = this._sourcePlayerIndex != -1;
      bitsByte[1] = this._sourceNPCIndex != -1;
      bitsByte[2] = this._sourceProjectileLocalIndex != -1;
      bitsByte[3] = this._sourceOtherIndex != -1;
      bitsByte[4] = this._sourceProjectileType != 0;
      bitsByte[5] = this._sourceItemType != 0;
      bitsByte[6] = this._sourceItemPrefix != 0;
      bitsByte[7] = this._sourceCustomReason != null;
      writer.Write((byte) bitsByte);
      if (bitsByte[0])
        writer.Write((short) this._sourcePlayerIndex);
      if (bitsByte[1])
        writer.Write((short) this._sourceNPCIndex);
      if (bitsByte[2])
        writer.Write((short) this._sourceProjectileLocalIndex);
      if (bitsByte[3])
        writer.Write((byte) this._sourceOtherIndex);
      if (bitsByte[4])
        writer.Write((short) this._sourceProjectileType);
      if (bitsByte[5])
        writer.Write((short) this._sourceItemType);
      if (bitsByte[6])
        writer.Write((byte) this._sourceItemPrefix);
      if (!bitsByte[7])
        return;
      writer.Write(this._sourceCustomReason);
    }

    public static PlayerDeathReason FromReader(BinaryReader reader)
    {
      PlayerDeathReason playerDeathReason = new PlayerDeathReason();
      BitsByte bitsByte = (BitsByte) reader.ReadByte();
      if (bitsByte[0])
        playerDeathReason._sourcePlayerIndex = (int) reader.ReadInt16();
      if (bitsByte[1])
        playerDeathReason._sourceNPCIndex = (int) reader.ReadInt16();
      if (bitsByte[2])
        playerDeathReason._sourceProjectileLocalIndex = (int) reader.ReadInt16();
      if (bitsByte[3])
        playerDeathReason._sourceOtherIndex = (int) reader.ReadByte();
      if (bitsByte[4])
        playerDeathReason._sourceProjectileType = (int) reader.ReadInt16();
      if (bitsByte[5])
        playerDeathReason._sourceItemType = (int) reader.ReadInt16();
      if (bitsByte[6])
        playerDeathReason._sourceItemPrefix = (int) reader.ReadByte();
      if (bitsByte[7])
        playerDeathReason._sourceCustomReason = reader.ReadString();
      return playerDeathReason;
    }
  }
}
