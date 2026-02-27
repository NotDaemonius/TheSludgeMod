using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium
{
    [AutoloadEquip(EquipType.Head)]
    public class OsmiumSeerHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 4, silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 21;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OsmiumBreastplateBody>() && legs.type == ModContent.ItemType<OsmiumLeggingsLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.12f;
            player.GetDamage(DamageClass.Melee) += 0.15f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.15f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 15% increased melee critical strike chance";
            player.GetCritChance(DamageClass.Melee) += 0.15f;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<OsmiumBar>(15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}