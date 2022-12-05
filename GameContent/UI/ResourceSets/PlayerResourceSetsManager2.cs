// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.ResourceSets.PlayerResourceSetsManager2
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content;
using Terraria.DataStructures;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
  public class PlayerResourceSetsManager2 : SelectionHolder<IPlayerResourcesDisplaySet>
  {
    protected override void Configuration_Save(Preferences obj) => obj.Put("PlayerResourcesSet", (object) this.ActiveSelectionConfigKey);

    protected override void Configuration_OnLoad(Preferences obj) => this.ActiveSelectionConfigKey = Main.Configuration.Get<string>("PlayerResourcesSet", "New");

    protected override void PopulateOptionsAndLoadContent(AssetRequestMode mode)
    {
      this.Options["New"] = (IPlayerResourcesDisplaySet) new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
      this.Options["Default"] = (IPlayerResourcesDisplaySet) new ClassicPlayerResourcesDisplaySet("Default", "Default");
      this.Options["HorizontalBarsWithFullText"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
      this.Options["HorizontalBarsWithText"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
      this.Options["HorizontalBars"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
      this.Options["NewWithText"] = (IPlayerResourcesDisplaySet) new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
    }

    public void TryToHoverOverResources() => this.ActiveSelection.TryToHover();

    public void Draw() => this.ActiveSelection.Draw();
  }
}
