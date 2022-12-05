// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.CameraModifiers.PunchCameraModifier
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System;

namespace Terraria.Graphics.CameraModifiers
{
  public class PunchCameraModifier : ICameraModifier
  {
    private int _framesToLast;
    private Vector2 _startPosition;
    private Vector2 _direction;
    private float _distanceFalloff;
    private float _strength;
    private float _vibrationCyclesPerSecond;
    private int _framesLasted;

    public string UniqueIdentity { get; private set; }

    public bool Finished { get; private set; }

    public PunchCameraModifier(
      Vector2 startPosition,
      Vector2 direction,
      float strength,
      float vibrationCyclesPerSecond,
      int frames,
      float distanceFalloff = -1f,
      string uniqueIdentity = null)
    {
      this._startPosition = startPosition;
      this._direction = direction;
      this._strength = strength;
      this._vibrationCyclesPerSecond = vibrationCyclesPerSecond;
      this._framesToLast = frames;
      this._distanceFalloff = distanceFalloff;
      this.UniqueIdentity = uniqueIdentity;
    }

    public void Update(ref CameraInfo cameraInfo)
    {
      float num1 = (float) Math.Cos((double) this._framesLasted / 60.0 * (double) this._vibrationCyclesPerSecond * 6.2831854820251465);
      float num2 = Utils.Remap((float) this._framesLasted, 0.0f, (float) this._framesToLast, 1f, 0.0f);
      float num3 = Utils.Remap(Vector2.Distance(this._startPosition, cameraInfo.OriginalCameraCenter), 0.0f, this._distanceFalloff, 1f, 0.0f);
      if ((double) this._distanceFalloff == -1.0)
        num3 = 1f;
      cameraInfo.CameraPosition += this._direction * num1 * this._strength * num2 * num3;
      ++this._framesLasted;
      if (this._framesLasted < this._framesToLast)
        return;
      this.Finished = true;
    }
  }
}
