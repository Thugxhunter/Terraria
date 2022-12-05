// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.ResourceSets.PlayerResourceSetsManager
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.IO;

namespace Terraria.GameContent.UI.ResourceSets
{
  public class PlayerResourceSetsManager
  {
    private Dictionary<string, IPlayerResourcesDisplaySet> _sets = new Dictionary<string, IPlayerResourcesDisplaySet>();
    private IPlayerResourcesDisplaySet _activeSet;
    private string _activeSetConfigKey;
    private bool _loadedContent;

    public string ActiveSetKeyName { get; private set; }

    public void BindTo(Preferences preferences)
    {
      preferences.OnLoad += new Action<Preferences>(this.Configuration_OnLoad);
      preferences.OnSave += new Action<Preferences>(this.Configuration_OnSave);
    }

    private void Configuration_OnLoad(Preferences obj)
    {
      this._activeSetConfigKey = obj.Get<string>("PlayerResourcesSet", "New");
      if (!this._loadedContent)
        return;
      this.SetActiveFromLoadedConfigKey();
    }

    private void Configuration_OnSave(Preferences obj) => obj.Put("PlayerResourcesSet", (object) this._activeSetConfigKey);

    public void LoadContent(AssetRequestMode mode)
    {
      this._sets["New"] = (IPlayerResourcesDisplaySet) new FancyClassicPlayerResourcesDisplaySet("New", "New", "FancyClassic", mode);
      this._sets["Default"] = (IPlayerResourcesDisplaySet) new ClassicPlayerResourcesDisplaySet("Default", "Default");
      this._sets["HorizontalBarsWithFullText"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithFullText", "HorizontalBarsWithFullText", "HorizontalBars", mode);
      this._sets["HorizontalBarsWithText"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBarsWithText", "HorizontalBarsWithText", "HorizontalBars", mode);
      this._sets["HorizontalBars"] = (IPlayerResourcesDisplaySet) new HorizontalBarsPlayerResourcesDisplaySet("HorizontalBars", "HorizontalBars", "HorizontalBars", mode);
      this._sets["NewWithText"] = (IPlayerResourcesDisplaySet) new FancyClassicPlayerResourcesDisplaySet("NewWithText", "NewWithText", "FancyClassic", mode);
      this._loadedContent = true;
      this.SetActiveFromLoadedConfigKey();
    }

    public void SetActiveFromLoadedConfigKey() => this.SetActive(this._activeSetConfigKey);

    private void SetActive(string name) => this.SetActiveFrame(this._sets.FirstOrDefault<KeyValuePair<string, IPlayerResourcesDisplaySet>>((Func<KeyValuePair<string, IPlayerResourcesDisplaySet>, bool>) (pair => pair.Key == name)).Value ?? this._sets.Values.First<IPlayerResourcesDisplaySet>());

    private void SetActiveFrame(IPlayerResourcesDisplaySet set)
    {
      this._activeSet = set;
      this._activeSetConfigKey = set.ConfigKey;
      this.ActiveSetKeyName = set.NameKey;
    }

    public void TryToHoverOverResources() => this._activeSet.TryToHover();

    public void Draw() => this._activeSet.Draw();

    public void CycleResourceSet()
    {
      IPlayerResourcesDisplaySet lastFrame = (IPlayerResourcesDisplaySet) null;
      this._sets.Values.FirstOrDefault<IPlayerResourcesDisplaySet>((Func<IPlayerResourcesDisplaySet, bool>) (frame =>
      {
        if (frame == this._activeSet)
          return true;
        lastFrame = frame;
        return false;
      }));
      if (lastFrame == null)
        lastFrame = this._sets.Values.Last<IPlayerResourcesDisplaySet>();
      this.SetActiveFrame(lastFrame);
    }
  }
}
