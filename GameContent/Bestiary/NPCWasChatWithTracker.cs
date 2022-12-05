// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Bestiary.NPCWasChatWithTracker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using System.IO;
using Terraria.GameContent.NetModules;
using Terraria.ID;
using Terraria.Net;

namespace Terraria.GameContent.Bestiary
{
  public class NPCWasChatWithTracker : IPersistentPerWorldContent, IOnPlayerJoining
  {
    private object _entryCreationLock = new object();
    private HashSet<string> _chattedWithPlayer;

    public NPCWasChatWithTracker() => this._chattedWithPlayer = new HashSet<string>();

    public void RegisterChatStartWith(NPC npc)
    {
      string bestiaryCreditId = npc.GetBestiaryCreditId();
      bool flag = !this._chattedWithPlayer.Contains(bestiaryCreditId);
      lock (this._entryCreationLock)
        this._chattedWithPlayer.Add(bestiaryCreditId);
      if (!(Main.netMode == 2 & flag))
        return;
      NetManager.Instance.Broadcast(NetBestiaryModule.SerializeChat(npc.netID));
    }

    public void SetWasChatWithDirectly(string persistentId)
    {
      lock (this._entryCreationLock)
        this._chattedWithPlayer.Add(persistentId);
    }

    public bool GetWasChatWith(NPC npc) => this._chattedWithPlayer.Contains(npc.GetBestiaryCreditId());

    public bool GetWasChatWith(string persistentId) => this._chattedWithPlayer.Contains(persistentId);

    public void Save(BinaryWriter writer)
    {
      lock (this._entryCreationLock)
      {
        writer.Write(this._chattedWithPlayer.Count);
        foreach (string str in this._chattedWithPlayer)
          writer.Write(str);
      }
    }

    public void Load(BinaryReader reader, int gameVersionSaveWasMadeOn)
    {
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        this._chattedWithPlayer.Add(reader.ReadString());
    }

    public void ValidateWorld(BinaryReader reader, int gameVersionSaveWasMadeOn)
    {
      int num = reader.ReadInt32();
      for (int index = 0; index < num; ++index)
        reader.ReadString();
    }

    public void Reset() => this._chattedWithPlayer.Clear();

    public void OnPlayerJoining(int playerIndex)
    {
      foreach (string key in this._chattedWithPlayer)
      {
        int npcNetId;
        if (ContentSamples.NpcNetIdsByPersistentIds.TryGetValue(key, out npcNetId))
          NetManager.Instance.SendToClient(NetBestiaryModule.SerializeChat(npcNetId), playerIndex);
      }
    }
  }
}
