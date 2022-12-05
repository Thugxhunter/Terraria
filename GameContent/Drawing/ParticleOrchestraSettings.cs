// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.Drawing.ParticleOrchestraSettings
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.IO;

namespace Terraria.GameContent.Drawing
{
  public struct ParticleOrchestraSettings
  {
    public Vector2 PositionInWorld;
    public Vector2 MovementVector;
    public int UniqueInfoPiece;
    public byte IndexOfPlayerWhoInvokedThis;
    public const int SerializationSize = 21;

    public void Serialize(BinaryWriter writer)
    {
      writer.WriteVector2(this.PositionInWorld);
      writer.WriteVector2(this.MovementVector);
      writer.Write(this.UniqueInfoPiece);
      writer.Write(this.IndexOfPlayerWhoInvokedThis);
    }

    public void DeserializeFrom(BinaryReader reader)
    {
      this.PositionInWorld = reader.ReadVector2();
      this.MovementVector = reader.ReadVector2();
      this.UniqueInfoPiece = reader.ReadInt32();
      this.IndexOfPlayerWhoInvokedThis = reader.ReadByte();
    }
  }
}
