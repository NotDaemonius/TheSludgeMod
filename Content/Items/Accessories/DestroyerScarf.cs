using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Accessories
{
    [AutoloadEquip(EquipType.Neck)]
    public class DestroyerScarf : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.LightPurple;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.endurance += 0.30f;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.WormScarf)
            .AddIngredient(ItemID.HallowedBar, 10)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddTile(TileID.MythrilAnvil)
            .Register();
        }
    }
}