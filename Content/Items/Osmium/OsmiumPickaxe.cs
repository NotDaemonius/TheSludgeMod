using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TheSludgeMod.Content.Items.Osmium;

public class OsmiumPickaxe : ModItem
{
    public override void SetDefaults()
    {
        Item.damage = 19;
        Item.DamageType = DamageClass.Melee;
        Item.width = 42;
        Item.height = 42;
        Item.useTime = 9;
        Item.useAnimation = 24;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.knockBack = 5;
        Item.value = Item.buyPrice(gold: 3, silver: 24);
        Item.rare = ItemRarityID.LightRed;
        Item.UseSound = SoundID.Item1;
        Item.autoReuse = true;
        Item.pick = 170; 
        Item.attackSpeedOnlyAffectsWeaponAnimation = true;
        Item.useTurn = true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient<OsmiumBar>(18);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}
