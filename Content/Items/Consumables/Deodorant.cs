using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Items.Consumables;

public class Deodorant : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 30;

    public override void SetDefaults()
    {
        Item.width = 16;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 17;
        Item.useTime = 17;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 20);
        Item.buffType = ModContent.BuffType<DeodorantBuff>();
        Item.buffTime = 60 * 60 * 5;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.Waterleaf);
        recipe.AddIngredient(ItemID.PinkGel);
        recipe.AddTile(TileID.Bottles);
        recipe.Register();
    }
}