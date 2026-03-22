using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Weapons;

namespace TheSludgeMod.Common.Systems;

public class ShimmerRecipes : ModSystem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DiamondBlade>()] = ModContent.ItemType<DiamondSword>();
        ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DiamondSword>()] = ModContent.ItemType<DiamondBlade>();
    }
}