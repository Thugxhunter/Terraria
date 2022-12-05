// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.FishingAttempt
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct FishingAttempt
  {
    public PlayerFishingConditions playerFishingConditions;
    public int X;
    public int Y;
    public int bobberType;
    public bool common;
    public bool uncommon;
    public bool rare;
    public bool veryrare;
    public bool legendary;
    public bool crate;
    public bool inLava;
    public bool inHoney;
    public int waterTilesCount;
    public int waterNeededToFish;
    public float waterQuality;
    public int chumsInWater;
    public int fishingLevel;
    public bool CanFishInLava;
    public float atmo;
    public int questFish;
    public int heightLevel;
    public int rolledItemDrop;
    public int rolledEnemySpawn;
  }
}
