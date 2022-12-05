// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.EntrySorter`2
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System.Collections.Generic;
using Terraria.Localization;

namespace Terraria.DataStructures
{
  public class EntrySorter<TEntryType, TStepType> : IComparer<TEntryType>
    where TEntryType : new()
    where TStepType : IEntrySortStep<TEntryType>
  {
    public List<TStepType> Steps = new List<TStepType>();
    private int _prioritizedStep;

    public void AddSortSteps(List<TStepType> sortSteps) => this.Steps.AddRange((IEnumerable<TStepType>) sortSteps);

    public int Compare(TEntryType x, TEntryType y)
    {
      int num = 0;
      if (this._prioritizedStep != -1)
      {
        num = this.Steps[this._prioritizedStep].Compare(x, y);
        if (num != 0)
          return num;
      }
      for (int index = 0; index < this.Steps.Count; ++index)
      {
        if (index != this._prioritizedStep)
        {
          num = this.Steps[index].Compare(x, y);
          if (num != 0)
            return num;
        }
      }
      return num;
    }

    public void SetPrioritizedStepIndex(int index) => this._prioritizedStep = index;

    public string GetDisplayName() => Language.GetTextValue(this.Steps[this._prioritizedStep].GetDisplayNameKey());
  }
}
