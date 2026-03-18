using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Tiles;

namespace TheSludgeMod.Content.Items.Aluminium
{
    public class AluminiumChandelier : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Chandeliers>(), 1);
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.buyPrice(silver: 48);
            Item.maxStack = Item.CommonMaxStack;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient<AluminiumBar>(4)
                .AddIngredient(ItemID.Torch, 4)
                .AddIngredient(ItemID.Chain)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}