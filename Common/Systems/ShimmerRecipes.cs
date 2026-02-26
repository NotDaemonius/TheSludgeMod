using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Items.Materials;
using TheSludgeMod.Content.Items.Zinc;
using TheSludgeMod.Content.Items.Aluminium;
using TheSludgeMod.Content.Items.Iridium;
using TheSludgeMod.Content.Tiles;
using TheSludgeMod.Content.Items.Weapons;

namespace TheSludgeMod.Common.Systems
{
    public class ShimmerRecipes : ModSystem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DiamondBlade>()] = ModContent.ItemType<DiamondSword>();
            ItemID.Sets.ShimmerTransformToItem[ModContent.ItemType<DiamondSword>()] = ModContent.ItemType<DiamondBlade>();
        }
    }
}