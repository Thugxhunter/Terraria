// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Steam.WorkshopHelper
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using Terraria.IO;
using Terraria.Social.Base;
using Terraria.Utilities;

namespace Terraria.Social.Steam
{
  public class WorkshopHelper
  {
    public class UGCBased
    {
      public const string ManifestFileName = "workshop.json";

      public struct SteamWorkshopItem
      {
        public string ContentFolderPath;
        public string Description;
        public string PreviewImagePath;
        public string[] Tags;
        public string Title;
        public ERemoteStoragePublishedFileVisibility? Visibility;
      }

      public class Downloader
      {
        public List<string> ResourcePackPaths { get; private set; }

        public List<string> WorldPaths { get; private set; }

        public Downloader()
        {
          this.ResourcePackPaths = new List<string>();
          this.WorldPaths = new List<string>();
        }

        public static WorkshopHelper.UGCBased.Downloader Create() => new WorkshopHelper.UGCBased.Downloader();

        public List<string> GetListOfSubscribedItemsPaths()
        {
          PublishedFileId_t[] publishedFileIdTArray = new PublishedFileId_t[(int) SteamUGC.GetNumSubscribedItems()];
          int subscribedItems = (int) SteamUGC.GetSubscribedItems(publishedFileIdTArray, (uint) publishedFileIdTArray.Length);
          ulong num1 = 0;
          string empty = string.Empty;
          uint num2 = 0;
          List<string> subscribedItemsPaths = new List<string>();
          foreach (PublishedFileId_t publishedFileIdT in publishedFileIdTArray)
          {
            if (SteamUGC.GetItemInstallInfo(publishedFileIdT, ref num1, ref empty, 1024U, ref num2))
              subscribedItemsPaths.Add(empty);
          }
          return subscribedItemsPaths;
        }

        public bool Prepare(WorkshopIssueReporter issueReporter) => this.Refresh(issueReporter);

        public bool Refresh(WorkshopIssueReporter issueReporter)
        {
          this.ResourcePackPaths.Clear();
          this.WorldPaths.Clear();
          foreach (string subscribedItemsPath in this.GetListOfSubscribedItemsPaths())
          {
            if (subscribedItemsPath != null)
            {
              try
              {
                string path = subscribedItemsPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
                if (File.Exists(path))
                {
                  string str = AWorkshopEntry.ReadHeader(File.ReadAllText(path));
                  if (!(str == "World"))
                  {
                    if (str == "ResourcePack")
                      this.ResourcePackPaths.Add(subscribedItemsPath);
                  }
                  else
                    this.WorldPaths.Add(subscribedItemsPath);
                }
              }
              catch (Exception ex)
              {
                issueReporter.ReportDownloadProblem("Workshop.ReportIssue_FailedToLoadSubscribedFile", subscribedItemsPath, ex);
                return false;
              }
            }
          }
          return true;
        }
      }

      public class PublishedItemsFinder
      {
        private Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem> _items = new Dictionary<ulong, WorkshopHelper.UGCBased.SteamWorkshopItem>();
        private UGCQueryHandle_t m_UGCQueryHandle;
        private CallResult<SteamUGCQueryCompleted_t> OnSteamUGCQueryCompletedCallResult;
        private CallResult<SteamUGCRequestUGCDetailsResult_t> OnSteamUGCRequestUGCDetailsResultCallResult;

        public bool HasItemOfId(ulong id) => this._items.ContainsKey(id);

        public static WorkshopHelper.UGCBased.PublishedItemsFinder Create()
        {
          WorkshopHelper.UGCBased.PublishedItemsFinder publishedItemsFinder = new WorkshopHelper.UGCBased.PublishedItemsFinder();
          publishedItemsFinder.LoadHooks();
          return publishedItemsFinder;
        }

        private void LoadHooks()
        {
          // ISSUE: method pointer
          this.OnSteamUGCQueryCompletedCallResult = CallResult<SteamUGCQueryCompleted_t>.Create(new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate((object) this, __methodptr(OnSteamUGCQueryCompleted)));
          // ISSUE: method pointer
          this.OnSteamUGCRequestUGCDetailsResultCallResult = CallResult<SteamUGCRequestUGCDetailsResult_t>.Create(new CallResult<SteamUGCRequestUGCDetailsResult_t>.APIDispatchDelegate((object) this, __methodptr(OnSteamUGCRequestUGCDetailsResult)));
        }

        public void Prepare() => this.Refresh();

        public void Refresh()
        {
          CSteamID steamId = SteamUser.GetSteamID();
          this.m_UGCQueryHandle = SteamUGC.CreateQueryUserUGCRequest(((CSteamID) ref steamId).GetAccountID(), (EUserUGCList) 0, (EUGCMatchingUGCType) -1, (EUserUGCListSortOrder) 0, SteamUtils.GetAppID(), SteamUtils.GetAppID(), 1U);
          CoreSocialModule.SetSkipPulsing(true);
          // ISSUE: method pointer
          this.OnSteamUGCQueryCompletedCallResult.Set(SteamUGC.SendQueryUGCRequest(this.m_UGCQueryHandle), new CallResult<SteamUGCQueryCompleted_t>.APIDispatchDelegate((object) this, __methodptr(OnSteamUGCQueryCompleted)));
          CoreSocialModule.SetSkipPulsing(false);
        }

        private void OnSteamUGCQueryCompleted(SteamUGCQueryCompleted_t pCallback, bool bIOFailure)
        {
          this._items.Clear();
          if ((bIOFailure ? 1 : (pCallback.m_eResult != 1 ? 1 : 0)) != 0)
          {
            SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
          }
          else
          {
            for (uint index = 0; index < pCallback.m_unNumResultsReturned; ++index)
            {
              SteamUGCDetails_t steamUgcDetailsT;
              SteamUGC.GetQueryUGCResult(this.m_UGCQueryHandle, index, ref steamUgcDetailsT);
              this._items.Add(steamUgcDetailsT.m_nPublishedFileId.m_PublishedFileId, new WorkshopHelper.UGCBased.SteamWorkshopItem()
              {
                Title = ((SteamUGCDetails_t) ref steamUgcDetailsT).m_rgchTitle,
                Description = ((SteamUGCDetails_t) ref steamUgcDetailsT).m_rgchDescription
              });
            }
            SteamUGC.ReleaseQueryUGCRequest(this.m_UGCQueryHandle);
          }
        }

        private void OnSteamUGCRequestUGCDetailsResult(
          SteamUGCRequestUGCDetailsResult_t pCallback,
          bool bIOFailure)
        {
        }
      }

      public abstract class APublisherInstance
      {
        protected WorkshopItemPublicSettingId _publicity;
        protected WorkshopHelper.UGCBased.SteamWorkshopItem _entryData;
        protected PublishedFileId_t _publishedFileID;
        private UGCUpdateHandle_t _updateHandle;
        private CallResult<CreateItemResult_t> _createItemHook;
        private CallResult<SubmitItemUpdateResult_t> _updateItemHook;
        private WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction _endAction;
        private WorkshopIssueReporter _issueReporter;

        public void PublishContent(
          WorkshopHelper.UGCBased.PublishedItemsFinder finder,
          WorkshopIssueReporter issueReporter,
          WorkshopHelper.UGCBased.APublisherInstance.FinishedPublishingAction endAction,
          string itemTitle,
          string itemDescription,
          string contentFolderPath,
          string previewImagePath,
          WorkshopItemPublicSettingId publicity,
          string[] tags)
        {
          this._issueReporter = issueReporter;
          this._endAction = endAction;
          // ISSUE: method pointer
          this._createItemHook = CallResult<CreateItemResult_t>.Create(new CallResult<CreateItemResult_t>.APIDispatchDelegate((object) this, __methodptr(CreateItemResult)));
          // ISSUE: method pointer
          this._updateItemHook = CallResult<SubmitItemUpdateResult_t>.Create(new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate((object) this, __methodptr(UpdateItemResult)));
          ERemoteStoragePublishedFileVisibility visibility = this.GetVisibility(publicity);
          this._entryData = new WorkshopHelper.UGCBased.SteamWorkshopItem()
          {
            Title = itemTitle,
            Description = itemDescription,
            ContentFolderPath = contentFolderPath,
            Tags = tags,
            PreviewImagePath = previewImagePath,
            Visibility = new ERemoteStoragePublishedFileVisibility?(visibility)
          };
          ulong? nullable = new ulong?();
          FoundWorkshopEntryInfo info;
          if (AWorkshopEntry.TryReadingManifest(contentFolderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json", out info))
            nullable = new ulong?(info.workshopEntryId);
          if ((!nullable.HasValue ? 0 : (finder.HasItemOfId(nullable.Value) ? 1 : 0)) != 0)
          {
            this._publishedFileID = new PublishedFileId_t(nullable.Value);
            this.PreventUpdatingCertainThings();
            this.UpdateItem();
          }
          else
            this.CreateItem();
        }

        private void PreventUpdatingCertainThings()
        {
          this._entryData.Title = (string) null;
          this._entryData.Description = (string) null;
        }

        private ERemoteStoragePublishedFileVisibility GetVisibility(
          WorkshopItemPublicSettingId publicityId)
        {
          switch (publicityId)
          {
            case WorkshopItemPublicSettingId.FriendsOnly:
              return (ERemoteStoragePublishedFileVisibility) 1;
            case WorkshopItemPublicSettingId.Public:
              return (ERemoteStoragePublishedFileVisibility) 0;
            default:
              return (ERemoteStoragePublishedFileVisibility) 2;
          }
        }

        private void CreateItem()
        {
          CoreSocialModule.SetSkipPulsing(true);
          // ISSUE: method pointer
          this._createItemHook.Set(SteamUGC.CreateItem(SteamUtils.GetAppID(), (EWorkshopFileType) 0), new CallResult<CreateItemResult_t>.APIDispatchDelegate((object) this, __methodptr(CreateItemResult)));
          CoreSocialModule.SetSkipPulsing(false);
        }

        private void CreateItemResult(CreateItemResult_t param, bool bIOFailure)
        {
          if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
          {
            this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
            this._endAction(this);
          }
          else if (param.m_eResult == 1)
          {
            this._publishedFileID = param.m_nPublishedFileId;
            this.UpdateItem();
          }
          else
          {
            this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
            this._endAction(this);
          }
        }

        protected abstract string GetHeaderText();

        protected abstract void PrepareContentForUpdate();

        private void UpdateItem()
        {
          if (!this.TryWritingManifestToFolder(this._entryData.ContentFolderPath, this.GetHeaderText()))
          {
            this._endAction(this);
          }
          else
          {
            this.PrepareContentForUpdate();
            UGCUpdateHandle_t ugcUpdateHandleT = SteamUGC.StartItemUpdate(SteamUtils.GetAppID(), this._publishedFileID);
            if (this._entryData.Title != null)
              SteamUGC.SetItemTitle(ugcUpdateHandleT, this._entryData.Title);
            if (this._entryData.Description != null)
              SteamUGC.SetItemDescription(ugcUpdateHandleT, this._entryData.Description);
            SteamUGC.SetItemContent(ugcUpdateHandleT, this._entryData.ContentFolderPath);
            SteamUGC.SetItemTags(ugcUpdateHandleT, (IList<string>) this._entryData.Tags);
            if (this._entryData.PreviewImagePath != null)
              SteamUGC.SetItemPreview(ugcUpdateHandleT, this._entryData.PreviewImagePath);
            if (this._entryData.Visibility.HasValue)
              SteamUGC.SetItemVisibility(ugcUpdateHandleT, this._entryData.Visibility.Value);
            CoreSocialModule.SetSkipPulsing(true);
            SteamAPICall_t steamApiCallT = SteamUGC.SubmitItemUpdate(ugcUpdateHandleT, "");
            this._updateHandle = ugcUpdateHandleT;
            // ISSUE: method pointer
            this._updateItemHook.Set(steamApiCallT, new CallResult<SubmitItemUpdateResult_t>.APIDispatchDelegate((object) this, __methodptr(UpdateItemResult)));
            CoreSocialModule.SetSkipPulsing(false);
          }
        }

        private void UpdateItemResult(SubmitItemUpdateResult_t param, bool bIOFailure)
        {
          if (param.m_bUserNeedsToAcceptWorkshopLegalAgreement)
          {
            this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_UserDidNotAcceptWorkshopTermsOfService");
            this._endAction(this);
          }
          else
          {
            EResult eResult = param.m_eResult;
            if (eResult <= 9)
            {
              if (eResult != 1)
              {
                if (eResult != 8)
                {
                  if (eResult == 9)
                  {
                    this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_CouldNotFindFolderToUpload");
                    goto label_16;
                  }
                }
                else
                {
                  this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_InvalidParametersForPublishing");
                  goto label_16;
                }
              }
              else
              {
                SteamFriends.ActivateGameOverlayToWebPage("steam://url/CommunityFilePage/" + (object) this._publishedFileID.m_PublishedFileId, (EActivateGameOverlayToWebPageMode) 0);
                goto label_16;
              }
            }
            else if (eResult != 15)
            {
              if (eResult != 25)
              {
                if (eResult == 33)
                {
                  this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_SteamFileLockFailed");
                  goto label_16;
                }
              }
              else
              {
                this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_LimitExceeded");
                goto label_16;
              }
            }
            else
            {
              this._issueReporter.ReportDelayedUploadProblem("Workshop.ReportIssue_FailedToPublish_AccessDeniedBecauseUserDoesntOwnLicenseForApp");
              goto label_16;
            }
            this._issueReporter.ReportDelayedUploadProblemWithoutKnownReason("Workshop.ReportIssue_FailedToPublish_WithoutKnownReason", param.m_eResult.ToString());
label_16:
            this._endAction(this);
          }
        }

        private bool TryWritingManifestToFolder(string folderPath, string manifestText)
        {
          string path = folderPath + Path.DirectorySeparatorChar.ToString() + "workshop.json";
          bool folder = true;
          try
          {
            File.WriteAllText(path, manifestText);
          }
          catch (Exception ex)
          {
            this._issueReporter.ReportManifestCreationProblem("Workshop.ReportIssue_CouldNotCreateResourcePackManifestFile", ex);
            folder = false;
          }
          return folder;
        }

        public bool TryGetProgress(out float progress)
        {
          progress = 0.0f;
          if (UGCUpdateHandle_t.op_Equality(this._updateHandle, new UGCUpdateHandle_t()))
            return false;
          ulong num1;
          ulong num2;
          SteamUGC.GetItemUpdateProgress(this._updateHandle, ref num1, ref num2);
          if (num2 == 0UL)
            return false;
          progress = (float) num1 / (float) num2;
          return true;
        }

        public delegate void FinishedPublishingAction(
          WorkshopHelper.UGCBased.APublisherInstance instance);
      }

      public class ResourcePackPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
      {
        private ResourcePack _resourcePack;

        public ResourcePackPublisherInstance(ResourcePack resourcePack) => this._resourcePack = resourcePack;

        protected override string GetHeaderText() => TexturePackWorkshopEntry.GetHeaderTextFor(this._resourcePack, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);

        protected override void PrepareContentForUpdate()
        {
        }
      }

      public class WorldPublisherInstance : WorkshopHelper.UGCBased.APublisherInstance
      {
        private WorldFileData _world;

        public WorldPublisherInstance(WorldFileData world) => this._world = world;

        protected override string GetHeaderText() => WorldWorkshopEntry.GetHeaderTextFor(this._world, this._publishedFileID.m_PublishedFileId, this._entryData.Tags, this._publicity, this._entryData.PreviewImagePath);

        protected override void PrepareContentForUpdate()
        {
          if (this._world.IsCloudSave)
            FileUtilities.CopyToLocal(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld");
          else
            FileUtilities.Copy(this._world.Path, this._entryData.ContentFolderPath + Path.DirectorySeparatorChar.ToString() + "world.wld", false);
        }
      }
    }
  }
}
