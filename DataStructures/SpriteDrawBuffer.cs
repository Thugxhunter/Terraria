// Decompiled with JetBrains decompiler
// Type: Terraria.DataStructures.SpriteDrawBuffer
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.Graphics;

namespace Terraria.DataStructures
{
  public class SpriteDrawBuffer
  {
    private GraphicsDevice graphicsDevice;
    private DynamicVertexBuffer vertexBuffer;
    private IndexBuffer indexBuffer;
    private VertexPositionColorTexture[] vertices = new VertexPositionColorTexture[0];
    private Texture[] textures = new Texture[0];
    private int maxSprites;
    private int vertexCount;
    private VertexBufferBinding[] preBindVertexBuffers;
    private IndexBuffer preBindIndexBuffer;

    public SpriteDrawBuffer(GraphicsDevice graphicsDevice, int defaultSize)
    {
      this.graphicsDevice = graphicsDevice;
      this.maxSprites = defaultSize;
      this.CreateBuffers();
    }

    public void CheckGraphicsDevice(GraphicsDevice graphicsDevice)
    {
      if (this.graphicsDevice == graphicsDevice)
        return;
      this.graphicsDevice = graphicsDevice;
      this.CreateBuffers();
    }

    private void CreateBuffers()
    {
      if (this.vertexBuffer != null)
        this.vertexBuffer.Dispose();
      this.vertexBuffer = new DynamicVertexBuffer(this.graphicsDevice, typeof (VertexPositionColorTexture), this.maxSprites * 4, BufferUsage.WriteOnly);
      if (this.indexBuffer != null)
        this.indexBuffer.Dispose();
      this.indexBuffer = new IndexBuffer(this.graphicsDevice, typeof (ushort), this.maxSprites * 6, BufferUsage.WriteOnly);
      this.indexBuffer.SetData<ushort>(SpriteDrawBuffer.GenIndexBuffer(this.maxSprites));
      Array.Resize<VertexPositionColorTexture>(ref this.vertices, this.maxSprites * 6);
      Array.Resize<Texture>(ref this.textures, this.maxSprites);
    }

    private static ushort[] GenIndexBuffer(int maxSprites)
    {
      ushort[] numArray1 = new ushort[maxSprites * 6];
      int num1 = 0;
      ushort num2 = 0;
      while (num1 < maxSprites)
      {
        ushort[] numArray2 = numArray1;
        int index1 = num1;
        int num3 = index1 + 1;
        int num4 = (int) num2;
        numArray2[index1] = (ushort) num4;
        ushort[] numArray3 = numArray1;
        int index2 = num3;
        int num5 = index2 + 1;
        int num6 = (int) (ushort) ((uint) num2 + 1U);
        numArray3[index2] = (ushort) num6;
        ushort[] numArray4 = numArray1;
        int index3 = num5;
        int num7 = index3 + 1;
        int num8 = (int) (ushort) ((uint) num2 + 2U);
        numArray4[index3] = (ushort) num8;
        ushort[] numArray5 = numArray1;
        int index4 = num7;
        int num9 = index4 + 1;
        int num10 = (int) (ushort) ((uint) num2 + 3U);
        numArray5[index4] = (ushort) num10;
        ushort[] numArray6 = numArray1;
        int index5 = num9;
        int num11 = index5 + 1;
        int num12 = (int) (ushort) ((uint) num2 + 2U);
        numArray6[index5] = (ushort) num12;
        ushort[] numArray7 = numArray1;
        int index6 = num11;
        num1 = index6 + 1;
        int num13 = (int) (ushort) ((uint) num2 + 1U);
        numArray7[index6] = (ushort) num13;
        num2 += (ushort) 4;
      }
      return numArray1;
    }

    public void UploadAndBind()
    {
      if (this.vertexCount > 0)
        this.vertexBuffer.SetData<VertexPositionColorTexture>(this.vertices, 0, this.vertexCount, SetDataOptions.Discard);
      this.vertexCount = 0;
      this.Bind();
    }

    public void Bind()
    {
      this.preBindVertexBuffers = this.graphicsDevice.GetVertexBuffers();
      this.preBindIndexBuffer = this.graphicsDevice.Indices;
      this.graphicsDevice.SetVertexBuffer((VertexBuffer) this.vertexBuffer);
      this.graphicsDevice.Indices = this.indexBuffer;
    }

    public void Unbind()
    {
      this.graphicsDevice.SetVertexBuffers(this.preBindVertexBuffers);
      this.graphicsDevice.Indices = this.preBindIndexBuffer;
      this.preBindVertexBuffers = (VertexBufferBinding[]) null;
      this.preBindIndexBuffer = (IndexBuffer) null;
    }

    public void DrawRange(int index, int count)
    {
      this.graphicsDevice.Textures[0] = this.textures[index];
      this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, index * 4, 0, count * 4, 0, count * 2);
    }

    public void DrawSingle(int index) => this.DrawRange(index, 1);

    public void Draw(Texture2D texture, Vector2 position, VertexColors colors) => this.Draw(texture, position, new Rectangle?(), colors, 0.0f, Vector2.Zero, 1f, SpriteEffects.None);

    public void Draw(Texture2D texture, Rectangle destination, VertexColors colors) => this.Draw(texture, destination, new Rectangle?(), colors);

    public void Draw(
      Texture2D texture,
      Rectangle destination,
      Rectangle? sourceRectangle,
      VertexColors colors)
    {
      this.Draw(texture, destination, sourceRectangle, colors, 0.0f, Vector2.Zero, SpriteEffects.None);
    }

    public void Draw(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      VertexColors color,
      float rotation,
      Vector2 origin,
      float scale,
      SpriteEffects effects)
    {
      this.Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale, scale), effects);
    }

    public void Draw(
      Texture2D texture,
      Vector2 position,
      Rectangle? sourceRectangle,
      VertexColors colors,
      float rotation,
      Vector2 origin,
      Vector2 scale,
      SpriteEffects effects)
    {
      float z;
      float w;
      if (sourceRectangle.HasValue)
      {
        z = (float) sourceRectangle.Value.Width * scale.X;
        w = (float) sourceRectangle.Value.Height * scale.Y;
      }
      else
      {
        z = (float) texture.Width * scale.X;
        w = (float) texture.Height * scale.Y;
      }
      this.Draw(texture, new Vector4(position.X, position.Y, z, w), sourceRectangle, colors, rotation, origin, effects, 0.0f);
    }

    public void Draw(
      Texture2D texture,
      Rectangle destination,
      Rectangle? sourceRectangle,
      VertexColors colors,
      float rotation,
      Vector2 origin,
      SpriteEffects effects)
    {
      this.Draw(texture, new Vector4((float) destination.X, (float) destination.Y, (float) destination.Width, (float) destination.Height), sourceRectangle, colors, rotation, origin, effects, 0.0f);
    }

    public void Draw(
      Texture2D texture,
      Vector4 destinationRectangle,
      Rectangle? sourceRectangle,
      VertexColors colors,
      float rotation,
      Vector2 origin,
      SpriteEffects effect,
      float depth)
    {
      Vector4 sourceRectangle1;
      if (sourceRectangle.HasValue)
      {
        sourceRectangle1.X = (float) sourceRectangle.Value.X;
        sourceRectangle1.Y = (float) sourceRectangle.Value.Y;
        sourceRectangle1.Z = (float) sourceRectangle.Value.Width;
        sourceRectangle1.W = (float) sourceRectangle.Value.Height;
      }
      else
      {
        sourceRectangle1.X = 0.0f;
        sourceRectangle1.Y = 0.0f;
        sourceRectangle1.Z = (float) texture.Width;
        sourceRectangle1.W = (float) texture.Height;
      }
      Vector2 texCoordTL;
      texCoordTL.X = sourceRectangle1.X / (float) texture.Width;
      texCoordTL.Y = sourceRectangle1.Y / (float) texture.Height;
      Vector2 texCoordBR;
      texCoordBR.X = (sourceRectangle1.X + sourceRectangle1.Z) / (float) texture.Width;
      texCoordBR.Y = (sourceRectangle1.Y + sourceRectangle1.W) / (float) texture.Height;
      if ((effect & SpriteEffects.FlipVertically) != SpriteEffects.None)
      {
        float y = texCoordBR.Y;
        texCoordBR.Y = texCoordTL.Y;
        texCoordTL.Y = y;
      }
      if ((effect & SpriteEffects.FlipHorizontally) != SpriteEffects.None)
      {
        float x = texCoordBR.X;
        texCoordBR.X = texCoordTL.X;
        texCoordTL.X = x;
      }
      this.QueueSprite(destinationRectangle, -origin, colors, sourceRectangle1, texCoordTL, texCoordBR, texture, depth, rotation);
    }

    private void QueueSprite(
      Vector4 destinationRect,
      Vector2 origin,
      VertexColors colors,
      Vector4 sourceRectangle,
      Vector2 texCoordTL,
      Vector2 texCoordBR,
      Texture2D texture,
      float depth,
      float rotation)
    {
      float num1 = origin.X / sourceRectangle.Z;
      double num2 = (double) origin.Y / (double) sourceRectangle.W;
      float x = destinationRect.X;
      float y = destinationRect.Y;
      float z = destinationRect.Z;
      float w = destinationRect.W;
      float num3 = num1 * z;
      double num4 = (double) w;
      float num5 = (float) (num2 * num4);
      float num6;
      float num7;
      if ((double) rotation != 0.0)
      {
        num6 = (float) Math.Cos((double) rotation);
        num7 = (float) Math.Sin((double) rotation);
      }
      else
      {
        num6 = 1f;
        num7 = 0.0f;
      }
      if (this.vertexCount + 4 >= this.maxSprites * 4)
      {
        this.maxSprites *= 2;
        this.CreateBuffers();
      }
      this.textures[this.vertexCount / 4] = (Texture) texture;
      this.PushVertex(new Vector3((float) ((double) x + (double) num3 * (double) num6 - (double) num5 * (double) num7), (float) ((double) y + (double) num3 * (double) num7 + (double) num5 * (double) num6), depth), colors.TopLeftColor, texCoordTL);
      this.PushVertex(new Vector3((float) ((double) x + ((double) num3 + (double) z) * (double) num6 - (double) num5 * (double) num7), (float) ((double) y + ((double) num3 + (double) z) * (double) num7 + (double) num5 * (double) num6), depth), colors.TopRightColor, new Vector2(texCoordBR.X, texCoordTL.Y));
      this.PushVertex(new Vector3((float) ((double) x + (double) num3 * (double) num6 - ((double) num5 + (double) w) * (double) num7), (float) ((double) y + (double) num3 * (double) num7 + ((double) num5 + (double) w) * (double) num6), depth), colors.BottomLeftColor, new Vector2(texCoordTL.X, texCoordBR.Y));
      this.PushVertex(new Vector3((float) ((double) x + ((double) num3 + (double) z) * (double) num6 - ((double) num5 + (double) w) * (double) num7), (float) ((double) y + ((double) num3 + (double) z) * (double) num7 + ((double) num5 + (double) w) * (double) num6), depth), colors.BottomRightColor, texCoordBR);
    }

    private void PushVertex(Vector3 pos, Color color, Vector2 texCoord) => SpriteDrawBuffer.SetVertex(ref this.vertices[this.vertexCount++], pos, color, texCoord);

    private static void SetVertex(
      ref VertexPositionColorTexture vertex,
      Vector3 pos,
      Color color,
      Vector2 texCoord)
    {
      vertex.Position = pos;
      vertex.Color = color;
      vertex.TextureCoordinate = texCoord;
    }
  }
}
