using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Items.Consumables
{
    public class Steroids : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.EatFood;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item17;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = Item.buyPrice(silver: 30);
            Item.buffType = ModContent.BuffType<SteroidsBuff>();
            Item.buffTime = 15 * 60;
        }
        public override bool CanUseItem(Player player)
        {
            return !player.HasBuff(ModContent.BuffType<SteroidsDebuff>()) && !player.HasBuff(ModContent.BuffType<SteroidsBuff>());
        }
        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.BottledWater)
                .AddIngredient(ItemID.VilePowder)
                .AddIngredient(ItemID.Daybloom)
                .AddIngredient(ItemID.SpiderFang)
                .AddTile(TileID.Bottles)
                .Register();
        }
    }
}