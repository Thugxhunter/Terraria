// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Creative.ICreativePower
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using System.IO;
using Terraria.UI;

namespace Terraria.GameContent.Creative
{
  public interface ICreativePower
  {
    ushort PowerId { get; set; }

    string ServerConfigName { get; set; }

    PowerPermissionLevel CurrentPermissionLevel { get; set; }

    PowerPermissionLevel DefaultPermissionLevel { get; set; }

    void DeserializeNetMessage(BinaryReader reader, int userId);

    void ProvidePowerButtons(CreativePowerUIElementRequestInfo info, List<UIElement> elements);

    bool GetIsUnlocked();
  }
}
