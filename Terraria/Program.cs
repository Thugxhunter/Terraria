// Decompiled with JetBrains decompiler
// Type: Terraria.Program
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.IO;
using ReLogic.OS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Terraria.Initializers;
using Terraria.Localization;
using Terraria.Social;
using Terraria.Utilities;

namespace Terraria
{
  public static class Program
  {
    public static bool IsXna = true;
    public static bool IsFna = false;
    public static bool IsMono = Type.GetType("Mono.Runtime") != (Type) null;
    public const bool IsDebug = false;
    public static Dictionary<string, string> LaunchParameters = new Dictionary<string, string>();
    public static string SavePath;
    public const string TerrariaSaveFolderPath = "Terraria";
    private static int ThingsToLoad;
    private static int ThingsLoaded;
    public static bool LoadedEverything;
    public static IntPtr JitForcedMethodCache;

    public static float LoadedPercentage => Program.ThingsToLoad == 0 ? 1f : (float) Program.ThingsLoaded / (float) Program.ThingsToLoad;

    public static void StartForceLoad()
    {
      if (!Main.SkipAssemblyLoad)
        new Thread(new ParameterizedThreadStart(Program.ForceLoadThread))
        {
          IsBackground = true
        }.Start();
      else
        Program.LoadedEverything = true;
    }

    public static void ForceLoadThread(object threadContext)
    {
      Program.ForceLoadAssembly(Assembly.GetExecutingAssembly(), true);
      Program.LoadedEverything = true;
    }

    private static void ForceJITOnAssembly(Assembly assembly)
    {
      foreach (Type type in assembly.GetTypes())
      {
        foreach (MethodInfo methodInfo in Program.IsMono ? type.GetMethods() : type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
          if (!methodInfo.IsAbstract && !methodInfo.ContainsGenericParameters && methodInfo.GetMethodBody() != null)
          {
            if (Program.IsMono)
              Program.JitForcedMethodCache = methodInfo.MethodHandle.GetFunctionPointer();
            else
              RuntimeHelpers.PrepareMethod(methodInfo.MethodHandle);
          }
        }
        ++Program.ThingsLoaded;
      }
    }

    private static void ForceStaticInitializers(Assembly assembly)
    {
      foreach (Type type in assembly.GetTypes())
      {
        if (!type.IsGenericType)
          RuntimeHelpers.RunClassConstructor(type.TypeHandle);
      }
    }

    private static void ForceLoadAssembly(Assembly assembly, bool initializeStaticMembers)
    {
      Program.ThingsToLoad = assembly.GetTypes().Length;
      Program.ForceJITOnAssembly(assembly);
      if (!initializeStaticMembers)
        return;
      Program.ForceStaticInitializers(assembly);
    }

    private static void ForceLoadAssembly(string name, bool initializeStaticMembers)
    {
      Assembly assembly = (Assembly) null;
      Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
      for (int index = 0; index < assemblies.Length; ++index)
      {
        if (assemblies[index].GetName().Name.Equals(name))
        {
          assembly = assemblies[index];
          break;
        }
      }
      if (assembly == (Assembly) null)
        assembly = Assembly.Load(name);
      Program.ForceLoadAssembly(assembly, initializeStaticMembers);
    }

    private static void SetupLogging()
    {
      if (Program.LaunchParameters.ContainsKey("-logfile"))
      {
        string launchParameter = Program.LaunchParameters["-logfile"];
        ConsoleOutputMirror.ToFile(launchParameter == null || launchParameter.Trim() == "" ? Path.Combine(Program.SavePath, "Logs", string.Format("Log_{0:yyyyMMddHHmmssfff}.log", (object) DateTime.Now)) : Path.Combine(launchParameter, string.Format("Log_{0:yyyyMMddHHmmssfff}.log", (object) DateTime.Now)));
      }
      CrashWatcher.Inititialize();
      CrashWatcher.DumpOnException = Program.LaunchParameters.ContainsKey("-minidump");
      CrashWatcher.LogAllExceptions = Program.LaunchParameters.ContainsKey("-logerrors");
      if (!Program.LaunchParameters.ContainsKey("-fulldump"))
        return;
      Console.WriteLine("Full Dump logs enabled.");
      CrashWatcher.EnableCrashDumps(CrashDump.Options.WithFullMemory);
    }

    private static void InitializeConsoleOutput()
    {
      if (Debugger.IsAttached)
        return;
      try
      {
        Console.OutputEncoding = Encoding.UTF8;
        if (Platform.IsWindows)
          Console.InputEncoding = Encoding.Unicode;
        else
          Console.InputEncoding = Encoding.UTF8;
      }
      catch
      {
      }
    }

    public static void LaunchGame(string[] args, bool monoArgs = false)
    {
      Thread.CurrentThread.Name = "Main Thread";
      if (monoArgs)
        args = Utils.ConvertMonoArgsToDotNet(args);
      if (Program.IsFna)
        Program.TrySettingFNAToOpenGL(args);
      Program.LaunchParameters = Utils.ParseArguements(args);
      Program.SavePath = Program.LaunchParameters.ContainsKey("-savedirectory") ? Program.LaunchParameters["-savedirectory"] : Platform.Get<IPathService>().GetStoragePath("Terraria");
      ThreadPool.SetMinThreads(8, 8);
      Program.InitializeConsoleOutput();
      Program.SetupLogging();
      Platform.Get<IWindowService>().SetQuickEditEnabled(false);
      Program.RunGame();
    }

    public static void RunGame()
    {
      LanguageManager.Instance.SetLanguage(GameCulture.DefaultCulture);
      if (Platform.IsOSX)
        Main.OnEngineLoad += (Action) (() => Main.instance.IsMouseVisible = false);
      using (Main game = new Main())
      {
        try
        {
          Lang.InitializeLegacyLocalization();
          SocialAPI.Initialize();
          LaunchInitializer.LoadParameters(game);
          Main.OnEnginePreload += new Action(Program.StartForceLoad);
          if (Main.dedServ)
            game.DedServ();
          game.Run();
        }
        catch (Exception ex)
        {
          Program.DisplayException(ex);
        }
      }
    }

    private static void TrySettingFNAToOpenGL(string[] args)
    {
      bool flag = false;
      for (int index = 0; index < args.Length; ++index)
      {
        if (args[index].Contains("gldevice"))
        {
          flag = true;
          break;
        }
      }
      if (flag)
        return;
      Environment.SetEnvironmentVariable("FNA3D_FORCE_DRIVER", "OpenGL");
    }

    private static void DisplayException(Exception e)
    {
      try
      {
        string text = e.ToString();
        if (WorldGen.gen)
        {
          try
          {
            text = string.Format("Creating world - Seed: {0} Width: {1}, Height: {2}, Evil: {3}, IsExpert: {4}\n{5}", (object) Main.ActiveWorldFileData.Seed, (object) Main.maxTilesX, (object) Main.maxTilesY, (object) WorldGen.WorldGenParam_Evil, (object) Main.expertMode, (object) text);
          }
          catch
          {
          }
        }
        using (StreamWriter streamWriter = new StreamWriter("client-crashlog.txt", true))
        {
          streamWriter.WriteLine((object) DateTime.Now);
          streamWriter.WriteLine(text);
          streamWriter.WriteLine("");
        }
        if (Main.dedServ)
          Console.WriteLine(Language.GetTextValue("Error.ServerCrash"), (object) DateTime.Now, (object) text);
        int num = (int) MessageBox.Show(text, "Terraria: Error");
      }
      catch
      {
      }
    }
  }
}
