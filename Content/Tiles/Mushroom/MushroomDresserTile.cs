using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TheSludgeMod.Common.GlobalTiles;
using TheSludgeMod.Content.Items.Placeable.Mushroom;

namespace TheSludgeMod.Content.Tiles.Mushroom;

public class MushroomDresserTile : ModTile
{
    public override void SetStaticDefaults()
    {
        this.SetUpDresser(ModContent.ItemType<MushroomDresser>());
        AddMapEntry(new Color(237, 160, 69), FurnitureCommon.GetItemName<MushroomDresser>(), FurnitureCommon.GetMapChestName);
    }

    public override bool CanExplode(int i, int j) => false;

    public override bool CreateDust(int i, int j, ref int type)
    {
        Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, DustID.Pumpkin, 0f, 0f, 1, new Color(255, 255, 255), 1f);
        return false;
    }

    public override void NumDust(int i, int j, bool fail, ref int num) => num = fail ? 1 : 3;

    public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true;

    public override LocalizedText DefaultContainerName(int frameX, int frameY) => FurnitureCommon.GetItemName<MushroomDresser>();

    public override void MouseOver(int i, int j) => FurnitureCommon.DresserMouseOver<MushroomDresser>();

    public override void MouseOverFar(int i, int j) => FurnitureCommon.DresserMouseFar<MushroomDresser>();

    public override void KillMultiTile(int i, int j, int frameX, int frameY) => Chest.DestroyChest(i, j);

    public override bool RightClick(int i, int j) => FurnitureCommon.DresserRightClick();
}