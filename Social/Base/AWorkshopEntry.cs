// Decompiled with JetBrains decompiler
// Type: Terraria.Social.Base.AWorkshopEntry
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Terraria.Social.Base
{
  public abstract class AWorkshopEntry
  {
    public const int CurrentWorkshopPublishVersion = 1;
    public const string ContentTypeName_World = "World";
    public const string ContentTypeName_ResourcePack = "ResourcePack";
    protected const string HeaderFileName = "Workshop.json";
    protected const string ContentTypeJsonCategoryField = "ContentType";
    protected const string WorkshopPublishedVersionField = "WorkshopPublishedVersion";
    protected const string WorkshopEntryField = "SteamEntryId";
    protected const string TagsField = "Tags";
    protected const string PreviewImageField = "PreviewImagePath";
    protected const string PublictyField = "Publicity";
    protected static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings()
    {
      TypeNameHandling = (TypeNameHandling) 0,
      MetadataPropertyHandling = (MetadataPropertyHandling) 1,
      Formatting = (Formatting) 1
    };

    public static string ReadHeader(string jsonText)
    {
      JToken jtoken;
      return !JObject.Parse(jsonText).TryGetValue("ContentType", ref jtoken) ? (string) null : jtoken.ToObject<string>();
    }

    protected static string CreateHeaderJson(
      string contentTypeName,
      ulong workshopEntryId,
      string[] tags,
      WorkshopItemPublicSettingId publicity,
      string previewImagePath)
    {
      JObject jobject = new JObject();
      Dictionary<string, object> dictionary = new Dictionary<string, object>();
      dictionary["WorkshopPublishedVersion"] = (object) 1;
      dictionary["ContentType"] = (object) contentTypeName;
      dictionary["SteamEntryId"] = (object) workshopEntryId;
      if (tags != null && tags.Length != 0)
        dictionary["Tags"] = (object) JArray.FromObject((object) tags);
      dictionary["Publicity"] = (object) publicity;
      return JsonConvert.SerializeObject((object) dictionary, AWorkshopEntry.SerializerSettings);
    }

    public static bool TryReadingManifest(string filePath, out FoundWorkshopEntryInfo info)
    {
      info = (FoundWorkshopEntryInfo) null;
      if (!File.Exists(filePath))
        return false;
      string str = File.ReadAllText(filePath);
      info = new FoundWorkshopEntryInfo();
      JsonSerializerSettings serializerSettings = AWorkshopEntry.SerializerSettings;
      Dictionary<string, object> dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(str, serializerSettings);
      if (dict == null || !AWorkshopEntry.TryGet<ulong>(dict, "SteamEntryId", out info.workshopEntryId))
        return false;
      int outputValue1;
      if (!AWorkshopEntry.TryGet<int>(dict, "WorkshopPublishedVersion", out outputValue1))
        outputValue1 = 1;
      info.publishedVersion = outputValue1;
      JArray outputValue2;
      if (AWorkshopEntry.TryGet<JArray>(dict, "Tags", out outputValue2))
        info.tags = ((JToken) outputValue2).ToObject<string[]>();
      int outputValue3;
      if (AWorkshopEntry.TryGet<int>(dict, "Publicity", out outputValue3))
        info.publicity = (WorkshopItemPublicSettingId) outputValue3;
      AWorkshopEntry.TryGet<string>(dict, "PreviewImagePath", out info.previewImagePath);
      return true;
    }

    protected static bool TryGet<T>(
      Dictionary<string, object> dict,
      string name,
      out T outputValue)
    {
      outputValue = default (T);
      try
      {
        object obj1;
        if (!dict.TryGetValue(name, out obj1))
          return false;
        switch (obj1)
        {
          case T obj2:
            outputValue = obj2;
            return true;
          case JObject _:
            outputValue = JsonConvert.DeserializeObject<T>(((object) (JObject) obj1).ToString());
            return true;
          default:
            outputValue = (T) Convert.ChangeType(obj1, typeof (T));
            return true;
        }
      }
      catch
      {
        return false;
      }
    }
  }
}
