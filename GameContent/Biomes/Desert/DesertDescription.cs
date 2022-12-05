// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.Desert.DesertDescription
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.Desert
{
  public class DesertDescription
  {
    public static readonly DesertDescription Invalid = new DesertDescription()
    {
      IsValid = false
    };
    private static readonly Vector2D DefaultBlockScale = new Vector2D(4.0, 2.0);
    private const int SCAN_PADDING = 5;

    public Microsoft.Xna.Framework.Rectangle CombinedArea { get; private set; }

    public Microsoft.Xna.Framework.Rectangle Desert { get; private set; }

    public Microsoft.Xna.Framework.Rectangle Hive { get; private set; }

    public Vector2D BlockScale { get; private set; }

    public int BlockColumnCount { get; private set; }

    public int BlockRowCount { get; private set; }

    public bool IsValid { get; private set; }

    public SurfaceMap Surface { get; private set; }

    private DesertDescription()
    {
    }

    public void UpdateSurfaceMap() => this.Surface = SurfaceMap.FromArea(this.CombinedArea.Left - 5, this.CombinedArea.Width + 10);

    public static DesertDescription CreateFromPlacement(Point origin)
    {
      Vector2D defaultBlockScale = DesertDescription.DefaultBlockScale;
      double num1 = (double) Main.maxTilesX / 4200.0;
      int num2 = (int) (80.0 * num1);
      int num3 = (int) ((WorldGen.genRand.NextDouble() * 0.5 + 1.5) * 170.0 * num1);
      if (WorldGen.remixWorldGen)
        num3 = (int) (340.0 * num1);
      int width = (int) (defaultBlockScale.X * (double) num2);
      int num4 = (int) (defaultBlockScale.Y * (double) num3);
      origin.X -= width / 2;
      SurfaceMap surfaceMap = SurfaceMap.FromArea(origin.X - 5, width + 10);
      if (DesertDescription.RowHasInvalidTiles(origin.X, surfaceMap.Bottom, width))
        return DesertDescription.Invalid;
      int y = (int) (surfaceMap.Average + (double) surfaceMap.Bottom) / 2;
      origin.Y = y + WorldGen.genRand.Next(40, 60);
      int num5 = 0;
      if (Main.tenthAnniversaryWorld)
        num5 = (int) (20.0 * num1);
      return new DesertDescription()
      {
        CombinedArea = new Microsoft.Xna.Framework.Rectangle(origin.X, y, width, origin.Y + num4 - y),
        Hive = new Microsoft.Xna.Framework.Rectangle(origin.X, origin.Y + num5, width, num4 - num5),
        Desert = new Microsoft.Xna.Framework.Rectangle(origin.X, y, width, origin.Y + num4 / 2 - y + num5),
        BlockScale = defaultBlockScale,
        BlockColumnCount = num2,
        BlockRowCount = num3,
        Surface = surfaceMap,
        IsValid = true
      };
    }

    private static bool RowHasInvalidTiles(int startX, int startY, int width)
    {
      if (GenVars.skipDesertTileCheck)
        return false;
      for (int index = startX; index < startX + width; ++index)
      {
        switch (Main.tile[index, startY].type)
        {
          case 59:
          case 60:
            return true;
          case 147:
          case 161:
            return true;
          default:
            continue;
        }
      }
      return false;
    }
  }
}
