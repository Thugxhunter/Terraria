// Decompiled with JetBrains decompiler
// Type: Terraria.IO.FavoritesFile
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Terraria.UI;
using Terraria.Utilities;

namespace Terraria.IO
{
  public class FavoritesFile
  {
    public readonly string Path;
    public readonly bool IsCloudSave;
    private Dictionary<string, Dictionary<string, bool>> _data = new Dictionary<string, Dictionary<string, bool>>();
    private UTF8Encoding _ourEncoder = new UTF8Encoding(true, true);

    public FavoritesFile(string path, bool isCloud)
    {
      this.Path = path;
      this.IsCloudSave = isCloud;
    }

    public void SaveFavorite(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        this._data.Add(fileData.Type, new Dictionary<string, bool>());
      this._data[fileData.Type][fileData.GetFileName()] = fileData.IsFavorite;
      this.Save();
    }

    public void ClearEntry(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        return;
      this._data[fileData.Type].Remove(fileData.GetFileName());
      this.Save();
    }

    public bool IsFavorite(FileData fileData)
    {
      if (!this._data.ContainsKey(fileData.Type))
        return false;
      string fileName = fileData.GetFileName();
      bool flag;
      return this._data[fileData.Type].TryGetValue(fileName, out flag) && flag;
    }

    public void Save()
    {
      try
      {
        FileUtilities.WriteAllBytes(this.Path, this._ourEncoder.GetBytes(JsonConvert.SerializeObject((object) this._data, (Formatting) 1)), this.IsCloudSave);
      }
      catch (Exception ex)
      {
        string path = this.Path;
        FancyErrorPrinter.ShowFileSavingFailError(ex, path);
        throw;
      }
    }

    public void Load()
    {
      if (!FileUtilities.Exists(this.Path, this.IsCloudSave))
      {
        this._data.Clear();
      }
      else
      {
        try
        {
          byte[] bytes = FileUtilities.ReadAllBytes(this.Path, this.IsCloudSave);
          string str;
          try
          {
            str = this._ourEncoder.GetString(bytes);
          }
          catch
          {
            str = Encoding.ASCII.GetString(bytes);
          }
          this._data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, bool>>>(str);
          if (this._data != null)
            return;
          this._data = new Dictionary<string, Dictionary<string, bool>>();
        }
        catch (Exception ex)
        {
          Console.WriteLine("Unable to load favorites.json file ({0} : {1})", (object) this.Path, this.IsCloudSave ? (object) "Cloud Save" : (object) "Local Save");
        }
      }
    }
  }
}
