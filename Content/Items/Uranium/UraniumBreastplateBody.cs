using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium
{
    [AutoloadEquip(EquipType.Body)]
    public class UraniumBreastplateBody : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 2, silver: 40);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 11;
        }
        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Generic) += 0.02f;
            player.GetCritChance(DamageClass.Generic) += 0.03f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<UraniumBar>(25);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}