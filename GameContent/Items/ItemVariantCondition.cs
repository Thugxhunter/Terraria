// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Items.ItemVariantCondition
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Terraria.Localization;

namespace Terraria.GameContent.Items
{
  public class ItemVariantCondition
  {
    public readonly NetworkText Description;
    public readonly ItemVariantCondition.Condition IsMet;

    public ItemVariantCondition(NetworkText description, ItemVariantCondition.Condition condition)
    {
      this.Description = description;
      this.IsMet = condition;
    }

    public override string ToString() => this.Description.ToString();

    public delegate bool Condition();
  }
}
