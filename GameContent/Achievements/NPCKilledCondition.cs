// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.NPCKilledCondition
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class NPCKilledCondition : AchievementCondition
  {
    private const string Identifier = "NPC_KILLED";
    private static Dictionary<short, List<NPCKilledCondition>> _listeners = new Dictionary<short, List<NPCKilledCondition>>();
    private static bool _isListenerHooked;
    private short[] _npcIds;

    private NPCKilledCondition(short npcId)
      : base("NPC_KILLED_" + (object) npcId)
    {
      this._npcIds = new short[1]{ npcId };
      NPCKilledCondition.ListenForPickup(this);
    }

    private NPCKilledCondition(short[] npcIds)
      : base("NPC_KILLED_" + (object) npcIds[0])
    {
      this._npcIds = npcIds;
      NPCKilledCondition.ListenForPickup(this);
    }

    private static void ListenForPickup(NPCKilledCondition condition)
    {
      if (!NPCKilledCondition._isListenerHooked)
      {
        AchievementsHelper.OnNPCKilled += new AchievementsHelper.NPCKilledEvent(NPCKilledCondition.NPCKilledListener);
        NPCKilledCondition._isListenerHooked = true;
      }
      for (int index = 0; index < condition._npcIds.Length; ++index)
      {
        if (!NPCKilledCondition._listeners.ContainsKey(condition._npcIds[index]))
          NPCKilledCondition._listeners[condition._npcIds[index]] = new List<NPCKilledCondition>();
        NPCKilledCondition._listeners[condition._npcIds[index]].Add(condition);
      }
    }

    private static void NPCKilledListener(Player player, short npcId)
    {
      if (player.whoAmI != Main.myPlayer || !NPCKilledCondition._listeners.ContainsKey(npcId))
        return;
      foreach (AchievementCondition achievementCondition in NPCKilledCondition._listeners[npcId])
        achievementCondition.Complete();
    }

    public static AchievementCondition Create(params short[] npcIds) => (AchievementCondition) new NPCKilledCondition(npcIds);

    public static AchievementCondition Create(short npcId) => (AchievementCondition) new NPCKilledCondition(npcId);

    public static AchievementCondition[] CreateMany(params short[] npcs)
    {
      AchievementCondition[] many = new AchievementCondition[npcs.Length];
      for (int index = 0; index < npcs.Length; ++index)
        many[index] = (AchievementCondition) new NPCKilledCondition(npcs[index]);
      return many;
    }
  }
}
