// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.FileBrowser.ExtensionFilter
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Utilities.FileBrowser
{
  public struct ExtensionFilter
  {
    public string Name;
    public string[] Extensions;

    public ExtensionFilter(string filterName, params string[] filterExtensions)
    {
      this.Name = filterName;
      this.Extensions = filterExtensions;
    }
  }
}
