// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.FileBrowser.FileBrowser
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.Utilities.FileBrowser
{
  public class FileBrowser
  {
    private static IFileBrowser _platformWrapper = (IFileBrowser) new NativeFileDialog();

    public static string OpenFilePanel(string title, string extension)
    {
      ExtensionFilter[] extensionFilterArray;
      if (!string.IsNullOrEmpty(extension))
        extensionFilterArray = new ExtensionFilter[1]
        {
          new ExtensionFilter("", new string[1]{ extension })
        };
      else
        extensionFilterArray = (ExtensionFilter[]) null;
      ExtensionFilter[] extensions = extensionFilterArray;
      return Terraria.Utilities.FileBrowser.FileBrowser.OpenFilePanel(title, extensions);
    }

    public static string OpenFilePanel(string title, ExtensionFilter[] extensions) => Terraria.Utilities.FileBrowser.FileBrowser._platformWrapper.OpenFilePanel(title, extensions);
  }
}
