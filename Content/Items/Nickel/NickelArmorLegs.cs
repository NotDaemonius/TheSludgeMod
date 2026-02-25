using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel
{
    [AutoloadEquip(EquipType.Legs)]
    public class NickelArmorLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 16);
            Item.rare = ItemRarityID.White;
            Item.defense = 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<NickelBar>(20);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}