// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.TownRoomManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.ID;

namespace Terraria.GameContent
{
  public class TownRoomManager
  {
    public static object EntityCreationLock = new object();
    private List<Tuple<int, Point>> _roomLocationPairs = new List<Tuple<int, Point>>();
    private bool[] _hasRoom = new bool[(int) NPCID.Count];

    public void AddOccupantsToList(int x, int y, List<int> occupantsList) => this.AddOccupantsToList(new Point(x, y), occupantsList);

    public void AddOccupantsToList(Point tilePosition, List<int> occupants)
    {
      foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
      {
        if (roomLocationPair.Item2 == tilePosition)
          occupants.Add(roomLocationPair.Item1);
      }
    }

    public bool HasRoomQuick(int npcID) => this._hasRoom[npcID];

    public bool HasRoom(int npcID, out Point roomPosition)
    {
      if (!this._hasRoom[npcID])
      {
        roomPosition = new Point(0, 0);
        return false;
      }
      foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
      {
        if (roomLocationPair.Item1 == npcID)
        {
          roomPosition = roomLocationPair.Item2;
          return true;
        }
      }
      roomPosition = new Point(0, 0);
      return false;
    }

    public void SetRoom(int npcID, int x, int y)
    {
      this._hasRoom[npcID] = true;
      this.SetRoom(npcID, new Point(x, y));
    }

    public void SetRoom(int npcID, Point pt)
    {
      lock (TownRoomManager.EntityCreationLock)
      {
        this._roomLocationPairs.RemoveAll((Predicate<Tuple<int, Point>>) (x => x.Item1 == npcID));
        this._roomLocationPairs.Add(Tuple.Create<int, Point>(npcID, pt));
      }
    }

    public void KickOut(NPC n)
    {
      this.KickOut(n.type);
      this._hasRoom[n.type] = false;
    }

    public void KickOut(int npcType)
    {
      lock (TownRoomManager.EntityCreationLock)
        this._roomLocationPairs.RemoveAll((Predicate<Tuple<int, Point>>) (x => x.Item1 == npcType));
    }

    public void DisplayRooms()
    {
      foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
        Dust.QuickDust(roomLocationPair.Item2, Main.hslToRgb((float) ((double) roomLocationPair.Item1 * 0.05000000074505806 % 1.0), 1f, 0.5f));
    }

    public void Save(BinaryWriter writer)
    {
      lock (TownRoomManager.EntityCreationLock)
      {
        writer.Write(this._roomLocationPairs.Count);
        foreach (Tuple<int, Point> roomLocationPair in this._roomLocationPairs)
        {
          writer.Write(roomLocationPair.Item1);
          writer.Write(roomLocationPair.Item2.X);
          writer.Write(roomLocationPair.Item2.Y);
        }
      }
    }

    public void Load(BinaryReader reader)
    {
      this.Clear();
      int num = reader.ReadInt32();
      for (int index1 = 0; index1 < num; ++index1)
      {
        int index2 = reader.ReadInt32();
        Point point = new Point(reader.ReadInt32(), reader.ReadInt32());
        this._roomLocationPairs.Add(Tuple.Create<int, Point>(index2, point));
        this._hasRoom[index2] = true;
      }
    }

    public void Clear()
    {
      this._roomLocationPairs.Clear();
      for (int index = 0; index < this._hasRoom.Length; ++index)
        this._hasRoom[index] = false;
    }

    public byte GetHouseholdStatus(NPC n)
    {
      byte householdStatus = 0;
      if (n.homeless)
        householdStatus = (byte) 1;
      else if (this.HasRoomQuick(n.type))
        householdStatus = (byte) 2;
      return householdStatus;
    }

    public bool CanNPCsLiveWithEachOther(int npc1ByType, NPC npc2)
    {
      NPC npc1;
      return !ContentSamples.NpcsByNetId.TryGetValue(npc1ByType, out npc1) || this.CanNPCsLiveWithEachOther(npc1, npc2);
    }

    public bool CanNPCsLiveWithEachOther(NPC npc1, NPC npc2) => npc1.housingCategory != npc2.housingCategory;

    public bool CanNPCsLiveWithEachOther_ShopHelper(NPC npc1, NPC npc2) => this.CanNPCsLiveWithEachOther(npc1, npc2);
  }
}
