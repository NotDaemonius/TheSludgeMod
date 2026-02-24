using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Nickel
{
    [AutoloadEquip(EquipType.Head)]
    public class NickelArmorHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 12);
            Item.rare = ItemRarityID.White;
            Item.defense = 3;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<NickelArmorBody>() && legs.type == ModContent.ItemType<NickelArmorLegs>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 3 defense";
            player.statDefense += 3;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<NickelBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}