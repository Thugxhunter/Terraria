// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Achievements.TileDestroyedCondition
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.Achievements;

namespace Terraria.GameContent.Achievements
{
  public class TileDestroyedCondition : AchievementCondition
  {
    private const string Identifier = "TILE_DESTROYED";
    private static Dictionary<ushort, List<TileDestroyedCondition>> _listeners = new Dictionary<ushort, List<TileDestroyedCondition>>();
    private static bool _isListenerHooked;
    private ushort[] _tileIds;

    private TileDestroyedCondition(ushort[] tileIds)
      : base("TILE_DESTROYED_" + (object) tileIds[0])
    {
      this._tileIds = tileIds;
      TileDestroyedCondition.ListenForDestruction(this);
    }

    private static void ListenForDestruction(TileDestroyedCondition condition)
    {
      if (!TileDestroyedCondition._isListenerHooked)
      {
        AchievementsHelper.OnTileDestroyed += new AchievementsHelper.TileDestroyedEvent(TileDestroyedCondition.TileDestroyedListener);
        TileDestroyedCondition._isListenerHooked = true;
      }
      for (int index = 0; index < condition._tileIds.Length; ++index)
      {
        if (!TileDestroyedCondition._listeners.ContainsKey(condition._tileIds[index]))
          TileDestroyedCondition._listeners[condition._tileIds[index]] = new List<TileDestroyedCondition>();
        TileDestroyedCondition._listeners[condition._tileIds[index]].Add(condition);
      }
    }

    private static void TileDestroyedListener(Player player, ushort tileId)
    {
      if (player.whoAmI != Main.myPlayer || !TileDestroyedCondition._listeners.ContainsKey(tileId))
        return;
      foreach (AchievementCondition achievementCondition in TileDestroyedCondition._listeners[tileId])
        achievementCondition.Complete();
    }

    public static AchievementCondition Create(params ushort[] tileIds) => (AchievementCondition) new TileDestroyedCondition(tileIds);
  }
}
