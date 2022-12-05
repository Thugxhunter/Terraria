// Decompiled with JetBrains decompiler
// Type: Terraria.ID.TorchID
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Runtime.InteropServices;

namespace Terraria.ID
{
  public static class TorchID
  {
    public static int[] Dust = new int[24]
    {
      6,
      59,
      60,
      61,
      62,
      63,
      64,
      65,
      75,
      135,
      158,
      169,
      156,
      234,
      66,
      242,
      293,
      294,
      295,
      296,
      297,
      298,
      307,
      310
    };
    private static TorchID.ITorchLightProvider[] _lights;
    public const short Torch = 0;
    public const short Blue = 1;
    public const short Red = 2;
    public const short Green = 3;
    public const short Purple = 4;
    public const short White = 5;
    public const short Yellow = 6;
    public const short Demon = 7;
    public const short Cursed = 8;
    public const short Ice = 9;
    public const short Orange = 10;
    public const short Ichor = 11;
    public const short UltraBright = 12;
    public const short Bone = 13;
    public const short Rainbow = 14;
    public const short Pink = 15;
    public const short Desert = 16;
    public const short Coral = 17;
    public const short Corrupt = 18;
    public const short Crimson = 19;
    public const short Hallowed = 20;
    public const short Jungle = 21;
    public const short Mushroom = 22;
    public const short Shimmer = 23;
    public static readonly short Count = 24;

    public static void Initialize()
    {
      TorchID.ITorchLightProvider[] torchLightProviderArray = new TorchID.ITorchLightProvider[(int) TorchID.Count];
      torchLightProviderArray[0] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1f, 0.95f, 0.8f);
      torchLightProviderArray[1] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.0f, 0.1f, 1.3f);
      torchLightProviderArray[2] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1f, 0.1f, 0.1f);
      torchLightProviderArray[3] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.0f, 1f, 0.1f);
      torchLightProviderArray[4] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.9f, 0.0f, 0.9f);
      torchLightProviderArray[5] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1.4f, 1.4f, 1.4f);
      torchLightProviderArray[6] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.9f, 0.9f, 0.0f);
      torchLightProviderArray[7] = (TorchID.ITorchLightProvider) new TorchID.DemonTorchLight();
      torchLightProviderArray[8] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1f, 1.6f, 0.5f);
      torchLightProviderArray[9] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.75f, 0.85f, 1.4f);
      torchLightProviderArray[10] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1f, 0.5f, 0.0f);
      torchLightProviderArray[11] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1.4f, 1.4f, 0.7f);
      torchLightProviderArray[12] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.75f, 1.3499999f, 1.5f);
      torchLightProviderArray[13] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.95f, 0.75f, 1.3f);
      torchLightProviderArray[14] = (TorchID.ITorchLightProvider) new TorchID.DiscoTorchLight();
      torchLightProviderArray[15] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1f, 0.0f, 1f);
      torchLightProviderArray[16] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1.4f, 0.85f, 0.55f);
      torchLightProviderArray[17] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.25f, 1.3f, 0.8f);
      torchLightProviderArray[18] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.95f, 0.4f, 1.4f);
      torchLightProviderArray[19] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1.4f, 0.7f, 0.5f);
      torchLightProviderArray[20] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(1.25f, 0.6f, 1.2f);
      torchLightProviderArray[21] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.75f, 1.45f, 0.9f);
      torchLightProviderArray[22] = (TorchID.ITorchLightProvider) new TorchID.ConstantTorchLight(0.3f, 0.78f, 1.2f);
      torchLightProviderArray[23] = (TorchID.ITorchLightProvider) new TorchID.ShimmerTorchLight();
      TorchID._lights = torchLightProviderArray;
    }

    public static void TorchColor(int torchID, out float R, out float G, out float B)
    {
      if (torchID < 0 || torchID >= TorchID._lights.Length)
        R = G = B = 0.0f;
      else
        TorchID._lights[torchID].GetRGB(out R, out G, out B);
    }

    private interface ITorchLightProvider
    {
      void GetRGB(out float r, out float g, out float b);
    }

    private struct ConstantTorchLight : TorchID.ITorchLightProvider
    {
      public float R;
      public float G;
      public float B;

      public ConstantTorchLight(float Red, float Green, float Blue)
      {
        this.R = Red;
        this.G = Green;
        this.B = Blue;
      }

      public void GetRGB(out float r, out float g, out float b)
      {
        r = this.R;
        g = this.G;
        b = this.B;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct DemonTorchLight : TorchID.ITorchLightProvider
    {
      public void GetRGB(out float r, out float g, out float b)
      {
        r = (float) (0.5 * (double) Main.demonTorch + (1.0 - (double) Main.demonTorch));
        g = 0.3f;
        b = Main.demonTorch + (float) (0.5 * (1.0 - (double) Main.demonTorch));
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct ShimmerTorchLight : TorchID.ITorchLightProvider
    {
      public void GetRGB(out float r, out float g, out float b)
      {
        float num1 = 0.9f;
        float num2 = 0.9f;
        float num3 = num1 + (float) (270 - (int) Main.mouseTextColor) / 900f;
        float num4 = num2 + (float) (270 - (int) Main.mouseTextColor) / 125f;
        float num5 = MathHelper.Clamp(num3, 0.0f, 1f);
        float num6 = MathHelper.Clamp(num4, 0.0f, 1f);
        r = num5 * 0.9f;
        g = num6 * 0.55f;
        b = num5 * 1.2f;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct DiscoTorchLight : TorchID.ITorchLightProvider
    {
      public void GetRGB(out float r, out float g, out float b)
      {
        r = (float) Main.DiscoR / (float) byte.MaxValue;
        g = (float) Main.DiscoG / (float) byte.MaxValue;
        b = (float) Main.DiscoB / (float) byte.MaxValue;
      }
    }
  }
}
