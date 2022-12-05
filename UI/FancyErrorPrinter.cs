// Decompiled with JetBrains decompiler
// Type: Terraria.UI.FancyErrorPrinter
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Content;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Terraria.UI
{
  public class FancyErrorPrinter
  {
    public static void ShowFailedToLoadAssetError(Exception exception, string filePath)
    {
      bool flag = false;
      if (exception is UnauthorizedAccessException)
        flag = true;
      if (exception is FileNotFoundException)
        flag = true;
      if (exception is DirectoryNotFoundException)
        flag = true;
      if (exception is AssetLoadException)
        flag = true;
      if (!flag)
        return;
      StringBuilder text = new StringBuilder();
      text.AppendLine("Failed to load asset: \"" + filePath.Replace("/", "\\") + "\"!");
      List<string> suggestions = new List<string>();
      suggestions.Add("Try verifying integrity of game files via Steam, the asset may be missing.");
      suggestions.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
      text.AppendLine();
      text.AppendLine("Suggestions:");
      FancyErrorPrinter.AppendSuggestions(text, suggestions);
      text.AppendLine();
      FancyErrorPrinter.IncludeOriginalMessage(text, exception);
      FancyErrorPrinter.ShowTheBox(text.ToString());
      Console.WriteLine(text.ToString());
    }

    public static void ShowFileSavingFailError(Exception exception, string filePath)
    {
      bool flag = false;
      if (exception is UnauthorizedAccessException)
        flag = true;
      if (exception is FileNotFoundException)
        flag = true;
      if (exception is DirectoryNotFoundException)
        flag = true;
      if (!flag)
        return;
      StringBuilder text = new StringBuilder();
      text.AppendLine("Failed to create the file: \"" + filePath.Replace("/", "\\") + "\"!");
      List<string> suggestions = new List<string>();
      suggestions.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
      suggestions.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
      suggestions.Add("Try verifying integrity of game files via Steam.");
      if (filePath.ToLower().Contains("onedrive"))
        suggestions.Add("Try updating OneDrive.");
      text.AppendLine();
      text.AppendLine("Suggestions:");
      FancyErrorPrinter.AppendSuggestions(text, suggestions);
      text.AppendLine();
      FancyErrorPrinter.IncludeOriginalMessage(text, exception);
      FancyErrorPrinter.ShowTheBox(text.ToString());
      Console.WriteLine(text.ToString());
    }

    public static void ShowDirectoryCreationFailError(Exception exception, string folderPath)
    {
      bool flag = false;
      if (exception is UnauthorizedAccessException)
        flag = true;
      if (exception is FileNotFoundException)
        flag = true;
      if (exception is DirectoryNotFoundException)
        flag = true;
      if (!flag)
        return;
      StringBuilder text = new StringBuilder();
      text.AppendLine("Failed to create the folder: \"" + folderPath.Replace("/", "\\") + "\"!");
      List<string> suggestions = new List<string>();
      suggestions.Add("If you are using an Anti-virus, please make sure it does not block Terraria in any way.");
      suggestions.Add("Try making sure your `Documents/My Games/Terraria` folder is not set to 'read-only'.");
      suggestions.Add("Try verifying integrity of game files via Steam.");
      if (folderPath.ToLower().Contains("onedrive"))
        suggestions.Add("Try updating OneDrive.");
      text.AppendLine();
      text.AppendLine("Suggestions:");
      FancyErrorPrinter.AppendSuggestions(text, suggestions);
      text.AppendLine();
      FancyErrorPrinter.IncludeOriginalMessage(text, exception);
      FancyErrorPrinter.ShowTheBox(text.ToString());
      Console.WriteLine((object) exception);
    }

    private static void IncludeOriginalMessage(StringBuilder text, Exception exception)
    {
      text.AppendLine("The original Error below");
      text.Append((object) exception);
    }

    private static void AppendSuggestions(StringBuilder text, List<string> suggestions)
    {
      for (int index = 0; index < suggestions.Count; ++index)
      {
        string suggestion = suggestions[index];
        text.AppendLine((index + 1).ToString() + ". " + suggestion);
      }
    }

    private static void ShowTheBox(string preparedMessage)
    {
      if (!Platform.IsWindows || Main.dedServ)
        return;
      int num = (int) MessageBox.Show(preparedMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }
}
