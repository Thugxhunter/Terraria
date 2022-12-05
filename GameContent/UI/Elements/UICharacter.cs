// Decompiled with JetBrains decompiler
// Type: Terraria.GameContent.UI.Elements.UICharacter
// Assembly: Terraria, Version=1.4.4.9, Culture=neutral, PublicKeyToken=null
// MVID: CD1A926A-5330-4A76-ABC1-173FBEBCC76B
// Assembly location: D:\SteamLibrary\steamapps\common\Terraria\Terraria.exe

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ID;
using Terraria.UI;

namespace Terraria.GameContent.UI.Elements
{
  public class UICharacter : UIElement
  {
    private Player _player;
    private Projectile[] _petProjectiles;
    private Asset<Texture2D> _texture;
    private static Item _blankItem = new Item();
    private bool _animated;
    private bool _drawsBackPanel;
    private float _characterScale = 1f;
    private int _animationCounter;
    private static readonly Projectile[] NoPets = new Projectile[0];

    public UICharacter(
      Player player,
      bool animated = false,
      bool hasBackPanel = true,
      float characterScale = 1f,
      bool useAClone = false)
    {
      this._player = player;
      if (useAClone)
      {
        this._player = player.SerializedClone();
        this._player.dead = false;
        this._player.PlayerFrame();
      }
      this.Width.Set(59f, 0.0f);
      this.Height.Set(58f, 0.0f);
      this._texture = Main.Assets.Request<Texture2D>("Images/UI/PlayerBackground", (AssetRequestMode) 1);
      this.UseImmediateMode = true;
      this._animated = animated;
      this._drawsBackPanel = hasBackPanel;
      this._characterScale = characterScale;
      this.OverrideSamplerState = SamplerState.PointClamp;
      this._petProjectiles = UICharacter.NoPets;
      this.PreparePetProjectiles();
    }

    private void PreparePetProjectiles()
    {
      if (this._player.hideMisc[0])
        return;
      Item miscEquip = this._player.miscEquips[0];
      if (miscEquip.IsAir)
        return;
      this._petProjectiles = new Projectile[1]
      {
        this.PreparePetProjectiles_CreatePetProjectileDummy(miscEquip.shoot)
      };
    }

    private Projectile PreparePetProjectiles_CreatePetProjectileDummy(int projectileId)
    {
      Projectile petProjectileDummy = new Projectile();
      petProjectileDummy.SetDefaults(projectileId);
      petProjectileDummy.isAPreviewDummy = true;
      return petProjectileDummy;
    }

    public override void Update(GameTime gameTime)
    {
      this._player.ResetEffects();
      this._player.ResetVisibleAccessories();
      this._player.UpdateMiscCounter();
      this._player.UpdateDyes();
      this._player.PlayerFrame();
      if (this._animated)
        ++this._animationCounter;
      base.Update(gameTime);
    }

    private void UpdateAnim()
    {
      if (!this._animated)
      {
        this._player.bodyFrame.Y = this._player.legFrame.Y = this._player.headFrame.Y = 0;
      }
      else
      {
        this._player.bodyFrame.Y = this._player.legFrame.Y = this._player.headFrame.Y = ((int) ((double) Main.GlobalTimeWrappedHourly / 0.070000000298023224) % 14 + 6) * 56;
        this._player.WingFrame(false);
      }
    }

    protected override void DrawSelf(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      if (this._drawsBackPanel)
        spriteBatch.Draw(this._texture.Value, dimensions.Position(), Color.White);
      this.UpdateAnim();
      this.DrawPets(spriteBatch);
      Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
      Item obj = this._player.inventory[this._player.selectedItem];
      this._player.inventory[this._player.selectedItem] = UICharacter._blankItem;
      Main.PlayerRenderer.DrawPlayer(Main.Camera, this._player, playerPosition + Main.screenPosition, 0.0f, Vector2.Zero, scale: this._characterScale);
      this._player.inventory[this._player.selectedItem] = obj;
    }

    private Vector2 GetPlayerPosition(ref CalculatedStyle dimensions)
    {
      Vector2 playerPosition = dimensions.Position() + new Vector2(dimensions.Width * 0.5f - (float) (this._player.width >> 1), dimensions.Height * 0.5f - (float) (this._player.height >> 1));
      if (this._petProjectiles.Length != 0)
        playerPosition.X -= 10f;
      return playerPosition;
    }

    public void DrawPets(SpriteBatch spriteBatch)
    {
      CalculatedStyle dimensions = this.GetDimensions();
      Vector2 playerPosition = this.GetPlayerPosition(ref dimensions);
      for (int index = 0; index < this._petProjectiles.Length; ++index)
      {
        Projectile petProjectile = this._petProjectiles[index];
        Vector2 vector2 = playerPosition + new Vector2(0.0f, (float) this._player.height) + new Vector2(20f, 0.0f) + new Vector2(0.0f, (float) -petProjectile.height);
        petProjectile.position = vector2 + Main.screenPosition;
        petProjectile.velocity = new Vector2(0.1f, 0.0f);
        petProjectile.direction = 1;
        petProjectile.owner = Main.myPlayer;
        ProjectileID.Sets.CharacterPreviewAnimations[petProjectile.type].ApplyTo(petProjectile, this._animated);
        Player player = Main.player[Main.myPlayer];
        Main.player[Main.myPlayer] = this._player;
        Main.instance.DrawProjDirect(petProjectile);
        Main.player[Main.myPlayer] = player;
      }
      spriteBatch.End();
      spriteBatch.Begin(SpriteSortMode.Immediate, spriteBatch.GraphicsDevice.BlendState, spriteBatch.GraphicsDevice.SamplerStates[0], spriteBatch.GraphicsDevice.DepthStencilState, spriteBatch.GraphicsDevice.RasterizerState, (Effect) null, Main.UIScaleMatrix);
    }

    public void SetAnimated(bool animated) => this._animated = animated;

    public bool IsAnimated => this._animated;
  }
}
