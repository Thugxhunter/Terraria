// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.FileBrowser.NativeFileDialog
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Collections.Generic;
using System.Linq;

namespace Terraria.Utilities.FileBrowser
{
  public class NativeFileDialog : IFileBrowser
  {
    public string OpenFilePanel(string title, ExtensionFilter[] extensions)
    {
      string outPath;
      return nativefiledialog.NFD_OpenDialog(string.Join(",", ((IEnumerable<ExtensionFilter>) extensions).SelectMany<ExtensionFilter, string>((Func<ExtensionFilter, IEnumerable<string>>) (x => (IEnumerable<string>) x.Extensions)).ToArray<string>()), (string) null, out outPath) == nativefiledialog.nfdresult_t.NFD_OKAY ? outPath : (string) null;
    }
  }
}
