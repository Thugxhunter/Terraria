// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.AssetSourceController
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content;
using ReLogic.Content.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Audio;
using Terraria.IO;
using Terraria.Localization;

namespace Terraria.GameContent
{
  public class AssetSourceController
  {
    private readonly List<IContentSource> _staticSources;
    private readonly IAssetRepository _assetRepository;

    public event Action<ResourcePackList> OnResourcePackChange;

    public ResourcePackList ActiveResourcePackList { get; private set; }

    public AssetSourceController(
      IAssetRepository assetRepository,
      IEnumerable<IContentSource> staticSources)
    {
      this._assetRepository = assetRepository;
      this._staticSources = staticSources.ToList<IContentSource>();
      this.UseResourcePacks(new ResourcePackList());
    }

    public void Refresh()
    {
      foreach (ResourcePack allPack in this.ActiveResourcePackList.AllPacks)
        allPack.Refresh();
      this.UseResourcePacks(this.ActiveResourcePackList);
    }

    public void UseResourcePacks(ResourcePackList resourcePacks)
    {
      if (this.OnResourcePackChange != null)
        this.OnResourcePackChange(resourcePacks);
      this.ActiveResourcePackList = resourcePacks;
      List<IContentSource> icontentSourceList1 = new List<IContentSource>(resourcePacks.EnabledPacks.OrderBy<ResourcePack, int>((Func<ResourcePack, int>) (pack => pack.SortingOrder)).Select<ResourcePack, IContentSource>((Func<ResourcePack, IContentSource>) (pack => pack.GetContentSource())));
      icontentSourceList1.AddRange((IEnumerable<IContentSource>) this._staticSources);
      foreach (IContentSource icontentSource in icontentSourceList1)
        icontentSource.ClearRejections();
      List<IContentSource> icontentSourceList2 = new List<IContentSource>();
      for (int index = icontentSourceList1.Count - 1; index >= 0; --index)
        icontentSourceList2.Add(icontentSourceList1[index]);
      this._assetRepository.SetSources((IEnumerable<IContentSource>) icontentSourceList1, (AssetRequestMode) 1);
      LanguageManager.Instance.UseSources(icontentSourceList2);
      Main.audioSystem.UseSources(icontentSourceList2);
      SoundEngine.Reload();
      Main.changeTheTitle = true;
    }
  }
}
