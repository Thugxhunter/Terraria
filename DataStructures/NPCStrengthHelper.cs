// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.NPCStrengthHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.DataStructures
{
  public struct NPCStrengthHelper
  {
    private float _strengthOverride;
    private float _gameModeDifficulty;
    private GameModeData _gameModeData;

    public bool IsExpertMode => (double) this._strengthOverride >= 2.0 || (double) this._gameModeDifficulty >= 2.0;

    public bool IsMasterMode => (double) this._strengthOverride >= 3.0 || (double) this._gameModeDifficulty >= 3.0;

    public bool ExtraDamageForGetGoodWorld => (double) this._strengthOverride >= 4.0 || (double) this._gameModeDifficulty >= 4.0;

    public NPCStrengthHelper(GameModeData data, float strength, bool isGetGoodWorld)
    {
      this._strengthOverride = strength;
      this._gameModeData = data;
      this._gameModeDifficulty = 1f;
      if (this._gameModeData.IsExpertMode)
        ++this._gameModeDifficulty;
      if (this._gameModeData.IsMasterMode)
        ++this._gameModeDifficulty;
      if (!isGetGoodWorld)
        return;
      ++this._gameModeDifficulty;
    }
  }
}
