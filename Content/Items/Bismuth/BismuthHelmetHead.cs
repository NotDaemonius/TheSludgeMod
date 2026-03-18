using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Bismuth
{
    [AutoloadEquip(EquipType.Head)]
    public class BismuthHelmetHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 9;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<BismuthBreastplateBody>() && legs.type == ModContent.ItemType<BismuthLeggingsLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
            player.GetDamage(DamageClass.Ranged) += 0.18f;
            player.GetCritChance(DamageClass.Ranged) += 0.18f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 15% ranged attack speed";
            player.GetAttackSpeed(DamageClass.Ranged) += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<BismuthBar>(15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}