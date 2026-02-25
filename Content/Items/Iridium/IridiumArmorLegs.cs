using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Iridium
{
    [AutoloadEquip(EquipType.Legs)]
    public class IridiumArmorLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 80);
            Item.rare = ItemRarityID.White;
            Item.defense = 6;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<IridiumBar>(25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}