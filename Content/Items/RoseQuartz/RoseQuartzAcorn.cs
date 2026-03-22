using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.RoseQuartz;

public class RoseQuartzAcorn : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 5;

    public override void SetDefaults()
    {
        Item.CloneDefaults(ItemID.GemTreeDiamondSeed);
        Item.createTile = ModContent.TileType<RoseQuartzSapling>();
    }
}
