// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.SelectionHolder`1
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.IO;

namespace Terraria.DataStructures
{
  public abstract class SelectionHolder<TCycleType> where TCycleType : class, IConfigKeyHolder
  {
    protected Dictionary<string, TCycleType> Options = new Dictionary<string, TCycleType>();
    protected TCycleType ActiveSelection;
    protected string ActiveSelectionConfigKey;
    protected bool LoadedContent;

    public string ActiveSelectionKeyName { get; private set; }

    public void BindTo(Preferences preferences)
    {
      preferences.OnLoad += new Action<Preferences>(this.Wrapped_Configuration_OnLoad);
      preferences.OnSave += new Action<Preferences>(this.Configuration_Save);
    }

    protected abstract void Configuration_Save(Preferences obj);

    protected abstract void Configuration_OnLoad(Preferences obj);

    protected void Wrapped_Configuration_OnLoad(Preferences obj)
    {
      this.Configuration_OnLoad(obj);
      if (!this.LoadedContent)
        return;
      this.SetActiveMinimapFromLoadedConfigKey();
    }

    protected abstract void PopulateOptionsAndLoadContent(AssetRequestMode mode);

    public void LoadContent(AssetRequestMode mode)
    {
      this.PopulateOptionsAndLoadContent(mode);
      this.LoadedContent = true;
      this.SetActiveMinimapFromLoadedConfigKey();
    }

    public void CycleSelection()
    {
      TCycleType lastFrame = default (TCycleType);
      this.Options.Values.FirstOrDefault<TCycleType>((Func<TCycleType, bool>) (frame =>
      {
        if ((object) frame == (object) this.ActiveSelection)
          return true;
        lastFrame = frame;
        return false;
      }));
      if ((object) lastFrame == null)
        lastFrame = this.Options.Values.Last<TCycleType>();
      this.SetActiveFrame(lastFrame);
    }

    public void SetActiveMinimapFromLoadedConfigKey() => this.SetActiveFrame(this.ActiveSelectionConfigKey);

    private void SetActiveFrame(string frameName) => this.SetActiveFrame(this.Options.FirstOrDefault<KeyValuePair<string, TCycleType>>((Func<KeyValuePair<string, TCycleType>, bool>) (pair => pair.Key == frameName)).Value ?? this.Options.Values.First<TCycleType>());

    private void SetActiveFrame(TCycleType frame)
    {
      this.ActiveSelection = frame;
      this.ActiveSelectionConfigKey = frame.ConfigKey;
      this.ActiveSelectionKeyName = frame.NameKey;
    }
  }
}
