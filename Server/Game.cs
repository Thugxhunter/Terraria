// Decompiled with JetBrains decompiler
// Type: Terraria.Server.Game
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Terraria.Server
{
  public class Game : IDisposable
  {
    public GameComponentCollection Components => (GameComponentCollection) null;

    public ContentManager Content
    {
      get => (ContentManager) null;
      set
      {
      }
    }

    public GraphicsDevice GraphicsDevice => (GraphicsDevice) null;

    public TimeSpan InactiveSleepTime
    {
      get => TimeSpan.Zero;
      set
      {
      }
    }

    public bool IsActive => true;

    public bool IsFixedTimeStep
    {
      get => true;
      set
      {
      }
    }

    public bool IsMouseVisible
    {
      get => false;
      set
      {
      }
    }

    public LaunchParameters LaunchParameters => (LaunchParameters) null;

    public GameServiceContainer Services => (GameServiceContainer) null;

    public TimeSpan TargetElapsedTime
    {
      get => TimeSpan.Zero;
      set
      {
      }
    }

    public GameWindow Window => (GameWindow) null;

    public event EventHandler<EventArgs> Activated;

    public event EventHandler<EventArgs> Deactivated;

    public event EventHandler<EventArgs> Disposed;

    public event EventHandler<EventArgs> Exiting;

    protected virtual bool BeginDraw() => true;

    protected virtual void BeginRun()
    {
    }

    public void Dispose()
    {
    }

    protected virtual void Dispose(bool disposing)
    {
    }

    protected virtual void Draw(GameTime gameTime)
    {
    }

    protected virtual void EndDraw()
    {
    }

    protected virtual void EndRun()
    {
    }

    public void Exit()
    {
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void LoadContent()
    {
    }

    protected virtual void OnActivated(object sender, EventArgs args)
    {
    }

    protected virtual void OnDeactivated(object sender, EventArgs args)
    {
    }

    protected virtual void OnExiting(object sender, EventArgs args)
    {
    }

    public void ResetElapsedTime()
    {
    }

    public void Run()
    {
    }

    public void RunOneFrame()
    {
    }

    protected virtual bool ShowMissingRequirementMessage(Exception exception) => true;

    public void SuppressDraw()
    {
    }

    public void Tick()
    {
    }

    protected virtual void UnloadContent()
    {
    }

    protected virtual void Update(GameTime gameTime)
    {
    }
  }
}
