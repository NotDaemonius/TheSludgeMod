using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth
{
    [AutoloadEquip(EquipType.Body)]
    public class BismuthBreastplateBody : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 4, silver: 80);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 17;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.08f;
            player.GetCritChance(DamageClass.Generic) += 0.08f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismuthBar>(25);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}