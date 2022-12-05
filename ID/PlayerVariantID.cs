// Decompiled with JetBrains decompiler
// Type: Terraria.ID.PlayerVariantID
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

namespace Terraria.ID
{
  public static class PlayerVariantID
  {
    public const int MaleStarter = 0;
    public const int MaleSticker = 1;
    public const int MaleGangster = 2;
    public const int MaleCoat = 3;
    public const int FemaleStarter = 4;
    public const int FemaleSticker = 5;
    public const int FemaleGangster = 6;
    public const int FemaleCoat = 7;
    public const int MaleDress = 8;
    public const int FemaleDress = 9;
    public const int MaleDisplayDoll = 10;
    public const int FemaleDisplayDoll = 11;
    public static readonly int Count = 12;

    public class Sets
    {
      public static SetFactory Factory = new SetFactory(PlayerVariantID.Count);
      public static bool[] Male = PlayerVariantID.Sets.Factory.CreateBoolSet(0, 1, 2, 3, 8, 10);
      public static int[] AltGenderReference = PlayerVariantID.Sets.Factory.CreateIntSet(0, 0, 4, 4, 0, 1, 5, 5, 1, 2, 6, 6, 2, 3, 7, 7, 3, 8, 9, 9, 8, 10, 11, 11, 10);
      public static int[] VariantOrderMale = new int[6]
      {
        0,
        1,
        2,
        3,
        8,
        10
      };
      public static int[] VariantOrderFemale = new int[6]
      {
        4,
        5,
        6,
        7,
        9,
        11
      };
    }
  }
}
