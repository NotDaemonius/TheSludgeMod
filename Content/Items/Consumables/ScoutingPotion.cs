using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TheSludgeMod.Content.Buffs;

namespace TheSludgeMod.Content.Items.Consumables;

public class ScoutingPotion : ModItem
{
    public override void SetStaticDefaults() => Item.ResearchUnlockCount = 30;

    public override void SetDefaults()
    {
        Item.width = 32;
        Item.height = 32;
        Item.useStyle = ItemUseStyleID.DrinkLiquid;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.useTurn = true;
        Item.UseSound = SoundID.Item3;
        Item.maxStack = 9999;
        Item.consumable = true;
        Item.rare = ItemRarityID.Blue;
        Item.value = Item.buyPrice(silver: 40);
        Item.buffType = ModContent.BuffType<ScoutingBuff>();
        Item.buffTime = 7200;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.BottledWater);
        recipe.AddIngredient(ItemID.Lens);
        recipe.AddIngredient(ItemID.Blinkroot);
        recipe.AddTile(TileID.Bottles);
        recipe.Register();
    }
}