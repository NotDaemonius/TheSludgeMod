using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Common.Players;

namespace TheSludgeMod.Content.Items
{
    public class BloodThinner : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.value = Item.buyPrice(gold: 5);
            Item.rare = ItemRarityID.LightPurple;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<BloodThinnerPlayer>().bloodThinnerEquipped = true;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddIngredient(ItemID.LifeFruit, 1);
            recipe.AddIngredient(ItemID.LifeCrystal, 5);
            recipe.AddIngredient<Plastic>(30);
            recipe.AddTile(TileID.Furnaces);
            recipe.Register();
        }
    }
}