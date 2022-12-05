// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.TexturePackWorkshopEntry
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.IO;

namespace Terraria.Social.Base
{
  public class TexturePackWorkshopEntry : AWorkshopEntry
  {
    public static string GetHeaderTextFor(
      ResourcePack resourcePack,
      ulong workshopEntryId,
      string[] tags,
      WorkshopItemPublicSettingId publicity,
      string previewImagePath)
    {
      return AWorkshopEntry.CreateHeaderJson("ResourcePack", workshopEntryId, tags, publicity, previewImagePath);
    }
  }
}
