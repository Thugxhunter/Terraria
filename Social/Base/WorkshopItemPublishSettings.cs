// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.WorkshopItemPublishSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Social.Base
{
  public class WorkshopItemPublishSettings
  {
    public WorkshopTagOption[] UsedTags = new WorkshopTagOption[0];
    public WorkshopItemPublicSettingId Publicity;
    public string PreviewImagePath;

    public string[] GetUsedTagsInternalNames() => ((IEnumerable<WorkshopTagOption>) this.UsedTags).Select<WorkshopTagOption, string>((Func<WorkshopTagOption, string>) (x => x.InternalNameForAPIs)).ToArray<string>();
  }
}
