﻿// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ItemDropRules.ItemDropRuleResolveAction
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.GameContent.ItemDropRules
{
  public delegate ItemDropAttemptResult ItemDropRuleResolveAction(
    IItemDropRule rule,
    DropAttemptInfo info);
}