using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium
{
    [AutoloadEquip(EquipType.Head)]
    public class OsmiumHeadpieceHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 4, silver: 50);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 4;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<OsmiumBreastplateBody>() && legs.type == ModContent.ItemType<OsmiumLeggingsLegs>();
        }
        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 60;
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.GetAttackSpeed(DamageClass.Magic) += 0.15f;
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 20% reduced mana costs";
            player.manaCost = 0.8f;
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