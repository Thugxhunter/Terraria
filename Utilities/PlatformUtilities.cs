// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.PlatformUtilities
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.IO;

namespace Terraria.Utilities
{
  public static class PlatformUtilities
  {
    public static void SavePng(
      Stream stream,
      int width,
      int height,
      int imgWidth,
      int imgHeight,
      byte[] data)
    {
      throw new NotSupportedException("Use Bitmap to save png images on windows");
    }
  }
}
