// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.TilePlacementHooksModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;

namespace Terraria.Modules
{
  public class TilePlacementHooksModule
  {
    public PlacementHook check;
    public PlacementHook postPlaceEveryone;
    public PlacementHook postPlaceMyPlayer;
    public PlacementHook placeOverride;

    public TilePlacementHooksModule(TilePlacementHooksModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.check = new PlacementHook();
        this.postPlaceEveryone = new PlacementHook();
        this.postPlaceMyPlayer = new PlacementHook();
        this.placeOverride = new PlacementHook();
      }
      else
      {
        this.check = copyFrom.check;
        this.postPlaceEveryone = copyFrom.postPlaceEveryone;
        this.postPlaceMyPlayer = copyFrom.postPlaceMyPlayer;
        this.placeOverride = copyFrom.placeOverride;
      }
    }
  }
}
