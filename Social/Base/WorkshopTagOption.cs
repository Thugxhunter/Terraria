// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.WorkshopTagOption
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Social.Base
{
  public class WorkshopTagOption
  {
    public readonly string NameKey;
    public readonly string InternalNameForAPIs;

    public WorkshopTagOption(string nameKey, string internalName)
    {
      this.NameKey = nameKey;
      this.InternalNameForAPIs = internalName;
    }
  }
}
