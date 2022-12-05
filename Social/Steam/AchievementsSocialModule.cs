// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.AchievementsSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Terraria.Social.Steam
{
  public class AchievementsSocialModule : Terraria.Social.Base.AchievementsSocialModule
  {
    private const string FILE_NAME = "/achievements-steam.dat";
    private Callback<UserStatsReceived_t> _userStatsReceived;
    private bool _areStatsReceived;
    private Dictionary<string, int> _intStatCache = new Dictionary<string, int>();
    private Dictionary<string, float> _floatStatCache = new Dictionary<string, float>();

    public override void Initialize()
    {
      // ISSUE: method pointer
      this._userStatsReceived = Callback<UserStatsReceived_t>.Create(new Callback<UserStatsReceived_t>.DispatchDelegate((object) this, __methodptr(OnUserStatsReceived)));
      SteamUserStats.RequestCurrentStats();
      while (!this._areStatsReceived)
      {
        CoreSocialModule.Pulse();
        Thread.Sleep(10);
      }
    }

    public override void Shutdown()
    {
      this._userStatsReceived.Unregister();
      this.StoreStats();
    }

    public override bool IsAchievementCompleted(string name)
    {
      bool flag;
      return SteamUserStats.GetAchievement(name, ref flag) & flag;
    }

    public override byte[] GetEncryptionKey()
    {
      byte[] destinationArray = new byte[16];
      byte[] bytes = BitConverter.GetBytes(SteamUser.GetSteamID().m_SteamID);
      Array.Copy((Array) bytes, (Array) destinationArray, 8);
      Array.Copy((Array) bytes, 0, (Array) destinationArray, 8, 8);
      return destinationArray;
    }

    public override string GetSavePath() => "/achievements-steam.dat";

    private int GetIntStat(string name)
    {
      int intStat;
      if (this._intStatCache.TryGetValue(name, out intStat) || !SteamUserStats.GetStat(name, ref intStat))
        return intStat;
      this._intStatCache.Add(name, intStat);
      return intStat;
    }

    private float GetFloatStat(string name)
    {
      float floatStat;
      if (this._floatStatCache.TryGetValue(name, out floatStat) || !SteamUserStats.GetStat(name, ref floatStat))
        return floatStat;
      this._floatStatCache.Add(name, floatStat);
      return floatStat;
    }

    private bool SetFloatStat(string name, float value)
    {
      this._floatStatCache[name] = value;
      return SteamUserStats.SetStat(name, value);
    }

    public override void UpdateIntStat(string name, int value)
    {
      if (this.GetIntStat(name) >= value)
        return;
      this.SetIntStat(name, value);
    }

    private bool SetIntStat(string name, int value)
    {
      this._intStatCache[name] = value;
      return SteamUserStats.SetStat(name, value);
    }

    public override void UpdateFloatStat(string name, float value)
    {
      if ((double) this.GetFloatStat(name) >= (double) value)
        return;
      this.SetFloatStat(name, value);
    }

    public override void StoreStats() => SteamUserStats.StoreStats();

    public override void CompleteAchievement(string name) => SteamUserStats.SetAchievement(name);

    private void OnUserStatsReceived(UserStatsReceived_t results)
    {
      if (results.m_nGameID != 105600UL || !CSteamID.op_Equality(results.m_steamIDUser, SteamUser.GetSteamID()))
        return;
      this._areStatsReceived = true;
    }
  }
}
