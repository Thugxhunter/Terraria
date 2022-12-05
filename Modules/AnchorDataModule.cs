// Decompiled with JetBrains decompiler
// Type: Terraria.Modules.AnchorDataModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.DataStructures;

namespace Terraria.Modules
{
  public class AnchorDataModule
  {
    public AnchorData top;
    public AnchorData bottom;
    public AnchorData left;
    public AnchorData right;
    public bool wall;

    public AnchorDataModule(AnchorDataModule copyFrom = null)
    {
      if (copyFrom == null)
      {
        this.top = new AnchorData();
        this.bottom = new AnchorData();
        this.left = new AnchorData();
        this.right = new AnchorData();
        this.wall = false;
      }
      else
      {
        this.top = copyFrom.top;
        this.bottom = copyFrom.bottom;
        this.left = copyFrom.left;
        this.right = copyFrom.right;
        this.wall = copyFrom.wall;
      }
    }
  }
}
