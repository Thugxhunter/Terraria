// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ChumBucketProjectileHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.GameContent
{
  public class ChumBucketProjectileHelper
  {
    private Dictionary<Point, int> _chumCountsPendingForThisFrame = new Dictionary<Point, int>();
    private Dictionary<Point, int> _chumCountsFromLastFrame = new Dictionary<Point, int>();

    public void OnPreUpdateAllProjectiles()
    {
      Utils.Swap<Dictionary<Point, int>>(ref this._chumCountsPendingForThisFrame, ref this._chumCountsFromLastFrame);
      this._chumCountsPendingForThisFrame.Clear();
    }

    public void AddChumLocation(Vector2 spot)
    {
      Point tileCoordinates = spot.ToTileCoordinates();
      int num1 = 0;
      this._chumCountsPendingForThisFrame.TryGetValue(tileCoordinates, out num1);
      int num2 = num1 + 1;
      this._chumCountsPendingForThisFrame[tileCoordinates] = num2;
    }

    public int GetChumsInLocation(Point tileCoords)
    {
      int chumsInLocation = 0;
      this._chumCountsFromLastFrame.TryGetValue(tileCoords, out chumsInLocation);
      return chumsInLocation;
    }
  }
}
