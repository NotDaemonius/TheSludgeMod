using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Uranium
{
    [AutoloadEquip(EquipType.Head)]
    public class UraniumGreathelmHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 15;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UraniumBreastplateBody>() && legs.type == ModContent.ItemType<UraniumLeggingsLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.1f;
            player.GetDamage(DamageClass.Melee) += 0.1f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.1f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 15% increased melee damage";
            player.GetDamage(DamageClass.Melee) += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<UraniumBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}