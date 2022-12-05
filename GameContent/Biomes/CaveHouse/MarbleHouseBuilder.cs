// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Biomes.CaveHouse.MarbleHouseBuilder
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent.Generation;
using Terraria.WorldBuilding;

namespace Terraria.GameContent.Biomes.CaveHouse
{
  public class MarbleHouseBuilder : HouseBuilder
  {
    public MarbleHouseBuilder(IEnumerable<Microsoft.Xna.Framework.Rectangle> rooms)
      : base(HouseType.Marble, rooms)
    {
      this.TileType = (ushort) 357;
      this.WallType = (ushort) 179;
      this.BeamType = (ushort) 561;
      this.PlatformStyle = 29;
      this.DoorStyle = 35;
      this.TableStyle = 34;
      this.WorkbenchStyle = 30;
      this.PianoStyle = 29;
      this.BookcaseStyle = 31;
      this.ChairStyle = 35;
      this.ChestStyle = 51;
    }

    protected override void AgeRoom(Microsoft.Xna.Framework.Rectangle room)
    {
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.6), (GenAction) new Modifiers.Blotches(chance: 0.6), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        this.TileType
      }), (GenAction) new Actions.SetTileKeepWall((ushort) 367, true)));
      WorldUtils.Gen(new Point(room.X + 1, room.Y), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.8), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X + 1, room.Y + room.Height - 1), (GenShape) new Shapes.Rectangle(room.Width - 2, 1), Actions.Chain((GenAction) new Modifiers.Dither(0.8), (GenAction) new Modifiers.OnlyTiles(new ushort[1]
      {
        (ushort) 367
      }), (GenAction) new Modifiers.Offset(0, 1), (GenAction) new ActionStalagtite()));
      WorldUtils.Gen(new Point(room.X, room.Y), (GenShape) new Shapes.Rectangle(room.Width, room.Height), Actions.Chain((GenAction) new Modifiers.Dither(0.85), (GenAction) new Modifiers.Blotches(), (GenAction) new Actions.PlaceWall((ushort) 178)));
    }
  }
}
