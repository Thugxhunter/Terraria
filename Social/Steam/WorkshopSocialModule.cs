// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.WorkshopSocialModule
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria.DataStructures;
using Terraria.GameContent.UI.States;
using Terraria.IO;
using Terraria.Social.Base;

namespace Terraria.Social.Steam
{
  public class WorkshopSocialModule : Terraria.Social.Base.WorkshopSocialModule
  {
    private WorkshopHelper.UGCBased.Downloader _downloader;
    private WorkshopHelper.UGCBased.PublishedItemsFinder _publishedItems;
    private List<WorkshopHelper.UGCBased.APublisherInstance> _publisherInstances;
    private string _contentBaseFolder;

    public override void Initialize()
    {
      this.Branding = new WorkshopBranding()
      {
        ResourcePackBrand = ResourcePack.BrandingType.SteamWorkshop
      };
      this._publisherInstances = new List<WorkshopHelper.UGCBased.APublisherInstance>();
      this.ProgressReporter = (AWorkshopProgressReporter) new WorkshopProgressReporter(this._publisherInstances);
      this.SupportedTags = (AWorkshopTagsCollection) new SupportedWorkshopTags();
      this._contentBaseFolder = Main.SavePath + Path.DirectorySeparatorChar.ToString() + "Workshop";
      this._downloader = WorkshopHelper.UGCBased.Downloader.Create();
      this._publishedItems = WorkshopHelper.UGCBased.PublishedItemsFinder.Create();
      WorkshopIssueReporter workshopIssueReporter = new WorkshopIssueReporter();
      workshopIssueReporter.OnNeedToOpenUI += new Action(this._issueReporter_OnNeedToOpenUI);
      workshopIssueReporter.OnNeedToNotifyUI += new Action(this._issueReporter_OnNeedToNotifyUI);
      this.IssueReporter = workshopIssueReporter;
      UIWorkshopHub.OnWorkshopHubMenuOpened += new Action(this.RefreshSubscriptionsAndPublishings);
    }

    private void _issueReporter_OnNeedToNotifyUI()
    {
      Main.IssueReporterIndicator.AttemptLettingPlayerKnow();
      Main.WorkshopPublishingIndicator.Hide();
    }

    private void _issueReporter_OnNeedToOpenUI() => Main.OpenReportsMenu();

    public override void Shutdown()
    {
    }

    public override void LoadEarlyContent() => this.RefreshSubscriptionsAndPublishings();

    private void RefreshSubscriptionsAndPublishings()
    {
      this._downloader.Refresh(this.IssueReporter);
      this._publishedItems.Refresh();
    }

    public override List<string> GetListOfSubscribedWorldPaths() => this._downloader.WorldPaths.Select<string, string>((Func<string, string>) (folderPath => folderPath + Path.DirectorySeparatorChar.ToString() + "world.wld")).ToList<string>();

    public override List<string> GetListOfSubscribedResourcePackPaths() => this._downloader.ResourcePackPaths;

    public override bool TryGetPath(string pathEnd, out string fullPathFound)
    {
      fullPathFound = (string) null;
      string str = this._downloader.ResourcePackPaths.FirstOrDefault<string>((Func<string, bool>) (x => x.EndsWith(pathEnd)));
      if (str == null)
        return false;
      fullPathFound = str;
      return true;
    }

    private void Forget(
      WorkshopHelper.UGCBased.APublisherInstance instance)
    {
      this._publisherInstances.Remove(instance);
      this.RefreshSubscriptionsAndPublishings();
    }

    public override void PublishWorld(WorldFileData world, WorkshopItemPublishSettings settings)
    {
      string name = world.Name;
      string textForWorld = this.GetTextForWorld(world);
      string[] tagsInternalNames = settings.GetUsedTagsInternalNames();
      string str = this.GetTemporaryFolderPath() + world.GetFileName(false);
      if (!this.MakeTemporaryFolder(str))
        return;
      WorkshopHelper.UGCBased.WorldPublisherInstance publisherInstance = new WorkshopHelper.UGCBased.WorldPublisherInstance(world);
      this._publisherInstances.Add((WorkshopHelper.UGCBased.APublisherInstance) publisherInstance);
      publisherInstance.PublishContent(this._publishedItems, this.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, textForWorld, str, settings.PreviewImagePath, settings.Publicity, tagsInternalNames);
    }

    private string GetTextForWorld(WorldFileData world)
    {
      string str1 = "This is \"" + world.Name;
      string str2;
      switch (world.WorldSizeX)
      {
        case 4200:
          str2 = "small";
          break;
        case 6400:
          str2 = "medium";
          break;
        case 8400:
          str2 = "large";
          break;
        default:
          str2 = "custom";
          break;
      }
      string str3;
      switch (world.GameMode)
      {
        case 0:
          str3 = "classic";
          break;
        case 1:
          str3 = "expert";
          break;
        case 2:
          str3 = "master";
          break;
        case 3:
          str3 = "journey";
          break;
        default:
          str3 = "custom";
          break;
      }
      string str4 = str1 + "\", a " + str2.ToLower() + " " + str3.ToLower() + " world" + " infected by the " + (world.HasCorruption ? "corruption" : "crimson");
      if (world.IsHardMode)
        str4 += ", in hardmode";
      return str4 + ".";
    }

    public override void PublishResourcePack(
      ResourcePack resourcePack,
      WorkshopItemPublishSettings settings)
    {
      if (resourcePack.IsCompressed)
      {
        this.IssueReporter.ReportInstantUploadProblem("Workshop.ReportIssue_CannotPublishZips");
      }
      else
      {
        string name = resourcePack.Name;
        string itemDescription = resourcePack.Description;
        if (string.IsNullOrWhiteSpace(itemDescription))
          itemDescription = "";
        string[] tagsInternalNames = settings.GetUsedTagsInternalNames();
        string fullPath = resourcePack.FullPath;
        WorkshopHelper.UGCBased.ResourcePackPublisherInstance publisherInstance = new WorkshopHelper.UGCBased.ResourcePackPublisherInstance(resourcePack);
        this._publisherInstances.Add((WorkshopHelper.UGCBased.APublisherInstance) publisherInstance);
        publisherInstance.PublishContent(this._publishedItems, this.IssueReporter, new WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction(this.Forget), name, itemDescription, fullPath, settings.PreviewImagePath, settings.Publicity, tagsInternalNames);
      }
    }

    private string GetTemporaryFolderPath()
    {
      ulong steamId = SteamUser.GetSteamID().m_SteamID;
      string contentBaseFolder = this._contentBaseFolder;
      char directorySeparatorChar = Path.DirectorySeparatorChar;
      string str1 = directorySeparatorChar.ToString();
      string str2 = steamId.ToString();
      directorySeparatorChar = Path.DirectorySeparatorChar;
      string str3 = directorySeparatorChar.ToString();
      return contentBaseFolder + str1 + str2 + str3;
    }

    private bool MakeTemporaryFolder(string temporaryFolderPath)
    {
      bool flag = true;
      if (!Utils.TryCreatingDirectory(temporaryFolderPath))
      {
        this.IssueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_CouldNotCreateTemporaryFolder!");
        flag = false;
      }
      return flag;
    }

    public override void ImportDownloadedWorldToLocalSaves(
      WorldFileData world,
      string newFileName = null,
      string newDisplayName = null)
    {
      Main.menuMode = 10;
      world.CopyToLocal(newFileName, newDisplayName);
    }

    public List<IssueReport> GetReports()
    {
      List<IssueReport> reports = new List<IssueReport>();
      if (this.IssueReporter != null)
        reports.AddRange((IEnumerable<IssueReport>) this.IssueReporter.GetReports());
      return reports;
    }

    public override bool TryGetInfoForWorld(WorldFileData world, out FoundWorkshopEntryInfo info)
    {
      info = (FoundWorkshopEntryInfo) null;
      string path = this.GetTemporaryFolderPath() + world.GetFileName(false);
      return Directory.Exists(path) && AWorkshopEntry.TryReadingManifest(path + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
    }

    public override bool TryGetInfoForResourcePack(
      ResourcePack resourcePack,
      out FoundWorkshopEntryInfo info)
    {
      info = (FoundWorkshopEntryInfo) null;
      string fullPath = resourcePack.FullPath;
      return Directory.Exists(fullPath) && AWorkshopEntry.TryReadingManifest(fullPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info);
    }
  }
}
