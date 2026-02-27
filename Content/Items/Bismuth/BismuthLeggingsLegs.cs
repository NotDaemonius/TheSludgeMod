using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth
{
    [AutoloadEquip(EquipType.Legs)]
    public class BismuthLeggingsLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3, silver: 60);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 13;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.12f;
            player.GetDamage(DamageClass.Generic) += 0.12f;
            player.GetCritChance(DamageClass.Generic) += 0.12f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismuthBar>(20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}