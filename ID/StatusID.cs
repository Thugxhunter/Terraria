// Decompiled with JetBrains decompiler
// Type: Terraria.ID.StatusID
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using ReLogic.Reflection;

namespace Terraria.ID
{
  public class StatusID
  {
    public const int Ok = 0;
    public const int LaterVersion = 1;
    public const int UnknownError = 2;
    public const int EmptyFile = 3;
    public const int DecryptionError = 4;
    public const int BadSectionPointer = 5;
    public const int BadFooter = 6;
    public static readonly IdDictionary Search = IdDictionary.Create<StatusID, int>();
  }
}
