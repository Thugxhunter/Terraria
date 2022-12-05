// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.WorldWorkshopEntry
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.IO;

namespace Terraria.Social.Base
{
  public class WorldWorkshopEntry : AWorkshopEntry
  {
    public static string GetHeaderTextFor(
      WorldFileData world,
      ulong workshopEntryId,
      string[] tags,
      WorkshopItemPublicSettingId publicity,
      string previewImagePath)
    {
      return AWorkshopEntry.CreateHeaderJson("World", workshopEntryId, tags, publicity, previewImagePath);
    }
  }
}
