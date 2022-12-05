// Decompiled with JetBrains decompiler
// Type: Terraria.IO.ResourcePackList
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.Social;
using Terraria.Social.Base;

namespace Terraria.IO
{
  public class ResourcePackList
  {
    private readonly List<ResourcePack> _resourcePacks = new List<ResourcePack>();

    public IEnumerable<ResourcePack> EnabledPacks => (IEnumerable<ResourcePack>) this._resourcePacks.Where<ResourcePack>((Func<ResourcePack, bool>) (pack => pack.IsEnabled)).OrderBy<ResourcePack, int>((Func<ResourcePack, int>) (pack => pack.SortingOrder)).ThenBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.Name)).ThenBy<ResourcePack, ResourcePackVersion>((Func<ResourcePack, ResourcePackVersion>) (pack => pack.Version)).ThenBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.FileName));

    public IEnumerable<ResourcePack> DisabledPacks => (IEnumerable<ResourcePack>) this._resourcePacks.Where<ResourcePack>((Func<ResourcePack, bool>) (pack => !pack.IsEnabled)).OrderBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.Name)).ThenBy<ResourcePack, ResourcePackVersion>((Func<ResourcePack, ResourcePackVersion>) (pack => pack.Version)).ThenBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.FileName));

    public IEnumerable<ResourcePack> AllPacks => (IEnumerable<ResourcePack>) this._resourcePacks.OrderBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.Name)).ThenBy<ResourcePack, ResourcePackVersion>((Func<ResourcePack, ResourcePackVersion>) (pack => pack.Version)).ThenBy<ResourcePack, string>((Func<ResourcePack, string>) (pack => pack.FileName));

    public ResourcePackList()
    {
    }

    public ResourcePackList(IEnumerable<ResourcePack> resourcePacks) => this._resourcePacks.AddRange(resourcePacks);

    public JArray ToJson()
    {
      List<ResourcePackList.ResourcePackEntry> resourcePackEntryList = new List<ResourcePackList.ResourcePackEntry>(this._resourcePacks.Count);
      resourcePackEntryList.AddRange(this._resourcePacks.Select<ResourcePack, ResourcePackList.ResourcePackEntry>((Func<ResourcePack, ResourcePackList.ResourcePackEntry>) (pack => new ResourcePackList.ResourcePackEntry(pack.FileName, pack.IsEnabled, pack.SortingOrder))));
      return JArray.FromObject((object) resourcePackEntryList);
    }

    public static ResourcePackList FromJson(
      JArray serializedState,
      IServiceProvider services,
      string searchPath)
    {
      if (!Directory.Exists(searchPath))
        return new ResourcePackList();
      List<ResourcePack> resourcePacks = new List<ResourcePack>();
      ResourcePackList.CreatePacksFromSavedJson(serializedState, services, searchPath, resourcePacks);
      ResourcePackList.CreatePacksFromZips(services, searchPath, resourcePacks);
      ResourcePackList.CreatePacksFromDirectories(services, searchPath, resourcePacks);
      ResourcePackList.CreatePacksFromWorkshopFolders(services, resourcePacks);
      return new ResourcePackList((IEnumerable<ResourcePack>) resourcePacks);
    }

    public static ResourcePackList Publishable(
      JArray serializedState,
      IServiceProvider services,
      string searchPath)
    {
      if (!Directory.Exists(searchPath))
        return new ResourcePackList();
      List<ResourcePack> resourcePacks = new List<ResourcePack>();
      ResourcePackList.CreatePacksFromZips(services, searchPath, resourcePacks);
      ResourcePackList.CreatePacksFromDirectories(services, searchPath, resourcePacks);
      return new ResourcePackList((IEnumerable<ResourcePack>) resourcePacks);
    }

    private static void CreatePacksFromSavedJson(
      JArray serializedState,
      IServiceProvider services,
      string searchPath,
      List<ResourcePack> resourcePacks)
    {
      foreach (ResourcePackList.ResourcePackEntry resourcePackEntry in ResourcePackList.CreatePackEntryListFromJson(serializedState))
      {
        if (resourcePackEntry.FileName != null)
        {
          string path = Path.Combine(searchPath, resourcePackEntry.FileName);
          try
          {
            bool flag = File.Exists(path) || Directory.Exists(path);
            ResourcePack.BrandingType branding = ResourcePack.BrandingType.None;
            string fullPathFound;
            if (!flag && SocialAPI.Workshop != null && SocialAPI.Workshop.TryGetPath(resourcePackEntry.FileName, out fullPathFound))
            {
              path = fullPathFound;
              flag = true;
              branding = SocialAPI.Workshop.Branding.ResourcePackBrand;
            }
            if (flag)
            {
              ResourcePack resourcePack = new ResourcePack(services, path, branding)
              {
                IsEnabled = resourcePackEntry.Enabled,
                SortingOrder = resourcePackEntry.SortingOrder
              };
              resourcePacks.Add(resourcePack);
            }
          }
          catch (Exception ex)
          {
            Console.WriteLine("Failed to read resource pack {0}: {1}", (object) path, (object) ex);
          }
        }
      }
    }

    private static void CreatePacksFromDirectories(
      IServiceProvider services,
      string searchPath,
      List<ResourcePack> resourcePacks)
    {
      foreach (string directory in Directory.GetDirectories(searchPath))
      {
        try
        {
          string folderName = Path.GetFileName(directory);
          if (resourcePacks.All<ResourcePack>((Func<ResourcePack, bool>) (pack => pack.FileName != folderName)))
            resourcePacks.Add(new ResourcePack(services, directory));
        }
        catch (Exception ex)
        {
          Console.WriteLine("Failed to read resource pack {0}: {1}", (object) directory, (object) ex);
        }
      }
    }

    private static void CreatePacksFromZips(
      IServiceProvider services,
      string searchPath,
      List<ResourcePack> resourcePacks)
    {
      foreach (string file in Directory.GetFiles(searchPath, "*.zip"))
      {
        try
        {
          string fileName = Path.GetFileName(file);
          if (resourcePacks.All<ResourcePack>((Func<ResourcePack, bool>) (pack => pack.FileName != fileName)))
            resourcePacks.Add(new ResourcePack(services, file));
        }
        catch (Exception ex)
        {
          Console.WriteLine("Failed to read resource pack {0}: {1}", (object) file, (object) ex);
        }
      }
    }

    private static void CreatePacksFromWorkshopFolders(
      IServiceProvider services,
      List<ResourcePack> resourcePacks)
    {
      WorkshopSocialModule workshop = SocialAPI.Workshop;
      if (workshop == null)
        return;
      List<string> resourcePackPaths = workshop.GetListOfSubscribedResourcePackPaths();
      ResourcePack.BrandingType resourcePackBrand = workshop.Branding.ResourcePackBrand;
      foreach (string path in resourcePackPaths)
      {
        try
        {
          string folderName = Path.GetFileName(path);
          if (resourcePacks.All<ResourcePack>((Func<ResourcePack, bool>) (pack => pack.FileName != folderName)))
            resourcePacks.Add(new ResourcePack(services, path, resourcePackBrand));
        }
        catch (Exception ex)
        {
          Console.WriteLine("Failed to read resource pack {0}: {1}", (object) path, (object) ex);
        }
      }
    }

    private static IEnumerable<ResourcePackList.ResourcePackEntry> CreatePackEntryListFromJson(
      JArray serializedState)
    {
      try
      {
        if (serializedState != null)
        {
          if (((JContainer) serializedState).Count != 0)
            return (IEnumerable<ResourcePackList.ResourcePackEntry>) ((JToken) serializedState).ToObject<List<ResourcePackList.ResourcePackEntry>>();
        }
      }
      catch (JsonReaderException ex)
      {
        Console.WriteLine("Failed to parse configuration entry for resource pack list. {0}", (object) ex);
      }
      return (IEnumerable<ResourcePackList.ResourcePackEntry>) new List<ResourcePackList.ResourcePackEntry>();
    }

    private struct ResourcePackEntry
    {
      public string FileName;
      public bool Enabled;
      public int SortingOrder;

      public ResourcePackEntry(string name, bool enabled, int sortingOrder)
      {
        this.FileName = name;
        this.Enabled = enabled;
        this.SortingOrder = sortingOrder;
      }
    }
  }
}
