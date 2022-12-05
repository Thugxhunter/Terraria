// Decompiled with JetBrains decompiler
// Type: nativefiledialog
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Runtime.InteropServices;
using System.Text;

public static class nativefiledialog
{
  private const string nativeLibName = "nfd";

  private static int Utf8Size(string str) => str.Length * 4 + 1;

  private static unsafe byte* Utf8EncodeNullable(string str)
  {
    if (str == null)
      return (byte*) null;
    int num = nativefiledialog.Utf8Size(str);
    byte* bytes = (byte*) (void*) Marshal.AllocHGlobal(num);
    fixed (char* chars = str)
      Encoding.UTF8.GetBytes(chars, str != null ? str.Length + 1 : 0, bytes, num);
    return bytes;
  }

  private static unsafe string UTF8_ToManaged(IntPtr s, bool freePtr = false)
  {
    if (s == IntPtr.Zero)
      return (string) null;
    byte* numPtr = (byte*) (void*) s;
    while (*numPtr != (byte) 0)
      ++numPtr;
    int num = (int) (numPtr - (byte*) (void*) s);
    if (num == 0)
      return string.Empty;
    char* chars1 = stackalloc char[num];
    int chars2 = Encoding.UTF8.GetChars((byte*) (void*) s, num, chars1, num);
    string managed = new string(chars1, 0, chars2);
    if (!freePtr)
      return managed;
    nativefiledialog.free(s);
    return managed;
  }

  [DllImport("msvcrt", CallingConvention = CallingConvention.Cdecl)]
  private static extern void free(IntPtr ptr);

  [DllImport("nfd", EntryPoint = "NFD_OpenDialog", CallingConvention = CallingConvention.Cdecl)]
  private static extern unsafe nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialog(
    byte* filterList,
    byte* defaultPath,
    out IntPtr outPath);

  public static unsafe nativefiledialog.nfdresult_t NFD_OpenDialog(
    string filterList,
    string defaultPath,
    out string outPath)
  {
    byte* numPtr = nativefiledialog.Utf8EncodeNullable(filterList);
    byte* hglobal = nativefiledialog.Utf8EncodeNullable(defaultPath);
    byte* defaultPath1 = hglobal;
    IntPtr s;
    ref IntPtr local = ref s;
    nativefiledialog.nfdresult_t nfdresultT = nativefiledialog.INTERNAL_NFD_OpenDialog(numPtr, defaultPath1, out local);
    Marshal.FreeHGlobal((IntPtr) (void*) numPtr);
    Marshal.FreeHGlobal((IntPtr) (void*) hglobal);
    outPath = nativefiledialog.UTF8_ToManaged(s, true);
    return nfdresultT;
  }

  [DllImport("nfd", EntryPoint = "NFD_OpenDialogMultiple", CallingConvention = CallingConvention.Cdecl)]
  private static extern unsafe nativefiledialog.nfdresult_t INTERNAL_NFD_OpenDialogMultiple(
    byte* filterList,
    byte* defaultPath,
    out nativefiledialog.nfdpathset_t outPaths);

  public static unsafe nativefiledialog.nfdresult_t NFD_OpenDialogMultiple(
    string filterList,
    string defaultPath,
    out nativefiledialog.nfdpathset_t outPaths)
  {
    byte* numPtr = nativefiledialog.Utf8EncodeNullable(filterList);
    byte* hglobal = nativefiledialog.Utf8EncodeNullable(defaultPath);
    byte* defaultPath1 = hglobal;
    ref nativefiledialog.nfdpathset_t local = ref outPaths;
    nativefiledialog.nfdresult_t nfdresultT = nativefiledialog.INTERNAL_NFD_OpenDialogMultiple(numPtr, defaultPath1, out local);
    Marshal.FreeHGlobal((IntPtr) (void*) numPtr);
    Marshal.FreeHGlobal((IntPtr) (void*) hglobal);
    return nfdresultT;
  }

  [DllImport("nfd", EntryPoint = "NFD_SaveDialog", CallingConvention = CallingConvention.Cdecl)]
  private static extern unsafe nativefiledialog.nfdresult_t INTERNAL_NFD_SaveDialog(
    byte* filterList,
    byte* defaultPath,
    out IntPtr outPath);

  public static unsafe nativefiledialog.nfdresult_t NFD_SaveDialog(
    string filterList,
    string defaultPath,
    out string outPath)
  {
    byte* numPtr = nativefiledialog.Utf8EncodeNullable(filterList);
    byte* hglobal = nativefiledialog.Utf8EncodeNullable(defaultPath);
    byte* defaultPath1 = hglobal;
    IntPtr s;
    ref IntPtr local = ref s;
    nativefiledialog.nfdresult_t nfdresultT = nativefiledialog.INTERNAL_NFD_SaveDialog(numPtr, defaultPath1, out local);
    Marshal.FreeHGlobal((IntPtr) (void*) numPtr);
    Marshal.FreeHGlobal((IntPtr) (void*) hglobal);
    outPath = nativefiledialog.UTF8_ToManaged(s, true);
    return nfdresultT;
  }

  [DllImport("nfd", EntryPoint = "NFD_PickFolder", CallingConvention = CallingConvention.Cdecl)]
  private static extern unsafe nativefiledialog.nfdresult_t INTERNAL_NFD_PickFolder(
    byte* defaultPath,
    out IntPtr outPath);

  public static unsafe nativefiledialog.nfdresult_t NFD_PickFolder(
    string defaultPath,
    out string outPath)
  {
    byte* numPtr = nativefiledialog.Utf8EncodeNullable(defaultPath);
    IntPtr s;
    ref IntPtr local = ref s;
    nativefiledialog.nfdresult_t nfdresultT = nativefiledialog.INTERNAL_NFD_PickFolder(numPtr, out local);
    Marshal.FreeHGlobal((IntPtr) (void*) numPtr);
    outPath = nativefiledialog.UTF8_ToManaged(s, true);
    return nfdresultT;
  }

  [DllImport("nfd", EntryPoint = "NFD_GetError", CallingConvention = CallingConvention.Cdecl)]
  private static extern IntPtr INTERNAL_NFD_GetError();

  public static string NFD_GetError() => nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_GetError());

  [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
  public static extern IntPtr NFD_PathSet_GetCount(ref nativefiledialog.nfdpathset_t pathset);

  [DllImport("nfd", EntryPoint = "NFD_PathSet_GetPath", CallingConvention = CallingConvention.Cdecl)]
  private static extern IntPtr INTERNAL_NFD_PathSet_GetPath(
    ref nativefiledialog.nfdpathset_t pathset,
    IntPtr index);

  public static string NFD_PathSet_GetPath(ref nativefiledialog.nfdpathset_t pathset, IntPtr index) => nativefiledialog.UTF8_ToManaged(nativefiledialog.INTERNAL_NFD_PathSet_GetPath(ref pathset, index));

  [DllImport("nfd", CallingConvention = CallingConvention.Cdecl)]
  public static extern void NFD_PathSet_Free(ref nativefiledialog.nfdpathset_t pathset);

  public enum nfdresult_t
  {
    NFD_ERROR,
    NFD_OKAY,
    NFD_CANCEL,
  }

  public struct nfdpathset_t
  {
    private IntPtr buf;
    private IntPtr indices;
    private IntPtr count;
  }
}
