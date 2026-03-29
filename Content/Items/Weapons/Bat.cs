using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace TheSludgeMod.Content.Items.Weapons;

public class Bat : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 48;
        Item.height = 64;
        Item.scale = 1.5f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useTime = 35;
        Item.useTurn = true;
        Item.useAnimation = 35;
        Item.autoReuse = true;
        Item.shootSpeed = 15;
        Item.DamageType = DamageClass.Melee;
        Item.damage = 6;
        Item.knockBack = 8;
        Item.crit = 12;
        Item.value = Item.buyPrice(copper: 40);
        Item.rare = ItemRarityID.White;
        Item.UseSound = SoundID.Item1;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Wood, 35);
        recipe.AddTile(TileID.WorkBenches);
        recipe.Register();
    }
}
