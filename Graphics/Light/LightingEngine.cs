// Decompiled with JetBrains decompiler
// Type: Terraria.Graphics.Light.LightingEngine
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using ReLogic.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace Terraria.Graphics.Light
{
  public class LightingEngine : ILightingEngine
  {
    public const int AREA_PADDING = 28;
    private const int NON_VISIBLE_PADDING = 18;
    private readonly List<LightingEngine.PerFrameLight> _perFrameLights = new List<LightingEngine.PerFrameLight>();
    private TileLightScanner _tileScanner = new TileLightScanner();
    private LightMap _activeLightMap = new LightMap();
    private Rectangle _activeProcessedArea;
    private LightMap _workingLightMap = new LightMap();
    private Rectangle _workingProcessedArea;
    private readonly Stopwatch _timer = new Stopwatch();
    private LightingEngine.EngineState _state;

    public void AddLight(int x, int y, Vector3 color) => this._perFrameLights.Add(new LightingEngine.PerFrameLight(new Point(x, y), color));

    public void Clear()
    {
      this._activeLightMap.Clear();
      this._workingLightMap.Clear();
      this._perFrameLights.Clear();
    }

    public Vector3 GetColor(int x, int y)
    {
      if (!this._activeProcessedArea.Contains(x, y))
        return Vector3.Zero;
      x -= this._activeProcessedArea.X;
      y -= this._activeProcessedArea.Y;
      return this._activeLightMap[x, y];
    }

    public void ProcessArea(Rectangle area)
    {
      Main.renderCount = (Main.renderCount + 1) % 4;
      this._timer.Start();
      TimeLogger.LightingTime(0, 0.0);
      switch (this._state)
      {
        case LightingEngine.EngineState.MinimapUpdate:
          if (Main.mapDelay > 0)
            --Main.mapDelay;
          else
            this.ExportToMiniMap();
          TimeLogger.LightingTime(1, this._timer.Elapsed.TotalMilliseconds);
          break;
        case LightingEngine.EngineState.ExportMetrics:
          area.Inflate(28, 28);
          Main.SceneMetrics.ScanAndExportToMain(new SceneMetricsScanSettings()
          {
            VisualScanArea = new Rectangle?(area),
            BiomeScanCenterPositionInWorld = new Vector2?(Main.LocalPlayer.Center),
            ScanOreFinderData = Main.LocalPlayer.accOreFinder
          });
          TimeLogger.LightingTime(2, this._timer.Elapsed.TotalMilliseconds);
          break;
        case LightingEngine.EngineState.Scan:
          this.ProcessScan(area);
          TimeLogger.LightingTime(3, this._timer.Elapsed.TotalMilliseconds);
          break;
        case LightingEngine.EngineState.Blur:
          this.ProcessBlur();
          this.Present();
          TimeLogger.LightingTime(4, this._timer.Elapsed.TotalMilliseconds);
          break;
      }
      this.IncrementState();
      this._timer.Reset();
    }

    private void IncrementState() => this._state = (LightingEngine.EngineState) ((int) (this._state + 1) % 4);

    private void ProcessScan(Rectangle area)
    {
      area.Inflate(28, 28);
      this._workingProcessedArea = area;
      this._workingLightMap.SetSize(area.Width, area.Height);
      this._workingLightMap.NonVisiblePadding = 18;
      this._tileScanner.Update();
      this._tileScanner.ExportTo(area, this._workingLightMap, new TileLightScannerOptions()
      {
        DrawInvisibleWalls = Main.ShouldShowInvisibleWalls()
      });
    }

    private void ProcessBlur()
    {
      this.UpdateLightDecay();
      this.ApplyPerFrameLights();
      this._workingLightMap.Blur();
    }

    private void Present()
    {
      Utils.Swap<LightMap>(ref this._activeLightMap, ref this._workingLightMap);
      Utils.Swap<Rectangle>(ref this._activeProcessedArea, ref this._workingProcessedArea);
    }

    private void UpdateLightDecay()
    {
      LightMap workingLightMap = this._workingLightMap;
      workingLightMap.LightDecayThroughAir = 0.91f;
      workingLightMap.LightDecayThroughSolid = 0.56f;
      workingLightMap.LightDecayThroughHoney = new Vector3(0.75f, 0.7f, 0.6f) * 0.91f;
      switch (Main.waterStyle)
      {
        case 0:
        case 1:
        case 7:
        case 8:
          workingLightMap.LightDecayThroughWater = new Vector3(0.88f, 0.96f, 1.015f) * 0.91f;
          break;
        case 2:
          workingLightMap.LightDecayThroughWater = new Vector3(0.94f, 0.85f, 1.01f) * 0.91f;
          break;
        case 3:
          workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.95f, 1.015f) * 0.91f;
          break;
        case 4:
          workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 0.86f, 1.01f) * 0.91f;
          break;
        case 5:
          workingLightMap.LightDecayThroughWater = new Vector3(0.84f, 0.99f, 1.01f) * 0.91f;
          break;
        case 6:
          workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 0.93f, 0.98f) * 0.91f;
          break;
        case 9:
          workingLightMap.LightDecayThroughWater = new Vector3(1f, 0.88f, 0.84f) * 0.91f;
          break;
        case 10:
          workingLightMap.LightDecayThroughWater = new Vector3(0.83f, 1f, 1f) * 0.91f;
          break;
        case 12:
          workingLightMap.LightDecayThroughWater = new Vector3(0.95f, 0.98f, 0.85f) * 0.91f;
          break;
        case 13:
          workingLightMap.LightDecayThroughWater = new Vector3(0.9f, 1f, 1.02f) * 0.91f;
          break;
      }
      if (Main.player[Main.myPlayer].nightVision)
      {
        workingLightMap.LightDecayThroughAir *= 1.03f;
        workingLightMap.LightDecayThroughSolid *= 1.03f;
      }
      if (Main.player[Main.myPlayer].blind)
      {
        workingLightMap.LightDecayThroughAir *= 0.95f;
        workingLightMap.LightDecayThroughSolid *= 0.95f;
      }
      if (Main.player[Main.myPlayer].blackout)
      {
        workingLightMap.LightDecayThroughAir *= 0.85f;
        workingLightMap.LightDecayThroughSolid *= 0.85f;
      }
      if (Main.player[Main.myPlayer].headcovered)
      {
        workingLightMap.LightDecayThroughAir *= 0.85f;
        workingLightMap.LightDecayThroughSolid *= 0.85f;
      }
      workingLightMap.LightDecayThroughAir *= Player.airLightDecay;
      workingLightMap.LightDecayThroughSolid *= Player.solidLightDecay;
    }

    private void ApplyPerFrameLights()
    {
      for (int index = 0; index < this._perFrameLights.Count; ++index)
      {
        Point position = this._perFrameLights[index].Position;
        if (this._workingProcessedArea.Contains(position))
        {
          Vector3 result = this._perFrameLights[index].Color;
          Vector3 workingLight = this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y];
          Vector3.Max(ref workingLight, ref result, out result);
          this._workingLightMap[position.X - this._workingProcessedArea.X, position.Y - this._workingProcessedArea.Y] = result;
        }
      }
      this._perFrameLights.Clear();
    }

    public void Rebuild()
    {
      this._activeProcessedArea = Rectangle.Empty;
      this._workingProcessedArea = Rectangle.Empty;
      this._state = LightingEngine.EngineState.MinimapUpdate;
      this._activeLightMap = new LightMap();
      this._workingLightMap = new LightMap();
    }

    private void ExportToMiniMap()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LightingEngine.\u003C\u003Ec__DisplayClass22_0 cDisplayClass220 = new LightingEngine.\u003C\u003Ec__DisplayClass22_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass220.\u003C\u003E4__this = this;
      if (!Main.mapEnabled || this._activeProcessedArea.Width <= 0 || this._activeProcessedArea.Height <= 0)
        return;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass220.area = new Rectangle(this._activeProcessedArea.X + 28, this._activeProcessedArea.Y + 28, this._activeProcessedArea.Width - 56, this._activeProcessedArea.Height - 56);
      Rectangle rectangle = new Rectangle(0, 0, Main.maxTilesX, Main.maxTilesY);
      rectangle.Inflate(-40, -40);
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass220.area = Rectangle.Intersect(cDisplayClass220.area, rectangle);
      // ISSUE: reference to a compiler-generated field
      Main.mapMinX = cDisplayClass220.area.Left;
      // ISSUE: reference to a compiler-generated field
      Main.mapMinY = cDisplayClass220.area.Top;
      // ISSUE: reference to a compiler-generated field
      Main.mapMaxX = cDisplayClass220.area.Right;
      // ISSUE: reference to a compiler-generated field
      Main.mapMaxY = cDisplayClass220.area.Bottom;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      FastParallel.For(cDisplayClass220.area.Left, cDisplayClass220.area.Right, new ParallelForAction((object) cDisplayClass220, __methodptr(\u003CExportToMiniMap\u003Eb__0)), (object) null);
      Main.updateMap = true;
    }

    private enum EngineState
    {
      MinimapUpdate,
      ExportMetrics,
      Scan,
      Blur,
      Max,
    }

    private struct PerFrameLight
    {
      public readonly Point Position;
      public readonly Vector3 Color;

      public PerFrameLight(Point position, Vector3 color)
      {
        this.Position = position;
        this.Color = color;
      }
    }
  }
}
