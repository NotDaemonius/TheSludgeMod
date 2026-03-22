using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalTiles;
using TheSludgeMod.Content.Items.Placeable.Mushroom;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomToiletTile : ModTile
{
    public override void SetStaticDefaults() => this.SetUpChair(ModContent.ItemType<MushroomToilet>(), true);

    public override bool CreateDust(int i, int j, ref int type)
    {
        Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, DustID.Pumpkin, 0f, 0f, 1, new Color(255, 255, 255), 1f);
        return false;
    }
    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void ModifySittingTargetInfo(int i, int j, ref TileRestingInfo info) => FurnitureCommon.ChairSitInfo(i, j, ref info, shitter: true);

    public override bool RightClick(int i, int j) => FurnitureCommon.ChairRightClick(i, j);

    public override void MouseOver(int i, int j) => FurnitureCommon.ChairMouseOver(i, j, ModContent.ItemType<MushroomChair>());

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => settings.player.IsWithinSnappngRangeToTile(i, j, PlayerSittingHelper.ChairSittingMaxDistance);

    public override void HitWire(int i, int j)
    {
        Tile tile = Main.tile[i, j];
        int spawnX = i;
        int spawnY = j - tile.TileFrameY / 18;
        Wiring.SkipWire(spawnX, spawnY);
        Wiring.SkipWire(spawnX, spawnY + 1);
        if (Wiring.CheckMech(spawnX, spawnY, 60)) Projectile.NewProjectile(Wiring.GetProjectileSource(spawnX, spawnY), spawnX * 16 + 8, spawnY * 16 + 12, 0f, 0f, ProjectileID.ToiletEffect, 0, 0f, Main.myPlayer);
    }
}