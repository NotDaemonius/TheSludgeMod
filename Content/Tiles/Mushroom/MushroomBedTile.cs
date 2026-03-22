using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalTiles;
using TheSludgeMod.Content.Items.Placeable.Mushroom;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomBedTile : ModTile
{
    public override void SetStaticDefaults() => this.SetUpBed(ModContent.ItemType<MushroomBed>());

    public override bool CanExplode(int i, int j) => false;

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override bool CreateDust(int i, int j, ref int type)
    {
        Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, DustID.Pumpkin, 0f, 0f, 1, new Color(255, 255, 255), 1f);
        return false;
    }

    public override bool RightClick(int i, int j) => FurnitureCommon.BedRightClick(i, j);

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override void MouseOver(int i, int j) => FurnitureCommon.MouseOver(i, j, ModContent.ItemType<MushroomBed>());
}