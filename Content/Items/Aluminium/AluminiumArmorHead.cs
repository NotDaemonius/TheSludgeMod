using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Aluminium
{
    [AutoloadEquip(EquipType.Head)]
    public class AluminiumArmorHead : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = Item.sellPrice(silver: 30);
            Item.rare = ItemRarityID.White;
            Item.defense = 4;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<AluminiumArmorBody>() && legs.type == ModContent.ItemType<AluminiumArmorLegs>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Set bonus: 4 defense";
            player.statDefense += 4;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient<AluminiumBar>(15);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}