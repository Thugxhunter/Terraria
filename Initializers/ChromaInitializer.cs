// Decompiled with JetBrains decompiler
// Type: Terraria.Initializers.ChromaInitializer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Graphics;
using ReLogic.OS;
using ReLogic.Peripherals.RGB;
using ReLogic.Peripherals.RGB.Corsair;
using ReLogic.Peripherals.RGB.Logitech;
using ReLogic.Peripherals.RGB.Razer;
using ReLogic.Peripherals.RGB.SteelSeries;
using SteelSeries.GameSense;
using SteelSeries.GameSense.DeviceZone;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Terraria.GameContent.RGB;
using Terraria.GameInput;
using Terraria.Graphics.Effects;
using Terraria.IO;

namespace Terraria.Initializers
{
  public static class ChromaInitializer
  {
    private static ChromaEngine _engine;
    private const string GAME_NAME_ID = "TERRARIA";
    private static float _rgbUpdateRate;
    private static bool _useRazer;
    private static bool _useCorsair;
    private static bool _useLogitech;
    private static bool _useSteelSeries;
    private static VendorColorProfile _razerColorProfile;
    private static VendorColorProfile _corsairColorProfile;
    private static VendorColorProfile _logitechColorProfile;
    private static VendorColorProfile _steelSeriesColorProfile;
    private static Dictionary<string, ChromaInitializer.EventLocalization> _localizedEvents = new Dictionary<string, ChromaInitializer.EventLocalization>()
    {
      {
        "KEY_MOUSELEFT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Left Mouse Button"
        }
      },
      {
        "KEY_MOUSERIGHT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Right Mouse Button"
        }
      },
      {
        "KEY_UP",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Up"
        }
      },
      {
        "KEY_DOWN",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Down"
        }
      },
      {
        "KEY_LEFT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Left"
        }
      },
      {
        "KEY_RIGHT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Right"
        }
      },
      {
        "KEY_JUMP",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Jump"
        }
      },
      {
        "KEY_THROW",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Throw"
        }
      },
      {
        "KEY_INVENTORY",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Inventory"
        }
      },
      {
        "KEY_GRAPPLE",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Grapple"
        }
      },
      {
        "KEY_SMARTSELECT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Smart Select"
        }
      },
      {
        "KEY_SMARTCURSOR",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Smart Cursor"
        }
      },
      {
        "KEY_QUICKMOUNT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Quick Mount"
        }
      },
      {
        "KEY_QUICKHEAL",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Quick Heal"
        }
      },
      {
        "KEY_QUICKMANA",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Quick Mana"
        }
      },
      {
        "KEY_QUICKBUFF",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Quick Buff"
        }
      },
      {
        "KEY_MAPZOOMIN",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Zoom In"
        }
      },
      {
        "KEY_MAPZOOMOUT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Zoom Out"
        }
      },
      {
        "KEY_MAPALPHAUP",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Transparency Up"
        }
      },
      {
        "KEY_MAPALPHADOWN",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Transparency Down"
        }
      },
      {
        "KEY_MAPFULL",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Full"
        }
      },
      {
        "KEY_MAPSTYLE",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Map Style"
        }
      },
      {
        "KEY_HOTBAR1",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 1"
        }
      },
      {
        "KEY_HOTBAR2",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 2"
        }
      },
      {
        "KEY_HOTBAR3",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 3"
        }
      },
      {
        "KEY_HOTBAR4",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 4"
        }
      },
      {
        "KEY_HOTBAR5",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 5"
        }
      },
      {
        "KEY_HOTBAR6",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 6"
        }
      },
      {
        "KEY_HOTBAR7",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 7"
        }
      },
      {
        "KEY_HOTBAR8",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 8"
        }
      },
      {
        "KEY_HOTBAR9",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 9"
        }
      },
      {
        "KEY_HOTBAR10",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar 10"
        }
      },
      {
        "KEY_HOTBARMINUS",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar Minus"
        }
      },
      {
        "KEY_HOTBARPLUS",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Hotbar Plus"
        }
      },
      {
        "KEY_DPADRADIAL1",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Radial 1"
        }
      },
      {
        "KEY_DPADRADIAL2",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Radial 2"
        }
      },
      {
        "KEY_DPADRADIAL3",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Radial 3"
        }
      },
      {
        "KEY_DPADRADIAL4",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Radial 4"
        }
      },
      {
        "KEY_RADIALHOTBAR",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Radial Hotbar"
        }
      },
      {
        "KEY_RADIALQUICKBAR",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Radial Quickbar"
        }
      },
      {
        "KEY_DPADSNAP1",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Snap 1"
        }
      },
      {
        "KEY_DPADSNAP2",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Snap 2"
        }
      },
      {
        "KEY_DPADSNAP3",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Snap 3"
        }
      },
      {
        "KEY_DPADSNAP4",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Dpad Snap 4"
        }
      },
      {
        "KEY_MENUUP",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Menu Up"
        }
      },
      {
        "KEY_MENUDOWN",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Menu Down"
        }
      },
      {
        "KEY_MENULEFT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Menu Left"
        }
      },
      {
        "KEY_MENURIGHT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Menu Right"
        }
      },
      {
        "KEY_LOCKON",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Lock On"
        }
      },
      {
        "KEY_VIEWZOOMIN",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zoom In"
        }
      },
      {
        "KEY_VIEWZOOMOUT",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zoom Out"
        }
      },
      {
        "KEY_TOGGLECREATIVEMENU",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Toggle Creative Menu"
        }
      },
      {
        "DO_RAINBOWS",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Theme"
        }
      },
      {
        "ZONE1",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 1"
        }
      },
      {
        "ZONE2",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 2"
        }
      },
      {
        "ZONE3",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 3"
        }
      },
      {
        "ZONE4",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 4"
        }
      },
      {
        "ZONE5",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 5"
        }
      },
      {
        "ZONE6",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 6"
        }
      },
      {
        "ZONE7",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 7"
        }
      },
      {
        "ZONE8",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 8"
        }
      },
      {
        "ZONE9",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 9"
        }
      },
      {
        "ZONE10",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 10"
        }
      },
      {
        "ZONE11",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 11"
        }
      },
      {
        "ZONE12",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Zone 12"
        }
      },
      {
        "LIFE",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Life Percent"
        }
      },
      {
        "MANA",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Mana Percent"
        }
      },
      {
        "BREATH",
        new ChromaInitializer.EventLocalization()
        {
          DefaultDisplayName = "Breath Percent"
        }
      }
    };
    public static IntRgbGameValueTracker Event_LifePercent;
    public static IntRgbGameValueTracker Event_ManaPercent;
    public static IntRgbGameValueTracker Event_BreathPercent;

    public static void BindTo(Preferences preferences)
    {
      preferences.OnSave += new Action<Preferences>(ChromaInitializer.Configuration_OnSave);
      preferences.OnLoad += new Action<Preferences>(ChromaInitializer.Configuration_OnLoad);
    }

    private static void Configuration_OnLoad(Preferences obj)
    {
      ChromaInitializer._useRazer = obj.Get<bool>("UseRazerRGB", true);
      ChromaInitializer._useCorsair = obj.Get<bool>("UseCorsairRGB", true);
      ChromaInitializer._useLogitech = obj.Get<bool>("UseLogitechRGB", true);
      ChromaInitializer._useSteelSeries = obj.Get<bool>("UseSteelSeriesRGB", true);
      ChromaInitializer._razerColorProfile = obj.Get<VendorColorProfile>("RazerColorProfile", new VendorColorProfile(new Vector3(1f, 0.765f, 0.568f)));
      ChromaInitializer._corsairColorProfile = obj.Get<VendorColorProfile>("CorsairColorProfile", new VendorColorProfile());
      ChromaInitializer._logitechColorProfile = obj.Get<VendorColorProfile>("LogitechColorProfile", new VendorColorProfile());
      ChromaInitializer._steelSeriesColorProfile = obj.Get<VendorColorProfile>("SteelSeriesColorProfile", new VendorColorProfile());
      ChromaInitializer._rgbUpdateRate = obj.Get<float>("RGBUpdatesPerSecond", 45f);
      if ((double) ChromaInitializer._rgbUpdateRate > 1.0000000116860974E-07)
        return;
      ChromaInitializer._rgbUpdateRate = 45f;
    }

    private static void Configuration_OnSave(Preferences preferences)
    {
      preferences.Put("RGBUpdatesPerSecond", (object) ChromaInitializer._rgbUpdateRate);
      preferences.Put("UseRazerRGB", (object) ChromaInitializer._useRazer);
      preferences.Put("RazerColorProfile", (object) ChromaInitializer._razerColorProfile);
      preferences.Put("UseCorsairRGB", (object) ChromaInitializer._useCorsair);
      preferences.Put("CorsairColorProfile", (object) ChromaInitializer._corsairColorProfile);
      preferences.Put("UseLogitechRGB", (object) ChromaInitializer._useLogitech);
      preferences.Put("LogitechColorProfile", (object) ChromaInitializer._logitechColorProfile);
      preferences.Put("UseSteelSeriesRGB", (object) ChromaInitializer._useSteelSeries);
      preferences.Put("SteelSeriesColorProfile", (object) ChromaInitializer._steelSeriesColorProfile);
    }

    private static void AddDevices()
    {
      ChromaInitializer._engine.AddDeviceGroup("Razer", (RgbDeviceGroup) new RazerDeviceGroup(ChromaInitializer._razerColorProfile));
      ChromaInitializer._engine.AddDeviceGroup("Corsair", (RgbDeviceGroup) new CorsairDeviceGroup(ChromaInitializer._corsairColorProfile));
      ChromaInitializer._engine.AddDeviceGroup("Logitech", (RgbDeviceGroup) new LogitechDeviceGroup(ChromaInitializer._logitechColorProfile));
      ChromaInitializer._engine.AddDeviceGroup("SteelSeries", (RgbDeviceGroup) new SteelSeriesDeviceGroup(ChromaInitializer._steelSeriesColorProfile, "TERRARIA", "Terraria", IconColor.Green));
      ChromaInitializer._engine.FrameTimeInSeconds = 1f / ChromaInitializer._rgbUpdateRate;
      if (ChromaInitializer._useRazer)
        ChromaInitializer._engine.EnableDeviceGroup("Razer");
      if (ChromaInitializer._useCorsair)
        ChromaInitializer._engine.EnableDeviceGroup("Corsair");
      if (ChromaInitializer._useLogitech)
        ChromaInitializer._engine.EnableDeviceGroup("Logitech");
      if (ChromaInitializer._useSteelSeries)
        ChromaInitializer._engine.EnableDeviceGroup("SteelSeries");
      ChromaInitializer.LoadSpecialRulesForDevices();
      AppDomain.CurrentDomain.ProcessExit += new EventHandler(ChromaInitializer.OnProcessExit);
      if (!Platform.IsWindows)
        return;
      Application.ApplicationExit += new EventHandler(ChromaInitializer.OnProcessExit);
    }

    private static void LoadSpecialRulesForDevices()
    {
      IntRgbGameValueTracker gameValueTracker1 = new IntRgbGameValueTracker();
      ((ARgbGameValueTracker) gameValueTracker1).EventName = "LIFE";
      ChromaInitializer.Event_LifePercent = gameValueTracker1;
      IntRgbGameValueTracker gameValueTracker2 = new IntRgbGameValueTracker();
      ((ARgbGameValueTracker) gameValueTracker2).EventName = "MANA";
      ChromaInitializer.Event_ManaPercent = gameValueTracker2;
      IntRgbGameValueTracker gameValueTracker3 = new IntRgbGameValueTracker();
      ((ARgbGameValueTracker) gameValueTracker3).EventName = "BREATH";
      ChromaInitializer.Event_BreathPercent = gameValueTracker3;
      ChromaInitializer.LoadSpecialRulesFor_GameSense();
    }

    public static void UpdateEvents()
    {
      if (Main.gameMenu)
      {
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_LifePercent).Update(0, false);
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_ManaPercent).Update(0, false);
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_BreathPercent).Update(0, false);
      }
      else
      {
        Player localPlayer = Main.LocalPlayer;
        int num1 = (int) Utils.Clamp<float>((float) ((double) localPlayer.statLife / (double) localPlayer.statLifeMax2 * 100.0), 0.0f, 100f);
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_LifePercent).Update(num1, true);
        int num2 = (int) Utils.Clamp<float>((float) ((double) localPlayer.statMana / (double) localPlayer.statManaMax2 * 100.0), 0.0f, 100f);
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_ManaPercent).Update(num2, true);
        int num3 = (int) Utils.Clamp<float>((float) ((double) localPlayer.breath / (double) localPlayer.breathMax * 100.0), 0.0f, 100f);
        ((ARgbGameValueTracker<int>) ChromaInitializer.Event_BreathPercent).Update(num3, true);
      }
    }

    private static void LoadSpecialRulesFor_GameSense()
    {
      GameSenseSpecificInfo senseSpecificInfo1 = new GameSenseSpecificInfo();
      List<Bind_Event> eventsToBind = new List<Bind_Event>();
      senseSpecificInfo1.EventsToBind = eventsToBind;
      ChromaInitializer.LoadSpecialRulesFor_GameSense_Keyboard(eventsToBind);
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE1", "zone1", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("one"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE2", "zone2", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("two"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE3", "zone3", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("three"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE4", "zone4", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("four"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE5", "zone5", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("five"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE6", "zone6", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("six"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE7", "zone7", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("seven"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE8", "zone8", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("eight"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE9", "zone9", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("nine"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE10", "zone10", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("ten"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE11", "zone11", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("eleven"));
      ChromaInitializer.LoadSpecialRulesFor_SecondaryDevice(eventsToBind, "ZONE12", "zone12", (AbstractIlluminationDevice_Zone) new RGBZonedDevice("twelve"));
      ChromaInitializer.AddGameplayEvents(eventsToBind);
      GameSenseSpecificInfo senseSpecificInfo2 = senseSpecificInfo1;
      List<ARgbGameValueTracker> gameValueTrackerList = new List<ARgbGameValueTracker>();
      gameValueTrackerList.Add((ARgbGameValueTracker) ChromaInitializer.Event_LifePercent);
      gameValueTrackerList.Add((ARgbGameValueTracker) ChromaInitializer.Event_ManaPercent);
      gameValueTrackerList.Add((ARgbGameValueTracker) ChromaInitializer.Event_BreathPercent);
      senseSpecificInfo2.MiscEvents = gameValueTrackerList;
      foreach (Bind_Event bindEvent in senseSpecificInfo1.EventsToBind)
      {
        ChromaInitializer.EventLocalization eventLocalization;
        if (ChromaInitializer._localizedEvents.TryGetValue(bindEvent.eventName, out eventLocalization))
        {
          bindEvent.defaultDisplayName = eventLocalization.DefaultDisplayName;
          bindEvent.localizedDisplayNames = eventLocalization.LocalizedNames;
        }
      }
      ChromaInitializer._engine.LoadSpecialRules((object) senseSpecificInfo1);
    }

    private static void AddGameplayEvents(List<Bind_Event> eventsToBind)
    {
      eventsToBind.Add(new Bind_Event("TERRARIA", ((ARgbGameValueTracker) ChromaInitializer.Event_LifePercent).EventName, 0, 100, EventIconId.Health, new AbstractHandler[0]));
      eventsToBind.Add(new Bind_Event("TERRARIA", ((ARgbGameValueTracker) ChromaInitializer.Event_ManaPercent).EventName, 0, 100, EventIconId.Mana, new AbstractHandler[0]));
      eventsToBind.Add(new Bind_Event("TERRARIA", ((ARgbGameValueTracker) ChromaInitializer.Event_BreathPercent).EventName, 0, 100, EventIconId.Air, new AbstractHandler[0]));
    }

    private static void LoadSpecialRulesFor_SecondaryDevice(
      List<Bind_Event> eventsToBind,
      string eventName,
      string contextFrameKey,
      AbstractIlluminationDevice_Zone zone)
    {
      Bind_Event bindEvent = new Bind_Event("TERRARIA", eventName, 0, 10, EventIconId.Default, new AbstractHandler[1]
      {
        (AbstractHandler) new ContextColorEventHandlerType()
        {
          ContextFrameKey = contextFrameKey,
          DeviceZone = zone
        }
      });
      eventsToBind.Add(bindEvent);
    }

    private static void LoadSpecialRulesFor_GameSense_Keyboard(List<Bind_Event> eventsToBind)
    {
      Dictionary<string, byte> steelSeriesKeyIndex = HIDCodes.XnaKeyNamesToSteelSeriesKeyIndex;
      Color white = Color.White;
      foreach (KeyValuePair<string, List<string>> keyStatu in PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard].KeyStatus)
      {
        string key1 = keyStatu.Key;
        List<string> stringList = keyStatu.Value;
        List<byte> byteList = new List<byte>();
        foreach (string key2 in stringList)
        {
          byte num;
          if (steelSeriesKeyIndex.TryGetValue(key2, out num))
            byteList.Add(num);
        }
        RGBPerkeyZoneCustom perkeyZoneCustom = new RGBPerkeyZoneCustom(byteList.ToArray());
        ColorStatic colorStatic = new ColorStatic()
        {
          red = white.R,
          green = white.G,
          blue = white.B
        };
        Bind_Event bindEvent = new Bind_Event("TERRARIA", "KEY_" + key1.ToUpper(), 0, 10, EventIconId.Default, new AbstractHandler[1]
        {
          (AbstractHandler) new ContextColorEventHandlerType()
          {
            ContextFrameKey = key1,
            DeviceZone = (AbstractIlluminationDevice_Zone) perkeyZoneCustom
          }
        });
        eventsToBind.Add(bindEvent);
      }
      Bind_Event bindEvent1 = new Bind_Event("TERRARIA", "DO_RAINBOWS", 0, 10, EventIconId.Default, new AbstractHandler[1]
      {
        (AbstractHandler) new PartialBitmapEventHandlerType()
        {
          EventsToExclude = eventsToBind.Select<Bind_Event, string>((Func<Bind_Event, string>) (x => x.eventName)).ToArray<string>()
        }
      });
      eventsToBind.Add(bindEvent1);
    }

    public static void DisableAllDeviceGroups()
    {
      if (ChromaInitializer._engine == null)
        return;
      ChromaInitializer._engine.DisableAllDeviceGroups();
    }

    private static void OnProcessExit(object sender, EventArgs e) => ChromaInitializer.DisableAllDeviceGroups();

    public static void Load()
    {
      ChromaInitializer._engine = Main.Chroma;
      ChromaInitializer.AddDevices();
      Color color = new Color(46, 23, 12);
      ChromaInitializer.RegisterShader("Base", (ChromaShader) new SurfaceBiomeShader(Color.Green, color), CommonConditions.InMenu, (ShaderLayer) 9);
      ChromaInitializer.RegisterShader("Surface Mushroom", (ChromaShader) new SurfaceBiomeShader(Color.DarkBlue, new Color(33, 31, 27)), CommonConditions.DrunkMenu, (ShaderLayer) 9);
      ChromaInitializer.RegisterShader("Sky", (ChromaShader) new SkyShader(new Color(34, 51, 128), new Color(5, 5, 5)), CommonConditions.Depth.Sky, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Surface", (ChromaShader) new SurfaceBiomeShader(Color.Green, color), CommonConditions.Depth.Surface, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Vines", (ChromaShader) new VineShader(), CommonConditions.Depth.Vines, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Underground", (ChromaShader) new CavernShader(new Color(122, 62, 32), new Color(25, 13, 7), 0.5f), CommonConditions.Depth.Underground, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Caverns", (ChromaShader) new CavernShader(color, new Color(25, 25, 25), 0.5f), CommonConditions.Depth.Caverns, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Magma", (ChromaShader) new CavernShader(new Color(181, 17, 0), new Color(25, 25, 25), 0.5f), CommonConditions.Depth.Magma, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Underworld", (ChromaShader) new UnderworldShader(Color.Red, new Color(1f, 0.5f, 0.0f), 1f), CommonConditions.Depth.Underworld, (ShaderLayer) 1);
      ChromaInitializer.RegisterShader("Surface Desert", (ChromaShader) new SurfaceBiomeShader(new Color(84, 49, 0), new Color(245, 225, 33)), CommonConditions.SurfaceBiome.Desert, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Surface Jungle", (ChromaShader) new SurfaceBiomeShader(Color.Green, Color.Teal), CommonConditions.SurfaceBiome.Jungle, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Surface Ocean", (ChromaShader) new SurfaceBiomeShader(Color.SkyBlue, Color.Blue), CommonConditions.SurfaceBiome.Ocean, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Surface Snow", (ChromaShader) new SurfaceBiomeShader(new Color(0, 10, 50), new Color(0.5f, 0.75f, 1f)), CommonConditions.SurfaceBiome.Snow, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Surface Mushroom", (ChromaShader) new SurfaceBiomeShader(Color.DarkBlue, new Color(33, 31, 27)), CommonConditions.SurfaceBiome.Mushroom, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Surface Hallow", (ChromaShader) new HallowSurfaceShader(), CommonConditions.SurfaceBiome.Hallow, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Surface Crimson", (ChromaShader) new CorruptSurfaceShader(Color.Red, new Color(25, 25, 40)), CommonConditions.SurfaceBiome.Crimson, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Surface Corruption", (ChromaShader) new CorruptSurfaceShader(new Color(73, 0, (int) byte.MaxValue), new Color(15, 15, 27)), CommonConditions.SurfaceBiome.Corruption, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Hive", (ChromaShader) new DrippingShader(new Color(0.05f, 0.01f, 0.0f), new Color((int) byte.MaxValue, 150, 0), 0.5f), CommonConditions.UndergroundBiome.Hive, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Underground Mushroom", (ChromaShader) new UndergroundMushroomShader(), CommonConditions.UndergroundBiome.Mushroom, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Underground Corrutpion", (ChromaShader) new UndergroundCorruptionShader(), CommonConditions.UndergroundBiome.Corrupt, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Underground Crimson", (ChromaShader) new DrippingShader(new Color(0.05f, 0.0f, 0.0f), new Color((int) byte.MaxValue, 0, 0)), CommonConditions.UndergroundBiome.Crimson, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Underground Hallow", (ChromaShader) new UndergroundHallowShader(), CommonConditions.UndergroundBiome.Hallow, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Meteorite", (ChromaShader) new MeteoriteShader(), CommonConditions.MiscBiome.Meteorite, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Temple", (ChromaShader) new TempleShader(), CommonConditions.UndergroundBiome.Temple, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Dungeon", (ChromaShader) new DungeonShader(), CommonConditions.UndergroundBiome.Dungeon, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Granite", (ChromaShader) new CavernShader(new Color(14, 19, 46), new Color(5, 0, 30), 0.5f), CommonConditions.UndergroundBiome.Granite, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Marble", (ChromaShader) new CavernShader(new Color(100, 100, 100), new Color(20, 20, 20), 0.5f), CommonConditions.UndergroundBiome.Marble, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Gem Cave", (ChromaShader) new GemCaveShader(color, new Color(25, 25, 25), new Vector4[7]
      {
        Color.White.ToVector4(),
        Color.Yellow.ToVector4(),
        Color.Orange.ToVector4(),
        Color.Red.ToVector4(),
        Color.Green.ToVector4(),
        Color.Blue.ToVector4(),
        Color.Purple.ToVector4()
      })
      {
        CycleTime = 100f,
        ColorRarity = 20f,
        TimeRate = 0.25f
      }, CommonConditions.UndergroundBiome.GemCave, (ShaderLayer) 3);
      Vector4[] gemColors = new Vector4[12];
      for (int index = 0; index < gemColors.Length; ++index)
        gemColors[index] = Main.hslToRgb((float) index / (float) gemColors.Length, 1f, 0.5f).ToVector4();
      ChromaInitializer.RegisterShader("Shimmer", (ChromaShader) new GemCaveShader(Color.Silver * 0.5f, new Color(125, 55, 125), gemColors)
      {
        CycleTime = 2f,
        ColorRarity = 4f,
        TimeRate = 0.5f
      }, CommonConditions.UndergroundBiome.Shimmer, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Underground Jungle", (ChromaShader) new JungleShader(), CommonConditions.UndergroundBiome.Jungle, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Underground Ice", (ChromaShader) new IceShader(new Color(0, 10, 50), new Color(0.5f, 0.75f, 1f)), CommonConditions.UndergroundBiome.Ice, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Corrupt Ice", (ChromaShader) new IceShader(new Color(5, 0, 25), new Color(152, 102, (int) byte.MaxValue)), CommonConditions.UndergroundBiome.CorruptIce, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Crimson Ice", (ChromaShader) new IceShader(new Color(0.1f, 0.0f, 0.0f), new Color(1f, 0.45f, 0.4f)), CommonConditions.UndergroundBiome.CrimsonIce, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Hallow Ice", (ChromaShader) new IceShader(new Color(0.2f, 0.0f, 0.1f), new Color(1f, 0.7f, 0.7f)), CommonConditions.UndergroundBiome.HallowIce, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Underground Desert", (ChromaShader) new DesertShader(new Color(60, 10, 0), new Color((int) byte.MaxValue, 165, 0)), CommonConditions.UndergroundBiome.Desert, (ShaderLayer) 2);
      ChromaInitializer.RegisterShader("Corrupt Desert", (ChromaShader) new DesertShader(new Color(15, 0, 15), new Color(116, 103, (int) byte.MaxValue)), CommonConditions.UndergroundBiome.CorruptDesert, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Crimson Desert", (ChromaShader) new DesertShader(new Color(20, 10, 0), new Color(195, 0, 0)), CommonConditions.UndergroundBiome.CrimsonDesert, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Hallow Desert", (ChromaShader) new DesertShader(new Color(29, 0, 56), new Color((int) byte.MaxValue, 221, (int) byte.MaxValue)), CommonConditions.UndergroundBiome.HallowDesert, (ShaderLayer) 3);
      ChromaInitializer.RegisterShader("Pumpkin Moon", (ChromaShader) new MoonShader(new Color(13, 0, 26), Color.Orange), CommonConditions.Events.PumpkinMoon, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Blood Moon", (ChromaShader) new MoonShader(new Color(10, 0, 0), Color.Red, Color.Red, new Color((int) byte.MaxValue, 150, 125)), CommonConditions.Events.BloodMoon, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Frost Moon", (ChromaShader) new MoonShader(new Color(0, 4, 13), new Color((int) byte.MaxValue, (int) byte.MaxValue, (int) byte.MaxValue)), CommonConditions.Events.FrostMoon, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Solar Eclipse", (ChromaShader) new MoonShader(new Color(0.02f, 0.02f, 0.02f), Color.Orange, Color.Black), CommonConditions.Events.SolarEclipse, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Pirate Invasion", (ChromaShader) new PirateInvasionShader(new Color(173, 173, 173), new Color(101, 101, (int) byte.MaxValue), Color.Blue, Color.Black), CommonConditions.Events.PirateInvasion, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("DD2 Event", (ChromaShader) new DD2Shader(new Color(222, 94, 245), Color.White), CommonConditions.Events.DD2Event, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Goblin Army", (ChromaShader) new GoblinArmyShader(new Color(14, 0, 79), new Color(176, 0, 144)), CommonConditions.Events.GoblinArmy, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Frost Legion", (ChromaShader) new FrostLegionShader(Color.White, new Color(27, 80, 201)), CommonConditions.Events.FrostLegion, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Martian Madness", (ChromaShader) new MartianMadnessShader(new Color(64, 64, 64), new Color(64, 113, 122), new Color((int) byte.MaxValue, (int) byte.MaxValue, 0), new Color(3, 3, 18)), CommonConditions.Events.MartianMadness, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Solar Pillar", (ChromaShader) new PillarShader(Color.Red, Color.Orange), CommonConditions.Events.SolarPillar, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Nebula Pillar", (ChromaShader) new PillarShader(new Color((int) byte.MaxValue, 144, 209), new Color(100, 0, 76)), CommonConditions.Events.NebulaPillar, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Vortex Pillar", (ChromaShader) new PillarShader(Color.Green, Color.Black), CommonConditions.Events.VortexPillar, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Stardust Pillar", (ChromaShader) new PillarShader(new Color(46, 63, (int) byte.MaxValue), Color.White), CommonConditions.Events.StardustPillar, (ShaderLayer) 4);
      ChromaInitializer.RegisterShader("Eater of Worlds", (ChromaShader) new WormShader(new Color(14, 0, 15), new Color(47, 51, 59), new Color(20, 25, 11)), CommonConditions.Boss.EaterOfWorlds, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Eye of Cthulhu", (ChromaShader) new EyeOfCthulhuShader(new Color(145, 145, 126), new Color(138, 0, 0), new Color(3, 3, 18)), CommonConditions.Boss.EyeOfCthulhu, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Skeletron", (ChromaShader) new SkullShader(new Color(110, 92, 47), new Color(36, 32, 51), new Color(0, 0, 0)), CommonConditions.Boss.Skeletron, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Brain Of Cthulhu", (ChromaShader) new BrainShader(new Color(54, 0, 0), new Color(186, 137, 139)), CommonConditions.Boss.BrainOfCthulhu, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Empress of Light", (ChromaShader) new EmpressShader(), CommonConditions.Boss.Empress, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Queen Slime", (ChromaShader) new QueenSlimeShader(new Color(72, 41, 130), new Color(126, 220, (int) byte.MaxValue)), CommonConditions.Boss.QueenSlime, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("King Slime", (ChromaShader) new KingSlimeShader(new Color(41, 70, 130), Color.White), CommonConditions.Boss.KingSlime, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Queen Bee", (ChromaShader) new QueenBeeShader(new Color(5, 5, 0), new Color((int) byte.MaxValue, 235, 0)), CommonConditions.Boss.QueenBee, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Wall of Flesh", (ChromaShader) new WallOfFleshShader(new Color(112, 48, 60), new Color(5, 0, 0)), CommonConditions.Boss.WallOfFlesh, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Destroyer", (ChromaShader) new WormShader(new Color(25, 25, 25), new Color(192, 0, 0), new Color(10, 0, 0)), CommonConditions.Boss.Destroyer, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Skeletron Prime", (ChromaShader) new SkullShader(new Color(110, 92, 47), new Color(79, 0, 0), new Color((int) byte.MaxValue, 29, 0)), CommonConditions.Boss.SkeletronPrime, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("The Twins", (ChromaShader) new TwinsShader(new Color(145, 145, 126), new Color(138, 0, 0), new Color(138, 0, 0), new Color(20, 20, 20), new Color(65, 140, 0), new Color(3, 3, 18)), CommonConditions.Boss.TheTwins, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Duke Fishron", (ChromaShader) new DukeFishronShader(new Color(0, 0, 122), new Color(100, 254, 194)), CommonConditions.Boss.DukeFishron, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Deerclops", (ChromaShader) new BlizzardShader(new Vector4(1f, 1f, 1f, 1f), new Vector4(0.15f, 0.1f, 0.4f, 1f), -0.1f, 0.4f), CommonConditions.Boss.Deerclops, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Plantera", (ChromaShader) new PlanteraShader(new Color((int) byte.MaxValue, 0, 220), new Color(0, (int) byte.MaxValue, 0), new Color(12, 4, 0)), CommonConditions.Boss.Plantera, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Golem", (ChromaShader) new GolemShader(new Color((int) byte.MaxValue, 144, 0), new Color((int) byte.MaxValue, 198, 0), new Color(10, 10, 0)), CommonConditions.Boss.Golem, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Cultist", (ChromaShader) new CultistShader(), CommonConditions.Boss.Cultist, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Moon Lord", (ChromaShader) new EyeballShader(false), CommonConditions.Boss.MoonLord, (ShaderLayer) 5);
      ChromaInitializer.RegisterShader("Rain", (ChromaShader) new RainShader(), CommonConditions.Weather.Rain, (ShaderLayer) 6);
      ChromaInitializer.RegisterShader("Snowstorm", (ChromaShader) new BlizzardShader(new Vector4(1f, 1f, 1f, 1f), new Vector4(0.1f, 0.1f, 0.3f, 1f), 0.35f, -0.35f), CommonConditions.Weather.Blizzard, (ShaderLayer) 6);
      ChromaInitializer.RegisterShader("Sandstorm", (ChromaShader) new SandstormShader(), CommonConditions.Weather.Sandstorm, (ShaderLayer) 6);
      ChromaInitializer.RegisterShader("Slime Rain", (ChromaShader) new SlimeRainShader(), CommonConditions.Weather.SlimeRain, (ShaderLayer) 6);
      ChromaInitializer.RegisterShader("Drowning", (ChromaShader) new DrowningShader(), CommonConditions.Alert.Drowning, (ShaderLayer) 7);
      ChromaInitializer.RegisterShader("Keybinds", (ChromaShader) new KeybindsMenuShader(), CommonConditions.Alert.Keybinds, (ShaderLayer) 7);
      ChromaInitializer.RegisterShader("Lava Indicator", (ChromaShader) new LavaIndicatorShader(Color.Black, Color.Red, new Color((int) byte.MaxValue, 188, 0)), CommonConditions.Alert.LavaIndicator, (ShaderLayer) 7);
      ChromaInitializer.RegisterShader("Moon Lord Spawn", (ChromaShader) new EyeballShader(true), CommonConditions.Alert.MoonlordComing, (ShaderLayer) 7);
      ChromaInitializer.RegisterShader("Low Life", (ChromaShader) new LowLifeShader(), CommonConditions.CriticalAlert.LowLife, (ShaderLayer) 8);
      ChromaInitializer.RegisterShader("Death", (ChromaShader) new DeathShader(new Color(36, 0, 10), new Color(158, 28, 53)), CommonConditions.CriticalAlert.Death, (ShaderLayer) 8);
    }

    private static void RegisterShader(
      string name,
      ChromaShader shader,
      ChromaCondition condition,
      ShaderLayer layer)
    {
      ChromaInitializer._engine.RegisterShader(shader, condition, layer);
    }

    [Conditional("DEBUG")]
    private static void AddDebugDraw()
    {
      BasicDebugDrawer basicDebugDrawer = new BasicDebugDrawer(Main.instance.GraphicsDevice);
      Filters.Scene.OnPostDraw += (Action) (() => { });
    }

    public struct EventLocalization
    {
      public string DefaultDisplayName;
      public Dictionary<string, string> LocalizedNames;
    }
  }
}
