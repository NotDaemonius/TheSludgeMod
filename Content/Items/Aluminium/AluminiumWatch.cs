using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium
{
    public class AluminiumWatch : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.buyPrice(silver: 20);
            Item.rare = ItemRarityID.White;
            Item.accessory = true;
        }

        public override void UpdateInfoAccessory(Player player)
        {
            if (player.accWatch < 2)
                player.accWatch = 2;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AluminiumBar>(10);
            recipe.AddIngredient(ItemID.Chain, 1);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}