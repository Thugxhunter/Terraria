// Decompiled with JetBrains decompiler
// Type: Terraria.Social.WeGame.IPCServer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using System;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace Terraria.Social.WeGame
{
  public class IPCServer : IPCBase
  {
    private string _serverName;
    private bool _haveClientAccessFlag;

    public event Action OnClientAccess;

    public override event Action<byte[]> OnDataArrive
    {
      add => this._onDataArrive = this._onDataArrive + value;
      remove => this._onDataArrive = this._onDataArrive - value;
    }

    private NamedPipeServerStream GetPipeStream() => (NamedPipeServerStream) this._pipeStream;

    public void Init(string serverName) => this._serverName = serverName;

    private void LazyCreatePipe()
    {
      if (this.GetPipeStream() != null)
        return;
      this._pipeStream = (PipeStream) new NamedPipeServerStream(this._serverName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
      this._cancelTokenSrc = new CancellationTokenSource();
    }

    public override void ReadCallback(IAsyncResult result)
    {
      IPCContent asyncState = (IPCContent) result.AsyncState;
      base.ReadCallback(result);
      if (!asyncState.CancelToken.IsCancellationRequested)
        this.ContinueReadOrWait();
      else
        WeGameHelper.WriteDebugString("servcer.ReadCallback cancel");
    }

    public void StartListen()
    {
      this.LazyCreatePipe();
      WeGameHelper.WriteDebugString("begin listen");
      this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), (object) this._cancelTokenSrc.Token);
    }

    private void RestartListen() => this.StartListen();

    private void ConnectionCallback(IAsyncResult result)
    {
      try
      {
        this._haveClientAccessFlag = true;
        WeGameHelper.WriteDebugString("Connected in");
        this.GetPipeStream().EndWaitForConnection(result);
        if (!((CancellationToken) result.AsyncState).IsCancellationRequested)
          this.BeginReadData();
        else
          WeGameHelper.WriteDebugString("ConnectionCallback but user cancel");
      }
      catch (IOException ex)
      {
        this._pipeBrokenFlag = true;
        WeGameHelper.WriteDebugString("ConnectionCallback Exception, {0}", (object) ex.Message);
      }
    }

    public void ContinueReadOrWait()
    {
      if (this.GetPipeStream().IsConnected)
      {
        this.BeginReadData();
      }
      else
      {
        try
        {
          this.GetPipeStream().BeginWaitForConnection(new AsyncCallback(this.ConnectionCallback), (object) null);
        }
        catch (IOException ex)
        {
          this._pipeBrokenFlag = true;
          WeGameHelper.WriteDebugString("ContinueReadOrWait Exception, {0}", (object) ex.Message);
        }
      }
    }

    private void ProcessClientAccessEvent()
    {
      if (!this._haveClientAccessFlag)
        return;
      if (this.OnClientAccess != null)
        this.OnClientAccess();
      this._haveClientAccessFlag = false;
    }

    private void CheckFlagAndFireEvent()
    {
      this.ProcessClientAccessEvent();
      this.ProcessDataArriveEvent();
      this.ProcessPipeBrokenEvent();
    }

    private void ProcessPipeBrokenEvent()
    {
      if (!this._pipeBrokenFlag)
        return;
      this.Reset();
      this._pipeBrokenFlag = false;
      this.RestartListen();
    }

    public void Tick() => this.CheckFlagAndFireEvent();
  }
}
