using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium
{
    [AutoloadEquip(EquipType.Legs)]
    public class OsmiumLeggingsLegs : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 2, silver: 70);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 11;
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.09f;
            player.GetDamage(DamageClass.Generic) += 0.09f;
            player.GetCritChance(DamageClass.Generic) += 0.09f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OsmiumBar>(20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}