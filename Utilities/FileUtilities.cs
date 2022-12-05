// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.FileUtilities
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.OS;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using Terraria.Social;

namespace Terraria.Utilities
{
  public static class FileUtilities
  {
    private static Regex FileNameRegex = new Regex("^(?<path>.*[\\\\\\/])?(?:$|(?<fileName>.+?)(?:(?<extension>\\.[^.]*$)|$))", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    public static bool Exists(string path, bool cloud) => cloud && SocialAPI.Cloud != null ? SocialAPI.Cloud.HasFile(path) : File.Exists(path);

    public static void Delete(string path, bool cloud, bool forceDeleteFile = false)
    {
      if (cloud && SocialAPI.Cloud != null)
        SocialAPI.Cloud.Delete(path);
      else if (forceDeleteFile)
        File.Delete(path);
      else
        Platform.Get<IPathService>().MoveToRecycleBin(path);
    }

    public static string GetFullPath(string path, bool cloud) => !cloud ? Path.GetFullPath(path) : path;

    public static void Copy(string source, string destination, bool cloud, bool overwrite = true)
    {
      if (!cloud)
      {
        try
        {
          File.Copy(source, destination, overwrite);
        }
        catch (IOException ex)
        {
          if (ex.GetType() != typeof (IOException))
          {
            throw;
          }
          else
          {
            using (FileStream fileStream = File.OpenRead(source))
            {
              using (FileStream destination1 = File.Create(destination))
                fileStream.CopyTo((Stream) destination1);
            }
          }
        }
      }
      else
      {
        if (SocialAPI.Cloud == null || !overwrite && SocialAPI.Cloud.HasFile(destination))
          return;
        SocialAPI.Cloud.Write(destination, SocialAPI.Cloud.Read(source));
      }
    }

    public static void Move(
      string source,
      string destination,
      bool cloud,
      bool overwrite = true,
      bool forceDeleteSourceFile = false)
    {
      FileUtilities.Copy(source, destination, cloud, overwrite);
      FileUtilities.Delete(source, cloud, forceDeleteSourceFile);
    }

    public static int GetFileSize(string path, bool cloud) => cloud && SocialAPI.Cloud != null ? SocialAPI.Cloud.GetFileSize(path) : (int) new FileInfo(path).Length;

    public static void Read(string path, byte[] buffer, int length, bool cloud)
    {
      if (cloud && SocialAPI.Cloud != null)
      {
        SocialAPI.Cloud.Read(path, buffer, length);
      }
      else
      {
        using (FileStream fileStream = File.OpenRead(path))
          fileStream.Read(buffer, 0, length);
      }
    }

    public static byte[] ReadAllBytes(string path, bool cloud) => cloud && SocialAPI.Cloud != null ? SocialAPI.Cloud.Read(path) : File.ReadAllBytes(path);

    public static void WriteAllBytes(string path, byte[] data, bool cloud) => FileUtilities.Write(path, data, data.Length, cloud);

    public static void Write(string path, byte[] data, int length, bool cloud)
    {
      if (cloud && SocialAPI.Cloud != null)
      {
        SocialAPI.Cloud.Write(path, data, length);
      }
      else
      {
        string parentFolderPath = FileUtilities.GetParentFolderPath(path);
        if (parentFolderPath != "")
          Utils.TryCreatingDirectory(parentFolderPath);
        FileUtilities.RemoveReadOnlyAttribute(path);
        using (FileStream fileStream = File.Open(path, FileMode.Create))
        {
          while (fileStream.Position < (long) length)
            fileStream.Write(data, (int) fileStream.Position, Math.Min(length - (int) fileStream.Position, 2048));
        }
      }
    }

    public static void RemoveReadOnlyAttribute(string path)
    {
      if (!File.Exists(path))
        return;
      try
      {
        FileAttributes attributes = File.GetAttributes(path);
        if ((attributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
          return;
        FileAttributes fileAttributes = attributes & ~FileAttributes.ReadOnly;
        File.SetAttributes(path, fileAttributes);
      }
      catch (Exception ex)
      {
      }
    }

    public static bool MoveToCloud(string localPath, string cloudPath)
    {
      if (SocialAPI.Cloud == null)
        return false;
      FileUtilities.WriteAllBytes(cloudPath, FileUtilities.ReadAllBytes(localPath, false), true);
      FileUtilities.Delete(localPath, false);
      return true;
    }

    public static bool MoveToLocal(string cloudPath, string localPath)
    {
      if (SocialAPI.Cloud == null)
        return false;
      FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
      FileUtilities.Delete(cloudPath, true);
      return true;
    }

    public static bool CopyToLocal(string cloudPath, string localPath)
    {
      if (SocialAPI.Cloud == null)
        return false;
      FileUtilities.WriteAllBytes(localPath, FileUtilities.ReadAllBytes(cloudPath, true), false);
      return true;
    }

    public static string GetFileName(string path, bool includeExtension = true)
    {
      Match match = FileUtilities.FileNameRegex.Match(path);
      if (match == null || match.Groups["fileName"] == null)
        return "";
      includeExtension &= match.Groups["extension"] != null;
      return match.Groups["fileName"].Value + (includeExtension ? match.Groups["extension"].Value : "");
    }

    public static string GetParentFolderPath(string path, bool includeExtension = true)
    {
      Match match = FileUtilities.FileNameRegex.Match(path);
      return match == null || match.Groups[nameof (path)] == null ? "" : match.Groups[nameof (path)].Value;
    }

    public static void CopyFolder(string sourcePath, string destinationPath)
    {
      Directory.CreateDirectory(destinationPath);
      foreach (string directory in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
        Directory.CreateDirectory(directory.Replace(sourcePath, destinationPath));
      foreach (string file in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
        File.Copy(file, file.Replace(sourcePath, destinationPath), true);
    }

    public static void ProtectedInvoke(Action action)
    {
      bool isBackground = Thread.CurrentThread.IsBackground;
      try
      {
        Thread.CurrentThread.IsBackground = false;
        action();
      }
      finally
      {
        Thread.CurrentThread.IsBackground = isBackground;
      }
    }
  }
}
