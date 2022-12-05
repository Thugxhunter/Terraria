// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.ObjectInteractions.ProjectileSmartInteractCandidateProvider
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Terraria.GameContent.ObjectInteractions
{
  public class ProjectileSmartInteractCandidateProvider : ISmartInteractCandidateProvider
  {
    private ProjectileSmartInteractCandidateProvider.ReusableCandidate _candidate = new ProjectileSmartInteractCandidateProvider.ReusableCandidate();

    public void ClearSelfAndPrepareForCheck() => Main.SmartInteractProj = -1;

    public bool ProvideCandidate(
      SmartInteractScanSettings settings,
      out ISmartInteractCandidate candidate)
    {
      candidate = (ISmartInteractCandidate) null;
      if (!settings.FullInteraction)
        return false;
      List<int> interactWithHack = settings.player.GetListOfProjectilesToInteractWithHack();
      bool flag = false;
      Vector2 mousevec = settings.mousevec;
      mousevec.ToPoint();
      int projectileIndex = -1;
      float projectileDistanceFromCursor = -1f;
      for (int index1 = 0; index1 < interactWithHack.Count; ++index1)
      {
        int index2 = interactWithHack[index1];
        Projectile projectile = Main.projectile[index2];
        if (projectile.active)
        {
          float num = projectile.Hitbox.Distance(mousevec);
          if ((projectileIndex == -1 ? 1 : ((double) Main.projectile[projectileIndex].Hitbox.Distance(mousevec) > (double) num ? 1 : 0)) != 0)
          {
            projectileIndex = index2;
            projectileDistanceFromCursor = num;
          }
          if ((double) num == 0.0)
          {
            flag = true;
            projectileIndex = index2;
            projectileDistanceFromCursor = num;
            break;
          }
        }
      }
      if (settings.DemandOnlyZeroDistanceTargets && !flag || projectileIndex == -1)
        return false;
      this._candidate.Reuse(projectileIndex, projectileDistanceFromCursor);
      candidate = (ISmartInteractCandidate) this._candidate;
      return true;
    }

    private class ReusableCandidate : ISmartInteractCandidate
    {
      private int _projectileIndexToTarget;

      public float DistanceFromCursor { get; private set; }

      public void WinCandidacy()
      {
        Main.SmartInteractProj = this._projectileIndexToTarget;
        Main.SmartInteractShowingGenuine = true;
      }

      public void Reuse(int projectileIndex, float projectileDistanceFromCursor)
      {
        this._projectileIndexToTarget = projectileIndex;
        this.DistanceFromCursor = projectileDistanceFromCursor;
      }
    }
  }
}
