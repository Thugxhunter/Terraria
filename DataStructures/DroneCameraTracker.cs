// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.DroneCameraTracker
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;

namespace Terraria.DataStructures
{
  public class DroneCameraTracker
  {
    private Projectile _trackedProjectile;
    private int _lastTrackedType;
    private bool _inUse;

    public void Track(Projectile proj)
    {
      this._trackedProjectile = proj;
      this._lastTrackedType = proj.type;
    }

    public void Clear() => this._trackedProjectile = (Projectile) null;

    public void WorldClear()
    {
      this._lastTrackedType = 0;
      this._trackedProjectile = (Projectile) null;
      this._inUse = false;
    }

    private void ValidateTrackedProjectile()
    {
      if (this._trackedProjectile != null && this._trackedProjectile.active && this._trackedProjectile.type == this._lastTrackedType && this._trackedProjectile.owner == Main.myPlayer && Main.LocalPlayer.remoteVisionForDrone)
        return;
      this.Clear();
    }

    public bool IsInUse() => this._inUse;

    public bool TryTracking(out Vector2 cameraPosition)
    {
      this.ValidateTrackedProjectile();
      cameraPosition = new Vector2();
      if (this._trackedProjectile == null)
      {
        this._inUse = false;
        return false;
      }
      cameraPosition = this._trackedProjectile.Center;
      this._inUse = true;
      return true;
    }
  }
}
