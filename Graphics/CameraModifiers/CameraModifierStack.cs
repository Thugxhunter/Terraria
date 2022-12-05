// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.CameraModifiers.CameraModifierStack
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.Graphics.CameraModifiers
{
  public class CameraModifierStack
  {
    private List<ICameraModifier> _modifiers = new List<ICameraModifier>();

    public void Add(ICameraModifier modifier)
    {
      this.RemoveIdenticalModifiers(modifier);
      this._modifiers.Add(modifier);
    }

    private void RemoveIdenticalModifiers(ICameraModifier modifier)
    {
      string uniqueIdentity = modifier.UniqueIdentity;
      if (uniqueIdentity == null)
        return;
      for (int index = this._modifiers.Count - 1; index >= 0; --index)
      {
        if (this._modifiers[index].UniqueIdentity == uniqueIdentity)
          this._modifiers.RemoveAt(index);
      }
    }

    public void ApplyTo(ref Vector2 cameraPosition)
    {
      CameraInfo cameraPosition1 = new CameraInfo(cameraPosition);
      this.ClearFinishedModifiers();
      for (int index = 0; index < this._modifiers.Count; ++index)
        this._modifiers[index].Update(ref cameraPosition1);
      cameraPosition = cameraPosition1.CameraPosition;
    }

    private void ClearFinishedModifiers()
    {
      for (int index = this._modifiers.Count - 1; index >= 0; --index)
      {
        if (this._modifiers[index].Finished)
          this._modifiers.RemoveAt(index);
      }
    }
  }
}
