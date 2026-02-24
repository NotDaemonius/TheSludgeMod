using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Zinc
{
    public class ZincChandelier : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Chandeliers>(), 0);
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(silver: 12);
            Item.maxStack = Item.CommonMaxStack;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<ZincBar>(4)
                .AddIngredient(ItemID.Torch, 4)
                .AddIngredient(ItemID.Chain)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}