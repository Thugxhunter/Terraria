// Decompiled with JetBrains decompiler
// Type: Terraria.Utilities.CrashWatcher
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace Terraria.Utilities
{
  public static class CrashWatcher
  {
    public static bool LogAllExceptions { get; set; }

    public static bool DumpOnException { get; set; }

    public static bool DumpOnCrash { get; private set; }

    public static CrashDump.Options CrashDumpOptions { get; private set; }

    private static string DumpPath => Path.Combine(Main.SavePath, "Dumps");

    public static void Inititialize()
    {
      Console.WriteLine("Error Logging Enabled.");
      AppDomain.CurrentDomain.FirstChanceException += (EventHandler<FirstChanceExceptionEventArgs>) ((sender, exceptionArgs) =>
      {
        if (!CrashWatcher.LogAllExceptions)
          return;
        Console.Write("================\r\n" + string.Format("{0}: First-Chance Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", (object) DateTime.Now, (object) Thread.CurrentThread.ManagedThreadId, (object) Thread.CurrentThread.Name, (object) Thread.CurrentThread.CurrentCulture.Name, (object) CrashWatcher.PrintException(exceptionArgs.Exception)) + "================\r\n\r\n");
      });
      AppDomain.CurrentDomain.UnhandledException += (UnhandledExceptionEventHandler) ((sender, exceptionArgs) =>
      {
        Console.Write("================\r\n" + string.Format("{0}: Unhandled Exception\r\nThread: {1} [{2}]\r\nCulture: {3}\r\nException: {4}\r\n", (object) DateTime.Now, (object) Thread.CurrentThread.ManagedThreadId, (object) Thread.CurrentThread.Name, (object) Thread.CurrentThread.CurrentCulture.Name, (object) CrashWatcher.PrintException((Exception) exceptionArgs.ExceptionObject)) + "================\r\n");
        if (!CrashWatcher.DumpOnCrash)
          return;
        CrashDump.WriteException(CrashWatcher.CrashDumpOptions, CrashWatcher.DumpPath);
      });
    }

    private static string PrintException(Exception ex)
    {
      string str = ex.ToString();
      try
      {
        int num = (int) typeof (Exception).GetProperty("HResult", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).GetGetMethod(true).Invoke((object) ex, (object[]) null);
        if (num != 0)
          str = str + "\nHResult: " + (object) num;
      }
      catch
      {
      }
      if (ex is ReflectionTypeLoadException)
      {
        foreach (Exception loaderException in ((ReflectionTypeLoadException) ex).LoaderExceptions)
          str = str + "\n+--> " + CrashWatcher.PrintException(loaderException);
      }
      return str;
    }

    public static void EnableCrashDumps(CrashDump.Options options)
    {
      CrashWatcher.DumpOnCrash = true;
      CrashWatcher.CrashDumpOptions = options;
    }

    public static void DisableCrashDumps() => CrashWatcher.DumpOnCrash = false;

    [Conditional("DEBUG")]
    private static void HookDebugExceptionDialog()
    {
    }
  }
}
